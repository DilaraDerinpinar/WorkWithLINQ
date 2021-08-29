using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqProject
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Category> categories = new List<Category>
            {
                new Category{CategoryId=1,CategoryName="bilgisayar"},
                new Category{CategoryId=2,CategoryName="tablet"}
            };


            List<Product> products = new List<Product>
            {
                new Product{ProductId=1, CategoryId=1,ProductName="asus i7",QuantityPerUnit="32 gb ram",UnitPrice=5000,UnitsInStock=3},
                new Product{ProductId=2, CategoryId=1,ProductName="asus i5",QuantityPerUnit="32 gb ram",UnitPrice=6000,UnitsInStock=6},
                new Product{ProductId=3, CategoryId=2,ProductName="iphone 11",QuantityPerUnit="8 gb ram",UnitPrice=7000,UnitsInStock=99},
                new Product{ProductId=4, CategoryId=2,ProductName="redmi note 8",QuantityPerUnit="4 gb ram",UnitPrice=8000,UnitsInStock=1}

            };
            //Test(products);

            //AnyTest(products);

            //FindTest(products);

            //FindAllTest(products);

            //AscDescTest(products);

            //ClassicLinqTest(products);

            var result = from p in products
                         join c in categories
                         on p.CategoryId equals c.CategoryId
                         where p.UnitPrice>5000
                         orderby p.UnitPrice descending
                         select new ProductDto { ProductId = p.ProductId, CategoryName = c.CategoryName, ProductName = p.ProductName, UnitPrice = p.UnitPrice };

            foreach(var x in result)
            {
                Console.WriteLine(x.ProductName + " " + x.CategoryName);
            }

            GetProducts(products);

        }

        private static void ClassicLinqTest(List<Product> products)
        {
            var result = from p in products //single line query yerine böyle uzunca da yazılabilmekte.
                         where p.UnitPrice > 6000
                         orderby p.ProductName descending, p.UnitPrice ascending
                         select p;

            var result2 = from p in products
                          where p.UnitPrice > 6000
                          orderby p.ProductName descending, p.UnitPrice ascending
                          select new ProductDto { ProductId = p.ProductId, ProductName = p.ProductName, UnitPrice = p.UnitPrice };


            foreach (var product in result2)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        private static void AscDescTest(List<Product> products)
        {
            //SINGLE LINE QUERY
            var result = products.Where(p => p.ProductName.Contains("top")).OrderByDescending(p => p.UnitPrice).ThenByDescending(p => p.ProductName); //top kelimesini içeren ürünleri fiyat olarak büyükten küçüğe doğru sıralar.Daha sonra da da ürün adına göre büyükten küçüğe doğru sıralanır.
        }

        private static void FindAllTest(List<Product> products)
        {
            var result = products.FindAll(p => p.ProductName.Contains("i")); // i harfi içeren ürünlerin tamamını listeler.yani dönüşte bir liste dönderir.!!!!

            Console.WriteLine(result);
        }

        private static void FindTest(List<Product> products)
        {
            var result = products.Find(p => p.ProductId == 3); //aradığımız kritere uygun nesnenin kendisini dönderir yani product dönderir.
            Console.WriteLine(result.ProductName);
        }

        private static void AnyTest(List<Product> products)
        {
            var result = products.Any(p => p.ProductName == "asus i7"); //asus i7 adında bir laptop var mı onu kontrol eder varsa true döner yoksa false döner.

            Console.WriteLine(result);
        }

        private static void Test(List<Product> products)
        {
            var result = products.Where(p => p.UnitPrice > 7000 && p.UnitsInStock > 2); //bir liste dönderir.

            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        static List<Product> GetProducts(List<Product> products)
        {
            return products.Where(p => p.UnitPrice > 5500).ToList();
        }

    }

    class ProductDto
    {
        public int ProductId { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }


    }
    


    class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
    }


    class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

}
