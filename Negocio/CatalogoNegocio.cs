using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class CatalogoNegocio
    {
        public List<Articulo> articuloLista()
        {
            List<Articulo> lista = new List<Articulo>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            try
            {
                conexion.ConnectionString = "server = DESKTOP-S17IMP8\\\\SQLEXPRESS; database=CATALOGO_DB; integrated security = true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Select A.Id , A.Codigo , A.Nombre, A.Descripcion, A.ImagenUrl , C.Descripcion Categoria , M.Descripcion Marca , A.Precio , A.IdMarca , A.IdCategoria From ARTICULOS A, CATEGORIAS C, MARCAS M Where C.Id = A.IdCategoria And M.Id = A.IdMarca";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.IdArticulo = (int)lector["Id"];
                    aux.CodigoArticulo = (string)lector["Codigo"];
                    aux.NombreArticulo = (string)lector["Nombre"];
                    aux.DescripcionArticulo = (string)lector["Descripcion"];
                    
                    if (!(lector["UrlImagen"] is DBNull))
                        aux.UrlImagen = (string)lector["ImagenUrl"];

                    aux.PrecioArticulo = (float)lector["Precio"];
                    
                    aux.MarcaArticulo = new Marca();
                    aux.MarcaArticulo.IdMarca = (int)lector["IdMarca"];
                    aux.MarcaArticulo.DescripcionMarca = (string)lector["Marca"];

                    aux.CategoriaArticulo = new Categoria();
                    aux.CategoriaArticulo.IdCategoria = (int)lector["IdCategoria"];
                    aux.CategoriaArticulo.DescripcionCategoria = (string)lector["Categoria"];

                    lista.Add(aux);
                }
                conexion.Close();

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
