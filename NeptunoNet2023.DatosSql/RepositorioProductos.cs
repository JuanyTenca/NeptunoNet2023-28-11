using Dapper;
using NeptunoNet2023.Comun.Interfaces;
using NeptunoNet2023.Entidades.Dtos.Producto;
using NeptunoNet2023.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeptunoNet2023.DatosSql
{
    public class RepositorioProductos : IRepositorioProductos
    {

        private readonly IDbTransaction _transaction;
        public RepositorioProductos(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public void Agregar(Producto producto)
        {
            string insertQuery = @"INSERT INTO Productos (NombreProducto, NombreLatin, 
                    ProveedorId, CategoriaId, PrecioUnitario, Stock, StockMinimo,
                    , Suspendido, Imagen) VALUES(@NombreProducto, @NombreLatin,
                    @ProveedorId, @CategoriaId, @PrecioUnitario, @UnidadesEnStock, 
                    @UnidadesEnPedido, @NivelDeReposicion, @Suspendido, @Imagen);
                    SELECT SCOPE_IDENTITY()";

            int id = _transaction.Connection.ExecuteScalar<int>(insertQuery, producto, transaction: _transaction);
            producto.ProductoId = id;
        }

        public void Borrar(int productoId)
        {
            string deleteQuery = @"DELETE FROM Productos WHERE
                ProductoId=@ProductoId";
            _transaction.Connection.Execute(deleteQuery, new { ProductoId = productoId }, transaction: _transaction);
        }

        public void Editar(Producto producto)
        {
            string updateQuery = @"UPDATE Productos SET NombreProducto=@NombreProducto, 
                NombreLatin=@NombreLatin, ProveedorId=@ProveedorId, CategoriaId=@CategoriaId,
                PrecioUnitario=@PrecioUnitario, UnidadesEnStock=@UnidadesEnStock, 
                UnidadesEnPedido=@UnidadesEnPedido, NivelDeReposicion=@NivelDeReposicion,
                Suspendido=@Suspendido, Imagen=@Imagen WHERE ProductoId=@ProductoId";
            _transaction.Connection.Execute(updateQuery, producto, transaction: _transaction);

        }

        public bool EstaRelacionado(Producto producto)
        {
            int cantidad = 0;
            string selectQuery = @"SELECT COUNT(*) FROM DetalleVentas WHERE ProductoId=@productoId";
            cantidad = _transaction.Connection.ExecuteScalar<int>(selectQuery, new { productoId = producto.ProductoId }, transaction: _transaction);
            return cantidad > 0;
        }

        public bool Existe(Producto producto)
        {
            var cantidad = 0;
            string selectQuery;
            if (producto.ProductoId == 0)
            {
                selectQuery = @"SELECT COUNT(*) FROM Productos 
                    WHERE NombreProducto=@NombreProducto";
                cantidad = _transaction.Connection.ExecuteScalar<int>(selectQuery, new { NombreProducto = producto.NombreProducto }, transaction: _transaction);
            }
            else
            {
                selectQuery = @"SELECT COUNT(*) FROM Productos 
                    WHERE NombreProducto=@NombreProducto 
                    AND ProductoId<>@ProductoId";
                cantidad = _transaction.Connection.ExecuteScalar<int>(selectQuery, producto, transaction: _transaction);

            }

            return cantidad > 0;
        }

        public int GetCantidad(int? categoriaId)
        {
            int cantidad = 0;
            if (categoriaId == null)
            {
                string selectQuery = "SELECT COUNT(*) FROM Productos";
                cantidad = _transaction.Connection.ExecuteScalar<int>(selectQuery, transaction: _transaction);

            }
            else
            {
                string selectQuery = @"SELECT COUNT(*) FROM Productos 
                            WHERE CategoriaId=@categoriaId";
                cantidad = _transaction.Connection.ExecuteScalar<int>(selectQuery, new { categoriaId }, transaction: _transaction);

            }
            return cantidad;
        }

        public Producto GetProductoPorId(int productoId)
        {
            Producto producto = null;
            string selectQuery = @"SELECT ProductoId, NombreProducto, NombreLatin,
                ProveedorId, CategoriaId, PrecioUnitario, UnidadesEnStock,
                UnidadesEnPedido, NivelDeReposicion, Suspendido, Imagen 
                FROM Productos WHERE ProductoId=@ProductoId";
            producto = _transaction.Connection.
                QuerySingleOrDefault<Producto>(selectQuery, new { ProductoId = productoId }, transaction: _transaction);
            return producto;

        }

        public List<ProductoListDto> GetProductos(int? categoriaId)
        {
            List<ProductoListDto> lista = new List<ProductoListDto>();
            if (categoriaId == null)
            {
                string selectQuery = @"SELECT ProductoId, NombreProducto, 
                    NombreCategoria, PrecioUnitario,
                    Stock, Suspendido FROM Productos 
                    INNER JOIN Categorias
                    ON Productos.CategoriaId=Categorias.CategoriaId
                    ORDER BY NombreProducto";
                lista = _transaction.Connection.Query<ProductoListDto>(selectQuery, transaction: _transaction).ToList();
            }
            else
            {
                string selectQuery = @"SELECT ProductoId, NombreProducto, 
                    NombreCategoria, PrecioUnitario,
                    Stock, Suspendido FROM Productos 
                    INNER JOIN Categorias
                    ON Productos.CategoriaId=Categorias.CategoriaId
                    WHERE Productos.CategoriaId=@categoriaId
                    ORDER BY NombreProducto";
                lista = _transaction.Connection.Query<ProductoListDto>(selectQuery, new { categoriaId }, transaction: _transaction).ToList();
            }
            return lista;

        }

        public List<ProductoListDto> GetProductosPorPagina(int cantidad, int paginaActual, int? categoriaId)
        {
            List<ProductoListDto> lista = new List<ProductoListDto>();
            if (categoriaId == null)
            {
                string selectQuery = @"SELECT ProductoId, NombreProducto, NombreCategoria, PrecioUnitario,
                    Stock, Suspendido FROM Productos INNER JOIN Categorias
                    ON Productos.CategoriaId=Categorias.CategoriaId
                    ORDER BY NombreProducto
                    OFFSET @cantidadRegistros ROWS 
                    FETCH NEXT @cantidadPorPagina ROWS ONLY";

                lista = _transaction.Connection.Query<ProductoListDto>(selectQuery, new
                {
                    cantidadRegistros = cantidad * (paginaActual - 1),
                    cantidadPorPagina = cantidad

                }, transaction: _transaction).ToList();

            }
            else
            {
                string selectQuery = @"SELECT ProductoId, NombreProducto, NombreCategoria, PrecioUnitario,
                    Stock, Suspendido FROM Productos INNER JOIN Categorias
                    ON Productos.CategoriaId=Categorias.CategoriaId
                    WHERE Productos.CategoriaId=@categoriaId
                    ORDER BY NombreProducto
                    OFFSET @cantidadRegistros ROWS 
                    FETCH NEXT @cantidadPorPagina ROWS ONLY";

                lista = _transaction.Connection.Query<ProductoListDto>(selectQuery, new
                {
                    cantidadRegistros = cantidad * (paginaActual - 1),
                    cantidadPorPagina = cantidad,
                    categoriaId = categoriaId.Value
                }, transaction: _transaction).ToList();

            }
            return lista;
        }
    }
}
