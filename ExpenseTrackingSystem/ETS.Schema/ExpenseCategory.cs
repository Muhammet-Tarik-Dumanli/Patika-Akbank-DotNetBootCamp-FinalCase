using ETS.Base.Schema;

namespace ETS.Schema;

public class ExpenseCategoryRequest : BaseRequest
{
    
    public string CategoryName { get; set; }
    public string Description { get; set; }
}

public class ExpenseCategoryResponse : BaseResponse
{
    public string CategoryName { get; set; }
    public string Description { get; set; }
}
