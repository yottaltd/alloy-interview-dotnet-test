using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Models;
using Engine.Services.Abstractions;

namespace Engine.Services
{
    internal class EngineService : IEngine
    {
        private ItemRepository Repository { get; }
        public EngineService(ItemRepository repository)
        {
            Repository = repository;
        }

        public Item Create(string designCode, IReadOnlyCollection<ItemAttribute> values)
        {
            var id = Guid.NewGuid().ToString().Trim('}', '{');
            var item = new Item(id, designCode, values);
            Repository.Create(item);
            return item;
        }

        public Item Get(string Id)
        {
            return Repository.Items.Single(x => x.Id == Id);
        }

        public IReadOnlyCollection<Item> Query(QueryModel query)
        {
            throw new NotImplementedException();
        }
    }
}