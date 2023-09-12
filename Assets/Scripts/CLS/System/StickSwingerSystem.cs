using CLS.Component;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Random = Unity.Mathematics.Random;

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
            var job = new SwingJob
            {
                time = (float) SystemAPI.Time.ElapsedTime
            };
            job.ScheduleParallel();
        }
        
        [BurstCompile]
        partial struct SwingJob : IJobEntity
        {
            public float time;

            void Execute(ref LocalTransform transform, in StickSwinger swinger)
            {
                var rand = new Random(((uint) swinger.index) + 1);
                rand.NextUInt4();
                
                var xform = transform.ToMatrix();
                var phase = math.PI * (time + rand.NextFloat(-1f, 1f) * 3f);
                
                var angle = math.cos(phase);
                var angle_unsmooth = math.smoothstep(-1, 1, angle) * 2 - 1;
                angle = math.lerp(angle, angle_unsmooth, rand.NextFloat());
                angle *= rand.NextFloat(.005f, .03f);
                
                var axis = math.float3(0, 0, 1);
                
                var result = math.mul(xform, float4x4.AxisAngle(axis, angle));
                transform = LocalTransform.FromMatrix(result);
            }
        }
    }
}