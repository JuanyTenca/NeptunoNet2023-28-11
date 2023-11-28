using NeptunoNet2023.Entidades.Dtos.Producto;
using NeptunoNet2023.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeptunoNet2023.Comun.Interfaces
{
    public interface IRepositorioProductos
    {
        void Agregar(Producto producto);
        void Borrar(int productoId);
        void Editar(Producto producto);
        bool Existe(Producto producto);
        bool EstaRelacionado(Producto producto);
        int GetCantidad(int? categoriaId);
        Producto GetProductoPorId(int productoId);
        List<ProductoListDto> GetProductos(int? categoriaId);
        List<ProductoListDto> GetProductosPorPagina(int cantidad, int pagina, int? categoriaId);
    }
}
