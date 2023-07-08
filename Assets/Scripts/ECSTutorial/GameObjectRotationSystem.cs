using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace ECSTutorial
{
    public partial struct GameObjectRotationSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            // Note: Update前に<T>が存在していることを要求する
            state.RequireForUpdate<DirectoryManager>();
        }
        
        public void OnUpdate(ref SystemState state)
        {
            var directoryManager = SystemAPI.ManagedAPI.GetSingleton<DirectoryManager>();
            if (!directoryManager.rotationToggle.isOn)
            {
                return;
            }
            
            var deltaTime = SystemAPI.Time.DeltaTime;
            
            foreach (var (transform, speed, go) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<GameObjectRotationSpeed>, RotatorGO>())
            {
                transform.ValueRW = transform.ValueRO.RotateY(speed.ValueRO.radiansPerSecond * deltaTime);
                go.value.transform.rotation = transform.ValueRO.Rotation;
            }
        }
    }
}