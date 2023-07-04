using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECSTutorial
{
    readonly partial struct VerticalMovementAspect : IAspect
    {
        readonly RefRW<LocalTransform> m_Transform;
        readonly RefRO<RotationSpeed> m_Speed;

        public void Move(double elapsedTime)
        {
            m_Transform.ValueRW.Position.y = (float) math.sin(elapsedTime * m_Speed.ValueRO.RadiansPerSecond);
        }
    }
}