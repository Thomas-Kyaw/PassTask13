using System;
using SplashKitSDK;

namespace PassTask13
{
    public class Customer:User
    {
        private int coupons;
        private List<Order> orders = new List<Order>();
        private List<Merchant> subscribedMerchants =  new List<Merchant>();

        private CustomerStatus customerStatus;
        public Customer() { }

        public Customer(string Username, string Email, string Password) 
            : base(Username, Email, Password) { }
        
        public CustomerStatus CustomerStatus
        {
            get{return customerStatus;}
            set{customerStatus = value;}
        }

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


        public Order OrderProduct(Product product, string paymentType)
        {

        }

        public void RateProduct(Product product, int rating)
        {

        }
    }
}
