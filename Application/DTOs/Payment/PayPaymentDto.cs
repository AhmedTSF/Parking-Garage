using Domain.Enums;
using System.Text.Json.Serialization;

namespace Application.DTOs.Payment;

public class PayPaymentDto
{
    public int SessionId { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PaymentMethod Method { get; set; }


}
