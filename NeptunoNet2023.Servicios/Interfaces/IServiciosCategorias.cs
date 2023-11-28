using NeptunoNet2023.Entidades.Dtos.Categoria;
using NeptunoNet2023.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeptunoNet2023.Servicios.Interfaces
{
    public interface IServiciosCategorias
    {
        void Guardar(Categoria categoria);
        void Borrar(int categoriaId);
        bool Existe(Categoria categoria);
        int GetCantidad();
        List<Categoria> GetCategorias();
        List<Categoria> GetCategoriasPorPagina(int cantidad, int pagina);
        List<CategoriaComboDto> GetCategoriasCombos();
    }
}
