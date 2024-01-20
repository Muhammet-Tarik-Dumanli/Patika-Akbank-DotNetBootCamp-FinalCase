using System.ComponentModel.DataAnnotations;
using ETS.Base.Schema;

namespace ETS.Schema;

public class ExpenseCategoryRequest : BaseRequest
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
}

public class ExpenseCategoryResponse : BaseResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
}
