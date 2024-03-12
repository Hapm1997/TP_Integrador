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
    public partial class DetalleFrm : Form
    {
        public DetalleFrm(Articulo art)
        {
            InitializeComponent();
        }

        private void DetalleFrm_Load(object sender, EventArgs e)
        {
            lblId.Text = "";
            lblCod.Text = "";
            lblNombre.Text = "";
            lblDesc.Text = "";
            lblMarca.Text = "";
            lblCategoria.Text = "";
            lblUrl.Text = "";
            lblPrecio.Text = "";

            CategoriaNegocio categoriaNeg = new CategoriaNegocio();
            MarcaNegocio marcNegocio = new MarcaNegocio();

            try
            {
                lblId.Text = 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //CategoriaNegocio categoriaNeg = new CategoriaNegocio();
            //MarcaNegocio marcNegocio = new MarcaNegocio();
            //try
            //{
            //    cbxCateg.DataSource = categoriaNeg.listaCategoria();
            //    cbxMarca.DataSource = marcNegocio.listaMarca();

            //    cbxCateg.DataSource = categoriaNeg.listaCategoria();
            //    cbxCateg.ValueMember = "IdCategoria";
            //    cbxCateg.DisplayMember = "DescripcionCategoria";
            //    cbxMarca.DataSource = marcNegocio.listaMarca();
            //    cbxMarca.ValueMember = "IdMarca";
            //    cbxMarca.DisplayMember = "DescripcionMarca";

            //    if (articulo != null)
            //    {
            //        idLbl.Text = articulo.IdArticulo.ToString();
            //        txtCod.Text = articulo.CodigoArticulo.ToString();
            //        txtNombre.Text = articulo.NombreArticulo.ToString();
            //        txtDesc.Text = articulo.DescripcionArticulo.ToString();
            //        txtUrl.Text = articulo.UrlArticulo.ToString();
            //        cargarImagen(articulo.UrlArticulo);

            //        cbxCateg.SelectedValue = articulo.DescripcionCategoriaArticulo.IdCategoria;
            //        cbxMarca.SelectedValue = articulo.DescripcionMarcaArticulo.IdMarca;

            //        txtPrecio.Text = articulo.PrecioArticulo.ToString();

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }
    }
}
