namespace WebScrapingMercadoLibre
{
    class Search
    {
        public string name { get; set; }
        public string param { get; set; }
        public Search(string line)
        {
            string[] data = line.Split("-");
            if(data.Length > 1)
            {
                name = data[0];
                param = data[1];
            }
            else
            {
                name = data[0];
                param = "";
            }
        }
        public override string ToString()
        {
            return name + ", " + param;
        }

    }
}
