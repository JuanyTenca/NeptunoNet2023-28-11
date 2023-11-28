using NeptunoNet2023.Comun.Interfaces;
using NeptunoNet2023.DatosSql;
using NeptunoNet2023.Entidades.Entidades;
using NeptunoNet2023.Servicios.Interfaces;

namespace NeptunoNet2023.Servicios.Servicios
{
	public class ServiciosPaises : IServiciosPaises
	{
		private readonly IRepositorioPaises _repositorioPaises;
		public ServiciosPaises()
		{
			_repositorioPaises = new RepositorioPaises();
		}

		public int Borrar(Pais pais)
		{
			try
			{
				return _repositorioPaises.Borrar(pais);
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public bool EstaRelacionado(Pais pais)
		{
			try
			{
				return _repositorioPaises.EstaRelacionado(pais);
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public bool Existe(Pais pais)
		{
			try
			{
				return _repositorioPaises.Existe(pais);
			}
			catch (Exception)
			{

				throw;
			}
		}

		public List<Pais> GetAll()
		{
			try
			{
				return _repositorioPaises.GetAll();
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

        public List<Pais> GetPaises(string textoFiltro)
        {
            try
            {
                return _repositorioPaises.GetPaises(textoFiltro);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Guardar(Pais pais)
		{
			try
			{

				if (pais.PaisId == 0)
				{
					return _repositorioPaises.Agregar(pais);
				}
				else
				{
					return _repositorioPaises.Editar(pais);
				}
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}
	}
}
