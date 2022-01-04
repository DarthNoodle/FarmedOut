using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Numerics;

namespace FarmLandSDK.Modals
{
    [FunctionOutput]
    public class OwnerOfFarmResult : IFunctionOutputDTO
    {
        [Parameter("uint256", "amount", 1)]
        public BigInteger Amount { get; set; }
        [Parameter("uint256", "compostedAmount", 2)]
        public BigInteger CompostedAmount { get; set; }
        [Parameter("uint256", "blockNumber", 3)]
        public BigInteger BlockNumber { get; set; }
        [Parameter("uint256", "lastHarvestedBlockNumber", 4)]
        public BigInteger LastHarvestedBlockNumber { get; set; }
        [Parameter("address", "harvesterAddress", 5)]
        public string? HarvesterAddress { get; set; }
        [Parameter("uint256", "numberOfCollectibles", 6)]
        public BigInteger NumberOfCollectibles { get; set; }
    }
}
