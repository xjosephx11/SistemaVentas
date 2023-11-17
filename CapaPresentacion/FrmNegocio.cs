using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FrmNegocio : Form
    {
        public FrmNegocio()
        {
            InitializeComponent();
        }

        public Image ByteToImage(byte[] imageBytes) 
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(imageBytes,0,imageBytes.Length);
            Image image = new Bitmap(ms);
            return image;
        }

        private void FrmNegocio_Load(object sender, EventArgs e)
        {
            bool obntenido = true;
            byte[] byteimage = new CNNegocio().ObtenerLogo(out obntenido);
            if (obntenido)
            {
                picklogo.Image = ByteToImage(byteimage);
            }
            Negocio datos = new CNNegocio().ObtenerDatos();

            txtNombreNegocio.Text = datos.Nombre;
            txtRuc.Text = datos.RUC;
            txtDireccion.Text = datos.Direccion;
        }

        private void btnSubirLogo_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            OpenFileDialog oOpenFileDialog = new OpenFileDialog();
            oOpenFileDialog.FileName = "Files|*.jpg;*.jpeg;*.png";

            if (oOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                byte[] byteImage = File.ReadAllBytes(oOpenFileDialog.FileName);
                bool respuesta = new CNNegocio().ActualizarLogo(byteImage, out mensaje);
                if (respuesta)
                {
                    picklogo.Image = ByteToImage(byteImage);
                }
                else 
                {
                    MessageBox.Show(mensaje,"Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            Negocio obj = new Negocio()
            {
                Nombre = txtNombreNegocio.Text,
                RUC = txtRuc.Text,
                Direccion = txtDireccion.Text
            };

            bool respuesta = new CNNegocio().GuardarDatos(obj, out mensaje);

            if (respuesta)
            {
                MessageBox.Show("Cambios guardados exitosamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se pudo guardar los cambios", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
