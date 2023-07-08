using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace ECSTutorial
{
    public partial struct RotatorInitSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<DirectoryManager>();
        }
        
        public void OnUpdate(ref SystemState state)
        {
            var directoryManager = SystemAPI.ManagedAPI.GetSingleton<DirectoryManager>();
            var ecb = new EntityCommandBuffer(Allocator.Temp);

            foreach (var (_, entity) in SystemAPI.Query<GameObjectRotationSpeed>().WithNone<RotatorGO>().WithEntityAccess())
            {
                var go = GameObject.Instantiate(directoryManager.rotatorPrefab);
                ecb.AddComponent(entity, new RotatorGO
                {
                    value = go
                });
                ecb.AddComponent(entity, new NotReparenting());
            }
            
            ecb.Playback(state.EntityManager);
        }
    }

    public class RotatorGO : IComponentData
    {
        public GameObject value;
        
        public RotatorGO(GameObject value)
        {
            this.value = value;
        }

        public RotatorGO()
        {
        }
    }
}