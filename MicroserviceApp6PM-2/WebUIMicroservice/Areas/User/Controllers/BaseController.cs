using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUIService.Areas.User.Controllers
{
    [Area("User")]
    public class BaseController : Controller
    {
        protected string token;
        protected HttpClient client;
        Uri baseAddress = new Uri("http://localhost:50929/");
        public BaseController(IHttpContextAccessor httpContext)
        {
            token = httpContext.HttpContext.Request.Cookies["token"];
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
    }
}