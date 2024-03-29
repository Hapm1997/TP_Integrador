﻿using Dominio;
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
            lblId.Visible = false;
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
                    idLbl.Text=articulo.IdArticulo.ToString();
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
            catch (Exception )
            {
                pictureBox1.Load("https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Placeholder_view_vector.svg/681px-Placeholder_view_vector.svg.png");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            CatalogoNegocio negocio = new CatalogoNegocio();
            try
            {
                if(articulo == null)
                    articulo = new Articulo();

                //articulo.IdArticulo = int.Parse(txtId.Text);
                articulo.CodigoArticulo = txtCod.Text;
                articulo.NombreArticulo = txtNombre.Text;
                articulo.DescripcionArticulo = txtDesc.Text;
                articulo.UrlArticulo = txtUrl.Text;
                articulo.DescripcionCategoriaArticulo = (Categoria)cbxCateg.SelectedItem;
                articulo.DescripcionMarcaArticulo = (Marca)cbxMarca.SelectedItem;
                articulo.PrecioArticulo = decimal.Parse(txtPrecio.Text);

                if (articulo.IdArticulo != 0)
                {
                    negocio.modificar(articulo);
                    MessageBox.Show("Modificado exitosamente");
                }
                else
                {
                    negocio.agregar(articulo);
                    MessageBox.Show("Agregado exitosamente");
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtUrl_TextChanged(object sender, EventArgs e)
        {
            cargarImagen(txtUrl.Text);
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
