namespace NeptunoNet2023.Windows
{
    partial class frmCiudadAE
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label1 = new Label();
            txtCiudad = new TextBox();
            btnCancelar = new Button();
            btnOK = new Button();
            label2 = new Label();
            cboPaises = new ComboBox();
            errorProvider1 = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(52, 39);
            label1.Name = "label1";
            label1.Size = new Size(48, 15);
            label1.TabIndex = 0;
            label1.Text = "Ciudad:";
            // 
            // txtCiudad
            // 
            txtCiudad.Location = new Point(125, 36);
            txtCiudad.MaxLength = 100;
            txtCiudad.Name = "txtCiudad";
            txtCiudad.Size = new Size(416, 23);
            txtCiudad.TabIndex = 1;
            // 
            // btnCancelar
            // 
            btnCancelar.Image = Properties.Resources.Cancelar;
            btnCancelar.Location = new Point(456, 192);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(85, 66);
            btnCancelar.TabIndex = 3;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextImageRelation = TextImageRelation.ImageAboveText;
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnOK
            // 
            btnOK.Image = Properties.Resources.Aceptar;
            btnOK.Location = new Point(52, 192);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(85, 66);
            btnOK.TabIndex = 4;
            btnOK.Text = "OK";
            btnOK.TextImageRelation = TextImageRelation.ImageAboveText;
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(52, 101);
            label2.Name = "label2";
            label2.Size = new Size(31, 15);
            label2.TabIndex = 0;
            label2.Text = "País:";
            // 
            // cboPaises
            // 
            cboPaises.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPaises.FormattingEnabled = true;
            cboPaises.Location = new Point(125, 98);
            cboPaises.Name = "cboPaises";
            cboPaises.Size = new Size(340, 23);
            cboPaises.TabIndex = 5;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // frmCiudadAE
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(594, 281);
            Controls.Add(cboPaises);
            Controls.Add(btnCancelar);
            Controls.Add(btnOK);
            Controls.Add(txtCiudad);
            Controls.Add(label2);
            Controls.Add(label1);
            MaximumSize = new Size(610, 320);
            MinimumSize = new Size(610, 320);
            Name = "frmCiudadAE";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmCiudadAE";
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtCiudad;
        private Button btnCancelar;
        private Button btnOK;
        private Label label2;
        private ComboBox cboPaises;
        private ErrorProvider errorProvider1;
    }
}