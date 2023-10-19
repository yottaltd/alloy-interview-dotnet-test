using Engine.Models;

namespace Engine
{
    public record NumberItemAttribute(string Code, double Value) : ItemAttribute(Code);
}