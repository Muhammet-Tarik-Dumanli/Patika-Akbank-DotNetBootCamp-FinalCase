namespace ETS.Data.Entity;

public class ExpenseReport
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal ApprovedAmount { get; set; }
    public decimal RejectedAmount { get; set; }
}