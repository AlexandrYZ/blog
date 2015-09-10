using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using jqGridMvcApp.Models;

namespace jqGridMvcApp.Persistence
{
    public class ProductRepository
    {
        private readonly IList<Product> _products;
        public ProductRepository()
        {
            var serializer = new XmlSerializer(typeof(List<Product>));

            using (var stream = new FileStream(HttpContext.Current.Server.MapPath("/App_Data/products.xml"),FileMode.Open))
            {
                _products = (IList<Product>) serializer.Deserialize(stream);
            }
        }

        public IQueryable<Product> GetProducts()
        {
            return _products.AsQueryable();
        }

    }
}