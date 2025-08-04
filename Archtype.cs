using System;
using System.Collections.Generic;
using ecs;

public abstract class Archetype
{
    public List<Entity> Entities { get; private set; } = new List<Entity>();
    public abstract Signature Signature { get; }

    public Entity AddEntity(EntityHandler entityHandler)
    {
        var entity = entityHandler.FetchEntity();
        Entities.Add(entity);
        return entity;
    }

    public override string ToString() => $"Archetype with signature: {Signature}";
}

public class Archetype<T1> : Archetype
    where T1 : struct
{
    public T1 Component1 = new T1();

    public override Signature Signature => new Signature().Toggle<T1>(true);

    public void ForEach(Action<int, T1> action) =>
        Entities.ForEach(entity => action(entity.Id, Component1));
}

public class Archetype<T1, T2> : Archetype
    where T1 : struct
    where T2 : struct
{
    public T1 Component1 = new T1();
    public T2 Component2 = new T2();

    public override Signature Signature => new Signature().Toggle<T1>(true).Toggle<T2>(true);

    public void ForEach(Action<int, T1, T2> action) =>
        Entities.ForEach(entity => action(entity.Id, Component1, Component2));
}

public class Archetype<T1, T2, T3> : Archetype
    where T1 : struct
    where T2 : struct
    where T3 : struct
{
    public T1 Component1 = new T1();
    public T2 Component2 = new T2();
    public T3 Component3 = new T3();

    public override Signature Signature =>
        new Signature().Toggle<T1>(true).Toggle<T2>(true).Toggle<T3>(true);

    public void ForEach(Action<int, T1, T2, T3> action) =>
        Entities.ForEach(entity => action(entity.Id, Component1, Component2, Component3));
}

public class Archetype<T1, T2, T3, T4> : Archetype
    where T1 : struct
    where T2 : struct
    where T3 : struct
    where T4 : struct
{
    public T1 Component1 = new();
    public T2 Component2 = new();
    public T3 Component3 = new();
    public T4 Component4 = new();

    public override Signature Signature =>
        new Signature().Toggle<T1>(true).Toggle<T2>(true).Toggle<T3>(true).Toggle<T4>(true);

    public void ForEach(Action<int, T1, T2, T3, T4> action) =>
        Entities.ForEach(entity =>
            action(entity.Id, Component1, Component2, Component3, Component4)
        );
}
