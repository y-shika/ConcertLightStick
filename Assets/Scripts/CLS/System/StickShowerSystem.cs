using CLS.Component;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace CLS.System
{
    public partial struct StickShowerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<StickShower>();
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
            
            var stickShower = SystemAPI.GetSingleton<StickShower>();
            var entities = state.EntityManager.Instantiate(stickShower.stickPrefab, stickShower.audience.Count, Allocator.Temp);

            var ecb = new EntityCommandBuffer(Allocator.Temp);
            
            for (int i = 0; i < entities.Length; i++)
            {
                var transform = SystemAPI.GetComponentRW<LocalTransform>(entities[i]);
                transform.ValueRW.Position = stickShower.audience.GetPositionByIndex(i);

                ecb.AddComponent(entities[i], new StickSwinger());
            }
            
            ecb.Playback(state.EntityManager);
        }
    }
}