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
                new Category{CategoryId=1, Categoryname = "Bilgisayar"},
                new Category{CategoryId=1, Categoryname = "Bilgisayar"}

            };

            List<Product> products = new List<Product>
            {
                new Product{ProductId=1, CategoryId=1, ProductName="Acer Laptop", QuantityPerUnit="32 G Ram", UnitPrice=10000, UnitsInStock=5},
                new Product{ProductId=2, CategoryId=1, ProductName="Asus Laptop", QuantityPerUnit="16 G Ram", UnitPrice=8000, UnitsInStock=3},
                new Product{ProductId=3, CategoryId=1, ProductName="HP Laptop", QuantityPerUnit="8 G Ram", UnitPrice=6000, UnitsInStock=2},
                new Product{ProductId=4, CategoryId=2, ProductName="Samsung Telefon", QuantityPerUnit="4 G Ram", UnitPrice=5000, UnitsInStock=15},
                new Product{ProductId=5, CategoryId=2, ProductName="Apple Telefon", QuantityPerUnit="4 G Ram", UnitPrice=8000, UnitsInStock=0}
            };

            //Test(products); bunu quick action sonrada extract method yapıp u şekide çağırabiliriz.

            //AnyTest(products); any için aşşağıdadır yine

            //FindTest(products);

            //FindAllTest(products); 

            //AscDescTest(products);

            //şu ana kadra yapılanlar single line query dir.

            //Sqlkomutlarıgibikomutlarlayapılan(products);

            //ClassicLinqTest(products);

            //Bir classta olan özellik diğer class ta yoksa eğer join kullanılıp birleştirilir.
            //products ile categories ler birleştirilcek. İkisindede ortak olan CategoryId dir.

            var result = from p in products
                         join c in categories
                         on p.CategoryId equals c.CategoryId
                         where p.UnitPrice>5000
                         orderby p.UnitPrice descending
                         select new ProductDto { ProductId = p.ProductId, CategoryName = c.Categoryname, ProductName = p.ProductName, UnitPrice = p.UnitPrice };

            foreach (var productDto in result)
            {
                Console.WriteLine(productDto.ProductName+" "+productDto.CategoryName);
                Console.WriteLine("{0}---{1}", productDto.ProductName, productDto.CategoryName); //her ikiside aynıdır. burde 0 yerine ilk ifade 1 yerinede 2. ifade kullanılır.
            }



        }

        private static void ClassicLinqTest(List<Product> products)
        {
            //product Dto class'ı kurduk

            var result = from p in products
                         where p.UnitPrice > 6000
                         orderby p.UnitPrice descending
                         select new ProductDto { ProductId = p.ProductId, ProductName = p.ProductName, UnitPrice = p.UnitPrice };
        }

        private static void Sqlkomutlarıgibikomutlarlayapılan(List<Product> products)
        {
            var result = from p in products                 //bu şekildede yapılabilir
                         where p.UnitPrice > 6000           //Koşul bloğu
                         orderby p.UnitPrice descending     //sıralama
                         select p;

            
            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        private static void AscDescTest(List<Product> products)
        {
            var result = products.Where(p => p.ProductName.Contains("top")).OrderByDescending(p => p.UnitPrice).ThenBy(p => p.ProductName);
            //Bu şekilde önce şarta uyanlar seçilir. sonra yazılan kodu unit price a göre sıralarız, en son eğer iki tane aynı fiyattan ürün varsa isimlerine göre sıralar.

            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        private static void FindAllTest(List<Product> products)
        {
            var result = products.FindAll(p => p.ProductName.Contains("top")); //koşula uyan elemanların hepsini getirir. top olan ne varsa
            Console.WriteLine(result);                                         //onun yerine where de kullanılabilir
        }

        private static void FindTest(List<Product> products)
        {
            var result = products.Find(p => p.ProductId == 3); //Find bir ürünü bulmamızı ister
            Console.WriteLine(result);
            Console.WriteLine(result.ProductName);
            //Hiç olmayan birşeyi sorarsak bize null değerini döndürür.
        }

        private static void AnyTest(List<Product> products)
        {
            var result = products.Any(p => p.ProductName == "Acer Laptop"); //Any ile bir listenin içinde bir eleman varmı onu bulur
            Console.WriteLine(result);
        }

        private static IEnumerable<Product> Test(List<Product> products)
        {
            Console.WriteLine("Algoritmik---------");

            foreach (var product in products)
            {
                if (product.UnitPrice > 5000 && product.UnitsInStock > 3)
                {
                    Console.WriteLine(product.ProductName);
                }
            }

            Console.WriteLine("Linq--------------");

            var result = products.Where(p => p.UnitPrice > 5000 && p.UnitsInStock > 3); //filtreleme yapar

            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }

            GetProducts(products);
            return result;
        }

        static List<Product> GetProducts(List<Product> products)
        {
            List<Product> filterProducts = new List<Product>();
            foreach (var product in products)
            {
                if (product.UnitPrice > 5000 && product.UnitsInStock > 3)
                {
                    filterProducts.Add(product);
                }
            }
            return filterProducts;
        }

        static List<Product> GetProductsLinq(List<Product> products)
        {
            return products.Where(p => p.UnitPrice > 5000 && p.UnitsInStock > 3).ToList(); //filtreleme yapar//Where foreach sağlıyor
        }
     }
}
