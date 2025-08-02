using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ecs;

public class EntityHandler
{
    const uint MaxEntities = 1024;
    private Queue<int> _availableId;

    public EntityHandler() => _availableId = new Queue<int>(Enumerable.Range(0, (int)MaxEntities));

    public Entity FetchEntity()
    {
        if (_availableId.Count == 0)
            throw new InvalidOperationException("No available entity IDs.");

        int id = _availableId.Dequeue();
        return new Entity(id);
    }

    public void ReleaseEntity(Entity entity)
    {
        _availableId.Enqueue(entity.Id);
    }

    public void SetSignature(Entity entity, Signature signature)
    {
        // Logic to set the signature for the entity
    }
}
