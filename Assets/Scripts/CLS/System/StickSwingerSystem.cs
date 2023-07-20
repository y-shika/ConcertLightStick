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

            var job = new SwingJob
            {
                deltaTime = deltaTime
            };
            job.ScheduleParallel();
        }
        
        [BurstCompile]
        partial struct SwingJob : IJobEntity
        {
            public float deltaTime;

            void Execute(ref LocalTransform transform, in StickSwinger _)
            {
                transform = transform.RotateX(3f * deltaTime);
            }
        }
    }
}