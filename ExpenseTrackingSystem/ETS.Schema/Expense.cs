using ETS.Base.Schema;

namespace ETS.Schema;

public class ExpenseRequest : BaseRequest
{
    public int CategoryId { get; set; }
    public string Title { get; set; }
    public decimal Amount { get; set; }
    public string Location { get; set; }
    public DateTime ExpenseDate { get; set; }
    public int ExpenseCategoryId { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; }
}

public class ExpenseResponse : BaseResponse
{
    public int CategoryId { get; set; }
    public string Title { get; set; }
    public decimal Amount { get; set; }
    public DateTime ExpenseDate { get; set; }
    public int ExpenseCategoryId { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; }
}
