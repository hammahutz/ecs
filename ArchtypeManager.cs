using System;
using System.Collections.Generic;

namespace ecs;

public class ArchetypeManager
{
    public Dictionary<ulong, Archetype> Archetypes { get; private set; } =
        new Dictionary<ulong, Archetype>();

    public void Create<T1>(EntityHandler entityHandler, uint amount = 1)
        where T1 : struct, IComponent
    {
        var mask = new Signature().Toggle<T1>(true).GetBits();

        if (!Archetypes.ContainsKey(mask))
        {
            Archetypes[mask] = new Archetype<T1>();
        }

        for (uint i = 0; i < amount; i++)
        {
            Archetypes[mask].AddEntity(entityHandler);
        }
    }

    public void Create<T1, T2>(EntityHandler entityHandler, uint amount = 1)
        where T1 : struct, IComponent
        where T2 : struct, IComponent
    {
        var mask = new Signature().Toggle<T1>(true).Toggle<T2>(true).GetBits();

        if (!Archetypes.ContainsKey(mask))
        {
            Archetypes[mask] = new Archetype<T1, T2>();
        }

        for (uint i = 0; i < amount; i++)
        {
            Archetypes[mask].AddEntity(entityHandler);
        }
    }

    public void Create<T1, T2, T3>(EntityHandler entityHandler, uint amount = 1)
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
    {
        var mask = new Signature().Toggle<T1>(true).Toggle<T2>(true).Toggle<T3>(true).GetBits();

        if (!Archetypes.ContainsKey(mask))
        {
            Archetypes[mask] = new Archetype<T1, T2, T3>();
        }

        for (uint i = 0; i < amount; i++)
        {
            Archetypes[mask].AddEntity(entityHandler);
        }
    }

    public void Create<T1, T2, T3, T4>(EntityHandler entityHandler, uint amount = 1)
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
        where T4 : struct, IComponent
    {
        var mask = new Signature()
            .Toggle<T1>(true)
            .Toggle<T2>(true)
            .Toggle<T3>(true)
            .Toggle<T4>(true)
            .GetBits();

        if (!Archetypes.ContainsKey(mask))
        {
            Archetypes[mask] = new Archetype<T1, T2, T3, T4>();
        }

        for (uint i = 0; i < amount; i++)
        {
            Archetypes[mask].AddEntity(entityHandler);
        }
    }

    public void QueryOnly<T1>(Action<int, T1> action)
        where T1 : struct, IComponent
    {
        var mask = new Signature().Toggle<T1>(true).GetBits();

        if (!Archetypes.ContainsKey(mask))
        {
            Archetypes[mask] = new Archetype<T1>();
        }

        ((Archetype<T1>)Archetypes[mask]).ForEach(action);
    }

    public void QueryOnly<T1, T2>(Action<int, T1, T2> action)
        where T1 : struct, IComponent
        where T2 : struct, IComponent
    {
        var mask = new Signature().Toggle<T1>(true).Toggle<T2>(true).GetBits();

        if (!Archetypes.ContainsKey(mask))
        {
            Archetypes[mask] = new Archetype<T1, T2>();
        }

        ((Archetype<T1, T2>)Archetypes[mask]).ForEach(action);
    }

    public void QueryOnly<T1, T2, T3>(Action<int, T1, T2, T3> action)
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
    {
        var mask = new Signature().Toggle<T1>(true).Toggle<T2>(true).Toggle<T3>(true).GetBits();

        if (!Archetypes.ContainsKey(mask))
        {
            Archetypes[mask] = new Archetype<T1, T2, T3>();
        }

        ((Archetype<T1, T2, T3>)Archetypes[mask]).ForEach(action);
    }

    public void QueryOnly<T1, T2, T3, T4>(Action<int, T1, T2, T3, T4> action)
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
        where T4 : struct, IComponent
    {
        var mask = new Signature()
            .Toggle<T1>(true)
            .Toggle<T2>(true)
            .Toggle<T3>(true)
            .Toggle<T4>(true)
            .GetBits();

        if (!Archetypes.ContainsKey(mask))
        {
            Archetypes[mask] = new Archetype<T1, T2, T3, T4>();
        }
        ((Archetype<T1, T2, T3, T4>)Archetypes[mask]).ForEach(action);
    }

    public void QueryWith<T1>(Action<int, T1> action)
        where T1 : struct, IComponent
    {
        var mask = new Signature().Toggle<T1>(true).GetBits();
        foreach (var archetype in Archetypes)
        {
            if ((archetype.Key & mask) == mask)
            {
                var component = archetype.Value.GetComponent<T1>();

                foreach (var entity in archetype.Value.Entities)
                {
                    action(entity.Id, (T1)component);
                }
            }
        }
    }

    public void QueryWith<T1, T2>(Action<int, T1, T2> action)
        where T1 : struct, IComponent
        where T2 : struct, IComponent
    {
        var mask = new Signature().Toggle<T1>(true).Toggle<T2>(true).GetBits();
        foreach (var keyValue in Archetypes)
        {
            if ((keyValue.Key & mask) == mask)
            {
                var component1 = keyValue.Value.GetComponent<T1>();
                var component2 = keyValue.Value.GetComponent<T2>();

                foreach (var entity in keyValue.Value.Entities)
                {
                    action(entity.Id, (T1)component1, (T2)component2);
                }
            }
        }
    }

    public void QueryWith<T1, T2, T3>(Action<int, T1, T2, T3> action)
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
    {
        var mask = new Signature().Toggle<T1>(true).Toggle<T2>(true).Toggle<T3>(true).GetBits();
        foreach (var keyValue in Archetypes)
        {
            if ((keyValue.Key & mask) == mask)
            {
                var component1 = keyValue.Value.GetComponent<T1>();
                var component2 = keyValue.Value.GetComponent<T2>();
                var component3 = keyValue.Value.GetComponent<T3>();

                foreach (var entity in keyValue.Value.Entities)
                {
                    action(entity.Id, (T1)component1, (T2)component2, (T3)component3);
                }
            }
        }
    }

    public void QueryWith<T1, T2, T3, T4>(Action<int, T1, T2, T3, T4> action)
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
        where T4 : struct, IComponent
    {
        var mask = new Signature()
            .Toggle<T1>(true)
            .Toggle<T2>(true)
            .Toggle<T3>(true)
            .Toggle<T4>(true)
            .GetBits();

        foreach (var keyValue in Archetypes)
        {
            if ((keyValue.Key & mask) == mask)
            {
                var component1 = keyValue.Value.GetComponent<T1>();
                var component2 = keyValue.Value.GetComponent<T2>();
                var component3 = keyValue.Value.GetComponent<T3>();
                var component4 = keyValue.Value.GetComponent<T4>();

                foreach (var entity in keyValue.Value.Entities)
                {
                    action(
                        entity.Id,
                        (T1)component1,
                        (T2)component2,
                        (T3)component3,
                        (T4)component4
                    );
                }
            }
        }
    }
}
