using System;
using System.Linq;
using System.Web.Mvc;
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

            // флаг установлен в случае наличия значений в фильтре
            if (_search)
            {
                if (null != searchProduct.ProductId)
                    list = list.Where(x => searchProduct.ProductId == x.ProductId);

                if (!String.IsNullOrEmpty(searchProduct.ProductName))
                    list = list.Where(x => x.ProductName == searchProduct.ProductName);

                if (!String.IsNullOrEmpty(searchProduct.Category))
                    list = list.Where(x => x.Category == searchProduct.Category);

                if (!String.IsNullOrEmpty(searchProduct.Supplier))
                    list = list.Where(x => x.Supplier == searchProduct.Supplier);

                if (null != searchProduct.UnitPrice)
                    list = list.Where(x => x.UnitPrice == searchProduct.UnitPrice);

                if (null != searchProduct.UnitsInStock)
                    list = list.Where(x => x.UnitsInStock == searchProduct.UnitsInStock);

                if (!String.IsNullOrEmpty(searchProduct.EnglishName))
                    list = list.Where(x => x.EnglishName == searchProduct.EnglishName);
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
