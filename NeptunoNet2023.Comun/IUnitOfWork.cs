using NeptunoNet2023.Comun.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeptunoNet2023.Comun
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        IRepositorioCategorias Categorias { get; }

        IRepositorioClientes Clientes { get; }

        IRepositorioProductos Productos { get; }
    }
}
