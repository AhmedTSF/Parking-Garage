using Domain.Enums;
using System.Text.Json.Serialization;

namespace Application.DTOs.Payment;

public class PayPaymentDto
{
    public int SessionId { get; set; }

    /// <summary>
    /// Payment method: 1 = PayPal, 2 = Credit Card, ..etc
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PaymentMethod Method { get; set; }


}
