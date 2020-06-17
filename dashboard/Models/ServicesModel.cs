using Microsoft.AspNetCore.Mvc;

namespace dashboard.Models
{
    public class ServicesModel : ControllerBase
    {
        internal string _key = null;
        internal string _url = null;
        
        public ServicesModel() {}

        public ServicesModel(string key, string url)
        {
            _key = key;
            _url = url;
        }
    }
}