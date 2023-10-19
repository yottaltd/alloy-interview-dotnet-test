using Engine.Models;

namespace Engine
{
    public record StringItemAttribute(string Code, string Value) : ItemAttribute(Code);
}