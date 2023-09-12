using CLS.Component;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace CLS.System
{
    public partial struct StickColorSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<StickColor>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var job = new ColorJob
            {
                time = (float) SystemAPI.Time.ElapsedTime
            };
            job.ScheduleParallel();
        }

        [BurstCompile]
        partial struct ColorJob: IJobEntity
        {
            public float time;
            
            void Execute(in Parent parent, ref URPMaterialPropertyBaseColor baseColor, in StickColor stickColor)
            {
                var rand = new Random((uint) parent.Value.Index);
                rand.NextFloat();
                baseColor.Value = RandomColor(ref rand);
            }
            
            private float4 RandomColor(ref Random random)
            {
                var hue = math.frac(random.NextFloat() + time + 0.83f);
                return (Vector4)Color.HSVToRGB(hue, 1.0f, 100f);
            }
        }
    }
}