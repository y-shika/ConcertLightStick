using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ECSTutorial
{
    public class RotationSpeedAuthoring : MonoBehaviour
    {
        [SerializeField] private float degreesPerSecond = 360f;
    
        class Baker : Baker<RotationSpeedAuthoring>
        {
            public override void Bake(RotationSpeedAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new RotationSpeed
                {
                    RadiansPerSecond = math.radians(authoring.degreesPerSecond)
                });
            }
        }
    }

    struct RotationSpeed : IComponentData
    {
        public float RadiansPerSecond;
    }
}