using System;
using System.Collections.Generic;

namespace ecs;

public class ArchetypeManager
{
    public Dictionary<ulong, Archetype> Archetypes { get; private set; } =
        new Dictionary<ulong, Archetype>();

    public Archetype<T1> Get<T1>()
        where T1 : struct
    {
        var mask = new Signature().Toggle<T1>(true).GetBits();
        if (!Archetypes.ContainsKey(mask))
        {
            Archetypes[mask] = new Archetype<T1>();
        }
        return (Archetype<T1>)Archetypes[mask];
    }

    public Archetype<T1, T2> Get<T1, T2>()
        where T1 : struct
        where T2 : struct
    {
        var mask = new Signature().Toggle<T1>(true).Toggle<T2>(true).GetBits();
        if (!Archetypes.ContainsKey(mask))
        {
            Archetypes[mask] = new Archetype<T1, T2>();
        }
        return (Archetype<T1, T2>)Archetypes[mask];
    }

    public Archetype<T1, T2, T3> Get<T1, T2, T3>()
        where T1 : struct
        where T2 : struct
        where T3 : struct
    {
        var mask = new Signature().Toggle<T1>(true).Toggle<T2>(true).Toggle<T3>(true).GetBits();
        if (!Archetypes.ContainsKey(mask))
        {
            Archetypes[mask] = new Archetype<T1, T2, T3>();
        }
        return (Archetype<T1, T2, T3>)Archetypes[mask];
    }

    public Archetype<T1, T2, T3, T4> Get<T1, T2, T3, T4>()
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
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
        return (Archetype<T1, T2, T3, T4>)Archetypes[mask];
    }
}
