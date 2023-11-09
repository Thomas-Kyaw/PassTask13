using System;
using SplashKitSDK;

namespace PassTask13
{
    public class Customer:User
    {
        /// <summary>
        /// coupons to use in making an order
        /// </summary>
        private int coupons;
        /// <summary>
        /// Orders made by this customer
        /// </summary>
        private List<Order> orders = new List<Order>();
        /// <summary>
        /// List of merchants subscribed to by this customer
        /// </summary>
        private List<Merchant> subscribedMerchants =  new List<Merchant>();
        /// <summary>
        /// enum to check if the customer has been approved or not by the admin
        /// </summary>
        private CustomerStatus customerStatus;
        /// <summary>
        /// empty constructor
        /// </summary>
        public Customer() { }
        /// <summary>
        /// normal constructor
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Email"></param>
        /// <param name="Password"></param>
        public Customer(string Username, string Email, string Password) 
            : base(Username, Email, Password) 
            { 
                coupons = 10;
            }
        /// <summary>
        /// property for Customer Status
        /// </summary>
        public CustomerStatus CustomerStatus
        {
            get{return customerStatus;}
            set{customerStatus = value;}
        }
        /// <summary>
        /// Read only property for Order
        /// </summary>
        public List<Order> Orders 
        {
            get{return orders;}
        }
        /// <summary>
        /// Read only property for merchants subscribed
        /// </summary>
        public List<Merchant> SubscribedMerchants
        {
            get{return subscribedMerchants;}
        }
        /// <summary>
        /// Use this to subscribe to a merchant
        /// </summary>
        public void Subscribe()
        {
            if(Admin.Merchants.Count < 1)
            {
                Console.WriteLine("Sorry. There are no registered merchants currrently");
                return;
            }
            Console.WriteLine("Select a merchant to subscribe:");

            for (int i = 0; i < Admin.Merchants.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Admin.Merchants[i].username}");
            }
            int merchantChoice;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out merchantChoice) || merchantChoice < 1 || merchantChoice > Admin.Merchants.Count)
                {
                    Console.WriteLine("Invalid choice. Please select a valid merchant.");
                }
            } while (merchantChoice < 1 || merchantChoice > Admin.Merchants.Count);

            Merchant selectedMerchant = Admin.Merchants[merchantChoice - 1];
            subscribedMerchants.Add(selectedMerchant);
            selectedMerchant.Subscribers.Add(this);
            Console.WriteLine($"Subscribed to {selectedMerchant.Username}");
        }
        /// <summary>
        /// Use this to unsubscribe to a merchant
        /// </summary>
        public void Unsubscribe()
        {
            if(subscribedMerchants.Count < 1)
            {
                Console.WriteLine("You are not subscribed to any merchants.");
                return;
            }
            Console.WriteLine("Select a merchant to unscribe");

            for (int i = 0; i < subscribedMerchants.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {subscribedMerchants[i].username}");
            }
            int merchantChoice;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out merchantChoice) || merchantChoice < 1 || merchantChoice > subscribedMerchants.Count)
                {
                    Console.WriteLine("Invalid choice. Please select a valid merchant.");
                }
            } while (merchantChoice < 1 || merchantChoice > subscribedMerchants.Count);

            Merchant selectedMerchant = subscribedMerchants[merchantChoice - 1];
            subscribedMerchants.Remove(selectedMerchant);
            selectedMerchant.Subscribers.Remove(this);
            Console.WriteLine($"Unsubscribed from {selectedMerchant.Username}");
        }
        /// <summary>
        /// You can view the products listed by the subscirbed by the merchant by category
        /// </summary>
        public void BrowseProducts()
        {
            if (subscribedMerchants.Count == 0)
            {
                Console.WriteLine("You are not subscribed to any merchants.");
                return;
            }

            Console.WriteLine("Select a merchant from your subscribed list:");
            for (int i = 0; i < subscribedMerchants.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {subscribedMerchants[i].username}");
            }

            int merchantChoice;
            if (int.TryParse(Console.ReadLine(), out merchantChoice) && merchantChoice > 0 && merchantChoice <= subscribedMerchants.Count)
            {
                Merchant selectedMerchant = subscribedMerchants[merchantChoice - 1];

                Console.WriteLine($"Select a product category from {selectedMerchant.username}:");
                List<string> productSubcategories = GetProductSubcategoriesForMerchantCategory(selectedMerchant);

                for (int i = 0; i < productSubcategories.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {productSubcategories[i]}");
                }

                int subcategoryChoice;
                if (int.TryParse(Console.ReadLine(), out subcategoryChoice) && subcategoryChoice > 0 && subcategoryChoice <= productSubcategories.Count)
                {
                    string selectedProductSubcategory = productSubcategories[subcategoryChoice - 1];
                    var productsInSubcategory = selectedMerchant.Products.Where(p => p.Category.Equals(selectedProductSubcategory, StringComparison.OrdinalIgnoreCase)).ToList();
                    DisplayProducts(productsInSubcategory);
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                }
            }
            else
            {
                Console.WriteLine("Invalid merchant choice.");
            }
        }
        /// <summary>
        /// To get the subcategory of the merchant product
        /// </summary>
        /// <param name="merchant"></param>
        /// <returns></returns>
        private List<string> GetProductSubcategoriesForMerchantCategory(Merchant merchant)
        {
            switch (merchant.MerchantCategory) // Assuming Merchant has a MerchantType property of type MerchantCategory
            {
                case MerchantCategory.FOODS:
                    return Enum.GetNames(typeof(FoodCategory)).ToList();
                case MerchantCategory.ELECTRONICS:
                    return Enum.GetNames(typeof(ElectronicsCategory)).ToList();
                case MerchantCategory.TOYS:
                    return Enum.GetNames(typeof(ToysCategory)).ToList();
                case MerchantCategory.ENTERTAINMENT:
                    return Enum.GetNames(typeof(EntertainmentCategory)).ToList();
                case MerchantCategory.FASHION:
                    return Enum.GetNames(typeof(FashionCategory)).ToList();
                case MerchantCategory.LEISURE:
                    return Enum.GetNames(typeof(LeisureCategory)).ToList();
                case MerchantCategory.OTHERS:
                    return Enum.GetNames(typeof(OthersCategory)).ToList();
                default:
                    return new List<string>();
            }
        }
        /// <summary>
        /// method to display the products. To be used in another method
        /// </summary>
        /// <param name="products"></param>
        private void DisplayProducts(List<Product> products)
        {
            if (products.Count == 0)
            {
                Console.WriteLine("No products found in the selected category.");
                return;
            }

            foreach (Product product in products)
            {
                Console.WriteLine($"Name: {product.Name}, Category: {product.Category}, Price: {product.Price}");
            }
        }
        /// <summary>
        /// You can make an order from a chosen merchant.
        /// </summary>
        public void OrderProduct()
        {
            if (subscribedMerchants.Count == 0)
            {
                Console.WriteLine("You are not subscribed to any merchants.");
                return;
            }

            Console.WriteLine("Select a merchant from your subscribed list:");
            for (int i = 0; i < subscribedMerchants.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {subscribedMerchants[i].username}");
            }

            int merchantChoice;
            if (!int.TryParse(Console.ReadLine(), out merchantChoice) || merchantChoice < 1 || merchantChoice > subscribedMerchants.Count)
            {
                Console.WriteLine("Invalid merchant choice.");
                return;
            }

            Merchant selectedMerchant = subscribedMerchants[merchantChoice - 1];
            var products = selectedMerchant.Products;

            Console.WriteLine($"Select a product from {selectedMerchant.username}:");
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {products[i].Name} - ${products[i].Price}");
            }

            int productChoice;
            if (!int.TryParse(Console.ReadLine(), out productChoice) || productChoice < 1 || productChoice > products.Count)
            {
                Console.WriteLine("Invalid product choice.");
                return;
            }

            Product selectedProduct = products[productChoice - 1];

            Console.WriteLine("Enter payment type (1. Cash, 2. Coupon, 3. Credit):");
            int paymentChoice;
            if (!int.TryParse(Console.ReadLine(), out paymentChoice) || paymentChoice < 1 || paymentChoice > 3)
            {
                Console.WriteLine("Invalid payment type choice.");
                return;
            }

            PaymentType selectedPaymentType = (PaymentType)paymentChoice;

            // Check if the payment type is COUPON and if the customer has coupons available
            if (selectedPaymentType == PaymentType.COUPON)
            {
                if (coupons > 0)
                {
                    Console.WriteLine("Your coupon was redeemed successfully");
                    coupons--; // Decrement the coupon count by 1
                }
                else
                {
                    Console.WriteLine("You do not have enough coupons.");
                    return;
                }
            }

            Order newOrder = new Order(this, selectedProduct, selectedPaymentType);
            orders.Add(newOrder);
            selectedMerchant.Orders.Add(newOrder);

            Console.WriteLine($"Ordered {selectedProduct.Name} using {selectedPaymentType}.");
        }
        /// <summary>
        /// You can cancel your order with this method
        /// </summary>
        public void CancelOrder()
        {
            if (orders.Count == 0)
            {
                Console.WriteLine("You have no orders to cancel.");
                return;
            }

            Console.WriteLine("Select an order to cancel:");
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
            selectedOrder.Status = OrderStatus.CANCELLED;

            // Remove the order from the merchant's list of orders
            Merchant merchant = selectedOrder.Product.Merchant;
            merchant.Orders.Remove(selectedOrder);

            orders.Remove(selectedOrder);
            Data.CancelledOrders.Add(selectedOrder);
            Console.WriteLine($"Order {selectedOrder.Id} has been cancelled.");
        }
        /// <summary>
        /// Rate the product you have made an order
        /// </summary>
        public void RateProduct()
        {
            Console.WriteLine("Rating an order");
            if(orders.Count < 1)
            {
                Console.WriteLine("Sorry. You haven't made any Orders");
                return;
            }
            Console.WriteLine("Select a product you have ordered:");

            for (int i = 0; i < orders.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {orders[i].Product.Name}");
            }
            int rateChoice;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out rateChoice) || rateChoice < 1 || rateChoice > orders.Count)
                {
                    Console.WriteLine("Invalid choice. Please select a valid ordered product.");
                }
            } while (rateChoice < 1 || rateChoice > orders.Count);

            Product selectedProduct = orders[rateChoice - 1].Product;
            float rating = 0;
            do
            {
                Console.WriteLine("Enter the rating you want to give (1 to 5):"); // Adjusted the message here
                if(!float.TryParse(Console.ReadLine(), out rating) || rating < 1 || rating > 5)
                {
                    Console.WriteLine("Invalid rating. Rating can be 1 to 5.");
                }
                else
                {
                    selectedProduct.AddRating(rating);
                }
            } while (rating < 1 || rating > 5);
        }

        /// <summary>
        /// Get the invoices for where you made the order.
        /// </summary>
        public void ViewInvoices()
        {
            var customerInvoices = Data.AllInvoices.Where(invoice => invoice.Customer == this).ToList();

            if (customerInvoices.Count == 0)
            {
                Console.WriteLine("You have no invoices.");
                return;
            }

            Console.WriteLine("Your Invoices:");
            foreach (var invoice in customerInvoices)
            {
                float totalAmount = invoice.CalculateTotal();
                Console.WriteLine($"Invoice from Merchant: {invoice.Merchant.Username}");
                Console.WriteLine($"Total Amount: ${totalAmount}");
                Console.WriteLine("----------------------------");
            }
        }

    }
}
