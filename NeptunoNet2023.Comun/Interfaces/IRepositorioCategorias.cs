using NeptunoNet2023.Entidades.Dtos.Categoria;
using NeptunoNet2023.Entidades.Entidades;

namespace NeptunoNet2023.Comun.Interfaces
{
	public interface IRepositorioCategorias
	{
        void Agregar(Categoria categoria);
        void Borrar(int categoriaId);
        void Editar(Categoria categoria);
        bool Existe(Categoria categoria);
        bool EstaRelacionada(Categoria categoria);
        int GetCantidad();
        List<Categoria> GetCategorias();
        List<Categoria> GetCategoriasPorPagina(int cantidad, int pagina);
        List<CategoriaComboDto> GetCategoriasCombos();
    }
}