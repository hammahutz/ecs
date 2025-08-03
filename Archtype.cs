using System.Collections.Generic;
using ecs;

public abstract class Archetype
{
    public List<Entity> Entities { get; private set; } = new List<Entity>();
    public Signature Signature { get; protected set; }

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
    public T1 Component1;

    public Archetype()
    {
        Signature = new Signature().Toggle<T1>(true);
        Component1 = new T1();
    }
}

public class Archetype<T1, T2> : Archetype
    where T1 : struct
    where T2 : struct
{
    public T1 Component1;
    public T2 Component2;

    public Archetype()
    {
        Signature = new Signature().Toggle<T1>(true).Toggle<T2>(true);
        Component1 = new T1();
        Component2 = new T2();
    }
}

public class Archetype<T1, T2, T3> : Archetype
{
    public T1 Component1;
    public T2 Component2;
    public T3 Component3;

    public Archetype()
    {
        Signature = new Signature().Toggle<T1>(true).Toggle<T2>(true).Toggle<T3>(true);
        Component1 = default(T1);
        Component2 = default(T2);
        Component3 = default(T3);
    }
}

public class Archetype<T1, T2, T3, T4> : Archetype
{
    public T1 Component1;
    public T2 Component2;
    public T3 Component3;
    public T4 Component4;

    public Archetype()
    {
        Signature = new Signature()
            .Toggle<T1>(true)
            .Toggle<T2>(true)
            .Toggle<T3>(true)
            .Toggle<T4>(true);
        Component1 = default(T1);
        Component2 = default(T2);
        Component3 = default(T3);
        Component4 = default(T4);
    }
}
