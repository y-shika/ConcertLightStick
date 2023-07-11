using CLS.Component;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace CLS.System
{
    public partial struct StickShowerSystem : ISystem
    {
        private uint _updateCounter;
        
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
            var instances = state.EntityManager.Instantiate(stickShower.prefab, stickShower.audience.Count, Allocator.Temp);

            for (int i = 0; i < instances.Length; i++)
            {
                var transform = SystemAPI.GetComponentRW<LocalTransform>(instances[i]);
                transform.ValueRW.Position = stickShower.audience.GetPositionByIndex(i);
            }
        }
    }
}