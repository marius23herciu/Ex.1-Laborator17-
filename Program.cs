// See https://aka.ms/new-console-template for more information
using DataLayer.Models;
using Ex._1_Laborator17_;


/*
Exercitiul – 1 – gestiune magazin
1. Stabiliti relatiile dintre clase
2. Realizati diagrama uml
3. Stabiliti relatiile dintre entitati (1-
1,1-*,*-*)
4. Stabiliti Navigation Property-urile si
FK-urile necesare
5. Creeati DB-ul, DBContext-ul, precum
si o functie statica seedDB/resetDB
*/


ResetDb();

using var ctx = new ShopManagementDbContext();

var cars = new Category()
{
    Name = "Cars",
    Pictogram = "https://static.posters.cz/image/750/postere/cars-characters-i33475.jpg"
};
BusinessLayer.AddCategory("Cars", "https://static.posters.cz/image/750/postere/cars-characters-i33475.jpg");

var bmw = new Producer()
{
    Name = "BMW",
    Adresse = "Speed Street, 300",
};
BusinessLayer.AddProducer("BMW", "Speed Street, 300");


var bmwX1 = new Product()
{
    Name = "X1",
    Category = cars,
    Stoc = 12,
    Producer = bmw,
    Label = new Label()
    {
        Price = 200
    }
};
BusinessLayer.ChangePrice(bmwX1, 25000);
var totalStockValue = BusinessLayer.GetTotalStockValue();
var totalStockValueByProducer = BusinessLayer.GetStockValueByProducer(bmw);

var foodProducts = new Category()
{
    Name = "Food Products",
    Pictogram = "https://i0.wp.com/www.fb101.com/wp-content/uploads/2019/07/FoodProductTop.jpg?fit=678%2C298&ssl=1"
};
var totalStockValueByOneCategory = BusinessLayer.GetStockValueByCategory(foodProducts);
var totalStockValueByCategory = BusinessLayer.GetStockValueForEachCategory();
BusinessLayer.AddProduct("320d", 5, 23000, bmw, cars);
var totalValueByProducer = BusinessLayer.GetStockValueForEachProducer();




static void ResetDb()
{
    using var ctx = new ShopManagementDbContext();
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();
    var foodProducts = new Category()
    {
        Name = "Food Products",
        Pictogram = "https://i0.wp.com/www.fb101.com/wp-content/uploads/2019/07/FoodProductTop.jpg?fit=678%2C298&ssl=1"
    };
    var clothingProducts = new Category()
    {
        Name = "Clothing Products",
        Pictogram = "https://dfr4rssi07fv7.cloudfront.net/htx/images/Slider%20-%20main%20site/new-products/nowosci-SLIDER-480.png"
    };
    var officeSupplies = new Category()
    {
        Name = "Office supplies",
        Pictogram = "https://englishlanguageblog.files.wordpress.com/2012/10/office-supplies.jpg"
    };
    ctx.Categories.Add(foodProducts);
    ctx.Categories.Add(clothingProducts);
    ctx.Categories.Add(officeSupplies);

    var boulangerie = new Producer()
    {
        Name="Boulangerie SA",
        Adresse="Bread Street, 20",
    };
    var covalact = new Producer()
    {
        Name = "Covalact SA",
        Adresse = "Milk Street, 11",
    };



    var bread = new Product()
    {
        Name="Bread",
        Category=foodProducts,
        Stoc=20,
        Producer=boulangerie,
        Label=new Label()
        {
            Price=3.5
        }
    };
    var milk = new Product()
    {
        Name = "Milk",
        Category = foodProducts,
        Stoc = 50,
        Producer = covalact,
        Label = new Label()
        {
            Price = 8
        }
    };
    var cheese = new Product()
    {
        Name = "Cheese",
        Category = foodProducts,
        Stoc = 33,
        Producer = covalact,
        Label = new Label()
        {
            Price = 15
        }
    };
    ctx.Products.Add(bread);
    ctx.Products.Add(cheese);
    ctx.Products.Add(milk);

    var adidas = new Producer()
    {
        Name = "Adidas SA",
        Adresse = "Michael Jordan Street, 23",
    };
    var nike = new Producer()
    {
        Name = "Nike SA",
        Adresse = "China Street, 12",
    };


    var adidasShoes = new Product()
    {
        Name = "X14",
        Category = clothingProducts,
        Stoc = 14,
        Producer = adidas,
        Label = new Label()
        {
            Price = 350
        }
    };
    var adidasShoes2 = new Product()
    {
        Name = "Cloud 66",
        Category = clothingProducts,
        Stoc = 21,
        Producer = adidas,
        Label = new Label()
        {
            Price = 200
        }
    };
    var nikeTshirt = new Product()
    {
        Name = "Air",
        Category = clothingProducts,
        Stoc = 12,
        Producer = nike,
        Label = new Label()
        {
            Price = 199
        }
    };
    ctx.Products.Add(adidasShoes);
    ctx.Products.Add(nikeTshirt);   
    ctx.Products.Add(adidasShoes2);

    var mightyPen = new Producer()
    {
        Name = "Mighty Pen SA",
        Adresse = "Library Street, 52",
    };


    var pen = new Product()
    {
        Name = "Pen",
        Category = officeSupplies,
        Stoc = 41,
        Producer = mightyPen,
        Label = new Label()
        {
            Price = 12
        }
    };
    var noteBook = new Product()
    {
        Name = "Notebook",
        Category = officeSupplies,
        Stoc = 35,
        Producer = mightyPen,
        Label = new Label()
        {
            Price = 10
        }
    };
    var stapler = new Product()
    {
        Name = "Stapler",
        Category = officeSupplies,
        Stoc = 10,
        Producer = mightyPen,
        Label = new Label()
        {
            Price = 25
        }
    };
    ctx.Products.Add(pen);
    ctx.Products.Add(noteBook);
    ctx.Products.Add(stapler);

    ctx.SaveChanges();
}