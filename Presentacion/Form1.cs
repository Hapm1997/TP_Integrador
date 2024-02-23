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

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
