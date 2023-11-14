using Engine.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Engine.Services
{
    internal class ItemRepository
    {
        private IOptions<RepositoryOptions> Options { get; }
        public ItemRepository(IOptions<RepositoryOptions> options)
        {
            Options = options;
        }

        public void Create(Item item)
        {
            var items = GetItems();
            SetItems(items.Append(item));
        }

        public IReadOnlyCollection<Item> Items => GetItems();

        private string DbFile => Path.Combine(Options.Value.BasePath, "items.json");
        private IReadOnlyCollection<Item> GetItems() => File.Exists(DbFile) ? JArray.Parse(File.ReadAllText(DbFile)).Select(DeserializeItem).ToArray() : Array.Empty<Item>();
        private void SetItems(IEnumerable<Item> items) => File.WriteAllText(DbFile, new JArray(items.Select(SerializeItem)).ToString(Formatting.None));

        private Item DeserializeItem(JToken item)
        {
            var id = item.Value<string>("id");
            var designCode = item.Value<string>("designCode");
            var attributes = item.Value<JArray>("attributes").Select(x =>
            {
                var code = x.Value<string>("code");
                return x.Value<string>("discriminator") switch
                {
                    "string" => (ItemAttribute)new StringItemAttribute(code, x.Value<string>("value")),
                    "number" => new NumberItemAttribute(code, x.Value<double>("value")),
                    "reference" => new ReferenceItemAttribute(code, x["value"].Cast<string>().ToArray()),
                    _ => throw new InvalidDataException()
                };
            }).ToArray();
            return new Item(id, designCode, attributes);
        }

        private JObject SerializeItem(Item item)
        {
            return new JObject
            {
                ["id"] = item.Id,
                ["designCode"] = item.DesignCode,
                ["attributes"] = new JArray(item.Attributes.Select(a => a switch
                {
                    StringItemAttribute s => new JObject { ["code"] = a.Code, ["value"] = s.Value, ["discriminator"] = "string" },
                    NumberItemAttribute s => new JObject { ["code"] = a.Code, ["value"] = s.Value, ["discriminator"] = "number" },
                    ReferenceItemAttribute s => new JObject { ["code"] = a.Code, ["value"] = new JArray(s.ItemIds), ["discriminator"] = "reference" },
                    _ => throw new InvalidDataException()
                }))
            };
        }
    }
}
