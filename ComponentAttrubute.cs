using System;

namespace ecs;

[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property,
    Inherited = false
)]
public class ComponentAttribute : Attribute
{
    public string Name => this.GetType().Name;

    public override string ToString() => $"Component: {Name}";
}
