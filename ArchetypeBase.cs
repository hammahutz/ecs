using System.Collections.Generic;
using ecs;

public class ArchetypeBase
{
    public List<Entity> Entities { get; private set; } = new List<Entity>();
    public Signature Signature { get; private set; } = new Signature();

    public ArchetypeBase()
    {
        Signature = new Signature();
    }

    public void AddComponent<T>(bool value) { }

    public bool HasComponent<T>()
    {
        int index = SignatureManager.GetComponentBit<T>();
        return Signature.Get(index);
    }

    public override string ToString()
    {
        return $"Archetype with signature: {Signature}";
    }
}
