
namespace WebScrapingMercadoLibre
{
    public class Producto
    {
        public double Precio { get; set; }
        public string Nombre { get; set; }

        public override string ToString()
        {
            return $"Nombre: {Nombre}, \nPrecio: {Precio}\n";
        }

        public string ToSqlSentence()
        {
            return $"INSERT INTO PRODUCTOS(Precio_Prod, Nombre_Prod) SELECT {Precio}, '{Nombre}'";
        }
    }
}
