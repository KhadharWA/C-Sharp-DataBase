


using Shared.Entities;
using Shared.Models;
using Shared.Services;



namespace ConsoleApp.Service;

public class MenuService( ProductService productService, CategoryService categoryService, ManufacturesService manufacturesService )
{
        private readonly ProductService _productService = productService;
        private readonly CategoryService _categoryService = categoryService;
        private readonly ManufacturesService _manufacturesService = manufacturesService;

        public void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("#  MAIN MENU  #");
                Console.WriteLine("-------------------------------------");
                Console.WriteLine();
                Console.WriteLine("1. Add a new Product");
                Console.WriteLine("2. Show all Product in the list");
                Console.WriteLine("3. Update Product in the list");
                Console.WriteLine("4. Remove a Product from the list");
                Console.WriteLine();
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("5. Exit");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        CompleteProductCreationWorkflow();
                        break;
                    case "2":
                        ShowAllProductsMenu();
                        break;
                    case "3":
                        UpdateProductInListMenu();
                        break;
                    case "4":
                        DeleteProductInListMenu();
                        break;
                    case "5":
                        Console.WriteLine("Exiting the program...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please choose a valid option.");
                        break;
                }
                Console.ReadKey();
            }
        }


        public  string  CreateProductMenu()
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


            public void ShowAllProductsMenu()
            {
                Console.Clear();
                Console.WriteLine("All Products\n");
                Console.WriteLine(new string('-', 120)); 
                Console.WriteLine("{0,-20} {1,-25} {2,-20} {3,-15} {4,-15} {5}", "Article Number", "Title", "Manufacturer", "Category", "Price Details", "Image URL");
                Console.WriteLine(new string('-', 120)); 

                try
                {
                 
                    var allProducts = _productService.GetAllProductDetails();

                    if (allProducts.Any())
                    {
                        foreach (var product in allProducts)
                        {
                            Console.WriteLine("{0,-20} {1,-30} {2,-15} {3,-20} {4,-10} {5}",
                            product.ArticleNumber,
                            product.Title,
                            product.ManufacturerName,
                            product.CategoryName,
                            product.PriceDetails,  
                            product.ImageUrl ?? "No Image");
                        }
                        Console.WriteLine(new string('-', 120)); 
                    }
                    else
                    {
                        Console.WriteLine("No products found.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                Console.WriteLine("\nPress any key to return to the main menu...");
                Console.ReadKey();
            }


        public void UpdateProductInListMenu()
        {
            Console.Clear();
            Console.WriteLine("Update Product");
            Console.Write("Enter the Article Number of the product you wish to update: ");
            string articleNumber = Console.ReadLine()!;

        
            var currentProductDetails = _productService.GetProductDetails(articleNumber);
            if (currentProductDetails == null)
            {
                Console.WriteLine($"Product with Article Number {articleNumber} not found.");
                Console.ReadKey();
                return;
            }

        
            Console.WriteLine($"\nCurrent Details for {currentProductDetails.Title}:");
            Console.WriteLine($"Manufacturer: {currentProductDetails.ManufacturerName}");
            Console.WriteLine($"Category: {currentProductDetails.CategoryName}");
            Console.WriteLine($"Ingress: {currentProductDetails.Ingress}");
            Console.WriteLine($"Description: {currentProductDetails.Description}");
            Console.WriteLine($"Specification: {currentProductDetails.Specification}");
        

        
            Console.WriteLine("\nEnter new details (press Enter to skip updating a field):");

            Console.Write("New Title: ");
            string newTitle = Console.ReadLine()!;
            string updatedTitle = !string.IsNullOrWhiteSpace(newTitle) ? newTitle : currentProductDetails.Title;

            Console.Write("New Manufacturer Name: ");
            string newManufacturerName = Console.ReadLine()!;

            Console.Write("New Category Name: ");
            string newCategoryName = Console.ReadLine()!;

            Console.Write("New Ingress: ");
            string newIngress = Console.ReadLine()!;
            string updatedIngress = !string.IsNullOrWhiteSpace(newIngress) ? newIngress : currentProductDetails.Ingress!;

            Console.Write("New Description: ");
            string newDescription = Console.ReadLine()!;
            string updatedDescription = !string.IsNullOrWhiteSpace(newDescription) ? newDescription : currentProductDetails.Description!;

            Console.Write("New Specification: ");
            string newSpecification = Console.ReadLine()!;
            string updatedSpecification = !string.IsNullOrWhiteSpace(newSpecification) ? newSpecification : currentProductDetails.Specification!;

            var manufacturer = _manufacturesService.GetOrCreateManufacturer(newManufacturerName); 
            int manufacturerId = manufacturer.Id;

        
            var category = _categoryService.GetorCreateCategory(newCategoryName); 
            int categoryId = category.Id;

            var updateResult = _productService.UpdateProduct(articleNumber, new ProductEntity
            {
                ArticleNumber = articleNumber, 
                Title = updatedTitle,
                ManufacturerId = manufacturerId,
                CategoryId = categoryId,
                Ingress = updatedIngress!,
                Description = updatedDescription!,
                Specification = updatedSpecification!
            
            });

            if (updateResult)
            {
                Console.WriteLine("\nProduct updated successfully.");
            }
            else
            {
                Console.WriteLine("\nFailed to update the product.");
            }

            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }

        public void DeleteProductInListMenu()
        {
            Console.Clear();
            Console.WriteLine("Delete Product");
            Console.Write("Enter the Article Number of the product you wish to delete: ");
            string articleNumber = Console.ReadLine()!;

        
            Console.WriteLine($"Are you sure you want to delete the product with Article Number: {articleNumber}? (y/n)");
            var confirmation = Console.ReadKey();
            if (confirmation.Key != ConsoleKey.Y)
            {
                Console.WriteLine("\nDeletion cancelled.");
                return;
            }

        
            bool Result = _productService.DeleteProduct(articleNumber!);
            if (Result)
            {
                Console.WriteLine($"\nProduct with Article Number: {articleNumber} successfully deleted.");
            }
            else
            {
                Console.WriteLine($"\nFailed to delete the product with Article Number: {articleNumber}. It may not exist or an error occurred.");
            }

            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }

        public void CompleteProductCreationWorkflow()
        {
            
            Console.WriteLine("### Product Creation Workflow ###\n");
            string articleNumber = CreateProductMenu();
            if (string.IsNullOrEmpty(articleNumber))
            {
                Console.WriteLine("Failed to create product. Exiting workflow.");
                return; 
            }

            
            Console.WriteLine("\n### Add Image to Product ###");
            AddImageToProductMenu(articleNumber); 

            
            Console.WriteLine("\n### Add Price to Product ###");
            AddProductPriceMenu(articleNumber); 

            Console.WriteLine("\nProduct creation workflow completed successfully!");
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }
}

