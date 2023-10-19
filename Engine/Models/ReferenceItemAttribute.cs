using System.Collections.Generic;
using Engine.Models;

namespace Engine
{
    public record ReferenceItemAttribute(string Code, IReadOnlyCollection<string> ItemIds) : ItemAttribute(Code);
}