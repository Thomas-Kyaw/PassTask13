using System;
using SplashKitSDK;

namespace PassTask13
{
    public class Customer:User
    {
        private int coupons;
        private List<Order> orders = new List<Order>();
        private List<Merchant> subscribedMerchant =  new List<Merchant>();

        private CustomerStatus customerStatus;
        public Customer() { }

        public Customer(string Username, string Email, string Password) 
            : base(Username, Email, Password) { }
        
        public CustomerStatus CustomerStatus
        {
            get{return customerStatus;}
            set{customerStatus = value;}
        }

        public bool Subscribe(Merchant merchant)
        {
            Console.WriteLine("Select a merchant to check their products:");

            for (int i = 0; i < Admin.Merchants.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Admin.Merchants[i].username}");
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
        }

        public bool Unsubscribe(Merchant merchant)
        {
            
        }

        public List<Product> BrowseByCategory(string category, Merchant merchant)
        {

        }
        
        public List<Product> BrowseByName(string name, Merchant merchant)
        {
            
        }

        public List<Product> BrowseByPriceRange(float min, float max, Merchant merchant)
        {
            
        }

        public Order OrderProduct(Product product, string paymentType)
        {

        }

        public void RateProduct(Product product, int rating)
        {

        }
    }
}
