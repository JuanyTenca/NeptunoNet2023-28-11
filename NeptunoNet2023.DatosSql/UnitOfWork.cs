using NeptunoNet2023.Comun;
using NeptunoNet2023.Comun.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeptunoNet2023.DatosSql
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;

        public IRepositorioCategorias Categorias { get; }
        public IRepositorioProductos Productos { get; }

        public IRepositorioClientes Clientes { get; }

        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public UnitOfWork(string cadenaConexion)
        {
            if (string.IsNullOrWhiteSpace(cadenaConexion))
            {
                throw new ArgumentException("La cadena de conexión no puede estar vacía.", nameof(cadenaConexion));
            }

            _connection = new SqlConnection(cadenaConexion);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            Categorias = new RepositorioCategorias(_transaction);
            Productos = new RepositorioProductos(_transaction);
            Clientes = new RepositorioClientes(_transaction);

        }
            
        }

    
}
