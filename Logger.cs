using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WebScrapingMercadoLibre
{
    static class Logger
    {
        static private bool isSave = false;
        static private List<string> annotations = new List<string>();
        static private DateTime start;
        static public void save()
        {
            if (isSave) return;
            using (FileStream file = new FileStream("log.txt", FileMode.Append, FileAccess.Write))
            {
                StreamWriter writer = new StreamWriter(file); 
                writer.WriteLine($"Day {start.ToShortDateString()}: Elapsed time: {Math.Truncate((DateTime.Now - start).TotalSeconds*100)/100} secs - Annotation: {String.Join("; ", annotations)}");
                writer.Close();
            }
            isSave = true;
        }
        static public void add(string log)
        {
            annotations.Add(log);
        }
        static public void setStart()
        {
            start = DateTime.Now;
        }
    }
}
