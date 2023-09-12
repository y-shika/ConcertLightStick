using CLS.Component;
using Unity.Entities;
using UnityEngine;

namespace CLS.Authoring
{
    public class StickColorAuthoring : MonoBehaviour
    {
        private class StickColorBaker : Baker<StickColorAuthoring>
        {
            public override void Bake(StickColorAuthoring authoring)
            {
                AddComponent(GetEntity(TransformUsageFlags.None), new StickColor());
            }
        }
    }
}