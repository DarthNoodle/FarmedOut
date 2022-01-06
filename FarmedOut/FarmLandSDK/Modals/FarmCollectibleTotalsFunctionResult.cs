using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Numerics;

namespace FarmLandSDK.Modals
{

    [FunctionOutput]
    public class FarmCollectibleTotalsFunctionResult : IFunctionOutputDTO
    {
        [Parameter("uint256", "totalMaxBoost", 1)]
        public virtual BigInteger TotalMaxBoost { get; set; }
        [Parameter("uint256", "lastAddedBlockNumber", 2)]
        public virtual BigInteger LastAddedBlockNumber { get; set; }
    }
}
