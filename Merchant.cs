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
            : base(Username, Email, Password) 
            {
                merchantCategory = MerchantCategory.OTHERS;
            }
        
        public MerchantCategory MerchantCategory
        {
            get{return merchantCategory;}
        }

        public MerchantStatus RegistrationStatus
        {
            get{return registrationStatus;}
            set{registrationStatus = value;}
        }

        public List<Order> Orders
        {
            get{return orders;}
            set{orders = value;}
        }

        public string Username
        {
            get{return base.username;}
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

            string input1;
            do
            {
                Console.WriteLine("Enter the name of the product");
                input1 = Console.ReadLine();
            } while (string.IsNullOrEmpty(input1));
            addedProduct.Name = input1;

            string input2;
            do
            {
                Console.WriteLine("Enter the product category");
                input2 = Console.ReadLine();
            } while (string.IsNullOrEmpty(input2));

            addedProduct.Category = input2; // This will validate and set the category using the property's setter

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
                    string newCategory;
                    do
                    {
                        Console.WriteLine("Enter the new category for the product:");
                        newCategory = Console.ReadLine();
                        if (string.IsNullOrEmpty(newCategory) || !IsValidCategory(newCategory))
                        {
                            Console.WriteLine("Invalid category. Please enter a valid category.");
                        }
                    } while (string.IsNullOrEmpty(newCategory) || !IsValidCategory(newCategory));
                    selectedProduct.Category = newCategory; 
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

        public void ManageOrder()
        {
            if (orders.Count == 0)
            {
                Console.WriteLine($"{username} has no orders to manage.");
                return;
            }

            Console.WriteLine("Select an order to manage:");
            for (int i = 0; i < orders.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Order ID: {orders[i].Id}, Product: {orders[i].Product.Name}, Status: {orders[i].Status}");
            }

            int orderChoice;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out orderChoice) || orderChoice < 1 || orderChoice > orders.Count)
                {
                    Console.WriteLine("Invalid choice. Please select a valid order.");
                }
            } while (orderChoice < 1 || orderChoice > orders.Count);

            Order selectedOrder = orders[orderChoice - 1];

            Console.WriteLine("Select which action you'd like to take:");
            Console.WriteLine("1. Accept");
            Console.WriteLine("2. Decline");
            Console.WriteLine("3. Mark as Shipped");
            Console.WriteLine("4. Mark as Delivered");
            Console.WriteLine("5. Cancel");

            int actionChoice;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out actionChoice) || actionChoice < 1 || actionChoice > 5)
                {
                    Console.WriteLine("Invalid choice. Please select a valid action.");
                }
            } while (actionChoice < 1 || actionChoice > 5);

            switch (actionChoice)
            {
                case 1:
                    selectedOrder.Status = OrderStatus.APPROVED;
                    Console.WriteLine($"Order {selectedOrder.Id} has been accepted.");
                    break;
                case 2:
                    selectedOrder.Status = OrderStatus.REJECTED;
                    Console.WriteLine($"Order {selectedOrder.Id} has been declined.");
                    break;
                case 3:
                    selectedOrder.Status = OrderStatus.SHIPPED;
                    Console.WriteLine($"Order {selectedOrder.Id} has been marked as shipped.");
                    break;
                case 4:
                    selectedOrder.Status = OrderStatus.DELIVERED;
                    Console.WriteLine($"Order {selectedOrder.Id} has been marked as delivered.");
                    break;
                case 5:
                    selectedOrder.Status = OrderStatus.CANCELLED;
                    orders.Remove(selectedOrder);
                    Data.CancelledOrders.Add(selectedOrder);  
                    Console.WriteLine($"Order {selectedOrder.Id} has been cancelled.");
                    break;
            }
        }


        public List<Invoice> viewInvoices()
        {
            return new List<Invoice>();
        }

        private bool IsValidCategory(string category)
        {
            return Enum.IsDefined(typeof(FoodCategory), category) ||
                Enum.IsDefined(typeof(ElectronicsCategory), category) ||
                Enum.IsDefined(typeof(ToysCategory), category) ||
                Enum.IsDefined(typeof(EntertainmentCategory), category) ||
                Enum.IsDefined(typeof(FashionCategory), category) ||
                Enum.IsDefined(typeof(LeisureCategory), category) ||
                Enum.IsDefined(typeof(OthersCategory), category);
        }

    }
}
