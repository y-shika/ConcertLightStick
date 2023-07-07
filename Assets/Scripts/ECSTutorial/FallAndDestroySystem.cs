using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECSTutorial
{
    public partial struct FallAndDestroySystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            // var movement = new float3(0, -SystemAPI.Time.DeltaTime * 5f, 0);

            foreach (var (transform, entity) in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<RotationSpeed>().WithEntityAccess())
            {
                // transform.ValueRW.Position += movement;
                if (transform.ValueRO.Position.y < 0)
                {
                    // ecb.DestroyEntity(entity);
                }
            }
        }
    }
}