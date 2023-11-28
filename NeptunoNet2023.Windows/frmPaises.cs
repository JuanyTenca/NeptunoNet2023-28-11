using NeptunoNet2023.Entidades.Entidades;
using NeptunoNet2023.Servicios.Servicios;
using NeptunoNet2023.Windows.Helpers;

namespace NeptunoNet2023.Windows
{
    public partial class frmPaises : Form
    {
        private readonly ServiciosPaises _serviciosPaises;
        List<Pais> listaPaises;
        public frmPaises()
        {
            InitializeComponent();
            _serviciosPaises = new ServiciosPaises();
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmPaises_Load(object sender, EventArgs e)
        {
            RecargarGrilla();
        }

        private void MostrarDatosEnGrilla()
        {
            GridHelper.LimpiarGrilla(dgvDatos);
            foreach (var item in listaPaises)
            {
                DataGridViewRow r = GridHelper.ConstruirFila(dgvDatos);
                GridHelper.SetearFila(r, item);
                GridHelper.AgregarFila(r, dgvDatos);
            }
        }

        private void SetearFila(DataGridViewRow r, Pais item)
        {
            r.Cells[colPais.Index].Value = item.NombrePais;
            r.Tag = item;
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmPaisAE frm = new frmPaisAE() { Text = "Agregar País" };
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                Pais pais = frm.GetPais();

                if (!_serviciosPaises.Existe(pais))
                {
                    _serviciosPaises.Guardar(pais);
                    var r =GridHelper.ConstruirFila(dgvDatos);
                    GridHelper.SetearFila(r, pais);
                    GridHelper.AgregarFila(r, dgvDatos);
                    MessageBox.Show("Registro Agregado Satisfactoriamente!!!",
                        "Mensaje",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                }
                else
                {
					MessageBox.Show("Registro Duplicado",
	                    "Error",
	                    MessageBoxButtons.OK,
	                    MessageBoxIcon.Error);

				}
			}
            catch (Exception ex)
            {
                //Exception exc = ex;
                //if (ex.Message != null && ex.Message.Contains("IX"))
                //{
                //    exc = new Exception("Registro duplicado");
                //}
                MessageBox.Show(ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            }
        }

        private void tsbBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            Pais pais = (Pais)r.Tag;
            DialogResult dr = MessageBox.Show($"¿Desea dar de baja a {pais.NombrePais}?",
                "Confirmar Operación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No)
            {
                return;
            }
            try
            {


                if (!_serviciosPaises.EstaRelacionado(pais))
                {
                    int registrosAfectados = _serviciosPaises.Borrar(pais);
                    if (registrosAfectados > 0)
                    {
                        GridHelper.AgregarFila(r, dgvDatos);
                        MessageBox.Show("Registro Borrado Satisfactoriamente!!!",
                            "Mensaje",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Registro modificado o borrado por otro usuario!!!",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                        RecargarGrilla();

                    }

                }
                else
                {
					MessageBox.Show("Registro Relacionado...Baja denegada!!!",
	                    "Error",
	                    MessageBoxButtons.OK,
	                    MessageBoxIcon.Error);

				}
			}
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            }
        }


        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            Pais pais = (Pais)r.Tag;
            frmPaisAE frm = new frmPaisAE() { Text = "Editar País" };
            frm.SetPais(pais);
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                pais = frm.GetPais();
                //Falta ver si el país existe!!!!

                int registrosAfectados=_serviciosPaises.Guardar(pais);

                if (registrosAfectados>0)
                {
                    SetearFila(r, pais);
                    MessageBox.Show("Registro Editado Satisfactoriamente!!!",
                        "Mensaje",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                }
                else
                {
					MessageBox.Show("Registro modificado o borrado por otro usuario!!!",
	                "Advertencia",
	                MessageBoxButtons.OK,
	                MessageBoxIcon.Warning);
                    RecargarGrilla();

				}
			}
            catch (Exception ex)
            {
                Exception exc = ex;
                if (ex.Message != null && ex.Message.Contains("IX"))
                {
                    exc = new Exception("Registro duplicado");
                }
                MessageBox.Show(exc.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            }
        }

		private void RecargarGrilla()
		{
			try
			{
				listaPaises = _serviciosPaises.GetAll();
				MostrarDatosEnGrilla();
			}
			catch (Exception)
			{

				throw;
			}

		}
	}
}
