using CapaEntidad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidad;
using FontAwesome.Sharp;
using CapaNegocio;
using CapaPresentacion.Modales;

namespace CapaPresentacion
{
    public partial class Inicio : Form
    {
        private static Usuario usuarioActual;
        private static IconMenuItem menuActivo = null;
        private static Form formularioActivo = null;
        public Inicio(Usuario objUsuario )
        {
            //if (objUsuario == null) usuarioActual = new Usuario() { NombreCompleto = "Admin predefinido", IdUsuario = 1};
            //else
            usuarioActual = objUsuario;
            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            List<Permiso> listaPermisos = new CNPermiso().listar(usuarioActual.IdUsuario);
            foreach (IconMenuItem iconmenu in Menu.Items) 
            {
                bool encontrado = listaPermisos.Any(m => m.NombreMenu == iconmenu.Name);
                if (encontrado == false)
                {
                    iconmenu.Visible = false;
                }
            }

            lblUsuario.Text = usuarioActual.NombreCompleto;//muestra el nombre del usuario por medio del label
        }

        private void AbrirFormulario(IconMenuItem menu, Form formulario) 
        {
            if (menuActivo != null)
            {
                menuActivo.BackColor = Color.White;
            }
            menu.BackColor = Color.Silver;
            menuActivo = menu;

            if (formularioActivo != null)
            {
                formularioActivo.Close();
            }

            formularioActivo = formulario;
            formulario.TopLevel = false;//no sea superior
            formulario.FormBorderStyle = FormBorderStyle.None;//no tenga ningun border
            formulario.Dock = DockStyle.Fill;//rellene todo el espacio posible
            formulario.BackColor = Color.SeaGreen;
            Contenedor.Controls.Add(formulario);
            formulario.Show();
        }

        private void MenuUsuario_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new fmrUsuarios());
        }

        private void subMenuCategoria_Click(object sender, EventArgs e)
        {
            AbrirFormulario(MenuMantenimiento, new frmCategoria());
        }

        private void subMenuProducto_Click(object sender, EventArgs e)
        {
            AbrirFormulario(MenuMantenimiento, new frmProducto());
        }

        private void subMenuRegistrarVenta_Click(object sender, EventArgs e)
        {
            AbrirFormulario(MenuVenta, new frmVentas(usuarioActual));
        }

        private void SubMenudetalleDeVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(MenuVenta, new frmDetalleVenta());
        }

        private void subMenuRegistrarCompra_Click(object sender, EventArgs e)
        {
            AbrirFormulario(MenuCompras, new frmCompras(usuarioActual));
        }

        private void subMenuVerDetalleCompra_Click(object sender, EventArgs e)
        {
            AbrirFormulario(MenuCompras, new frmDetalleCompra());
        }

        private void MenuClientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmClientes()); ;
        }

        private void MenuProveedores_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmProveedores());
        }

        private void submenunegocio_Click(object sender, EventArgs e)
        {
            AbrirFormulario(MenuMantenimiento, new FrmNegocio());
        }

        private void subMenuReporteCompras_Click(object sender, EventArgs e)
        {
            AbrirFormulario(MenuReportes, new frmReporteCompras());
        }

        private void subMenuReporteVentas_Click(object sender, EventArgs e)
        {
            AbrirFormulario(MenuReportes, new frmReporteVentas());
        }

        private void MenuAcercaDe_Click(object sender, EventArgs e)
        {
            mdAcercaDe md = new mdAcercaDe();
            md.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea salir?","Mensaje",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
