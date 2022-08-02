using System.Runtime.Serialization;

namespace Priyosaj.Core.Entities.OrderEntities;

public enum OrderStatus
{
    [EnumMember(Value = "Pending")]
    Pending, 
    
    [EnumMember(Value = "PaymentCompleted")]
    PaymentCompleted,
    
    [EnumMember(Value = "PaymentFailed")]
    PaymentFailed,
    
    // Shipped,
    // Delivered
}