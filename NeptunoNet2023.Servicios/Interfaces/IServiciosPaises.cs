using NeptunoNet2023.Entidades.Entidades;

namespace NeptunoNet2023.Servicios.Interfaces
{
	public interface IServiciosPaises
	{
		List<Pais> GetAll();
		int Borrar(Pais pais);
		int Guardar(Pais pais);

		bool Existe(Pais pais);
		bool EstaRelacionado(Pais pais);
        List<Pais> GetPaises(string textoFiltro);
    }
}
