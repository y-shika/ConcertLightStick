using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace ECSTutorial
{
    public partial struct ReparentingSystem : ISystem
    {
        private float _time;
        private const float Interval = 0.7f;
        private bool _attached;
        
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            _time = Interval;
            _attached = true;
            state.RequireForUpdate<RotationSpeed>();
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            _time -= SystemAPI.Time.DeltaTime;
            if (_time > 0)
            {
                return;
            }

            _time = Interval;

            var rotatorEntity = SystemAPI.GetSingletonEntity<RotationSpeed>();
            var ecb = new EntityCommandBuffer(Allocator.Temp);

            if (_attached)
            {
                var children = SystemAPI.GetBuffer<Child>(rotatorEntity);

                for (int i = 0; i < children.Length; i++)
                {
                    ecb.RemoveComponent<Parent>(children[i].Value);
                }
            }
            else
            {
                foreach (var (_, entity) in SystemAPI.Query<RefRO<LocalTransform>>().WithNone<RotationSpeed, NotReparenting>().WithEntityAccess())
                {
                    ecb.AddComponent(entity, new Parent { Value = rotatorEntity });
                }
            }
            
            ecb.Playback(state.EntityManager);
            _attached = !_attached;
        }
    }
}