using NeptunoNet2023.Entidades.Dtos.Ciudad;
using NeptunoNet2023.Entidades.Entidades;
using NeptunoNet2023.Servicios.Interfaces;
using NeptunoNet2023.Servicios.Servicios;
using NeptunoNet2023.Windows.Helpers;

namespace NeptunoNet2023.Windows
{
    public partial class frmCiudades : Form
    {
        private readonly IServiciosCiudades _serviciosCiudades;
        private List<CiudadListDto>? listaCiudades;
        public frmCiudades()
        {
            InitializeComponent();
            _serviciosCiudades = new ServiciosCiudades();
        }

        private void frmCiudades_Load(object sender, EventArgs e)
        {
            try
            {
                listaCiudades = _serviciosCiudades.GetAll();
                MostrarDatosEnGrilla();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void MostrarDatosEnGrilla()
        {
            GridHelper.LimpiarGrilla(dgvDatos);
            foreach (var item in listaCiudades)
            {
                DataGridViewRow r = GridHelper.ConstruirFila(dgvDatos);
                GridHelper.SetearFila(r, item);
                GridHelper.AgregarFila(r, dgvDatos);
            }
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmCiudadAE frm = new frmCiudadAE() { Text = "Nueva Ciudad" };
            DialogResult dr=frm.ShowDialog(this);
            if (dr==DialogResult.Cancel) { return; }
            try
            {
                Ciudad ciudad = frm.GetCiudad();

                if (!_serviciosCiudades.Existe(ciudad))
                {
                    int registrosAfectados = _serviciosCiudades.Guardar(ciudad);
                    if (registrosAfectados > 0)
                    {
                        CiudadListDto ciudadListDto = _serviciosCiudades
                        .GetCiudadPorId(ciudad.CiudadId);
                        DataGridViewRow r = GridHelper.ConstruirFila(dgvDatos);
                        GridHelper.SetearFila(r, ciudadListDto);
                        GridHelper.AgregarFila(r, dgvDatos);
                        MessageBox.Show("Registro agregado!!", "Mensaje", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo agregar registro", "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                    }

                }
                else
                {
                    MessageBox.Show("Ciudad Existente!!!", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);


                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
