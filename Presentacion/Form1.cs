using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;

namespace Presentacion
{
    public partial class frmCatalogo : Form
    {
        private List<Articulo> articuloLista;
        public frmCatalogo()
        {
            InitializeComponent();
        }

        private void frmCatalogo_Load(object sender, EventArgs e)
        {
            cargar();
            //CatalogoNegocio catalogoNegocio = new CatalogoNegocio();
            //dgv1.DataSource = catalogoNegocio.articuloLista();
        }

        private void cargar()
        {
            CatalogoNegocio negocio = new CatalogoNegocio();
            try
            {
                articuloLista = negocio.articuloLista();
                dgv1.DataSource = articuloLista;
                ocultarColumnas();
                cargarImagen(articuloLista[0].UrlArticulo);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void ocultarColumnas()
        {
            dgv1.Columns["UrlArticulo"].Visible = false;
            dgv1.Columns["IdArticulo"].Visible = false;            
        }
        private void cargarImagen(string url)
        {
            try
            {
                pictureBox1.Load(url);
            }
            catch (Exception ex)
            {
                pictureBox1.Load("https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Placeholder_view_vector.svg/681px-Placeholder_view_vector.svg.png");
            }
        }

        private void dgv1_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv1.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgv1.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.UrlArticulo);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarFrm agregar = new AgregarFrm();
            agregar.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgv1.CurrentRow.DataBoundItem;
            AgregarFrm modificar = new AgregarFrm(seleccionado);
            modificar.ShowDialog();
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
           CatalogoNegocio negocio = new CatalogoNegocio();
            Articulo seleccionado;
            try
            {
                DialogResult respuesta=  MessageBox.Show("¿De verdad queres eliminarlo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgv1.CurrentRow.DataBoundItem;
                    negocio.eliminar(seleccionado.IdArticulo);
                    cargar();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        //private void eliminar(bool logico = false)
        //{
        //    Articulo seleccionado;
        //    seleccionado = (Articulo)dgv1.CurrentRow.DataBoundItem;
        //}
    }
}
