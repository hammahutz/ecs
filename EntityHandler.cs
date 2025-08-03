using System;
using System.Collections.Generic;
using System.Linq;

namespace ecs;

public class EntityHandler
{
    const int MaxEntities = 5000;
    private Queue<int> _availableId;

    public EntityHandler() => _availableId = new Queue<int>(Enumerable.Range(0, MaxEntities));

    public Entity FetchEntity()
    {
        if (_availableId.Count == 0)
            throw new InvalidOperationException("No available entity IDs.");

        int id = _availableId.Dequeue();
        return new Entity((int)id);
    }

    public void ReleaseEntity(Entity entity)
    {
        _availableId.Enqueue(entity.Id);
    }
}
