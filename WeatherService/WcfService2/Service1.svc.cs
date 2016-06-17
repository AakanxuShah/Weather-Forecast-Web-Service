using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Linq;


namespace WcfService2
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string[] Weather(string zipcode)
        {
            //Date from a day after current day
           DateTime todaydate = DateTime.Now.AddDays(1);

            //Client Object of National weather service 
            weatherwebref.ndfdXML demo = new weatherwebref.ndfdXML();

            //gets the latitude longitude from zipcode 
            string result = demo.LatLonListZipCode(zipcode);

            //Parsing of XML response to get latitude longitude
            var locationElement = XElement.Parse(result);
            string l = locationElement.Element("latLonList").Value;

            decimal latitude = Convert.ToDecimal(l.Split(',')[0]);
            decimal longitude = Convert.ToDecimal(l.Split(',')[1]);

            //gets the forecasts from latitude, longitude and today's date 
            string forecasts = demo.NDFDgenByDay(latitude, longitude, todaydate, "5", "m", "24 hourly");

            //Parsing of XML response of forecast
            var forecastDoc = XDocument.Parse(forecasts);

            // LINQ to XML query to get maximum temperature from XML Response
            var maxTempQuery = from demo1 in forecastDoc.Descendants("temperature").Elements("value")
                           where demo1.Parent.Attribute("type").Value == "maximum" select (string)demo1;

            string[] max_temp = new String[5];

            int i = 0;

            foreach(string a in maxTempQuery)
            {
                max_temp[i] = a;
                i++;
            }


            //Query get the minimum temperature from XML Response
            var minTempQuery = from demo2 in forecastDoc.Descendants("temperature").Elements("value")
                               where demo2.Parent.Attribute("type").Value == "minimum" select (string)demo2;



            string[] min_temp = new String[5];

            int j = 0;

            foreach (string a in minTempQuery)
            {
                min_temp[j] = a;
                j++;
            }

            // Merge the two arrays of max and min temerature into third array named 'z'
            string[] z = max_temp.Concat(min_temp).ToArray();

            return z;

        }

       
    }
}
