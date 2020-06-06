using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebUIService.Controllers
{
    public class BaseController : Controller
    {
        protected HttpClient client;
        Uri baseAddress=new Uri("http://localhost:50929");
        public BaseController()
        {
            //var apiAddress = "182.16.238.5";
            //Environment.GetEnvironmentVariable("API_ADDRESS");
            //baseAddress= new Uri(apiAddress);
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
    }
}