using NeptunoNet2023.DatosSql;
using NeptunoNet2023.Entidades.Dtos.Cliente;
using NeptunoNet2023.Entidades.Entidades;
using NeptunoNet2023.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeptunoNet2023.Servicios.Servicios
{
    public class ServiciosClientes : IServiciosClientes
    {

        public ServiciosClientes()
        {

        }

        public void Borrar(int clienteId)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {

                try
                {
                    unitOfWork.Clientes.Borrar(clienteId);
                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork?.Rollback();

                    throw;
                }
            }
        }

        public bool EstaRelacionado(Cliente cliente)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    return unitOfWork.Clientes.EstaRelacionado(cliente);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public bool Existe(Cliente cliente)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {


                try
                {
                    return unitOfWork.Clientes.Existe(cliente);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public int GetCantidad(int? paisId, int? ciudadId)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    return unitOfWork.Clientes.GetCantidad(paisId, ciudadId);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public Cliente GetClientePorId(int clienteId)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    return unitOfWork.Clientes.GetClientePorId(clienteId);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public List<ClienteListDto> GetClientes(int? paisId, int? ciudadId)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    return unitOfWork.Clientes.GetClientes(paisId, ciudadId);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public List<ClienteListDto> GetClientesPorPagina(int registrosPorPagina,
            int paginaActual, int? paisId, int? ciudadId)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    return unitOfWork.Clientes
                        .GetClientesPorPagina(registrosPorPagina,
                        paginaActual, paisId, ciudadId);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public void Guardar(Cliente cliente)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    if (cliente.ClienteId == 0)
                    {
                        unitOfWork.Clientes.Agregar(cliente);
                    }
                    else
                    {
                        unitOfWork.Clientes.Editar(cliente);
                    }
                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                    unitOfWork.Dispose();
                    throw;
                }
            }
        }
    }
}
