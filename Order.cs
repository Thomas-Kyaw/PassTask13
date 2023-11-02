using System;
using SplashKitSDK;

namespace PassTask13
{
    public class Order
    {
        private int id;
        private Customer customer;
        private Product product;
        private PaymentType paymentType;
        private OrderStatus status;
        public Order(Customer customer, Product product, PaymentType paymentType)
        {
            this.id = Data.NextOrderId++;
            this.customer = customer;
            this.product = product;
            this.paymentType = paymentType;
            this.status = OrderStatus.PENDING;
        }

        public int Id
        {
            get { return id; }
        }

        public Customer Customer
        {
            get { return customer; }
        }

        public Product Product
        {
            get { return product; }
        }

        public PaymentType PaymentType
        {
            get { return paymentType; }
        }

        public OrderStatus Status
        {
            get { return status; }
            set { status = value; } 
        }

        public bool UpdateStatus(OrderStatus orderStatus)
        {
            status = orderStatus;
            return true;
        }
    }
}
