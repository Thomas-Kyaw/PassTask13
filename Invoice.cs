using System;
using SplashKitSDK;

namespace PassTask13
{
    public class Invoice
    {
        private int id;
        private Merchant merchant;

        private Customer customer;
        
        public Invoice(Merchant _merchant, Customer _customer)
        {
            id = Data.NextInvoiceId++;
            merchant = _merchant;
            customer = _customer;
        }

        public Merchant Merchant
        {
            get{return merchant;}
        }
        public Customer Customer
        {
            get{return customer;}
        }
        public int Id
        {
            get{return id;}
        }
        public float CalculateTotal()
        {
            float total = 0;
            foreach (Order order in this.customer.Orders) 
            {
                if(order.Product.Merchant == this.merchant) // Check the merchant via the product
                {
                    total += order.Product.Price;
                }
            }
            return total;
        }
    }
}
