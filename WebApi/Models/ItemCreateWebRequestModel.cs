using System.Collections.Generic;

namespace WebApi.Models
{
    public record ItemCreateWebRequestModel(string DesignCode, IReadOnlyCollection<ItemAttributeWebModel> Values);
}