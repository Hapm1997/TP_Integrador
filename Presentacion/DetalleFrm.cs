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
        private Articulo art = null;
        public DetalleFrm(Articulo art)
        {
            InitializeComponent();
            this.art = art;
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
            lblUrl.Visible = false;
            label7.Visible = false;

            CategoriaNegocio categoriaNeg = new CategoriaNegocio();
            MarcaNegocio marcNegocio = new MarcaNegocio();

            try
            {
                lblId.Text = art.IdArticulo.ToString();
                lblCod.Text = art.CodigoArticulo.ToString();
                lblNombre.Text = art.NombreArticulo.ToString();
                lblDesc.Text = art.DescripcionArticulo.ToString();
                lblMarca.Text = art.DescripcionMarcaArticulo.DescripcionMarca.ToString();
                lblCategoria.Text = art.DescripcionCategoriaArticulo.DescripcionCategoria.ToString();
                lblUrl.Text = art.UrlArticulo.ToString();
                lblPrecio.Text = art.PrecioArticulo.ToString();
                cargarImagen(lblUrl.Text);
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
        private void cargarImagen(string imagen)
        {
            try
            {
                pictureBox1.Load(imagen);
            }
            catch (Exception ex)
            {
                pictureBox1.Load("https://media.istockphoto.com/id/1409329028/vector/no-picture-available-placeholder-thumbnail-icon-illustration-design.jpg?s=612x612&w=0&k=20&c=_zOuJu755g2eEUioiOUdz_mHKJQJn-tDgIAhQzyeKUQ=");
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
