using System;
using SplashKitSDK;

namespace PassTask13
{
    public class Admin : User
    {
        /// <summary>
        /// List of approved registered merchants
        /// </summary>
        private static List<Merchant> merchants = new List<Merchant>();
        /// <summary>
        /// List of approved registered customers
        /// </summary>
        private static List<Customer> customers = new List<Customer>();

        /// <summary>
        /// read only property for the merchant list
        /// </summary>
        public static List<Merchant> Merchants
        {
            get{return merchants;}
        }
        /// <summary>
        /// read onnly property for the customers list
        /// </summary>
        public static List<Customer> Customers
        {
            get{return customers;}
        }
        /// <summary>
        /// inherited constructor for Admin
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Email"></param>
        /// <param name="Password"></param>
        public Admin(string Username, string Email, string Password):base(Username,Email,Password)
        {
            
        }
        /// <summary>
        /// You can view the products a apporoved merchnat listed
        /// </summary>
        public void CheckProductsByMerchant()
        {
            if (merchants.Count < 1)
            {
                Console.WriteLine("No merchants registered");
                return; // Exit the method if there are no merchants
            }

            Console.WriteLine("Select a merchant to check their products:");

            for (int i = 0; i < merchants.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {merchants[i].username}");
            }

            int merchantChoice;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out merchantChoice) || merchantChoice < 1 || merchantChoice > merchants.Count)
                {
                    Console.WriteLine("Invalid choice. Please select a valid merchant.");
                }
            } while (merchantChoice < 1 || merchantChoice > merchants.Count);

            Merchant selectedMerchant = merchants[merchantChoice - 1];

            if (selectedMerchant.Products.Count == 0)
            {
                Console.WriteLine("This merchant has no products.");
                return; // Exit the method if the merchant has no products
            }

            Console.WriteLine($"Products of {selectedMerchant.username}:");
            foreach (Product product in selectedMerchant.Products)
            {
                Console.WriteLine($"- {product.Name}, Category: {product.Category}, Price: {product.Price}");
            }
        }
        /// <summary>
        /// You can approve a product a merchant has added
        /// </summary>
        /// <param name="chosenProduct"></param>
        public void ApproveProduct(Product chosenProduct)
        {
            chosenProduct.Status = ProductStatus.APPROVED;

            Data.RegisteredProducts.Remove(chosenProduct);
            chosenProduct.Merchant.Products.Add(chosenProduct);  // use the Merchant property of the Product to know which merchant it belongs to

            Console.WriteLine($"Product {chosenProduct.Name} approved successfully!");
        }
        /// <summary>
        /// You can reject a product a merchant has added
        /// </summary>
        /// <param name="chosenProduct"></param>
        public void RejectProduct(Product chosenProduct)
        {
            chosenProduct.Status = ProductStatus.REJECTED;

            chosenProduct.Merchant.Products.Remove(chosenProduct); 
            Data.RegisteredProducts.Remove(chosenProduct);
            Data.RejectedProducts.Add(chosenProduct);

            Console.WriteLine($"Product {chosenProduct.Name} has been rejected and removed from pending products.");
        }
        /// <summary>
        /// method to approve or reject any product listed
        /// </summary>
        public void ReviewProducts()
        {
            Console.WriteLine("Pending products for approval:");

            var pendingProducts = Data.RegisteredProducts.Where(p => p.Status == ProductStatus.PENDING).ToList();

            if (pendingProducts.Count == 0)
            {
                Console.WriteLine("No pending products for approval.");
                return;
            }

            for (int i = 0; i < pendingProducts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {pendingProducts[i].Name}");
            }

            int productChoice;
            do
            {
                Console.WriteLine("Select a product to review:");
                if (!int.TryParse(Console.ReadLine(), out productChoice) 
                    || productChoice < 1 
                    || productChoice > pendingProducts.Count)
                {
                    Console.WriteLine("Invalid choice. Please select a valid product.");
                }
            } while (productChoice < 1 || productChoice > pendingProducts.Count);

            Product chosenProduct = pendingProducts[productChoice - 1];

            Console.WriteLine($"Do you want to approve or reject {chosenProduct.Name}? (Type 'approve' or 'reject')");
            string decision = Console.ReadLine().ToLower();

            if (decision == "approve")
            {
                ApproveProduct(chosenProduct);
                Console.WriteLine($"Product {chosenProduct.Name} approved successfully!");
            }
            else if (decision == "reject")
            {
                RejectProduct(chosenProduct);
                Console.WriteLine($"Product {chosenProduct.Name} rejected!");
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }
        /// <summary>
        /// print out a invoice for an order a customer has made from a merchant
        /// </summary>
        public void GetInvoice()
        {
            if (customers.Count == 0)
            {
                Console.WriteLine("No registered customers.");
                return;
            }

            Console.WriteLine("Select a customer:");
            for (int i = 0; i < customers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {customers[i].username}");
            }

            int customerChoice;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out customerChoice) || customerChoice < 1 || customerChoice > customers.Count)
                {
                    Console.WriteLine("Invalid choice. Please select a valid customer.");
                }
            } while (customerChoice < 1 || customerChoice > customers.Count);

            Customer selectedCustomer = customers[customerChoice - 1];

            if (merchants.Count == 0)
            {
                Console.WriteLine("No registered merchants.");
                return;
            }

            Console.WriteLine("Select a merchant to get the invoice for:");
            for (int i = 0; i < merchants.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {merchants[i].Username}");
            }

            int merchantChoice;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out merchantChoice) || merchantChoice < 1 || merchantChoice > merchants.Count)
                {
                    Console.WriteLine("Invalid choice. Please select a valid merchant.");
                }
            } while (merchantChoice < 1 || merchantChoice > merchants.Count);

            Merchant selectedMerchant = merchants[merchantChoice - 1];

            Invoice invoice = new Invoice(selectedMerchant, selectedCustomer); 
            Data.AllInvoices.Add(invoice);

            float totalAmount = invoice.CalculateTotal();

            Console.WriteLine($"Invoice for Customer: {selectedCustomer.username}, Merchant: {selectedMerchant.Username}");
            Console.WriteLine($"Total Amount: ${totalAmount}");
        }
        /// <summary>
        /// Get customer details
        /// </summary>
        public void ViewCustomerProfile()
        {
            if(customers.Count < 1)
            {
                Console.WriteLine("No registered Customers");
                return;
            }

            Console.WriteLine("Choose the customer");
            for(int i = 0; i < customers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {customers[i].username}");
            }

            int customerChoice;
            do
            {
                if(!int.TryParse(Console.ReadLine(), out customerChoice) || customerChoice < 1 || customerChoice > customers.Count)
                {
                    Console.WriteLine("Invalid choice. Please select a valid customer.");
                }
            } while(customerChoice < 1 || customerChoice > customers.Count);

            Customer selectedCustomer = customers[customerChoice - 1];
            Console.WriteLine("Displaying selected customer details");
            Console.WriteLine($"Name - {selectedCustomer.username}");
            Console.WriteLine($"Email - {selectedCustomer.email}");
            Console.WriteLine($"Subscribed to {selectedCustomer.SubscribedMerchants.Count} merchants");

            if(selectedCustomer.Orders.Count > 0)
            {
                Console.WriteLine("Orders made by the customer:");
                foreach(Order order in selectedCustomer.Orders)
                {
                    Console.WriteLine($"Order ID: {order.Id}, Product: {order.Product.Name}, Status: {order.Status}");
                }
            }
            else
            {
                Console.WriteLine("The customer has not made any orders.");
            }
        }
        /// <summary>
        /// Get merchant details
        /// </summary>
        public void ViewMerchantProfile()
        {
            if (merchants.Count < 1)
            {
                Console.WriteLine("No registered Merchants");
                return;
            }

            Console.WriteLine("Choose the merchant");
            for (int i = 0; i < merchants.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {merchants[i].username}");
            }

            int merchantChoice;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out merchantChoice) || merchantChoice < 1 || merchantChoice > merchants.Count)
                {
                    Console.WriteLine("Invalid choice. Please select a valid merchant.");
                }
            } while (merchantChoice < 1 || merchantChoice > merchants.Count);

            Merchant selectedMerchant = merchants[merchantChoice - 1];
            Console.WriteLine("Displaying selected merchant details");
            Console.WriteLine($"Name - {selectedMerchant.username}");
            Console.WriteLine($"Email - {selectedMerchant.email}");
            Console.WriteLine($"Subscribers: {selectedMerchant.Subscribers.Count}");

            if (selectedMerchant.Products.Count > 0)
            {
                Console.WriteLine("Products offered by the merchant:");
                foreach (Product product in selectedMerchant.Products)
                {
                    Console.WriteLine($"Product Name: {product.Name}, Category: {product.Category}, Price: {product.Price}");
                }
            }
            else
            {
                Console.WriteLine("The merchant has no products.");
            }

            if (selectedMerchant.Orders.Count > 0)
            {
                Console.WriteLine("Orders received by the merchant:");
                foreach (Order order in selectedMerchant.Orders)
                {
                    Console.WriteLine($"Order ID: {order.Id}, Product: {order.Product.Name}, Customer: {order.Customer.username}, Status: {order.Status}");
                }
            }
            else
            {
                Console.WriteLine("The merchant has not received any orders.");
            }
        }
        /// <summary>
        /// Approve a new register merchant
        /// </summary>
        public void ApproveMerchant()
        {
            if (Data.RegisteredMerchants.Count == 0)
            {
                Console.WriteLine("No merchants available for approval.");
                return;
            }

            Console.WriteLine("Select a merchant to approve:");

            for (int i = 0; i < Data.RegisteredMerchants.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Data.RegisteredMerchants[i].username}");
            }

            int merchantChoice;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out merchantChoice) || merchantChoice < 1 || merchantChoice > Data.RegisteredMerchants.Count)
                {
                    Console.WriteLine("Invalid choice. Please select a valid merchant.");
                }
            } while (merchantChoice < 1 || merchantChoice > Data.RegisteredMerchants.Count);

            Merchant selectedMerchant = Data.RegisteredMerchants[merchantChoice - 1];
            selectedMerchant.RegistrationStatus = MerchantStatus.APPROVED;

            // Move the merchant from RegisteredMerchants list to the admin's Merchants list
            Admin.Merchants.Add(selectedMerchant);
            Data.RegisteredMerchants.RemoveAt(merchantChoice - 1);

            Console.WriteLine($"Merchant {selectedMerchant.username} approved successfully!");
        }

        /// <summary>
        /// reject a new register merchant
        /// </summary>
        public void RejectMerchant()
        {
            if (Data.RegisteredMerchants.Count == 0)
            {
                Console.WriteLine("No merchants available for rejection.");
                return;
            }

            Console.WriteLine("Select a merchant to reject:");

            for (int i = 0; i < Data.RegisteredMerchants.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Data.RegisteredMerchants[i].username}");
            }

            int merchantChoice;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out merchantChoice) || merchantChoice < 1 || merchantChoice > Data.RegisteredMerchants.Count)
                {
                    Console.WriteLine("Invalid choice. Please select a valid merchant.");
                }
            } while (merchantChoice < 1 || merchantChoice > Data.RegisteredMerchants.Count);

            Merchant selectedMerchant = Data.RegisteredMerchants[merchantChoice - 1];
            selectedMerchant.RegistrationStatus = MerchantStatus.REJECTED;

            Data.RejectedMerchants.Add(selectedMerchant);
            Data.RegisteredMerchants.RemoveAt(merchantChoice - 1);

            Console.WriteLine($"Merchant {selectedMerchant.username} rejected successfully!");
        }
        /// <summary>
        /// approve a new register customer
        /// </summary>
        public void ApproveCustomer()
        {
            if (Data.RegisteredCustomers.Count == 0)
            {
                Console.WriteLine("No customers available for approval.");
                return;
            }

            Console.WriteLine("Select a customer to approve:");

            for (int i = 0; i < Data.RegisteredCustomers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Data.RegisteredCustomers[i].username}");
            }

            int customerChoice;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out customerChoice)
                    || customerChoice < 1 
                    || customerChoice > Data.RegisteredCustomers.Count)
                {
                    Console.WriteLine("Invalid choice. Please select a valid customer.");
                }
            } while (customerChoice < 1 || customerChoice > Data.RegisteredCustomers.Count);

            Customer selectedCustomer = Data.RegisteredCustomers[customerChoice - 1];
            selectedCustomer.CustomerStatus = CustomerStatus.APPROVED;

            Admin.Customers.Add(selectedCustomer);
            Data.RegisteredCustomers.RemoveAt(customerChoice - 1);


            Console.WriteLine($"Customer {selectedCustomer.username} approved successfully!");
        }
        /// <summary>
        /// reject a new register customer
        /// </summary>
        public void RejectCustomer()
        {
            if (Data.RegisteredCustomers.Count == 0)
            {
                Console.WriteLine("No customers available for rejection.");
                return;
            }

            Console.WriteLine("Select a customer to reject:");

            for (int i = 0; i < Data.RegisteredCustomers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Data.RegisteredCustomers[i].username}");
            }

            int customerChoice;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out customerChoice)
                    || customerChoice < 1 
                    || customerChoice > Data.RegisteredCustomers.Count)
                {
                    Console.WriteLine("Invalid choice. Please select a valid customer.");
                }
            } while (customerChoice < 1 || customerChoice > Data.RegisteredCustomers.Count);

            Customer selectedCustomer = Data.RegisteredCustomers[customerChoice - 1];
            selectedCustomer.CustomerStatus = CustomerStatus.REJECTED;

            Data.RejectedCustomers.Add(selectedCustomer);
            Data.RegisteredCustomers.RemoveAt(customerChoice - 1);

            Console.WriteLine($"Customer {selectedCustomer.username} rejected successfully!");
        }

    }   
}
