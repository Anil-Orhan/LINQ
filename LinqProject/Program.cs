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
                new Category{CategoryId = 1,CategoryName = "Bilgisayar"},
                new Category{CategoryId = 2,CategoryName = "Telefon"}

            };
            List<Product> products = new List<Product>
            {
                new Product(){CategoryId = 1,ProductId = 1,ProductName = "Acer Laptop",QuantityPerUnit = "32 GB Ram",UnitPrice = 10000,UnitsInStock = 5},
                new Product(){CategoryId = 1,ProductId = 2,ProductName = "Asus Laptop",QuantityPerUnit = "16 GB Ram",UnitPrice = 8000,UnitsInStock = 3},
                new Product(){CategoryId = 1,ProductId = 3,ProductName = "Hp Laptop",QuantityPerUnit = "8 GB Ram",UnitPrice = 6000,UnitsInStock = 2},
                new Product(){CategoryId = 2,ProductId = 4,ProductName = "Samsung Telefon",QuantityPerUnit = "4 GB Ram",UnitPrice = 5000,UnitsInStock = 15},
                new Product(){CategoryId = 2,ProductId = 5,ProductName = "Apple Telefon",QuantityPerUnit = "4 GB Ram",UnitPrice = 8000,UnitsInStock = 0},


            };

            // Test(products);

            // getProducts(products);
            // getProductsLinq(products);

            //var resultAny = AnyTest(products); //Any Metodu

            //FindTest(products); //Find Metodu

            // FindAllTest(products); //FindAll Metodu

            //WhereAndOrderByTest(products);// Where ve OrderBy metotları

            /* !!! Buraya kadar olan örneklerde Single Line LINQ kullandık. Ancak LINQ alternatif şekilde aşağıdaki örnekte olduğu gibi kullanılabilir.*/

            // notSingleLineLINQTest(products);

            /*---------------------------------------------------------------------------------------------------------------------------------------*/

            var resultJoinAndDto = from p in products
                join c in categories on p.CategoryId equals c.CategoryId 
                where p.UnitsInStock > 10
                orderby p.CategoryId ascending, p.ProductName ascending
                select new ProductDto
                {
                    ProductName = p.ProductName, ProductId = p.ProductId, UnitPrice = p.UnitPrice,
                    CategoryName = c.CategoryName
                };

            foreach (var item in resultJoinAndDto)
            {
                Console.WriteLine($"Product Name :{item.ProductName} Unit Price : {item.UnitPrice} Category : {item.CategoryName}");
            }



        }

        public class ProductDto //Dto : Data Transfer Object
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public decimal UnitPrice { get; set; }
            public string CategoryName { get; set; }

        }
        private static void notSingleLineLINQTest(List<Product> products)
        {
            var resultNotSingle = from p in products //Products listesini getir
                where p.UnitsInStock > 10 //Stoğu 10'dan fazla olanları
                orderby p.CategoryId ascending, p.ProductName
                    ascending //CategoryId'sine göre sırala ve her kategoriyi kendi içinde isme göre A -> Z şeklinde sırala
                select p; //Dönen elemanları tek tek seçip listeye at

            foreach (var item in resultNotSingle)
            {
                Console.WriteLine(item.ProductName);
            }
            //Bu kullanım ile products listesini şartlarımıza ve sıralama yöntemimize göre çekip değişkenimize atadık.
            //Tüm LINQ sorgularını bu yöntemle de yapmak mümkündür.
        }

        private static void WhereAndOrderByTest(List<Product> products)
        {
            var resultWhereAndOrderBy = products.Where(p => p.ProductName.Contains("top")).OrderBy(p => p.UnitPrice)
                .ThenBy(p => p.ProductName);
            foreach (var item in resultWhereAndOrderBy)
            {
                Console.WriteLine("Product Name :" + item.ProductName + " UnitPrice : " + item.UnitPrice);
            }
            //OrderBy sonuçları sıralamak için kullanılır
            //ThenBy- OrderBy'da yapılan sıralamada şartı eşit olanları kendi arasında verilen şarta göre sıralar
            //ThenByDescending ise verilen şartın tersi şeklinde sıralar. Örnekte ismine göre sıraladık. Yani Descending kullanımında Z -> A sıralanacaktır.
        }

        private static void FindAllTest(List<Product> products)
        {
            var resultFindAll = products.FindAll(p => p.CategoryId == 1 && p.ProductName.Contains("As"));
            //FindAll ile liste return ediliyor. (Şarta uygun)
        }

        private static void FindTest(List<Product> products)
        {
            var resultFind =
                products.Find(p =>
                    p.ProductId ==
                    3); // Find ile koşulu sağlayan liste elemanı döner. Bu örnekte Id'si 3 olan Product dönecektir.(Varsa)

            Console.WriteLine(resultFind.ProductName);
        }

        private static bool AnyTest(List<Product> products)
        {
            var result =
                products.Any(p =>
                    p.ProductName == "Acer Laptop"); // Any metodu bir listenin içerisinde bir değer var mı kontrol eder
            //Any metodu ile bir bool return edilir.
            //Any metodu ve linq kullanmasaydık listemizi döngüye sokup tek tek elemanları kontrol etmemiz gerekir ve kod uzun ve karmaşık olurdu
            return result;
        }

        private static void Test(List<Product> products)
        {
            Console.WriteLine("Linq ->");

            var result = products.Where(p => p.UnitPrice > 5000 && p.UnitsInStock > 3);
            foreach (var item in result)
            {
                Console.WriteLine(item.ProductName);
                Console.WriteLine(item.UnitPrice);
                Console.WriteLine(item.UnitsInStock);
            }
        }

        static List<Product> getProducts(List<Product> products)
        {
            List<Product> productsFiltered = new List<Product>();
            foreach (var item in products)
            {
                productsFiltered.Add(item);
            } 

            return productsFiltered;
        }


        static List<Product> getProductsLinq(List<Product> products)
        {
            return products.Where(p => p.CategoryId == 2).ToList();
            

        }



    }
}
