using System;

namespace ecs;

[AttributeUsage(AttributeTargets.Struct)]
public class ComponentAttribute : Attribute
{
    public string Name => this.GetType().Name;

    public override string ToString() => $"Component: {Name}";
}
