using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;
using System.Data.SqlTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;


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
                datos.setearConsulta("select A.Id , Codigo , Nombre , A.Descripcion , IdMarca , IdCategoria , M.Descripcion Marca , C.Descripcion Categoria , ImagenUrl , Precio from ARTICULOS A , CATEGORIAS C , MARCAS M where C.Id = A.IdCategoria and M.Id = A.IdMarca");
                //datos.setearConsulta("select Id , Codigo ,  Nombre , Descripcion , IdMarca , IdCategoria ,ImagenUrl , Precio from ARTICULOS");
                datos.ejecutarLectura();

                while(datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.IdArticulo = (int)datos.Lector["Id"];
                    aux.CodigoArticulo = (string)datos.Lector["Codigo"];
                    aux.NombreArticulo = (string)datos.Lector["Nombre"];
                    aux.DescripcionArticulo = (string)datos.Lector["Descripcion"];

                    aux.DescripcionCategoriaArticulo = new Categoria();
                    aux.DescripcionCategoriaArticulo.IdCategoria = (int)datos.Lector["IdCategoria"];
                    aux.DescripcionCategoriaArticulo.DescripcionCategoria = (string)datos.Lector["Categoria"];

                    aux.DescripcionMarcaArticulo = new Marca();
                    aux.DescripcionMarcaArticulo.IdMarca = (int)datos.Lector["IdMarca"];
                    aux.DescripcionMarcaArticulo.DescripcionMarca = (string)datos.Lector["Marca"];



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
        public void modificar(Articulo art)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update ARTICULOS set  Codigo = @codigo , Nombre = @nombre , Descripcion = @desc , IdMarca = @idMarca , IdCategoria = @categ , ImagenUrl = @url , Precio = @precio where Id = @id");
                datos.setearParametro("@codigo" , art.CodigoArticulo);
                datos.setearParametro("@nombre" , art.NombreArticulo);
                datos.setearParametro("@desc", art.DescripcionArticulo);
                datos.setearParametro("@idMarca", art.DescripcionMarcaArticulo.IdMarca);
                datos.setearParametro("@categ" , art.DescripcionCategoriaArticulo.IdCategoria);
                datos.setearParametro("@url", art.UrlArticulo);
                datos.setearParametro("@precio", art.PrecioArticulo);
                datos.setearParametro("@id", art.IdArticulo);

                datos.ejecutarAccion();
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
         public void agregar (Articulo art) 
         {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT into ARTICULOS (Codigo , Nombre , Descripcion , IdMarca , IdCategoria , ImagenUrl , Precio) values (@cod , @nombre , @desc , @idMarca , @idCateg , @url , @precio)" );
                
                datos.setearParametro("@cod" , art.CodigoArticulo);
                datos.setearParametro("@nombre", art.NombreArticulo);
                datos.setearParametro("@desc", art.DescripcionArticulo);
                datos.setearParametro("@idMarca", art.DescripcionMarcaArticulo.IdMarca);
                datos.setearParametro("@idCateg" , art.DescripcionCategoriaArticulo.IdCategoria);
                datos.setearParametro("@url", art.UrlArticulo);
                datos.setearParametro("@precio", art.PrecioArticulo);

                datos.ejecutarAccion();
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
         public void eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from ARTICULOS where Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Articulo> filtrar(string criterio)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "select A.Id , Codigo , Nombre , A.Descripcion , IdMarca , IdCategoria , M.Descripcion Marca , C.Descripcion Categoria , ImagenUrl , Precio from ARTICULOS A , CATEGORIAS C , MARCAS M where C.Id = A.IdCategoria and M.Id = A.IdMarca ";
                if (criterio == "Samsung")
                    consulta += "and M.Descripcion like '" + criterio + "'";
                if (criterio == "Apple")
                    consulta += "and M.Descripcion like '" + criterio + "'";
                if (criterio == "Sony")
                    consulta += "and M.Descripcion like '" + criterio + "'";
                if (criterio == "Huawei")
                    consulta += "and M.Descripcion like '" + criterio + "'";
                if (criterio == "Motorola")
                    consulta += "and M.Descripcion like '" + criterio + "'";
                if (criterio == "Celulares")
                    consulta += "and C.Descripcion like '" + criterio + "'";
                if (criterio == "Televisores")
                    consulta += "and C.Descripcion like '" + criterio + "'";
                if (criterio == "Media")
                    consulta += "and C.Descripcion like '" + criterio + "'";
                if (criterio == "Audio")
                    consulta += "and C.Descripcion like '" + criterio + "'";
                if (criterio == "")
                    consulta = consulta;

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.IdArticulo = (int)datos.Lector["Id"];
                    aux.CodigoArticulo = (string)datos.Lector["Codigo"];
                    aux.NombreArticulo = (string)datos.Lector["Nombre"];
                    aux.DescripcionArticulo = (string)datos.Lector["Descripcion"];

                    aux.DescripcionCategoriaArticulo = new Categoria();
                    aux.DescripcionCategoriaArticulo.IdCategoria = (int)datos.Lector["IdCategoria"];
                    aux.DescripcionCategoriaArticulo.DescripcionCategoria = (string)datos.Lector["Categoria"];

                    aux.DescripcionMarcaArticulo = new Marca();
                    aux.DescripcionMarcaArticulo.IdMarca = (int)datos.Lector["IdMarca"];
                    aux.DescripcionMarcaArticulo.DescripcionMarca = (string)datos.Lector["Marca"];



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
        }
    }
}
