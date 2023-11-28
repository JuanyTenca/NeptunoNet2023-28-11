using NeptunoNet2023.DatosSql;
using NeptunoNet2023.Entidades.Dtos.Categoria;
using NeptunoNet2023.Entidades.Entidades;
using NeptunoNet2023.Servicios.Interfaces;
using System.Configuration;

namespace NeptunoNet2023.Servicios.Servicios
{
    public class ServiciosCategorias:IServiciosCategorias
    {
        public void Borrar(int categoriaId)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {

                try
                {
                    unitOfWork.Categorias.Borrar(categoriaId);
                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork?.Rollback();
                    throw;
                }
            }
        }

        public bool Existe(Categoria categoria)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {

                try
                {
                    bool existe = unitOfWork.Categorias.Existe(categoria);
                    return existe;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public int GetCantidad()
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {

                try
                {
                    var cantidad = unitOfWork.Categorias.GetCantidad();
                    return cantidad;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public List<Categoria> GetCategorias()
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {

                try
                {
                    var lista = unitOfWork.Categorias.GetCategorias();
                    return lista;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public List<CategoriaComboDto> GetCategoriasCombos()
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {

                try
                {
                    return unitOfWork.Categorias.GetCategoriasCombos();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public List<Categoria> GetCategoriasPorPagina(int cantidad, int pagina)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {

                try
                {
                    var lista = unitOfWork.Categorias.GetCategoriasPorPagina(cantidad, pagina);
                    return lista;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public void Guardar(Categoria categoria)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    // Realiza operaciones en el repositorio
                    if (categoria.CategoriaId == 0)
                    {
                        unitOfWork.Categorias.Agregar(categoria);

                    }
                

                   

                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    
                    unitOfWork.Rollback();
                    unitOfWork.Dispose();
                    
                }
            }
        }
    }
}
