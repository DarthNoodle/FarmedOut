using FarmLandSDK.Modals;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;


namespace FarmLandSDK.Functions
{
    [Function("getFarmCollectibleTotals", typeof(FarmCollectibleTotalsFunctionResult))]
    public class GetFarmCollectibleTotalsFunction : FunctionMessage
    {
        [Parameter("address", "farmAddress", 1)]
        public virtual string FarmAddress { get; set; }
    }
}
