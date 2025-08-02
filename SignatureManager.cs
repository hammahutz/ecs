using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ecs;

public static class SignatureManager
{
    private static Dictionary<Type, int> _componentTypeIds = new Dictionary<Type, int>();
    private static int _nextBit = 0;

    public static void RegisterComponent(Type type)
    {
        if (!_componentTypeIds.ContainsKey(type))
        {
            _componentTypeIds[type] = 1 << _nextBit;
            _nextBit++;
        }
        else
        {
            Console.WriteLine($"Component type {type.Name} is already registered.");
        }
    }

    public static void RegisterComponents()
    {
        Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.GetCustomAttribute<ComponentAttribute>() != null)
            .ToList()
            .ForEach(t => RegisterComponent(t));
    }

    public static int GetComponentBit<T>()
    {
        if (_componentTypeIds.TryGetValue(typeof(T), out int bit))
        {
            return bit;
        }
        else
        {
            throw new InvalidOperationException(
                $"Could not register component type {typeof(T).Name}."
            );
        }
    }
}
