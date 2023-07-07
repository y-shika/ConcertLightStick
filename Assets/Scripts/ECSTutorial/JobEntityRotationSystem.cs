using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECSTutorial
{
    public partial struct JobEntityRotationSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var job = new RotateAndScaleJob
            {
                deltaTime = SystemAPI.Time.DeltaTime,
                elapsedTime = (float)SystemAPI.Time.ElapsedTime
            };
            job.Schedule();
        }
    
        [BurstCompile]
        partial struct RotateAndScaleJob : IJobEntity
        {
            public float deltaTime;
            public float elapsedTime;
        
            void Execute(ref LocalTransform transform, ref PostTransformMatrix postTransform, in RotationSpeed speed)
            {
                transform = transform.RotateY(speed.RadiansPerSecond * deltaTime);
                postTransform.Value = float4x4.Scale(1, math.sin(elapsedTime), 1);
            }
        }
    }
}