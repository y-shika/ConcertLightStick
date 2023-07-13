using CLS.Component;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace CLS.System
{
    public partial struct StickSwingerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<StickSwinger>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var (transform, _) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<StickSwinger>>())
            {
                transform.ValueRW = transform.ValueRO.RotateX(3f * deltaTime);
            }
        }
    }
}