using System;
using Unity.Mathematics;

namespace CLS.Entity
{
    [Serializable]
    public struct Audience
    {
        public int2 seatPerBlock;
        public int2 blockCount;
        
        public int Count => seatPerBlock.x * seatPerBlock.y * blockCount.x * blockCount.y;
        
        public float3 GetPositionByIndex(int index)
        {
            var (block, seat) = GetBlockAndSeat(index);
            return GetPositionByBlockAndSeat(block, seat);
        }

        private float3 GetPositionByBlockAndSeat(int2 block, int2 seat) => new(
            (block.x - blockCount.x / 2f) * seatPerBlock.x + seat.x,
            0,
            (block.y - blockCount.y / 2f) * seatPerBlock.y + seat.y
        );
        
        private (int2 block, int2 seat) GetBlockAndSeat(int index)
        {
            var block = index / (seatPerBlock.x * seatPerBlock.y);
            var seat = index % (seatPerBlock.x * seatPerBlock.y);
            return (new int2(block / blockCount.x, block % blockCount.x), new int2(seat / seatPerBlock.x, seat % seatPerBlock.y));
        }
    }
}