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
