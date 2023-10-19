using System.Collections.Generic;

namespace WebApi.Models
{
    public record QueryWebResponseModel(IReadOnlyCollection<ItemWebModel> Items);
}
