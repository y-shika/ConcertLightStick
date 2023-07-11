using CLS.Component;
using CLS.Entity;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace CLS.Authoring
{
    public class StickShowerAuthoring : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Audience audience = new()
        {
            seatPerBlock = new int2(20, 20),
            blockCount = new int2(10, 10)   
        };
        
        private class StickShowerBaker : Baker<StickShowerAuthoring>
        {
            public override void Bake(StickShowerAuthoring authoring)
            {
                var data = new StickShower
                {
                    prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
                    audience = authoring.audience
                };
                AddComponent(GetEntity(TransformUsageFlags.Dynamic), data);
            }
        }
    }
}