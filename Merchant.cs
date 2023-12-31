using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace PassTask13
{
    public class Merchant:User
    {
        /// <summary>
        /// Category of the merchant
        /// </summary>
        private MerchantCategory merchantCategory;
        /// <summary>
        /// Products listed by the merchant
        /// </summary>
        private List<Product> products = new List<Product>();
        /// <summary>
        /// Orders recieved by the merchant
        /// </summary>
        private List<Order> orders = new List<Order>();
        /// <summary>
        /// Customers who have subscribed to the merchant
        /// </summary>
        private List<Customer> subscribers = new List<Customer>();
        /// <summary>
        /// status of the newly register merchant
        /// </summary>
        private MerchantStatus registrationStatus;
        /// <summary>
        /// constructor of merchant, the category field is set the OTHERS(MISC) by default
        /// </summary>
        public Merchant() { }  
        public Merchant(string Username, string Email, string Password) 
            : base(Username, Email, Password) 
            {
                merchantCategory = MerchantCategory.OTHERS;
            }
        /// <summary>
        /// property for merchant category field
        /// </summary>
        public MerchantCategory MerchantCategory
        {
            get{return merchantCategory;}
            set{merchantCategory = value;}
        }
        /// <summary>
        /// property for merchant status field
        /// </summary>
        public MerchantStatus RegistrationStatus
        {
            get{return registrationStatus;}
            set{registrationStatus = value;}
        }
        /// <summary>
        /// property for orders field
        /// </summary>
        public List<Order> Orders
        {
            get{return orders;}
            set{orders = value;}
        }
        /// <summary>
        /// read only property for the merchant username
        /// </summary>
        public string Username
        {
            get{return base.username;}
        }
        /// <summary>
        /// property for Products field
        /// </summary>
        public List<Product> Products
        {
            get{return products;}
            set{products = value;}
        }
        /// <summary>
        /// property for Subscirbed Customers field
        /// </summary>
        public List<Customer> Subscribers
        {
            get{return subscribers;}
            set{subscribers = value;}
        }
        /// <summary>
        /// merchant can use this method to add a product
        /// </summary>
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

        /// <summary>
        /// merchant can use this method to edit a product details
        /// </summary>
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

        /// <summary>
        /// merchant can use this method to delete a product
        /// </summary>
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
        /// <summary>
        /// merchant can view the products it is listed 
        /// </summary>
        public void ViewProducts()
        {
            Console.WriteLine("The products you have listed--");
            for(int i = 0;i < products.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {products[i].Id}. {products[i].Name}. {products[i].Category}. {products[i].Price}");
            }
        }
        /// <summary>
        /// Merchant can update the status of the order
        /// </summary>
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
                    Data.CancelledOrders.Add(selectedOrder);
                    orders.Remove(selectedOrder);
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
        /// <summary>
        /// merchant can view the invoices of orders a particular customer has made from it
        /// </summary>
        public void ViewInvoices()
        {
            var merchantInvoices = Data.AllInvoices.Where(invoice => invoice.Merchant == this).ToList();

            if (merchantInvoices.Count == 0)
            {
                Console.WriteLine("You have no invoices.");
                return;
            }

            Console.WriteLine("Your Invoices:");
            foreach (var invoice in merchantInvoices)
            {
                float totalAmount = invoice.CalculateTotal();
                Console.WriteLine($"Invoice for Customer: {invoice.Customer.username}");
                Console.WriteLine($"Total Amount: ${totalAmount}");
                Console.WriteLine("----------------------------");
            }
        }

        /// <summary>
        /// to check whether the enterd category is valid or not.
        /// </summary>
        /// <param name="category"></param>
        /// <returns>bool</returns>
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
