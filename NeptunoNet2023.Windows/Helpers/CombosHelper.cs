using NeptunoNet2023.Entidades.Dtos.Categoria;
using NeptunoNet2023.Entidades.Entidades;
using NeptunoNet2023.Servicios.Interfaces;
using NeptunoNet2023.Servicios.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeptunoNet2023.Windows.Helpers
{
    public static class CombosHelper
    {
        public static void CargarComboPaises(ref ComboBox combo)
        {
            IServiciosPaises serviciosPaises = new ServiciosPaises();
            var lista = serviciosPaises.GetPaises(null);
            var defaultPais = new Pais()
            {
                PaisId = 0,
                NombrePais = "Seleccione País"
            };
            lista.Insert(0, defaultPais);
            combo.DataSource = lista;
            combo.DisplayMember = "NombrePais";
            combo.ValueMember = "PaisId";
            combo.SelectedIndex = 0;
        }

        public static void CargarComboCategorias(ref ComboBox combo)
        {
            IServiciosCategorias serviciosCategorias = new ServiciosCategorias();
            var lista = serviciosCategorias.GetCategoriasCombos();
            var defaultCategoria = new CategoriaComboDto()
            {
                CategoriaId = 0,
                NombreCategoria = "Seleccione Categoría"
            };
            lista.Insert(0, defaultCategoria);
            combo.DataSource = lista;
            combo.DisplayMember = "NombreCategoria";
            combo.ValueMember = "CategoriaId";
            combo.SelectedIndex = 0;
        }

    }
}
