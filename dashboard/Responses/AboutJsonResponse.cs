using System.Collections.Generic;

namespace dashboard.Responses
{
    public class Custumer
    {
        public string host { get; set; }
    }

    public class Params
    {
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Widgets
    {
        public string name { get; set; }
        public string description { get; set; }
        public List<Params> Params { get; set; }
    }

    public class Services
    { 
        public string name { get; set; }
        public List<Widgets> widgets { get; set; }
    }

    public class Server
    {
        public double current_time { get; set; }
        public List<Services>    services { get; set; }
    }
    
    public class AboutJsonResponse
    {
        public  Custumer custumer { get; set; }
        public  Server server { get; set; }
    }
}