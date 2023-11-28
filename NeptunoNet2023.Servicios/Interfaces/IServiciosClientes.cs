using NeptunoNet2023.Entidades.Dtos.Cliente;
using NeptunoNet2023.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeptunoNet2023.Servicios.Interfaces
{
    public interface IServiciosClientes
    {
        void Borrar(int clienteId);
        bool Existe(Cliente cliente);
        bool EstaRelacionado(Cliente cliente);
        int GetCantidad(int? paisId, int? ciudadId);      
        Cliente GetClientePorId(int clienteId);
        List<ClienteListDto> GetClientes(int? paisId, int? ciudadId);
        void Guardar(Cliente cliente);
        List<ClienteListDto> GetClientesPorPagina(int registrosPorPagina, int paginaActual, int? paisId, int? ciudadId);
    }
}
