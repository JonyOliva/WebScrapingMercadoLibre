using System.Collections.Generic;
using System.IO;

namespace WebScrapingMercadoLibre
{
    class SearchManagement
    {
        private List<Search> busquedas = new List<Search>();
        public SearchManagement()
        {
            using (FileStream file = new FileStream("busquedas.txt", FileMode.Open, FileAccess.Read))
            {
                StreamReader reader = new StreamReader(file);
                while (!reader.EndOfStream)
                {
                    busquedas.Add(new Search(reader.ReadLine()));
                }
                reader.Close();
            }
        }
        public Search[] getSearch()
        {
            return busquedas.ToArray();
        }

        public int getLenght()
        {
            return busquedas.Count;
        }

    }
}
