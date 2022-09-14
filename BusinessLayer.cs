using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ex._1_Laborator17_
{
    static class BusinessLayer
    {
        /*
         • Scrieti urmatoarele functii
• Adaugare de categorie

         */
        public static void AddCategory(string name, string pictogramURL)
        {
            var category = new Category()
            {
                Name = name,
                Pictogram = pictogramURL
            };
            using var ctx = new ShopManagementDbContext();
            ctx.Categories.Add(category);
            ctx.SaveChanges();
        }
        /*
         • Adaugare de producator
         */
        public static void AddProducer(string name, string adresse)
        {
            var producer = new Producer()
            {
                Name = name,
                Adresse = adresse
            };
            using var ctx = new ShopManagementDbContext();
            ctx.Producers.Add(producer);
            ctx.SaveChanges();
        }
        /*
         • Modificarea pretului unui produs
         */
        public static void ChangePrice(Product product, double newPrice)
        {
            using var ctx = new ShopManagementDbContext();
            var productChangePrice= ctx.Products
                .Include(p => p.Label)
                .FirstOrDefault(s=>s==product);
            if (productChangePrice!=null)
            {
                productChangePrice.Label.Price = newPrice;
            }
            ctx.SaveChanges();
        }
        /*
         • Obtinerea valorii totale a stocului
magazinului
         */
        public static double GetTotalStockValue()
        {
            using var ctx = new ShopManagementDbContext();
            var totalStock = ctx.Products
                .Include(l=>l.Label)
                .Sum(s => s.Stoc*s.Label.Price);
            return totalStock;
        }
        /*
         • Obtinearea valorii stocului de la un
anumit producator oferit ca parametru
         */
        public static double GetStockValueByProducer(Producer producer)
        {
            using var ctx = new ShopManagementDbContext();
            var stockByProducer = ctx.Products.Include(l => l.Label)
                .Include(p=>p.Producer)
                .Where(p=>p.Producer==producer)
                .Sum(s => s.Stoc * s.Label.Price);
            return stockByProducer;
        }
        /*
         • Obtinerea valorii totale a stocului per
categorie
         */
        public static double GetStockValueByCategory(Category category)
        {
            using var ctx = new ShopManagementDbContext();
            var stockByCategory = ctx.Products
                .Include(l => l.Label)
                .Include(p => p.Category)
                .Where(p => p.Category == category)
                .Sum(s => s.Stoc*s.Label.Price);
            return stockByCategory;
        }
        /*
         • Obtinerea valorii totale a stocului per
categorie
         */
        public static Dictionary<Category, double> GetStockValueForEachCategory()
        {
            var ctx = new ShopManagementDbContext();

            Dictionary<Category, double> stocksForEachCategory = new Dictionary<Category, double>();

            var groupByCategory = ctx.Products
                .Include(l => l.Label)
                .Include(p => p.Category)
                .GroupBy(p=>p.Category);
            foreach (var group in groupByCategory)
            {
                double stocByGroup = 0;
                foreach (var product in group)
                {
                    stocByGroup += product.Stoc*product.Label.Price;
                }
                stocksForEachCategory.Add((Category)group, stocByGroup);
            }

            return stocksForEachCategory;
        }
        /*
         • Suplimentar
• Adaugare de produs
       - Va adauga automat si eticheta
         */
        public static void AddProduct(string name, int stoc, double price, Producer producer, Category category)
        {
            var product = new Product()
            {
                Name = name,
                Producer=producer,
                Stoc=stoc,
                Category=category,
                Label=new Label()
                {
                    Price=price,
                }
            };
            using var ctx = new ShopManagementDbContext();
            ctx.Add(product);
            ctx.SaveChanges();
        }
        /*
         • Suplimentar
• Obtinerea valorii stocului per categorie
per producator
         */
        public static Dictionary<Producer, double> GetStockValueForEachProducer()
        {
            var ctx = new ShopManagementDbContext();

            Dictionary<Producer, double> stockValueForEachProducer = new Dictionary<Producer, double>();

            var groupByProducer = ctx.Products
                .Include(p => p.Producer)
                .Include(p => p.Label)
                .GroupBy(p => p.Producer);
            foreach (var group in groupByProducer)
            {
                double stocValueByGroup = 0;
                foreach (var product in group)
                {
                    stocValueByGroup += product.Stoc*product.Label.Price;
                }
                stockValueForEachProducer.Add((Producer)group, stocValueByGroup);
            }

            return stockValueForEachProducer;
        }
    }
}
