using NeptunoNet2023.Comun.Interfaces;
using NeptunoNet2023.DatosSql;
using NeptunoNet2023.Entidades.Dtos.Ciudad;
using NeptunoNet2023.Entidades.Entidades;
using NeptunoNet2023.Servicios.Interfaces;

namespace NeptunoNet2023.Servicios.Servicios
{
	public class ServiciosCiudades : IServiciosCiudades
	{
		private readonly IRepositorioCiudades _repositorioCiudades;
		public ServiciosCiudades()
		{
			_repositorioCiudades = new RepositorioCiudades();
		}

		public bool Existe(Ciudad ciudad)
		{
			try
			{
				return _repositorioCiudades.Existe(ciudad);
			}
			catch (Exception)
			{

				throw;
			}
		}
        public List<CiudadListDto> GetAll()
		{
			try
			{
				
				return _repositorioCiudades.GetAll();
			}
			catch (Exception)
			{

				throw;
			}
		}

        public CiudadListDto GetCiudadPorId(int ciudadId)
        {
			try
			{
				return _repositorioCiudades.GetCiudadPorId(ciudadId);
			}
			catch (Exception)
			{

				throw;
			}
        }

        public int Guardar(Ciudad ciudad)
        {
			try
			{
				if (ciudad.CiudadId==0)
				{
					return _repositorioCiudades.Agregar(ciudad);
				}
				return 0;//TODO: ojo arreglar
			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}
