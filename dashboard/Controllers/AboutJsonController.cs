using System;
using System.Collections.Generic;
using dashboard.Responses;
using Microsoft.AspNetCore.Mvc;


namespace dashboard.Controllers
{
    public class AboutJsonController : Controller
    {
        [HttpGet]
        [Route("/about.json")]
        public JsonResult Get()
        {
            AboutJsonResponse json = new AboutJsonResponse();

            json.custumer = new Custumer();
            json.custumer.host = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds.ToString();
            json.server = new Server();
            json.server.current_time = DateTime.Now.TimeOfDay.TotalSeconds;
            json.server.services = new List<Services>();
            Services services = new Services();
            services.name = "Weather";
            services.widgets = new List<Widgets>();
            Widgets widgets = new Widgets();
            widgets.name = "current temperatature";
            widgets.description = "Display the current temperature for a city";
            widgets.Params = new List<Params>();
            Params p = new Params();
            p.name = "city";
            p.type = "string";
            widgets.Params.Add(p);
            services.widgets.Add(widgets);
            Widgets widgets2 = new Widgets();
            widgets2.name = "Forecast over 7 days";
            widgets2.description = "Display the forecast for a city over 7 days";
            widgets2.Params = new List<Params>();
            Params p2 = new Params();
            p2.name = "city";
            p2.type = "string";
            widgets2.Params.Add(p2);
            services.widgets.Add(widgets2);
            Services services2 = new Services();
            services2.name = "Google Translate";
            services2.widgets = new List<Widgets>();
            Widgets widgets3 = new Widgets();
            widgets3.name = "Trasnlation";
            widgets3.description = "Translate a text from a language from another";
            widgets3.Params  = new List<Params>();
            Params p3 = new Params();
            p3.name = "Source Language";
            p3.type = "string";
            Params p4 = new Params();
            p4.name = "Traget Language";
            p4.type = "string";
            Params p5= new Params();
            p5.name = "Text";
            p5.type = "string";
            widgets3.Params.Add(p3);
            widgets3.Params.Add(p4);
            widgets3.Params.Add(p5);
            services2.widgets.Add(widgets3);
            json.server.services.Add(services);
            json.server.services.Add(services2);
            return Json(json);    
        }
    }
}