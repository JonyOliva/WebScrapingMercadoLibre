using System;
using System.Threading;
using System.Collections.Generic;

namespace WebScrapingMercadoLibre
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.setStart();
            SearchManagement sm = new SearchManagement();           
            List<Producto> productos = new List<Producto>();
            using (CountdownEvent evt = new CountdownEvent(sm.getLenght()))
            {
                foreach (var item in sm.getSearch())
                {
                    ThreadPool.QueueUserWorkItem(_=>fetchSearch(item, productos, evt));
                }
                evt.Wait();
            }
            foreach (var item in productos)
            {
                Console.WriteLine(item);
            }
            Logger.save();

        }
        static void fetchSearch(Search item, List<Producto> productos, CountdownEvent evt)
        {
            MercadolibreSearch sPage = new MercadolibreSearch(item);
            MercadoLibreWeb ml = new MercadoLibreWeb(sPage.getPage());
            productos.AddRange(ml.getProductos());
            evt.Signal();
        }

    }

}
