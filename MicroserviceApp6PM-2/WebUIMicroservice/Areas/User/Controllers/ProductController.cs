using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebUIService.Models;

namespace WebUIService.Areas.User.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController(IHttpContextAccessor httpContext): base(httpContext)
        {

        }
        public IActionResult Index()
        {
            List<ProductViewModel> model = new List<ProductViewModel>();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = client.GetAsync(client.BaseAddress + "catalog").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<List<ProductViewModel>>(data);
            }
            return View(model);
        }
    }
}