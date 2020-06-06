using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogService.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        DatabaseContext db;
        public CatalogController(DatabaseContext _db)
        {
            db = _db;
        }
        public IEnumerable<Product> GetProducts()
        {
            return db.Products.ToList();
        }
    }
}