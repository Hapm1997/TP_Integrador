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
            cbxMarca.Items.Add("");
            cbxMarca.Items.Add("Samsung");
            cbxMarca.Items.Add("Apple");
            cbxMarca.Items.Add("Sony");
            cbxMarca.Items.Add("Huawei");
            cbxMarca.Items.Add("Motorola");

            cbxCategoria.Items.Add("");
            cbxCategoria.Items.Add("Celulares");
            cbxCategoria.Items.Add("Televisores");
            cbxCategoria.Items.Add("Media");
            cbxCategoria.Items.Add("Audio");

            cbxMin.Items.Add("");
            cbxMin.Items.Add(0);
            cbxMin.Items.Add(20000);
            cbxMin.Items.Add(40000);
            cbxMin.Items.Add(60000);
            cbxMin.Items.Add(80000);

            cbxMax.Items.Add("");
            cbxMax.Items.Add(20000);
            cbxMax.Items.Add(40000);
            cbxMax.Items.Add(60000);
            cbxMax.Items.Add(80000);
            cbxMax.Items.Add(100000);
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
            
            if (dgv1.CurrentRow ==null) 
            {
                MessageBox.Show("Seleccione un artículo");
            }
            else
            {
                Articulo seleccionado;
                seleccionado = (Articulo)dgv1.CurrentRow.DataBoundItem;
                AgregarFrm modificar = new AgregarFrm(seleccionado);
                modificar.ShowDialog();
                cargar();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgv1.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un artículo");
            }
            else
            {
                CatalogoNegocio negocio = new CatalogoNegocio();
                Articulo seleccionado;
                try
                {
                    DialogResult respuesta = MessageBox.Show("¿De verdad queres eliminarlo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (respuesta == DialogResult.Yes)
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
           
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            if(dgv1.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un artículo");
            }
            else
            {
                Articulo seleccionado = new Articulo();
                seleccionado = (Articulo)dgv1.CurrentRow.DataBoundItem;
                DetalleFrm detalle = new DetalleFrm(seleccionado);
                detalle.ShowDialog();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtxBuscar_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = txtxBuscar.Text;

            if(filtro.Length >= 3)
            {
                listaFiltrada = articuloLista.FindAll(x=> x.CodigoArticulo.ToUpper().Contains(filtro.ToUpper()) || x.NombreArticulo.ToUpper().Contains(filtro.ToUpper()) || x.DescripcionArticulo.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                listaFiltrada = articuloLista;
            }

            dgv1.DataSource = null;
            dgv1.DataSource = listaFiltrada;
            ocultarColumnas();
        }

        private void cbxMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            CatalogoNegocio negocio = new CatalogoNegocio();
            string marca = cbxMarca.SelectedItem.ToString();
            dgv1.DataSource = negocio.filtrarMarca(marca);
            //MessageBox.Show(marca.ToString());
        }

        private void cbxCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            CatalogoNegocio negocio = new CatalogoNegocio();
            string categoria = cbxCategoria.SelectedItem.ToString();
            dgv1.DataSource = negocio.filtrarCategoria(categoria);
        }
    }
}
