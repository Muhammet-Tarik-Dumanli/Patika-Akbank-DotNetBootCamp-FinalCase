using ETS.Base.Schema;

namespace ETS.Schema;

public class PaymentRequest : BaseRequest
{
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public int ExpenseId { get; set; }
    public string PaymentMethod { get; set; }
    public string Description { get; set; }
}

public class PaymentResponse : BaseResponse
{
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public int ExpenseId { get; set; }
    public string PaymentMethod { get; set; }
    public string Description { get; set; }
}
