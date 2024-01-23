namespace Domain.OrderAggregate
{
    public enum OrderStatus
    {
        Placed,      
        Processing,  
        Shipped,     
        Delivered,   
        Canceled,    
        Refunded     
    }
}