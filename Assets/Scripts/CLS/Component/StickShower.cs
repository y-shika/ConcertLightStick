using CLS.Entity;
using Unity.Entities;

namespace CLS.Component
{
    public struct StickShower : IComponentData
    {
        public Unity.Entities.Entity stickPrefab;
        public Audience audience;
    }
}