using Unity.Entities;
using UnityEngine;

namespace ECSTutorial
{
    public class SpawnerAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        
        class Baker : Baker<SpawnerAuthoring>
        {
            public override void Bake(SpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new Spawner
                {
                    Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic)
                });
            }
        }
    }

    struct Spawner : IComponentData
    {
        public Entity Prefab;
    }
}