using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ECSTutorial
{
    public class GameObjectRotationSpeedAuthoring : MonoBehaviour
    {
        [SerializeField] private float degreesPerSecond = 360f;
        
        private class Baker : Baker<GameObjectRotationSpeedAuthoring>
        {
            public override void Bake(GameObjectRotationSpeedAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new GameObjectRotationSpeed
                {
                    radiansPerSecond = math.radians(authoring.degreesPerSecond)
                });
            }
        }
    }

    struct GameObjectRotationSpeed : IComponentData
    {
        public float radiansPerSecond;
    }
}