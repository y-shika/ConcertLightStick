using System;
using Unity.Mathematics;

namespace CLS.Entity
{
    [Serializable]
    public struct Audience
    {
        public int2 seatPerBlock;
        public int2 blockCount;

        public float2 seatPitch;
        public float2 aisleWidth;

        public float3 StagePosition { set => _stagePosition = value; }
        private float3 _stagePosition;
        public float distanceFromStage;
        
        public int Count => seatPerBlock.x * seatPerBlock.y * blockCount.x * blockCount.y;
        
        private float OriginPosX => _originPosX ??= _stagePosition.x - (seatPitch.x * seatPerBlock.x * blockCount.x + aisleWidth.x * (blockCount.x - 1)) / 2f;
        private float? _originPosX;

        private float OriginPosZ => _originPosZ ??= _stagePosition.z + distanceFromStage;
        private float? _originPosZ;

        private float WidthPerBlockWithAisle => _widthPerBlockWithAisle ??= seatPitch.x * seatPerBlock.x + aisleWidth.x;
        private float? _widthPerBlockWithAisle;
        
        private float DepthPerBlockWithAisle => _depthPerBlockWithAisle ??= seatPitch.y * seatPerBlock.y + aisleWidth.y;
        private float? _depthPerBlockWithAisle;
        
        public float3 GetPositionByIndex(int index)
        {
            var (block, seat) = GetBlockAndSeat(index);
            return GetPositionByBlockAndSeat(block, seat);
        }
        
        private float3 GetPositionByBlockAndSeat(int2 block, int2 seat) => new(
            OriginPosX + block.x * WidthPerBlockWithAisle + seat.x * seatPitch.x,
            0,
            OriginPosZ + block.y * DepthPerBlockWithAisle + seat.y * seatPitch.y
        );
        
        private (int2 block, int2 seat) GetBlockAndSeat(int index)
        {
            var block = index / (seatPerBlock.x * seatPerBlock.y);
            var seat = index % (seatPerBlock.x * seatPerBlock.y);
            return (new int2(block % blockCount.x, block / blockCount.x), new int2(seat % seatPerBlock.x, seat / seatPerBlock.x));
        }
    }
}