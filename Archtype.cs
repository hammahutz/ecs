using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ecs;

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

    public abstract IComponent GetComponent<T>()
        where T : struct, IComponent;

    public override string ToString() => $"Archetype with signature: {Signature}";
}

public class Archetype<T1> : Archetype
    where T1 : struct, IComponent
{
    public T1 Component1 = new T1();

    public override Signature Signature => new Signature().Toggle<T1>(true);

    public void ForEachSingel(Action<int, T1> action) =>
        Entities.ForEach(entity => action(entity.Id, Component1));

    public void ForEach(Action<int, T1> action) =>
        Parallel.ForEach(Entities, entitty => action(entitty.Id, Component1));

    public override IComponent GetComponent<T>()
    {
        if (typeof(T) == typeof(T1))
        {
            return Component1;
        }
        else
        {
            throw new InvalidOperationException(
                $"Component {typeof(T).Name} not found in Archetype."
            );
        }
    }
}

public class Archetype<T1, T2> : Archetype
    where T1 : struct, IComponent
    where T2 : struct, IComponent
{
    public T1 Component1 = new T1();
    public T2 Component2 = new T2();

    public override Signature Signature => new Signature().Toggle<T1>(true).Toggle<T2>(true);

    public void ForEach(Action<int, T1, T2> action) =>
        Parallel.ForEach(Entities, entity => action(entity.Id, Component1, Component2));

    public override IComponent GetComponent<T>()
    {
        if (typeof(T) == typeof(T1))
        {
            return Component1;
        }
        else if (typeof(T) == typeof(T2))
        {
            return Component2;
        }
        else
        {
            throw new InvalidOperationException(
                $"Component {typeof(T).Name} not found in Archetype."
            );
        }
    }
}

public class Archetype<T1, T2, T3> : Archetype
    where T1 : struct, IComponent
    where T2 : struct, IComponent
    where T3 : struct, IComponent
{
    public T1 Component1 = new T1();
    public T2 Component2 = new T2();
    public T3 Component3 = new T3();

    public override Signature Signature =>
        new Signature().Toggle<T1>(true).Toggle<T2>(true).Toggle<T3>(true);

    public void ForEach(Action<int, T1, T2, T3> action) =>
        Parallel.ForEach(Entities, entity => action(entity.Id, Component1, Component2, Component3));

    public override IComponent GetComponent<T>()
    {
        if (typeof(T) == typeof(T1))
        {
            return Component1;
        }
        else if (typeof(T) == typeof(T2))
        {
            return Component2;
        }
        else if (typeof(T) == typeof(T3))
        {
            return Component3;
        }
        else
        {
            throw new InvalidOperationException(
                $"Component {typeof(T).Name} not found in Archetype."
            );
        }
    }
}

public class Archetype<T1, T2, T3, T4> : Archetype
    where T1 : struct, IComponent
    where T2 : struct, IComponent
    where T3 : struct, IComponent
    where T4 : struct, IComponent
{
    public T1 Component1 = new();
    public T2 Component2 = new();
    public T3 Component3 = new();
    public T4 Component4 = new();

    public override Signature Signature =>
        new Signature().Toggle<T1>(true).Toggle<T2>(true).Toggle<T3>(true).Toggle<T4>(true);

    public void ForEach(Action<int, T1, T2, T3, T4> action) =>
        Parallel.ForEach(
            Entities,
            entity => action(entity.Id, Component1, Component2, Component3, Component4)
        );

    public override IComponent GetComponent<T>()
    {
        if (typeof(T) == typeof(T1))
            return Component1;
        else if (typeof(T) == typeof(T2))
            return Component2;
        else if (typeof(T) == typeof(T3))
            return Component3;
        else if (typeof(T) == typeof(T4))
            return Component4;
        else
            throw new InvalidOperationException(
                $"Component {typeof(T).Name} not found in Archetype."
            );
    }
}
