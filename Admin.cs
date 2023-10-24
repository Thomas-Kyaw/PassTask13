using System;
using SplashKitSDK;

namespace PassTask13
{
    public class Admin : User
    {
        private static List<Merchant> merchants = new List<Merchant>();
        private static List<Customer> customers = new List<Customer>();

        public static List<Merchant> Merchants
        {
            get{return merchants;}
        }

        public static List<Customer> Customers
        {
            get{return customers;}
        }
        public Admin(string Username, string Email, string Password):base(Username,Email,Password)
        {
            
        }

        public void CheckProductsByMerchant()
        {
            if(merchants.Count < 1)
            {
                Console.WriteLine("No merchants registered");
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

            List<Product> output = new List<Product>();
            foreach(Product product in selectedMerchant.Products)
            {
                output.Add(product);
            }

            //return output;
        }
        public void ApproveProduct(Product chosenProduct)
        {
            chosenProduct.Status = ProductStatus.APPROVED;

            Data.RegisteredProducts.Remove(chosenProduct);
            chosenProduct.Merchant.Products.Add(chosenProduct);  // use the Merchant property of the Product to know which merchant it belongs to

            Console.WriteLine($"Product {chosenProduct.Name} approved successfully!");
        }


        public void RejectProduct(Product chosenProduct)
        {
            chosenProduct.Status = ProductStatus.REJECTED;

            chosenProduct.Merchant.Products.Remove(chosenProduct); 
            Data.RegisteredProducts.Remove(chosenProduct);
            Data.RejectedProducts.Add(chosenProduct);

            Console.WriteLine($"Product {chosenProduct.Name} has been rejected and removed from pending products.");
        }

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

            Invoice invoice = new Invoice(selectedMerchant, selectedCustomer); // Pass the selected customer here
            Data.AllInvoices.Add(invoice);

            float totalAmount = invoice.CalculateTotal();

            Console.WriteLine($"Invoice for Customer: {selectedCustomer.username}, Merchant: {selectedMerchant.Username}");
            Console.WriteLine($"Total Amount: ${totalAmount}");
        }

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

    }   
}
