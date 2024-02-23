using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulo
    {
        public int IdArticulo { get; set; }
        public string CodigoArticulo { get; set; }     
        public string NombreArticulo { get; set; }
        public string DescripcionArticulo { get; set; }
        public int IdMarcaArticulo { get; set; }
        public int IdCategoriaArticulo { get; set; }
        public string UrlArticulo { get; set; }
        public decimal PrecioArticulo { get; set; }
    }
}
