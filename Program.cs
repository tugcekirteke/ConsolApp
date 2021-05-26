using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConsoleApp
{
    public class ShopContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
     
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public static readonly ILoggerFactory MyLoggerFactory
    = LoggerFactory.Create(builder => { builder.AddConsole(); });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            .UseLoggerFactory(MyLoggerFactory)
            .UseMySql(@"server=localhost;port=3306;database=ShopDb;user=root;password=regurjitasyon13;");

            //.UseSqlite("Data Source=shopdb.db");

        }
    }

    public class Product
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }
        public int categoryId { get; set; }

    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

     public class User
    { 

    public int Id { get; set; }  


    public string  Username { get; set; }

    public string Email { get; set; }

    public List<Address> Addresses{get;set;}

    }

    public class Address
    {

        public int Id { get; set; } 

      public string FullName { get; set; }
      
      public string Title { get; set; } 

      public string Body { get; set; }

        public User User { get; set; }


     // public int? UserId  { get; set; }   
       public int UserId  { get; set; }



    }



    class Program
    {
        static void Main(string[] args)
        {
            //AddProduct();
            //AddProducts();
            // GetAllProducts();
            //GetProductById(1);
            //  GetProductByName("SAMSUNG");
            //UpdateProduct();
          //  DeleteProduct(3);

        }

        static void DeleteProduct(int id)
        {
            using (var db = new ShopContext())
            {
                var p = new Product() { Id = 6 };
                db.Entry(p).State = EntityState.Deleted;
                db.SaveChanges();



                /* var p =db.Products.FirstOrDefault(i=>i.Id==id);
                 if(p!=null)
                 {
                   db.Products.Remove(p);
                   db.SaveChanges();     
                   Console.WriteLine("veri silindi");
                 }*/
            }

        }


        static void UpdateProduct()
        {


            using (var db = new ShopContext())
            {
                var p = db.Products.Where(i => i.Id == 1).FirstOrDefault();
                if (p != null)
                {
                    p.Price = 2400;
                    db.Products.Update(p);
                    db.SaveChanges();
                }
            }


            /*************************************************************************
                       using(var db=new ShopContext())
                       {
                         var entity= new Product(){Id=1};
                         db.Products.Attach(entity);
                         entity.Price=3000;
                         db.SaveChanges();
                       }

            /*************************************************************************/
            /* using (var db=new ShopContext())
             {
                 var p=db
                 .Products
                 .Where(i=>i.Id==1)
                 .FirstOrDefault();
                 if(p!=null)
                 {
                     p.Price *=1.2m;
                     db.SaveChanges();
                     Console.WriteLine("güncelleme yapıldı");

                 }
             }*/
        }

        static void GetProductByName(string name)
        {
            using (var context = new ShopContext())
            {
                var products = context
                .Products
                .Where(p => p.Name.ToLower().Contains(name.ToLower()))
                .Select(p =>
                new
                {
                    p.Name,
                    p.Price
                })

                .ToList();
                foreach (var p in products)
                {
                    Console.WriteLine($"name: {p.Name} Price: {p.Price}");
                }





            }
        }
        static void GetProductById(int id)
        {
            using (var context = new ShopContext())
            {
                var result = context
                .Products
                .Where(p => p.Id == id)
                .Select(p =>
                new
                {
                    p.Name,
                    p.Price
                })

                .FirstOrDefault();



                Console.WriteLine($"name: {result.Name} Price: {result.Price}");

            }
        }
        static void GetAllProducts()
        {
            using (var context = new ShopContext())
            {
                var products = context
                .Products
                .Select(p =>
                new
                {
                    p.Name,
                    p.Price
                })

                .ToList();

                foreach (var p in products)
                {
                    Console.WriteLine($"name: {p.Name} Price: {p.Price}");
                }
            }
        }

        static void AddProducts()
        {
            using (var db = new ShopContext())
            {


                var products = new List<Product>()
                {
                  new Product {Name="Samsung S5",Price=2000},
                  new Product {Name="Samsung S5",Price=2000},
                  new Product {Name="Samsung S5",Price=2000},
                  new Product {Name="Samsung S5",Price=2000}
                };

                // foreach (var p in products)
                //  {
                //        db.Products.Add(p);
                //  }
                db.Products.AddRange(products);
                db.SaveChanges();
                Console.WriteLine("veriler eklendi");
            }
        }

        static void AddProduct()
        {
            using (var db = new ShopContext())
            {


                var p = new Product { Name = "Samsung S55", Price = 24000 };


                // foreach (var p in products)
                //  {
                //        db.Products.Add(p);
                //  }
                db.Products.Add(p);
                db.SaveChanges();
                Console.WriteLine("veriler eklendi");
            }
        }

    }
}
