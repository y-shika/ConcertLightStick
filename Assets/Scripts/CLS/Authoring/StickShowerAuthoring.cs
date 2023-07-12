using CLS.Component;
using CLS.Entity;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace CLS.Authoring
{
    public class StickShowerAuthoring : MonoBehaviour
    {
        [SerializeField] private GameObject stickPrefab;
        [SerializeField] private Audience audience = new()
        {
            seatPerBlock = new int2(20, 20),
            blockCount = new int2(10, 10),
            seatPitch = new float2(.4f, .8f),
            aisleWidth = new float2(.7f, 1.2f),
            distanceFromStage = 10f
        };
        
        private class StickShowerBaker : Baker<StickShowerAuthoring>
        {
            public override void Bake(StickShowerAuthoring authoring)
            {
                var floor = GameObject.Find("Floor");

                if (floor == null)
                {
                    Debug.LogError("Stage Floor is not set");
                }
                else
                {
                    authoring.audience.StagePosition = floor.transform.position;
                }
                
                var data = new StickShower
                {
                    stickPrefab = GetEntity(authoring.stickPrefab, TransformUsageFlags.Dynamic),
                    audience = authoring.audience,
                };
                AddComponent(GetEntity(TransformUsageFlags.Dynamic), data);
            }
        }
    }
}