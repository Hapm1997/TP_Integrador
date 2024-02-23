using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;
using System.Data.SqlTypes;
using System.Security.Cryptography.X509Certificates;


namespace Negocio
{
    public class CatalogoNegocio
    {
        public List<Articulo> articuloLista()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select Id , Codigo ,  Nombre , Descripcion , IdMarca , IdCategoria ,ImagenUrl , Precio from ARTICULOS");
                datos.ejecutarLectura();

                while(datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.IdArticulo = (int)datos.Lector["Id"];
                    aux.CodigoArticulo = (string)datos.Lector["Codigo"];
                    aux.NombreArticulo = (string)datos.Lector["Nombre"];
                    aux.DescripcionArticulo = (string)datos.Lector["Descripcion"];
                    aux.IdMarcaArticulo = (int)datos.Lector["IdMarca"];
                    aux.IdCategoriaArticulo = (int)datos.Lector["IdCategoria"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.UrlArticulo = (string)datos.Lector["ImagenUrl"];

                    //aux.PrecioArticulo = (decimal)datos.Lector["Precio"];

                    aux.PrecioArticulo = Math.Round(Convert.ToDecimal(datos.Lector["Precio"]), 2);

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }           
        }

    }
}
