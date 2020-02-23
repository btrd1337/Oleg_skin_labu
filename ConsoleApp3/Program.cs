using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void magic(int i)
        {
            Uri url = new Uri("https://ciu.nstu.ru/kaf/persons/21874/a/file_get/" + i.ToString(System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU")) + "?nomenu=1");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    String filename = response.Headers["Content-disposition"].Replace("attachment; filename=", "");
                    myLog.Add(response.ResponseUri.ToString() + " " + filename);
                    //Console.WriteLine(response.ResponseUri.ToString() + " " + filename);
                }
                response.Close();

            }
            catch (Exception) { } //если не HTTP200 -> идем дальше

        }

        static List<string> myLog = new List<string>();
        static void Main(string[] args)
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 20;
            Parallel.For(250000, 303500, i => { magic(i); });
            using (StreamWriter w = File.AppendText("log.txt"))
            {
                foreach (string logMessage in myLog)
                {
                    w.WriteLine(logMessage);
                }
            }

        }
    }
}
