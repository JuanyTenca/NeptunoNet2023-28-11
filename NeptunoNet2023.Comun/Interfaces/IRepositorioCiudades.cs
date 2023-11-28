using NeptunoNet2023.Entidades.Dtos.Ciudad;
using NeptunoNet2023.Entidades.Entidades;

namespace NeptunoNet2023.Comun.Interfaces
{
    public interface IRepositorioCiudades
    {
        int Agregar(Ciudad ciudad);
        List<CiudadListDto> GetAll();
        CiudadListDto GetCiudadPorId(int ciudadId);

        bool Existe(Ciudad ciudad);
    }
}
