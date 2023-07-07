using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ECSTutorial
{
    public class EnableableRotationSpeedAuthoring : MonoBehaviour
    {
        public bool startEnabled;
        public float degreesPerSecond = 360f;
        
        private class Baker : Baker<EnableableRotationSpeedAuthoring>
        {
            public override void Bake(EnableableRotationSpeedAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new EnableableRotationSpeed { radiansPerSecond = math.radians(authoring.degreesPerSecond)});
                AddComponent(entity, new NotReparenting());
                SetComponentEnabled<EnableableRotationSpeed>(entity, authoring.startEnabled);
            }
        }
    }

    struct EnableableRotationSpeed : IComponentData, IEnableableComponent
    {
        public float radiansPerSecond;
    }

    struct NotReparenting : IComponentData
    {
    }
}