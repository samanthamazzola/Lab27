using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Lab27API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //communicate with API for the coordinates for Clawson MI
            HttpWebRequest apiRequest = WebRequest.CreateHttp("https://forecast.weather.gov/MapClick.php?lat=42.5334&lon=-83.1463&FcstType=json");

            //tell server who is using/client type
            apiRequest.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows Phone OS 7.5; Trident/5.0; IEMobile/9.0)";

            //response from request call
            HttpWebResponse apiResponse = (HttpWebResponse)apiRequest.GetResponse();
            if (apiResponse.StatusCode == HttpStatusCode.OK) //http error 200
            {
                //get data and parse
                StreamReader responseData = new StreamReader(apiResponse.GetResponseStream());

                //save string response back to a var
                string forecast = responseData.ReadToEnd();

                //add class JOjbect data structure to display better
                JObject jsonForecast = JObject.Parse(forecast);

                ViewBag.forecast = jsonForecast["data"]["text"];
                ViewBag.forecastImg = jsonForecast["data"]["iconLink"];
                ViewBag.forecastLocation = jsonForecast["location"]["areaDescription"];


            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}