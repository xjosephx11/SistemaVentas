using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;
using CapaEntidad;

namespace CapaPresentacion
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            List<Usuario> test = new CNUsuario().listar();
            Usuario oUsuario = new CNUsuario().listar().
                Where(u => u.Documento == txtCedula.Text &&
                u.Contrasenia == txtContrasenia.Text).FirstOrDefault();//devuelve un usuario dentro de nuestra lista con cedula y contrasenia similar a la que digitamos

            if (oUsuario != null)//valida si existe el usuario o no para ingresar
            {
                Inicio formInicio = new Inicio(oUsuario); //instancia del formulario
                                                  //volvemos al formulario de login al cerrar el inicio
                formInicio.Show();//que se pueda mostrar
                this.Hide();//formulario actual se oculta
                formInicio.FormClosing += frmCerrar;
            }
            else 
            {
                MessageBox.Show("No se encontro el usuario", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }

        private void frmCerrar(object sender, FormClosingEventArgs e) 
        {
            txtCedula.Text = "";//limpiamos las cajas de texto
            txtContrasenia.Text = "";
            this.Show();
        }
    }
}
