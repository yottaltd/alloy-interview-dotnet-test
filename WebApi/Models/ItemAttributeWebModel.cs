using Newtonsoft.Json.Linq;

namespace WebApi.Models
{
    public record ItemAttributeWebModel(string Code, JToken Value);
}
