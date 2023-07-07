using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace ECSTutorial
{
    public partial struct EnableableRotationSystem : ISystem
    {
        private float _time;
        private const float Interval = .7f;
        
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            _time = Interval;
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            _time -= SystemAPI.Time.DeltaTime;
            if (_time < 0)
            {
                foreach (var rotationSpeedEnabled in SystemAPI.Query<EnabledRefRW<EnableableRotationSpeed>>()
                             .WithOptions(EntityQueryOptions.IgnoreComponentEnabledState))
                {
                    rotationSpeedEnabled.ValueRW = !rotationSpeedEnabled.ValueRO;
                }
                
                _time = Interval;
            }

            foreach (var (transform, speed) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<EnableableRotationSpeed>>())
            {
                transform.ValueRW =
                    transform.ValueRO.RotateY(speed.ValueRO.radiansPerSecond * SystemAPI.Time.DeltaTime);
            }
        }
    }
}