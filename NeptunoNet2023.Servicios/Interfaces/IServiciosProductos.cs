using NeptunoNet2023.Entidades.Dtos.Producto;
using NeptunoNet2023.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeptunoNet2023.Servicios.Interfaces
{
    public interface IServiciosProductos
    {
        void Guardar(Producto producto);
        void Borrar(int productoId);
        bool Existe(Producto producto);
        bool EstaRelacionado(Producto producto);
        int GetCantidad(int? categoriaId);
        Producto GetProductoPorId(int productoId);
        List<ProductoListDto> GetProductos(int? categoriaId);
        List<ProductoListDto> GetProductosPorPagina(int cantidad, int pagina, int? categoriaId);
    }
}
