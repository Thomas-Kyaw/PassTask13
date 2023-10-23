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
        private Invoice invoice;
        public Order(Customer customer, Product product, PaymentType paymentType)
        {
            this.customer = customer;
            this.product = product;
            this.paymentType = paymentType;
            this.status = OrderStatus.PENDING;
        }

        public bool UpdateStatus(OrderStatus orderStatus)
        {
            status = orderStatus;
            return true;
        }
        public Product Product
        {
            get{return product;}
        }
    }
}
