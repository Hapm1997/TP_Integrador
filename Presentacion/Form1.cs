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
            cbxPri.Items.Add("");
            cbxPri.Items.Add("Categoria");
            cbxPri.Items.Add("Marca");
            cbxPri.Items.Add("Precio");
            cbxPri.SelectedIndex = 0;
            cbxSeg.Items.Add("");
            cbxSeg.SelectedIndex = 0;
            cbxTer.Items.Add("");
            cbxTer.SelectedIndex = 0;
            //lblMarca.Visible = false;
            //cbxPri.Visible = false;
            //lblCategoria.Visible = false;
            //cbxSeg.Visible = false;
            cbxTer.Visible = false;
            cbxCua.Visible = false;
            lblMin.Visible = false;
            lblMax.Visible = false;
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
            if(txtxBuscar.Text != "")
            {
                cbxPri.Visible = false;
                lblMarca.Visible = false;
                cbxSeg.Visible = false;
                lblCategoria.Visible = false;
                cbxTer.Visible = false;
                lblMin.Visible = false;
            }
            else
            {
                cbxPri.Visible = true;
                lblMarca.Visible = true;
                lblCategoria.Visible = true;
                cbxSeg.Visible = true;
                cbxPri.SelectedIndex = 0;
                cbxSeg.SelectedIndex = 0;
                cbxTer.SelectedIndex = 0;
            }
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

        private void cbxPri_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbxPri.SelectedIndex != 3)
            {
                cbxTer.Visible = false;
                lblMin.Visible = false;
            }
            if(cbxPri.SelectedIndex == 1)
            {
                cbxSeg.Items.Clear();
                cbxSeg.Items.Add("");
                cbxSeg.Items.Add("Celulares");
                cbxSeg.Items.Add("Televisores");
                cbxSeg.Items.Add("Media");
                cbxSeg.Items.Add("Audio");
            }
            if(cbxPri.SelectedIndex == 2)
            {
                cbxSeg.Items.Clear();
                cbxSeg.Items.Add("");
                cbxSeg.Items.Add("Samsung");
                cbxSeg.Items.Add("Apple");
                cbxSeg.Items.Add("Sony");
                cbxSeg.Items.Add("Huawei");
                cbxSeg.Items.Add("Motorola");
            }
            if (cbxPri.SelectedIndex == 3)
            {
                cbxSeg.Items.Clear();
                cbxSeg.Items.Add("");
                cbxSeg.Items.Add("Mayor a");
                cbxSeg.Items.Add("Menor a");
                cbxSeg.SelectedIndex = 0;
                cbxTer.Visible = true;
                lblMin.Visible= true;
                cbxTer.Items.Clear();
                cbxTer.Items.Add("");
                cbxTer.Items.Add(20000);
                cbxTer.Items.Add(40000);
                cbxTer.Items.Add(60000);
                cbxTer.Items.Add(80000);
                cbxTer.SelectedIndex = 0;
            }
            dgv1.DataSource = articuloLista;
        }

        private void cbxSeg_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CatalogoNegocio negocio = new CatalogoNegocio();
                string criterio = cbxSeg.SelectedItem.ToString();
                int precio = cbxTer.SelectedIndex;
                string condicion = cbxSeg.SelectedItem.ToString();
                dgv1.DataSource = negocio.filtrar(criterio, precio, condicion);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cbxTer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CatalogoNegocio negocio = new CatalogoNegocio();
                string criterio = cbxSeg.SelectedItem.ToString();
                int precio = cbxTer.SelectedIndex;
                string condicion = cbxSeg.SelectedItem.ToString();
                dgv1.DataSource = negocio.filtrar(criterio, precio, condicion);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnLimp_Click(object sender, EventArgs e)
        {
            txtxBuscar.Text = "";
            cbxPri.SelectedIndex = 0;
            cbxSeg.SelectedIndex = 0;
            cbxTer.SelectedIndex = 0;
            dgv1.DataSource = articuloLista;
        }
    }
}
