using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulo
    {
        public int IdArticulo { get; set; }
        [DisplayName("Código")]
        public string CodigoArticulo { get; set; }
        [DisplayName("Nombre")]
        public string NombreArticulo { get; set; }
        [DisplayName("Descripción")]
        public string DescripcionArticulo { get; set; }
        //public int IdMarcaArticulo { get; set; }
        //public int IdCategoriaArticulo { get; set; }
        public string UrlArticulo { get; set; }
        [DisplayName("Precio")]
        public decimal PrecioArticulo { get; set; }
        [DisplayName("Categoría")]
        public Categoria DescripcionCategoriaArticulo { get; set; }
        [DisplayName("Marca")]
        public Marca DescripcionMarcaArticulo { get; set; }
    }
}
