using System.Collections.Generic;

namespace WebApi.Models
{
    public record ItemWebModel(string Id, string DesignCode, IReadOnlyCollection<ItemAttributeWebModel> Attributes);
}
