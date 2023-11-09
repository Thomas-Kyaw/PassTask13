using System;
using SplashKitSDK;

namespace PassTask13
{
    public class Order
    {
        /// <summary>
        /// unique id of an order
        /// </summary>
        private int id;
        /// <summary>
        /// Customer who has made the order
        /// </summary>
        private Customer customer;
        /// <summary>
        /// Product that the order was made with
        /// </summary>
        private Product product;
        /// <summary>
        /// payment type the customer has chosen to make the order
        /// </summary>
        private PaymentType paymentType;
        /// <summary>
        /// status of the order
        /// </summary>
        private OrderStatus status;
        /// <summary>
        /// constructor of Order
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="product"></param>
        /// <param name="paymentType"></param>
        public Order(Customer customer, Product product, PaymentType paymentType)
        {
            this.id = Data.NextOrderId++;
            this.customer = customer;
            this.product = product;
            this.paymentType = paymentType;
            this.status = OrderStatus.PENDING;
        }
        /// <summary>
        /// read only property for id
        /// </summary>
        public int Id
        {
            get { return id; }
        }
        /// <summary>
        /// read only property for customer
        /// </summary>
        public Customer Customer
        {
            get { return customer; }
        }
        /// <summary>
        /// read only property for product
        /// </summary>
        public Product Product
        {
            get { return product; }
        }
        /// <summary>
        /// read only property for payment type
        /// </summary>
        public PaymentType PaymentType
        {
            get { return paymentType; }
        }
        /// <summary>
        /// property for status field
        /// </summary>
        public OrderStatus Status
        {
            get { return status; }
            set { status = value; } 
        }
        /// <summary>
        /// update the status of the Order
        /// </summary>
        /// <param name="orderStatus"></param>
        /// <returns></returns>
        public bool UpdateStatus(OrderStatus orderStatus)
        {
            status = orderStatus;
            return true;
        }
    }
}
