using System;
using SplashKitSDK;

namespace PassTask13
{
    public class Invoice
    {
        /// <summary>
        /// unique id of the invoice. Will be incremented by 1 everytime a new invoice is made.
        /// </summary>
        private int id;
        /// <summary>
        /// merchant associated with the order
        /// </summary>
        private Merchant merchant;
        /// <summary>
        /// customer who made the order
        /// </summary>
        private Customer customer;
        /// <summary>
        /// constructor for invoice
        /// </summary>
        /// <param name="_merchant"></param>
        /// <param name="_customer"></param>
        public Invoice(Merchant _merchant, Customer _customer)
        {
            id = Data.NextInvoiceId++;
            merchant = _merchant;
            customer = _customer;
        }
        /// <summary>
        /// read only property for merchant field
        /// </summary>
        public Merchant Merchant
        {
            get{return merchant;}
        }
        /// <summary>
        /// read only propoerty for customer field
        /// </summary>
        public Customer Customer
        {
            get{return customer;}
        }
        /// <summary>
        /// read only property for Id field
        /// </summary>
        public int Id
        {
            get{return id;}
        }
        /// <summary>
        /// method for calculating the total amount made by a customer
        /// </summary>
        /// <returns></returns>
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
