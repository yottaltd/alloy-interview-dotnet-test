using Engine;
using Engine.Models;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using WebApi.Models;

namespace WebApi.Converters
{
    public static class ItemConverters
    {
        public static ItemWebModel ToWebModel(this Item item)
        {
            return new ItemWebModel(item.Id, item.DesignCode, item.Attributes.Select(ToWebModel).ToArray());
        }

        public static ItemAttributeWebModel ToWebModel(this ItemAttribute attribute)
        {
            return attribute switch
            {
                StringItemAttribute s => new ItemAttributeWebModel(s.Code, JToken.FromObject(s.Value)),
                NumberItemAttribute n => new ItemAttributeWebModel(n.Code, JToken.FromObject(n.Value)),
                ReferenceItemAttribute r => new ItemAttributeWebModel(r.Code, new JArray(r.ItemIds)),
                _ => throw new InvalidDataException()
            };
        }

        public static ItemAttribute ToDomainModel(this ItemAttributeWebModel attribute)
        {
            return attribute.Value.Type switch
            {
                JTokenType.String => new StringItemAttribute(attribute.Code, attribute.Value.Value<string>()),
                JTokenType.Float => new NumberItemAttribute(attribute.Code, attribute.Value.Value<double>()),
                JTokenType.Array => new ReferenceItemAttribute(attribute.Code, attribute.Value.Values<string>().ToArray()),
                _ => throw new InvalidDataException()
            };
        }
    }
}
