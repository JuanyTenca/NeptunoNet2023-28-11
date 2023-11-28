using Microsoft.Win32;
using NeptunoNet2023.Entidades.Dtos.Producto;
using NeptunoNet2023.Entidades.Entidades;
using NeptunoNet2023.Servicios.Interfaces;
using NeptunoNet2023.Servicios.Servicios;
using NeptunoNet2023.Windows.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeptunoNet2023.Windows
{
    public partial class frmProductos : Form
    {
        public frmProductos()
        {
            InitializeComponent();
            _servicio = new ServiciosProductos();
        }

        private readonly IServiciosProductos _servicio;
        private List<ProductoListDto> lista;

        int paginaActual = 1;
        int registros = 0;
        int paginas = 0;
        int registrosPorPagina = 12;


        int? categoriaFiltro = null;
        bool filterOn = false;


        private void frmProductos_Load(object sender, EventArgs e)
        {
            RecargarGrilla();
        }

        private void RecargarGrilla()
        {
            try
            {
                registros = _servicio.GetCantidad(categoriaFiltro);
                paginas = FormHelper.CalcularPaginas(registros, registrosPorPagina);
                MostrarPaginado();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void MostrarPaginado()
        {
            lista = _servicio.GetProductosPorPagina(registrosPorPagina, paginaActual, categoriaFiltro);
            MostrarDatosEnGrilla();
        }

        private void MostrarDatosEnGrilla()
        {
            GridHelper.LimpiarGrilla(dgvDatos);
            foreach (var producto in lista)
            {
                DataGridViewRow r = GridHelper.ConstruirFila(dgvDatos);
                GridHelper.SetearFila(r, producto);
                GridHelper.AgregarFila(r, dgvDatos);
            }


        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }








}
