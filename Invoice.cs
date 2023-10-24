using System;
using SplashKitSDK;

namespace PassTask13
{
    public class Invoice
    {
        private int id;
        private Merchant merchant;
        
        public Invoice(Merchant _merchant)
        {
            id = Data.NextInvoiceId++;
            merchant = _merchant;
        }
        public float CalculateTotal()
        {
            float total = 0;
            foreach (Order order in this.merchant.Orders) 
            {
                total += order.Product.Price;
            }
            return total;
        }
    }
}