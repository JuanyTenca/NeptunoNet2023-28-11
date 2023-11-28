using NeptunoNet2023.Entidades.Entidades;
using NeptunoNet2023.Servicios.Interfaces;
using NeptunoNet2023.Servicios.Servicios;
using NeptunoNet2023.Windows.Helpers;

namespace NeptunoNet2023.Windows
{
    public partial class frmCiudadAE : Form
    {
        private readonly IServiciosPaises _servicioPaises;
        public frmCiudadAE()
        {
            InitializeComponent();
            _servicioPaises = new ServiciosPaises();
        }
      
        private Ciudad ciudad;
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CombosHelper.CargarComboPaises(ref cboPaises);
        }

        private void CargarDatosComboPaises(ref ComboBox cbo)
        {
            var listaPaises = _servicioPaises.GetAll();
            Pais defaultPais = new Pais()
            {
                PaisId = 0,
                NombrePais = "Seleccione País"
            };
            listaPaises.Insert(0, defaultPais);
            cbo.DataSource = listaPaises;
            cbo.DisplayMember = "NombrePais";
            cbo.ValueMember = "PaisId";

            cbo.SelectedIndex = 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (ciudad==null)
                {
                    ciudad = new Ciudad();
                }
                ciudad.NombreCiudad=txtCiudad.Text;
                ciudad.PaisId =(int) cboPaises.SelectedValue;

                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidarDatos()
        {
            bool validar = true;
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(txtCiudad.Text))
            {
                validar = false;
                errorProvider1.SetError(txtCiudad, "La ciudad es requerida!!!");
            }
            if (cboPaises.SelectedIndex==0) 
            { 
                validar = false;
                errorProvider1.SetError(cboPaises, "Debe seleccionar un país!!!");
            }
            return validar;
        }

        public Ciudad GetCiudad()
        {
            return ciudad;
        }
    }
}
