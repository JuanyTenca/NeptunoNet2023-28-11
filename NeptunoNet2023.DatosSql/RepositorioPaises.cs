using Dapper;
using NeptunoNet2023.Comun.Interfaces;
using NeptunoNet2023.Entidades.Entidades;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace NeptunoNet2023.DatosSql
{
	public class RepositorioPaises : IRepositorioPaises
	{
		private string connectionString;
		public RepositorioPaises()
		{
			connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ToString();
		}
		public List<Pais> GetAll()
		{
			var listaPaises = new List<Pais>();
			using (var conn = new SqlConnection(connectionString))
			{
				conn.Open();
				string selectQuery = @"SELECT PaisId, NombrePais, RowVersion 
					FROM Paises ORDER BY NombrePais";
				using (var command = new SqlCommand(selectQuery, conn))
				{
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var pais = ConstruirPais(reader);
							listaPaises.Add(pais);
						}
					}
				}

			}
			return listaPaises;
		}

		private Pais ConstruirPais(SqlDataReader reader)
		{
			return new Pais
			{
				PaisId = reader.GetInt32(0),
				NombrePais = reader.GetString(1),
				RowVersion = (byte[])reader[2]
			};
		}

		public int Agregar(Pais pais)
		{
			int registrosAfectados = 0;
			using (var conn = new SqlConnection(connectionString))
			{
				conn.Open();
				string insertQuery = @"INSERT INTO Paises (NombrePais) VALUES(@NombrePais)";

				using (var cmd = new SqlCommand(insertQuery, conn))
				{
					cmd.Parameters.AddWithValue("@NombrePais", pais.NombrePais);
					registrosAfectados = cmd.ExecuteNonQuery();
				}

				if (registrosAfectados > 0)
				{
					string selectQuery = @"SELECT @@IDENTITY";
					using (var comando = new SqlCommand(selectQuery, conn))
					{


						var id = Convert.ToInt32(comando.ExecuteScalar());
						pais.PaisId = id;

					}


					selectQuery = @"SELECT RowVersion FROM Paises 
							WHERE PaisId=@PaisId";
					using (var comando = new SqlCommand(selectQuery, conn))
					{
						comando.Parameters.Add("@PaisId", SqlDbType.Int);
						comando.Parameters["@PaisId"].Value = pais.PaisId;

						var row = (byte[])comando.ExecuteScalar();
						pais.RowVersion = row;

					}

				}
				return registrosAfectados;

			}
		}

		public int Borrar(Pais pais)
		{
			int registrosAfectados = 0;
			using (var conn = new SqlConnection(connectionString))
			{
				conn.Open();
				string deleteQuery = @"DELETE FROM Paises WHERE PaisId=@PaisId AND 
					RowVersion=@RowVersion";
				using (var cmd = new SqlCommand(deleteQuery, conn))
				{
					cmd.Parameters.Add("@PaisId", SqlDbType.Int);
					cmd.Parameters["@PaisId"].Value = pais.PaisId;

					cmd.Parameters.Add("@RowVersion", SqlDbType.Timestamp);
					cmd.Parameters["@RowVersion"].Value = pais.RowVersion;

					registrosAfectados = cmd.ExecuteNonQuery();
				}
			}
			return registrosAfectados;
		}

		public int Editar(Pais pais)
		{
			int registrosAfectados = 0;
			using (var conn = new SqlConnection(connectionString))
			{
				conn.Open();
				string updateQuery = @"UPDATE Paises SET NombrePais=@NombrePais 
							WHERE PaisId=@PaisId AND RowVersion=@RowVersion";
				using (var cmd = new SqlCommand(updateQuery, conn))
				{

					cmd.Parameters.Add("@NombrePais", SqlDbType.NVarChar);
					cmd.Parameters["@NombrePais"].Value = pais.NombrePais;

					cmd.Parameters.Add("@PaisId", SqlDbType.Int);
					cmd.Parameters["@PaisId"].Value = pais.PaisId;

					cmd.Parameters.Add("@RowVersion", SqlDbType.Timestamp);
					cmd.Parameters["@RowVersion"].Value = (byte[])pais.RowVersion;
					//cmd.Parameters.AddWithValue("@NombrePais", pais.NombrePais);
					//cmd.Parameters.AddWithValue("@PaisId",pais.PaisId);
					//cmd.Parameters.AddWithValue("@RowVersion", pais.RowVersion);

					registrosAfectados = cmd.ExecuteNonQuery();
					if (registrosAfectados > 0)
					{
						string selectQuery = @"SELECT RowVersion FROM Paises 
							WHERE PaisId=@PaisId";
						using (var comando = new SqlCommand(selectQuery, conn))
						{
							comando.Parameters.Add("@PaisId", SqlDbType.Int);
							comando.Parameters["@PaisId"].Value = pais.PaisId;

							var row = (byte[])comando.ExecuteScalar();
							pais.RowVersion = row;

						}

					}
				}
			}
			return registrosAfectados;
		}

		public bool Existe(Pais pais)
		{

			using (var conn = new SqlConnection(connectionString))
			{
				conn.Open();
				string selectQuery;
				if (pais.PaisId == 0)
				{
					selectQuery = @"SELECT COUNT(*) AS Cantidad FROM Paises
						WHERE NombrePais=@NombrePais";
					using (var cmd = new SqlCommand(selectQuery, conn))
					{
						cmd.Parameters.Add("@NombrePais", SqlDbType.NVarChar);
						cmd.Parameters["@NombrePais"].Value = pais.NombrePais;

						return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
					}

				}
				else
				{
					selectQuery = @"SELECT COUNT(*) AS Cantidad FROM Paises
						WHERE NombrePais=@NombrePais AND PaisId<>@PaisId";
					using (var cmd = new SqlCommand(selectQuery, conn))
					{
						cmd.Parameters.Add("@NombrePais", SqlDbType.NVarChar);
						cmd.Parameters["@NombrePais"].Value = pais.NombrePais;

						cmd.Parameters.Add("@PaisId", SqlDbType.Int);
						cmd.Parameters["@PaisId"].Value = pais.PaisId;


						return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
					}

				}

			}


		}

		public bool EstaRelacionado(Pais pais)
		{
			int registros = 0;

			using (var conn = new SqlConnection(connectionString))
			{
				conn.Open();
				string selectQuery = @"SELECT COUNT(*) FROM Ciudades WHERE PaisId=@PaisId";
				using (var comando = new SqlCommand(selectQuery, conn))
				{
					comando.Parameters.AddWithValue("@PaisId", pais.PaisId);
					registros=(int) comando.ExecuteScalar();
				}

			}
			return registros > 0;
		}

		public Pais GetPais(int paisId)
		{
			Pais pais = null;
			using (var conn=new SqlConnection(connectionString))
			{
				conn.Open();
				string selectQuery = @"SELECT PaisId, NombrePais, RowVersion FROM Paises
						WHERE PaisId=@PaisId";
				using (var cmd=new SqlCommand(selectQuery,conn))
				{
					cmd.Parameters.AddWithValue("@PaisId", paisId);
					using (var reader=cmd.ExecuteReader())
					{
						if (reader.HasRows)
						{
							reader.Read();
							pais = ConstruirPais(reader);
						}
					}
				}
			}
			return pais;
		}

        public List<Pais> GetPaises(string textoFiltro)
        {
            List<Pais> lista = new List<Pais>();
            using (var conn = new SqlConnection(connectionString))
            {
                string selectQuery;
                if (textoFiltro != null)
                {
                    selectQuery = @"SELECT PaisId, NombrePais FROM Paises
                            WHERE UPPER(NombrePais) LIKE @textoFiltro ORDER BY NombrePais";
                    textoFiltro = $"{textoFiltro.ToUpper()}%";
                    lista = conn.Query<Pais>(selectQuery, new { textoFiltro }).ToList();

                }
                else
                {
                    selectQuery = @"SELECT PaisId, NombrePais FROM Paises
                             ORDER BY NombrePais";
                    lista = conn.Query<Pais>(selectQuery).ToList();

                }
            }
            return lista;

        }
    }
}
