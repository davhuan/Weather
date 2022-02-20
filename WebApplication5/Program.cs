using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using WebApplication5.Models;
namespace WebApplication5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string url = "https://api.met.no/weatherapi/locationforecast/2.0/compact?lat=59.27700231883911&lon=15.216185676478492";
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.UserAgent = "abcdefg";
                var respone = httpRequest.GetResponse();
                using var streamReader = new StreamReader(respone.GetResponseStream());
                var result = streamReader.ReadToEnd();
                var idk = JsonConvert.DeserializeObject<WeatherMode>(result);
                var test = idk.properties["timeseries"];
                intelizedata(test);


                var hej = 2;
            }
            catch (Exception e) { Console.WriteLine(e.Message); }


            CreateHostBuilder(args).Build().Run();

        }
        public static void intelizedata(JToken token)
        {
            List<TimeSerie> timelista = new List<TimeSerie>();
            TimeSerie tid;
            details detalj;

            foreach (var obj in token)
            {
                 tid = new TimeSerie();

                foreach (var i in obj["data"]["instant"]["details"])
                {
                    detalj = new details()
                    {
                        air_pressure_at_sea_level = Convert.ToDouble(i["air_pressure_at_sea_level"]),
                        air_temperature = Convert.ToDouble(i["air_temperature"]),
                        cloud_area_fraction = Convert.ToDouble(i["cloud_area_fraction"]),
                        relative_humidity = Convert.ToDouble(i["relative_humidity"]),
                        wind_from_direction = Convert.ToDouble(i["wind_from_direction"]),
                        wind_speed = Convert.ToDouble(i["wind_speed"])


                    };
                    tid.detailslist = detalj; 
                    
                }

                timelista.Add(tid);
            }

            
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
