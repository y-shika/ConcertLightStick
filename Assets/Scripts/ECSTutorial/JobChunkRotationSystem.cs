using Unity.Assertions;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace ECSTutorial
{
    public partial struct JobChunkRotationSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var spinningCubesQuery = SystemAPI.QueryBuilder().WithAll<RotationSpeed, LocalTransform>().Build();

            var job = new RotationJob()
            {
                transformTypeHandle = SystemAPI.GetComponentTypeHandle<LocalTransform>(),
                rotationSpeedTypeHandle = SystemAPI.GetComponentTypeHandle<RotationSpeed>(true),
                deltaTime = SystemAPI.Time.DeltaTime
            };

            state.Dependency = job.Schedule(spinningCubesQuery, state.Dependency);
        }
    }

    [BurstCompile]
    struct RotationJob : IJobChunk 
    {
        public ComponentTypeHandle<LocalTransform> transformTypeHandle;
        [ReadOnly] public ComponentTypeHandle<RotationSpeed> rotationSpeedTypeHandle;
        public float deltaTime;
        
        public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
        {
            Assert.IsFalse(useEnabledMask);

            var transforms = chunk.GetNativeArray(ref transformTypeHandle);
            var rotationSpeeds = chunk.GetNativeArray(ref rotationSpeedTypeHandle);

            for (int i = 0, chunkEntityCount = chunk.Count; i < chunkEntityCount; i++)
            {
                transforms[i] = transforms[i].RotateY(rotationSpeeds[i].RadiansPerSecond * deltaTime);
            }
        }
    }
}