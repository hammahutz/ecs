using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ecs;

public static class ComponentTypes
{
    private static Dictionary<Type, int> _componentTypeIds = new Dictionary<Type, int>();
    private static int _nextBit = 0;

    private static void RegisterComponent(Type type)
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
        var types = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.GetCustomAttribute<ComponentAttribute>() != null)
            .ToList();

        foreach (var type in types)
        {
            if (!typeof(IComponent).IsAssignableFrom(type))
                throw new InvalidOperationException(
                    $"Component type {type.Name} must implement IComponent interface."
                );

            RegisterComponent(type);
        }
    }

    public static int GetComponentBit<T1>()
    {
        if (_componentTypeIds.TryGetValue(typeof(T1), out int bit))
        {
            return bit;
        }
        else
        {
            throw new InvalidOperationException(
                $"Could not register component type {typeof(T1).Name}."
            );
        }
    }

    public static int GetComponentBit<T1, T2>() =>
        GetComponentBit(typeof(T1)) | GetComponentBit(typeof(T2));

    public static int GetComponentBit<T1, T2, T3>() =>
        GetComponentBit(typeof(T1)) | GetComponentBit(typeof(T2)) | GetComponentBit(typeof(T3));

    public static int GetComponentBit<T1, T2, T3, T4>() =>
        GetComponentBit(typeof(T1))
        | GetComponentBit(typeof(T2))
        | GetComponentBit(typeof(T3))
        | GetComponentBit(typeof(T4));

    public static int GetComponentBit(Type type)
    {
        if (_componentTypeIds.TryGetValue(type, out int bit))
        {
            return bit;
        }
        else
        {
            throw new InvalidOperationException($"Could not register component type {type.Name}.");
        }
    }

    public static Signature GetSignature(params Type[] type)
    {
        var signature = new Signature();
        foreach (var t in type)
        {
            signature.Toggle(t, true);
        }
        return signature;
    }
}
