using System;
using System.Linq;
using System.Web.Mvc;
using jqGridMvcApp.Core;
using jqGridMvcApp.Models;
using jqGridMvcApp.Persistence;

namespace jqGridMvcApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _repository = new ProductRepository();
        public int PageSize { get { return 20; } }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ProductData(int? page, bool _search, Product searchProduct)
        {
            var list = _repository.GetProducts();

            if (_search)
            {
                list = list.WhereFilter(searchProduct);
            }

            // постраничная выборка данных 
            var data = list
                        .Skip((page - 1 ?? 0) * PageSize)
                        .Take(PageSize)
                        .ToList();

            // формирование ответа в формате JSON
            var result = new JsonResult
                             {
                                 Data = new
                                 {
                                     page,
                                     total = Math.Ceiling((double)list.Count() / PageSize),
                                     records = list.Count(),
                                     rows = data
                                 }
                             };

            return result;
        }
    }
}
