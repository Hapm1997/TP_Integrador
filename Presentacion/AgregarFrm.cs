using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class AgregarFrm : Form
    {
        private Articulo articulo=null;
        public AgregarFrm()
        {
            InitializeComponent();
        }

        public AgregarFrm(Articulo art)
        {
            InitializeComponent();
            this.articulo = art;
            Text = "Modificar Arctículo";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AgregarFrm_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categoriaNeg = new CategoriaNegocio();
            MarcaNegocio marcNegocio = new MarcaNegocio();
            try
            {
                cbxCateg.DataSource = categoriaNeg.listaCategoria();
                cbxMarca.DataSource = marcNegocio.listaMarca();

                cbxCateg.DataSource = categoriaNeg.listaCategoria();
                cbxCateg.ValueMember = "IdCategoria";
                cbxCateg.DisplayMember = "DescripcionCategoria";
                cbxMarca.DataSource = marcNegocio.listaMarca();
                cbxMarca.ValueMember = "IdMarca";
                cbxMarca.DisplayMember = "DescripcionMarca";

                if (articulo != null)
                {
                    txtId.Text = articulo.IdArticulo.ToString();
                    txtCod.Text = articulo.CodigoArticulo.ToString();
                    txtNombre.Text = articulo.NombreArticulo.ToString();
                    txtDesc.Text = articulo.DescripcionArticulo.ToString();
                    txtUrl.Text = articulo.UrlArticulo.ToString();
                    cargarImagen(articulo.UrlArticulo);

                    cbxCateg.SelectedValue = articulo.DescripcionCategoriaArticulo.IdCategoria;
                    cbxMarca.SelectedValue = articulo.DescripcionMarcaArticulo.IdMarca;

                    txtPrecio.Text = articulo.PrecioArticulo.ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void cargarImagen(string imagen)
        {
            try
            {
                pictureBox1.Load(imagen);
            }
            catch (Exception ex)
            {
                pictureBox1.Load("https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Placeholder_view_vector.svg/681px-Placeholder_view_vector.svg.png");
            }
        }
    }
}
