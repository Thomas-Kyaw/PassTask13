using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace PassTask13
{
    public class Merchant:User
    {
        private MerchantCategory merchantCategory;
        private List<Product> products = new List<Product>();
        private List<Order> orders = new List<Order>();
        private List<Customer> subscribers = new List<Customer>();
        private MerchantStatus registrationStatus;

        public Merchant() { }  // Parameterless constructor
        public Merchant(string Username, string Email, string Password) 
            : base(Username, Email, Password) { }
        
        public MerchantStatus RegistrationStatus
        {
            get{return registrationStatus;}
            set{registrationStatus = value;}
        }

        public List<Product> Products
        {
            get{return products;}
            set{products = value;}
        }

        public List<Customer> Subscribers
        {
            get{return subscribers;}
            set{subscribers = value;}
        }

        public void AddProduct()
        {
            Product addedProduct = new Product(this);
            Console.WriteLine("----Adding your product----");
            Console.WriteLine("Enter the name of the product");
            addedProduct.Name = Console.ReadLine();
            Console.WriteLine("Enter the product category");
            addedProduct.Category = Console.ReadLine();
            
            float enteredPrice;
            do
            {
                Console.WriteLine("Enter the price");
            } while (!float.TryParse(Console.ReadLine(), out enteredPrice));
            
            addedProduct.Price = enteredPrice;
            addedProduct.Status = ProductStatus.PENDING;
            Data.RegisteredProducts.Add(addedProduct);
            Console.WriteLine("Product added and awaits approval.");
        }

        public void EditProduct()
        {
            if (products.Count == 0)
            {
                Console.WriteLine($"{username} has no products.");
                return;
            }

            Console.WriteLine("Select a product to edit");
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {products[i].Id}. {products[i].Name}. {products[i].Price}");
            }
            int productChoice;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out productChoice) || productChoice < 1 || productChoice > products.Count)
                {
                    Console.WriteLine("Invalid choice. Please select a valid product.");
                }
            } while (productChoice < 1 || productChoice > products.Count);

            Product selectedProduct = products[productChoice - 1];

            Console.WriteLine("Select which detail you'd like to edit:");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Category");
            Console.WriteLine("3. Price");

            int editChoice;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out editChoice) || editChoice < 1 || editChoice > 3)
                {
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                }
            } while (editChoice < 1 || editChoice > 3);

            switch (editChoice)
            {
                case 1:
                    Console.WriteLine("Enter the new name of the product:");
                    selectedProduct.Name = Console.ReadLine();
                    break;
                case 2:
                    Console.WriteLine("Enter the new category for the product:");
                    selectedProduct.Category = Console.ReadLine();
                    break;
                case 3:
                    Console.WriteLine("Enter the new price for the product:");
                    float enteredPrice;
                    while (!float.TryParse(Console.ReadLine(), out enteredPrice))
                    {
                        Console.WriteLine("Invalid price. Please enter a valid number.");
                    }
                    selectedProduct.Price = enteredPrice;
                    break;
            }

            Console.WriteLine($"Product {selectedProduct.Id} updated successfully!");
        }

        public void DeleteProduct()
        {
            if (products.Count == 0)
            {
                Console.WriteLine($"{username} has no products.");
                return;
            }

            Console.WriteLine("Select a product to edit");
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {products[i].Id}. {products[i].Name}. {products[i].Price}");
            }
            int productChoice;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out productChoice) || productChoice < 1 || productChoice > products.Count)
                {
                    Console.WriteLine("Invalid choice. Please select a valid product.");
                }
            } while (productChoice < 1 || productChoice > products.Count);

            Product selectedProduct = products[productChoice - 1];
            products.Remove(selectedProduct);
            Console.WriteLine("Product removed successfully");
        }
        public void ViewProducts()
        {
            Console.WriteLine("The products you have listed--");
            for(int i = 0;i < products.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {products[i].Id}. {products[i].Name}. {products[i].Category}. {products[i].Price}");
            }
        }
        public bool ManageOrder(Order order, string action)
        {

        }
        public List<Invoice> viewInvoices()
        {

        }

    }
}
