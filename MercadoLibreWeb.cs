using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace WebScrapingMercadoLibre
{
    class MercadoLibreWeb
    {
        WebPage homePage;
        public MercadoLibreWeb(WebPage page)
        {
            homePage = page;
        }

        public Producto[] getProductos()
        {
            HtmlNode[] components = homePage.Html.CssSelect("div.item__info-container.highlighted ").ToArray();

            List<Producto> resultados = new List<Producto>();
            foreach (var item in components)
            {
                Producto prod = new Producto();
                string nombre = getNombre(item);
                prod.Nombre = nombre;
                prod.Precio = float.Parse(getPrecio(item));
                if(prod.Precio > 0) 
                resultados.Add(prod);
            }
            return resultados.ToArray();
        }

        protected string getNombre(HtmlNode item)
        {
            return item.CssSelect("h2.item__title.list-view-item-title span.main-title").FirstOrDefault().InnerText.Trim();
        }

        protected string getPrecio(HtmlNode item)
        {
            try
            {
                return item.CssSelect("div.item__price span.price__fraction").FirstOrDefault().InnerHtml.Trim();
            }
            catch (Exception)
            {
                return "0";
                throw;
            }
            
        }
    }
}
