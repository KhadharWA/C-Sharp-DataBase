

using Shared.Entities;
using Shared.Models;
using Shared.Services;


namespace ConsoleApp.Service;

public class MenuService(ProductService productServic)
{
    private readonly ProductService _productService = productServic;
    

    public string CreateProductMenu()
    {
        var product = new Product();
        

        Console.Clear();

        Console.Write("ArticleNumber: ");
        product.ArticleNumber = Console.ReadLine()!;

        Console.Write("Title: ");
        product.Title = Console.ReadLine()!;

        Console.Write("Manufacturer Name: ");
        product.Manufacturer = Console.ReadLine()!;


        Console.Write("Category Name: ");
        product.Category = Console.ReadLine()!;


        Console.Write("Ingress: ");
        product.Ingress = Console.ReadLine()!;

        Console.Write("Description: ");
        product.Description = Console.ReadLine()!;

        Console.Write("Specification: ");
        product.Specification = Console.ReadLine()!;

        

        var result = _productService.RegisterProduct(product);
        if (result)
        {
            Console.WriteLine("Product was Created");
            return product.ArticleNumber;
        }
        else
        {
            Console.WriteLine("Something went Wrong!");
            return string.Empty;
        }

            
        

    }

    public void AddImageToProductMenu(string articleNumber)
    {
        Console.Clear();

        Console.Write("Image URL: ");
        string imageUrl = Console.ReadLine()!;

        if (!string.IsNullOrEmpty(imageUrl))
        {
            var productImageEntity = _productService.AddImageToProduct(articleNumber, imageUrl);
            if (productImageEntity != null)
            {
                Console.WriteLine("Image was added to the product successfully.");
            }
            else
            {
                Console.WriteLine("Something went wrong while adding the image.");
            }
        }
        else
        {
            Console.WriteLine("No image URL provided.");
        }

        
    }

    public void AddProductPriceMenu(string articleNumber)
    {
        Console.Clear();

        Console.WriteLine("Enter Currency Code: ");
        string code = Console.ReadLine()!;

        Console.WriteLine("Enter Currency Name: ");
        string currencyName = Console.ReadLine()!;


        Console.WriteLine("Enter Price: ");
        decimal price = decimal.Parse(Console.ReadLine()!);

        

        Console.Write("Enter discount price (Optional): ");
        if (decimal.TryParse(Console.ReadLine(), out decimal discountPrice))
        {
            Console.WriteLine("Invalid input for discount price.");
        }

        


        try
        {
            
            var result = _productService.AddProductPrice(articleNumber, code ,currencyName, price, discountPrice);
            if (result != null)
            {
                Console.WriteLine($"Product price added for article number: {articleNumber}");
            }
            else
            {
                Console.WriteLine("Failed to add product price.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred: {ex.Message}");
        }

        Console.ReadKey();
    }

    


}
