using Microsoft.Win32;
using NeptunoNet2023.Entidades.Entidades;
using NeptunoNet2023.Servicios.Servicios;
using NeptunoNet2023.Windows.Helpers;

namespace NeptunoNet2023.Windows
{
    public partial class frmCategorias : Form
    {
        private readonly ServiciosCategorias _servicio;
        private List<Categoria> lista;
        int paginaActual = 1;
        int registros = 0;
        int paginas = 0;
        int registrosPorPagina = 12;
        public frmCategorias()
        {
            InitializeComponent();
            _servicio = new ServiciosCategorias();
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmCategorias_Load(object sender, EventArgs e)
        {
            RecargarGrilla();
        }

        private void RecargarGrilla()
        {
            try
            {
                registros = _servicio.GetCantidad();
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
            lista = _servicio.GetCategoriasPorPagina(registrosPorPagina, paginaActual);
            MostrarDatosEnGrilla();
        }

        private void MostrarDatosEnGrilla()
        {
            GridHelper.LimpiarGrilla(dgvDatos);
            foreach (var categoria in lista)
            {
                DataGridViewRow r = GridHelper.ConstruirFila(dgvDatos);
                GridHelper.SetearFila(r, categoria);
                GridHelper.AgregarFila(r, dgvDatos);
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmCategoriaAE frm = new frmCategoriaAE(_servicio) { Text = "Agregar Categoria" };
            DialogResult dr = frm.ShowDialog(this);
            RecargarGrilla();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            Categoria categoria = (Categoria)r.Tag;
            try
            {
                //TODO:Se debe controlar que no este relacionado
                DialogResult dr = MessageBox.Show("¿Desea borrar el registro seleccionado?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.No) { return; }
                _servicio.Borrar(categoria.CategoriaId);
                GridHelper.QuitarFila(r, dgvDatos);
                registros = _servicio.GetCantidad();
                paginas = FormHelper.CalcularPaginas(registros, registrosPorPagina);

                //lblCantidad.Text = _servicio.GetCantidad().ToString();
                MessageBox.Show("Registro borrado", "Mensaje",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            Categoria categoria = (Categoria)r.Tag;
            Categoria categoriaCopia = (Categoria)categoria.Clone();
            try
            {
                frmCategoriaAE frm = new frmCategoriaAE(_servicio) { Text = "Editar Categoria" };
                frm.SetCategoria(categoria);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    GridHelper.SetearFila(r, categoriaCopia);

                    return;
                }
                categoria = frm.GetCategoria();
                if (categoria != null)
                {
                    GridHelper.SetearFila(r, categoria);
                }
                else
                {
                    GridHelper.SetearFila(r, categoriaCopia);
                }
            }
            catch (Exception ex)
            {
                GridHelper.SetearFila(r, categoriaCopia);
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }
    }
}
