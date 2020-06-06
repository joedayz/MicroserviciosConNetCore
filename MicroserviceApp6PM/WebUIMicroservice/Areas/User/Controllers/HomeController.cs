using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUIService.Areas.User.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IHttpContextAccessor httpContext) : base(httpContext)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}