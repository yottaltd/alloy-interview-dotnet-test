using System.Collections.Generic;
using Engine.Models;

namespace Engine.Services.Abstractions
{
    public interface IEngine
    {
        public Item Get(string Id);
        public Item Create(string designCode, IReadOnlyCollection<ItemAttribute> values);
        public IReadOnlyCollection<Item> Query(QueryModel query);
    }
}