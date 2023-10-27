using CapaEntidad;
using CapaPresentacion.Utilidades;
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

namespace CapaPresentacion
{
    public partial class fmrUsuarios : Form
    {
        public fmrUsuarios()
        {
            InitializeComponent();
        }

        private void fmrUsuarios_Load(object sender, EventArgs e)
        {
            //listamos el estado para el crud de agregar usuario
            cboEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo"});
            cboEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;

            List<UsuarioRol> listaRol = new CNRol().listar();//listamos los tipos de rol para el crud de agregar un usuario
            foreach (UsuarioRol item in listaRol)
            {
                cboRol.Items.Add(new OpcionCombo() { Valor = item.IdRol, Texto = item.Descripcion});

            }
            cboRol.DisplayMember = "Texto";
            cboRol.ValueMember = "Valor";
            cboRol.SelectedIndex = 0;

            foreach (DataGridViewColumn columna in dgvData.Columns) //desplegamos del combobox la lista para el filtro de busqueda
            {
                if (columna.Visible == true && columna.Name != "btnSeleccionar")
                {
                    cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;

            List<Usuario> listaUsuario = new CNUsuario().listar();//mostramos lista de usuarios en el datagrid
            foreach (Usuario item in listaUsuario)
            {
                dgvData.Rows.Add(new object[] { "", item.IdUsuario, item.Documento, item.NombreCompleto, item.Correo, item.Contrasenia,
                item.oRol.IdRol,
                item.oRol.Descripcion,
                item.EstaActivo == true ? 1 : 0,
                item.EstaActivo == true ? "Activo" : "No Activo"
            });
            }

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //dgvData.Rows.Add(new object[] { "", txtId.Text, txtDocumento.Text, txtNombreCompleto.Text, txtCorreo.Text, txtContrasenia.Text,
            //    ((OpcionCombo)cboRol.SelectedItem).Valor.ToString(),
            //    ((OpcionCombo)cboRol.SelectedItem).Texto.ToString(),
            //    ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString(),
            //    ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString()
            //});

            //limpiar();

        }
        private void limpiar() 
        {
            txtIndice.Text = "-1";
            txtId.Text = "0";
            txtDocumento.Text = "";
            txtNombreCompleto.Text = "";
            txtCorreo.Text = "";
            txtContrasenia.Text = "";
            txtConfirmarContrasenia.Text = "";
            cboRol.SelectedIndex = 0;
            cboEstado.SelectedIndex = 0;
        }

        //cuando seleccionamos (click) un usuario se muestra en todos los texbox de cada variable
        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dgvData.Rows[indice].Cells["Id"].Value.ToString();
                    txtDocumento.Text = dgvData.Rows[indice].Cells["Documento"].Value.ToString();
                    txtNombreCompleto.Text = dgvData.Rows[indice].Cells["NombreCompleto"].Value.ToString();
                    txtCorreo.Text = dgvData.Rows[indice].Cells["Correo"].Value.ToString();
                    txtContrasenia.Text = dgvData.Rows[indice].Cells["Contrasenia"].Value.ToString();
                    txtConfirmarContrasenia.Text = dgvData.Rows[indice].Cells["Contrasenia"].Value.ToString();

                    foreach ( OpcionCombo oc in cboRol.Items)// para jalar la info del rol de usuario en el combobox crud usuarios
                    {
                        if (Convert.ToInt32( oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["IdRol"].Value))
                        {
                            int indiceCombo = cboRol.Items.IndexOf(oc);
                            cboRol.SelectedIndex = indiceCombo;
                            break;
                        }
                    }

                    foreach (OpcionCombo oc in cboEstado.Items)// para jalar la info del estado del usuario en el combobox crud usuarios
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indiceCombo = cboEstado.Items.IndexOf(oc);
                            cboEstado.SelectedIndex = indiceCombo;
                            break;
                        }
                    }
                }
            }
            
        }

        //private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        //{
        //    //pintar imagen
        //    if (e.RowIndex < 0)
        //        return;
        //    if (e.ColumnIndex == 0)
        //    {
        //        e.Paint(e.CellBounds,DataGridViewPaintParts.All);
        //        var w = Properties.Resources.check.Width;
        //        var h = Properties.Resources.check.Height;
        //        var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
        //        var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;
        //        e.Graphics.DrawImage(Properties.Resources.check, new Rectangle(x,y,w,h));
        //        e.Handled = true;

        //    }
        //}



    }
}
