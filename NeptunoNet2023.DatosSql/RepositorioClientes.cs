using Dapper;
using NeptunoNet2023.Comun.Interfaces;
using NeptunoNet2023.Entidades.Dtos.Cliente;
using NeptunoNet2023.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeptunoNet2023.DatosSql
{
    internal class RepositorioClientes : IRepositorioClientes
    {

        private IDbTransaction _transaction;
        public RepositorioClientes(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public void Agregar(Cliente cliente)
        {
            string addQuery = @"INSERT INTO Clientes (Nombres, Apellido, 
                            Direccion, CodigoPostal, PaisId, CiudadId,
                            Email, TelefonoFijo, TelefonoMovil)
                            VALUES (@Nombres, @Apellido, @Direccion,
                            @CodPostal, @PaisId, @CiudadId, @Email,
                            @TelefonoFijo, @TelefonoMovil);
                            SELECT SCOPE_IDENTITY()";


            int id = _transaction.Connection.ExecuteScalar<int>(addQuery, cliente, transaction: _transaction);
            cliente.ClienteId = id;

        }

        public void Borrar(int clienteId)
        {
            string deleteQuery = "DELETE FROM Clientes WHERE ClienteId=@ClienteId";
            _transaction.Connection.Execute(deleteQuery,
                new { ClienteId = clienteId }, transaction: _transaction);
        }

        public void Editar(Cliente cliente)
        {
            string updateQuery = @"UPDATE Clientes SET Nombres=@Nombres,
                            Apellido=@Apellido, Direccion=@Direccion, 
                            CodigoPostal=@CodPostal, PaisId=@PaisId, 
                            CiudadId=@CiudadId, Email=@Email, 
                            TelefonoFijo=@TelefonoFijo, TelefonoMovil=@TelefonoMovil 
                            WHERE ClienteId=@ClienteId";

            _transaction.Connection.Execute(updateQuery, transaction: _transaction);
        }

        public bool EstaRelacionado(Cliente cliente)
        {
            int cantidad = 0;
            string selectQuery = @"SELECT COUNT(*) FROM Ventas WHERE ClienteId=@clienteId";
            cantidad = _transaction.Connection.ExecuteScalar<int>(selectQuery, new { clienteId = cliente.ClienteId }, transaction: _transaction);
            return cantidad > 0;
        }

        public bool Existe(Cliente cliente)
        {
            var cantidad = 0;
            string selectQuery;
            if (cliente.ClienteId == 0)
            {
                selectQuery = @"SELECT COUNT(*) FROM Clientes 
                            WHERE Nombres=@Nombre AND Apellido=@Apellido";
                cantidad = _transaction.Connection.ExecuteScalar<int>(
                    selectQuery, new { Nombre = cliente.Nombres, Apellido = cliente.Apellido }, transaction: _transaction);
            }
            else
            {
                selectQuery = @"SELECT COUNT(*) FROM Clientes 
                WHERE Nombres=@Nombre AND Apellido=@Apellido AND ClienteId=@ClienteId";
                cantidad = _transaction.Connection.ExecuteScalar<int>(
                    selectQuery, new { Nombre = cliente.Nombres, Apellido = cliente.Apellido, ClienteId = cliente.ClienteId }, transaction: _transaction);

            }
            return cantidad > 0;
        }

        public int GetCantidad(int? paisId, int? ciudadId)
        {
            int cantidad = 0;
            string selectQuery = "SELECT COUNT(*) FROM Clientes";
            if (paisId == null)
            {
                cantidad = _transaction.Connection.ExecuteScalar<int>(selectQuery, transaction: _transaction);
            }
            else if (ciudadId == null)
            {
                selectQuery += " WHERE PaisId=@PaisId";
                cantidad = _transaction.Connection.ExecuteScalar<int>(selectQuery, new { PaisId = paisId }, transaction: _transaction);

            }
            else
            {
                selectQuery += " WHERE PaisId=@PaisId AND CiudadId=@CiudadId";
                cantidad = _transaction.Connection.ExecuteScalar<int>(selectQuery, new { PaisId = paisId, CiudadId = ciudadId }, transaction: _transaction);

            }
            return cantidad;

        }

        public Cliente GetClientePorId(int clienteId)
        {
            Cliente cliente = null;
            string selectQuery = @"SELECT ClienteId, NombreCliente,
                    Direccion, CodPostal,
                    TelFijo, TelMovil,
                    PaisId, CiudadId
                    FROM Clientes WHERE ClienteId=@ClienteId";
            cliente = _transaction.Connection.QuerySingleOrDefault<Cliente>(selectQuery, new { ClienteId = clienteId }, transaction: _transaction);
            return cliente;
        }

        public List<ClienteListDto> GetClientes(int? paisId, int? ciudadId)
        {
            List<ClienteListDto> lista = new List<ClienteListDto>();
            string selectQuery = @"SELECT ClienteId, NombreCliente,
                    Direccion, CodPostal,
                    TelFijo, TelMovil,
                    PaisId, CiudadId
                    FROM Clientes INNER JOIN Paises ON Clientes.PaisId=Paises.PaisId
                    INNER JOIN Ciudades ON Clientes.CiudadId=Ciudades.CiudadId ";
            string orderBy = " ORDER BY NombreCliente";
            if (paisId == null)
            {
                selectQuery += orderBy;
                lista = _transaction.Connection.Query<ClienteListDto>(selectQuery, transaction: _transaction).ToList();
            }
            else if (ciudadId == null)
            {
                selectQuery += " WHERE PaisId=@PaisId" + orderBy;
                lista = _transaction.Connection.Query<ClienteListDto>(selectQuery, new { PaisId = paisId }, transaction: _transaction).ToList();
            }
            else
            {
                selectQuery += " WHERE PaisId=@PaisId AND CiudadId=@CiudadId" + orderBy;
                lista = _transaction.Connection.Query<ClienteListDto>(selectQuery, new { PaisId = paisId, CiudadId = ciudadId }, transaction: _transaction).ToList();

            }
            return lista;
        }

        public List<ClienteListDto> GetClientesPorPagina(int cantidadPorPagina, int paginaActual, int? paisId, int? ciudadId)
        {
            List<ClienteListDto> lista = new List<ClienteListDto>();
            if (paisId == null)
            {
                string selectQuery = @"SELECT ClienteId, NombreCliente,
                    Direccion, CodPostal,
                    TelFijo, TelMovil,
                    NombrePais, NombreCiudad
                    FROM Clientes INNER JOIN Paises ON Clientes.PaisId=Paises.PaisId
                    INNER JOIN Ciudades ON Clientes.CiudadId=Ciudades.CiudadId
                    ORDER BY NombreCliente
                    OFFSET @registrosSaltados ROWS 
                    FETCH NEXT @cantidadPorPagina ROWS ONLY";
                lista = _transaction.Connection.Query<ClienteListDto>(selectQuery, new
                {
                    registrosSaltados = cantidadPorPagina * (paginaActual - 1),
                    cantidadPorPagina = cantidadPorPagina
                }, transaction: _transaction).ToList();

            }
            else if (ciudadId == null)
            {
                string selectQuery = @"SELECT ClienteId, NombreCliente,
                    Direccion, CodPostal,
                    TelFijo, TelMovil,
                    PaisId, CiudadId
                    FROM Clientes INNER JOIN Paises ON Clientes.PaisId=Paises.PaisId
                    INNER JOIN Ciudades ON Clientes.CiudadId=Ciudades.CiudadId
                    WHERE Clientes.PaisId=@PaisId
                    ORDER BY NombreCliente
                    OFFSET @registrosSaltados ROWS 
                    FETCH NEXT @cantidadPorPagina ROWS ONLY";
                lista = _transaction.Connection.Query<ClienteListDto>(selectQuery, new
                {
                    registrosSaltados = cantidadPorPagina * (paginaActual - 1),
                    cantidadPorPagina = cantidadPorPagina,
                    PaisId = paisId.Value,
                }, transaction: _transaction).ToList();

            }
            else
            {
                string selectQuery = @"SELECT ClienteId, NombreCliente,
                    Direccion, CodPostal,
                    TelFijo, TelMovil,
                    PaisId, CiudadId
                    FROM Clientes INNER JOIN Paises ON Clientes.PaisId=Paises.PaisId
                    INNER JOIN Ciudades ON Clientes.CiudadId=Ciudades.CiudadId
                    WHERE Clientes.PaisId=@PaisId AND Clientes.CiudadId=@CiudadId
                    ORDER BY NombreCliente
                    OFFSET @registrosSaltados ROWS 
                    FETCH NEXT @cantidadPorPagina ROWS ONLY";
                lista = _transaction.Connection.Query<ClienteListDto>(selectQuery, new
                {
                    registrosSaltados = cantidadPorPagina * (paginaActual - 1),
                    cantidadPorPagina = cantidadPorPagina,
                    PaisId = paisId.Value,
                    CiudadId = ciudadId.Value,
                }, transaction: _transaction).ToList();

            }
            return lista;
        }
    }
}
