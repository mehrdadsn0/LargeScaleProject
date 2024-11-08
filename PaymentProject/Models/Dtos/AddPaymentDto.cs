namespace PaymentProject.Models.Dtos;

public record AddPaymentDto{
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
}