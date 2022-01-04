using FarmLandSDK.Functions;
using FarmLandSDK.Modals;
using Nethereum.Contracts;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System.Numerics;

namespace FarmLandSDK
{
    /// <summary>
    /// Utility Class That Works With Both Arbi TestNet & Prod Versions of Farmland
    /// 
    /// Author:  DarthNoodle 
    /// </summary>ownerOfFarm
    public class FarmManager
    {
        private const string CORN_ABI = "[{\"inputs\":[{\"internalType\":\"address[3]\",\"name\":\"farmlandAddresses_\",\"type\":\"address[3]\"},{\"internalType\":\"address[]\",\"name\":\"farmAddresses_\",\"type\":\"address[]\"},{\"internalType\":\"uint256[]\",\"name\":\"composted_\",\"type\":\"uint256[]\"}],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"address\",\"name\":\"sender\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"blockNumber\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"address\",\"name\":\"farmAddress\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"burnedAmountIncrease\",\"type\":\"uint256\"}],\"name\":\"Allocated\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"value\",\"type\":\"uint256\"}],\"name\":\"Approval\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"tokenHolder\",\"type\":\"address\"}],\"name\":\"AuthorizedOperator\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"bytes\",\"name\":\"data\",\"type\":\"bytes\"},{\"indexed\":false,\"internalType\":\"bytes\",\"name\":\"operatorData\",\"type\":\"bytes\"}],\"name\":\"Burned\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"address\",\"name\":\"sender\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"blockNumber\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"TokenID\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"enum CollectibleType\",\"name\":\"collectibleType\",\"type\":\"uint8\"}],\"name\":\"CollectibleEquipped\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"address\",\"name\":\"sender\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"blockNumber\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"TokenID\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"enum CollectibleType\",\"name\":\"collectibleType\",\"type\":\"uint8\"}],\"name\":\"CollectibleReleased\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"address\",\"name\":\"sender\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"address\",\"name\":\"farmAddress\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"bonus\",\"type\":\"uint256\"}],\"name\":\"Composted\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"endMaturityBoost_\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"maxGrowthCycle_\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"maxGrowthCycleWithFarmer_\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"maxCompostBoost_\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"maxMaturityBoost_\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"maxMaturityCollectibleBoost_\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"maxFarmSizeWithoutFarmer_\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"maxFarmSizeWithoutTractor_\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"bonusCompostBoostWithFarmer_\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"bonusCompostBoostWithTractor_\",\"type\":\"uint256\"}],\"name\":\"FarmlandVariablesSet\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"address\",\"name\":\"sender\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"blockNumber\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"address\",\"name\":\"farmAddress\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"address\",\"name\":\"targetAddress\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"targetBlock\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"Harvested\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"bytes\",\"name\":\"data\",\"type\":\"bytes\"},{\"indexed\":false,\"internalType\":\"bytes\",\"name\":\"operatorData\",\"type\":\"bytes\"}],\"name\":\"Minted\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"previousOwner\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"OwnershipTransferred\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"}],\"name\":\"Paused\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"address\",\"name\":\"sender\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"burnedAmountDecrease\",\"type\":\"uint256\"}],\"name\":\"Released\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"tokenHolder\",\"type\":\"address\"}],\"name\":\"RevokedOperator\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"bytes\",\"name\":\"data\",\"type\":\"bytes\"},{\"indexed\":false,\"internalType\":\"bytes\",\"name\":\"operatorData\",\"type\":\"bytes\"}],\"name\":\"Sent\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"value\",\"type\":\"uint256\"}],\"name\":\"Transfer\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"}],\"name\":\"Unpaused\",\"type\":\"event\"},{\"inputs\":[],\"name\":\"L2_GATEWAY\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"farmAddress\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"allocate\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"holder\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"}],\"name\":\"allowance\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"value\",\"type\":\"uint256\"}],\"name\":\"approve\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"}],\"name\":\"authorizeOperator\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"tokenHolder\",\"type\":\"address\"}],\"name\":\"balanceOf\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"bridgeBurn\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"bridgeMint\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"},{\"internalType\":\"bytes\",\"name\":\"data\",\"type\":\"bytes\"}],\"name\":\"burn\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"farmAddress\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"compost\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"decimals\",\"outputs\":[{\"internalType\":\"uint8\",\"name\":\"\",\"type\":\"uint8\"}],\"stateMutability\":\"pure\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"defaultOperators\",\"outputs\":[{\"internalType\":\"address[]\",\"name\":\"\",\"type\":\"address[]\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"farmAddress\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"targetBlock\",\"type\":\"uint256\"}],\"name\":\"directCompost\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"tokenID\",\"type\":\"uint256\"},{\"internalType\":\"enum CollectibleType\",\"name\":\"collectibleType\",\"type\":\"uint8\"}],\"name\":\"equipCollectible\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"farmAddress\",\"type\":\"address\"}],\"name\":\"getAddressDetails\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"blockNumber\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"cropBalance\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"cropAvailableToHarvest\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"farmMaturityBoost\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"farmCompostBoost\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"farmTotalBoost\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"farmAddress\",\"type\":\"address\"}],\"name\":\"getCollectiblesByFarm\",\"outputs\":[{\"components\":[{\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"},{\"internalType\":\"enum CollectibleType\",\"name\":\"collectibleType\",\"type\":\"uint8\"},{\"internalType\":\"uint256\",\"name\":\"maxBoostLevel\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"addedBlockNumber\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"expiry\",\"type\":\"uint256\"},{\"internalType\":\"string\",\"name\":\"uri\",\"type\":\"string\"}],\"internalType\":\"struct Collectible[]\",\"name\":\"farmCollectibles\",\"type\":\"tuple[]\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"farmAddress\",\"type\":\"address\"},{\"internalType\":\"enum CollectibleType\",\"name\":\"collectibleType\",\"type\":\"uint8\"}],\"name\":\"getFarmCollectibleTotalOfType\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"ownsCollectibleTotal\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"farmAddress\",\"type\":\"address\"}],\"name\":\"getFarmCollectibleTotals\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"totalMaxBoost\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"lastAddedBlockNumber\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"getFarmlandAddresses\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"getFarmlandVariables\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"totalFarms\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"totalAllocatedAmount\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"totalCompostedAmount\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"maximumCompostBoost\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"maximumMaturityBoost\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"maximumGrowthCycle\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"maximumGrowthCycleWithFarmer\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"maximumMaturityCollectibleBoost\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"endMaturityBoostBlocks\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"maximumFarmSizeWithoutFarmer\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"maximumFarmSizeWithoutTractor\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"bonusCompostBoostWithAFarmer\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"bonusCompostBoostWithATractor\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"granularity\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"farmAddress\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"targetAddress\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"targetBlock\",\"type\":\"uint256\"}],\"name\":\"harvest\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"tokenHolder\",\"type\":\"address\"}],\"name\":\"isOperatorFor\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"bool\",\"name\":\"value\",\"type\":\"bool\"}],\"name\":\"isPaused\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"l1Address\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"name\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"},{\"internalType\":\"bytes\",\"name\":\"\",\"type\":\"bytes\"}],\"name\":\"onERC721Received\",\"outputs\":[{\"internalType\":\"bytes4\",\"name\":\"\",\"type\":\"bytes4\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"},{\"internalType\":\"bytes\",\"name\":\"data\",\"type\":\"bytes\"},{\"internalType\":\"bytes\",\"name\":\"operatorData\",\"type\":\"bytes\"}],\"name\":\"operatorBurn\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"sender\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"recipient\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"},{\"internalType\":\"bytes\",\"name\":\"data\",\"type\":\"bytes\"},{\"internalType\":\"bytes\",\"name\":\"operatorData\",\"type\":\"bytes\"}],\"name\":\"operatorSend\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"owner\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"name\":\"ownerOfCollectibles\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"},{\"internalType\":\"enum CollectibleType\",\"name\":\"collectibleType\",\"type\":\"uint8\"},{\"internalType\":\"uint256\",\"name\":\"maxBoostLevel\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"addedBlockNumber\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"expiry\",\"type\":\"uint256\"},{\"internalType\":\"string\",\"name\":\"uri\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"name\":\"ownerOfFarm\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"compostedAmount\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"blockNumber\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"lastHarvestedBlockNumber\",\"type\":\"uint256\"},{\"internalType\":\"address\",\"name\":\"harvesterAddress\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"numberOfCollectibles\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"paused\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"release\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"index\",\"type\":\"uint256\"}],\"name\":\"releaseCollectible\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"renounceOwnership\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"}],\"name\":\"revokeOperator\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"recipient\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"},{\"internalType\":\"bytes\",\"name\":\"data\",\"type\":\"bytes\"}],\"name\":\"send\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"landAddress_\",\"type\":\"address\"},{\"internalType\":\"address payable\",\"name\":\"farmerNFTAddress_\",\"type\":\"address\"},{\"internalType\":\"address payable\",\"name\":\"tractorNFTAddress_\",\"type\":\"address\"}],\"name\":\"setFarmlandAddresses\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"endMaturityBoost_\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"maxGrowthCycle_\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"maxGrowthCycleWithFarmer_\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"maxCompostBoost_\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"maxMaturityBoost_\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"maxMaturityCollectibleBoost_\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"maxFarmSizeWithoutFarmer_\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"maxFarmSizeWithoutTractor_\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"bonusCompostBoostWithFarmer_\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"bonusCompostBoostWithTractor_\",\"type\":\"uint256\"}],\"name\":\"setFarmlandVariables\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"bytes4\",\"name\":\"interfaceId\",\"type\":\"bytes4\"}],\"name\":\"supportsInterface\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"symbol\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"},{\"internalType\":\"bytes\",\"name\":\"\",\"type\":\"bytes\"},{\"internalType\":\"bytes\",\"name\":\"\",\"type\":\"bytes\"}],\"name\":\"tokensReceived\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"totalSupply\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"recipient\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"transfer\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"holder\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"recipient\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"transferFrom\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"transferOwnership\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";
        private const string ERC20_ABI = "[{\"constant\":true,\"inputs\":[],\"name\":\"name\",\"outputs\":[{\"name\":\"\",\"type\":\"string\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"_spender\",\"type\":\"address\"},{\"name\":\"_value\",\"type\":\"uint256\"}],\"name\":\"approve\",\"outputs\":[{\"name\":\"\",\"type\":\"bool\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"totalSupply\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"_from\",\"type\":\"address\"},{\"name\":\"_to\",\"type\":\"address\"},{\"name\":\"_value\",\"type\":\"uint256\"}],\"name\":\"transferFrom\",\"outputs\":[{\"name\":\"\",\"type\":\"bool\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"decimals\",\"outputs\":[{\"name\":\"\",\"type\":\"uint8\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"_owner\",\"type\":\"address\"}],\"name\":\"balanceOf\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"symbol\",\"outputs\":[{\"name\":\"\",\"type\":\"string\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"_to\",\"type\":\"address\"},{\"name\":\"_value\",\"type\":\"uint256\"}],\"name\":\"transfer\",\"outputs\":[{\"name\":\"\",\"type\":\"bool\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"_owner\",\"type\":\"address\"},{\"name\":\"_spender\",\"type\":\"address\"}],\"name\":\"allowance\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"name\":\"_from\",\"type\":\"address\"},{\"indexed\":true,\"name\":\"_to\",\"type\":\"address\"},{\"indexed\":false,\"name\":\"_value\",\"type\":\"uint256\"}],\"name\":\"Transfer\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"name\":\"_owner\",\"type\":\"address\"},{\"indexed\":true,\"name\":\"_spender\",\"type\":\"address\"},{\"indexed\":false,\"name\":\"_value\",\"type\":\"uint256\"}],\"name\":\"Approval\",\"type\":\"event\"}]";


        private const string ARRBITRUM_TESTNET_CORN = "0x86CdcfA5e6ac784776B54fb2e767BE1CAb3656b3";
        private const string ARRBITRUM_TESTNET_LAND = "0x2E228ef2A59686cC5c6fF06cD46ee302a4134F00";

        private const string ARRBITRUM_PROD_CORN = "0xFcc0351f3a1ff72409Df66a7589c1F9efBf53386";
        private const string ARRBITRUM_PROD_LAND = "0x3CD1833Ce959E087D0eF0Cb45ed06BffE60F23Ba";

        private const string ARBITRUM_PROD_NETWORK = "https://arb1.arbitrum.io/rpc";
        private const string ARBITRUM_TEST_NETWORK = "https://rinkeby.arbitrum.io/rpc";



        //how many L1 ETH blocks in a day (13.5s block avg)
        private const int BLOCKS_IN_A_DAY = 6400;
        private bool _isTestNet = false;


        private Web3 _Web3 { get; set; }
        private Account? _UserAccount { get; set; }


        public FarmManager(): this(ARBITRUM_PROD_NETWORK, false) { }

        public FarmManager(string web3URL, bool isTestNet = false)
        {
            _UserAccount = null;
            _Web3 = new Web3(web3URL);
            _isTestNet = isTestNet;
        }//public FarmManager(string web3URL, bool isTestNet = false)

        public FarmManager(string web3URL, Account web3Account, bool isTestNet = false)
        {
            _Web3 = new Web3(web3Account, web3URL);
            _UserAccount = web3Account;
            _isTestNet = isTestNet;
        }//public FarmManager(string web3URL, Account web3Account, bool isTestNet = false)

        public FarmManager(string web3URL, string privateKey, bool isTestNet = false)
        {
            _UserAccount = new Account(privateKey);
            _Web3 = new Web3(_UserAccount, web3URL);
            _isTestNet = isTestNet;
        }//public FarmManager(string web3URL, string privateKey, bool isTestNet = false)



        /// <summary>
        /// Get Farm Details From Contract
        /// </summary>
        /// <param name="address">ETH Address</param>
        /// <returns>Farm struct</returns>
        public async Task<OwnerOfFarmResult> GetFarm(string address)
        {
            object[] contractParams = new object[] { address };

            return await CallFunction<OwnerOfFarmResult>(GetCornContractAddress(), CORN_ABI, "ownerOfFarm", contractParams).ConfigureAwait(false);
        }//public async Task<Farm> GetFarm(string address)

        /// <summary>
        /// Get CORN Balance
        /// </summary>
        /// <param name="address">Target Address</param>
        /// <returns></returns>
        public async Task<BigInteger> GetCornBalance(string address) => await GetBalanceOfCoin(GetCornContractAddress(), address).ConfigureAwait(false);

        /// <summary>
        /// Get LAND Balance
        /// </summary>
        /// <param name="address">Target Address</param>
        /// <returns></returns>
        public async Task<BigInteger> GetLandBalance(string address) => await GetBalanceOfCoin(GetLandContractAddress(), address).ConfigureAwait(false);


        /// <summary>
        /// Gets The Balance of An ERC-20 Coin
        /// </summary>
        /// <param name="contractAddress">Coin Contract Address</param>
        /// <returns></returns>
        private async Task<BigInteger> GetBalanceOfCoin(string contractAddress, string targetAddress)
        {
            BalanceOfFunction contractParams = new BalanceOfFunction
            {
                 Owner = targetAddress
            };

            var contractFunction = _Web3.Eth.GetContractQueryHandler<BalanceOfFunction>();
            

            return await contractFunction.QueryAsync<BigInteger>(contractAddress, contractParams).ConfigureAwait(false);
        }//private async Task<BigInteger> GetBalanceOfCoin(string contractAddress)



        /// <summary>
        /// Calls A Specified Function Within A Smart Contract (returns a struct)
        /// </summary>
        /// <typeparam name="TReturn">Reurn Object (Deserialised)</typeparam>
        /// <param name="contractAddress">Contract Address</param>
        /// <param name="abi">Contract ABI</param>
        /// <param name="functionName">Function Name</param>
        /// <param name="functionParams">Parameters (if Any)</param>
        /// <returns></returns>
        private async Task<TReturn> CallFunction<TReturn>(string contractAddress, string abi, string functionName, object[]? functionParams = null) where TReturn : new()
        {
            Contract? contract = _Web3.Eth.GetContract(abi, contractAddress);
            Function? contractFunction = contract.GetFunction(functionName);

            TReturn? returnValue;

            if (functionParams != null) { returnValue = await contractFunction.CallDeserializingToObjectAsync<TReturn>(functionParams).ConfigureAwait(false); }
            else
            {
                returnValue = await contractFunction.CallDeserializingToObjectAsync<TReturn>().ConfigureAwait(false);
            }//end of if-else if (functionParams != null) 


            return returnValue;
        }//private async Task<T> CallFunction<T>(string contractAddress, string abi, string functionName, object[]? functionParams = null)


        /// <summary>
        /// CORN Production or TestNet Contract Address
        /// </summary>
        /// <returns></returns>
        private string GetCornContractAddress() => _isTestNet ? ARRBITRUM_TESTNET_CORN : ARRBITRUM_PROD_CORN;

        /// <summary>
        /// LAND Production or TestNet Contract Address
        /// </summary>
        /// <returns></returns>
        private string GetLandContractAddress() => _isTestNet ? ARRBITRUM_TESTNET_LAND : ARRBITRUM_PROD_LAND;
    }//end of class
}

/*

 interface GeneratedInterface {
  function L2_GATEWAY (  ) external view returns ( address );
  function allocate ( address farmAddress, uint256 amount ) external;
  function allowance ( address holder, address spender ) external view returns ( uint256 );
  function approve ( address spender, uint256 value ) external returns ( bool );
  function authorizeOperator ( address operator ) external;
  function balanceOf ( address tokenHolder ) external view returns ( uint256 );
  function bridgeBurn ( address account, uint256 amount ) external;
  function bridgeMint ( address account, uint256 amount ) external;
  function burn ( uint256 amount, bytes data ) external;
  function compost ( address farmAddress, uint256 amount ) external;
  function decimals (  ) external pure returns ( uint8 );
  function defaultOperators (  ) external view returns ( address[] );
  function directCompost ( address farmAddress, uint256 targetBlock ) external;
  function equipCollectible ( uint256 tokenID, uint8 collectibleType ) external;
  function getAddressDetails ( address farmAddress ) external view returns ( uint256 blockNumber, uint256 cropBalance, uint256 cropAvailableToHarvest, uint256 farmMaturityBoost, uint256 farmCompostBoost, uint256 farmTotalBoost );
  function getCollectiblesByFarm ( address farmAddress ) external view returns ( tuple[] farmCollectibles );
  function getFarmCollectibleTotalOfType ( address farmAddress, uint8 collectibleType ) external view returns ( uint256 ownsCollectibleTotal );
  function getFarmCollectibleTotals ( address farmAddress ) external view returns ( uint256 totalMaxBoost, uint256 lastAddedBlockNumber );
  function getFarmlandAddresses (  ) external view returns ( address, address, address, address, address );
  function getFarmlandVariables (  ) external view returns ( uint256 totalFarms, uint256 totalAllocatedAmount, uint256 totalCompostedAmount, uint256 maximumCompostBoost, uint256 maximumMaturityBoost, uint256 maximumGrowthCycle, uint256 maximumGrowthCycleWithFarmer, uint256 maximumMaturityCollectibleBoost, uint256 endMaturityBoostBlocks, uint256 maximumFarmSizeWithoutFarmer, uint256 maximumFarmSizeWithoutTractor, uint256 bonusCompostBoostWithAFarmer, uint256 bonusCompostBoostWithATractor );
  function granularity (  ) external view returns ( uint256 );
  function harvest ( address farmAddress, address targetAddress, uint256 targetBlock ) external;
  function isOperatorFor ( address operator, address tokenHolder ) external view returns ( bool );
  function isPaused ( bool value ) external;
  function l1Address (  ) external view returns ( address );
  function name (  ) external view returns ( string );
  function onERC721Received ( address, address, uint256, bytes ) external returns ( bytes4 );
  function operatorBurn ( address account, uint256 amount, bytes data, bytes operatorData ) external;
  function operatorSend ( address sender, address recipient, uint256 amount, bytes data, bytes operatorData ) external;
  function owner (  ) external view returns ( address );
  function ownerOfCollectibles ( address, uint256 ) external view returns ( uint256 id, uint8 collectibleType, uint256 maxBoostLevel, uint256 addedBlockNumber, uint256 expiry, string uri );
  function ownerOfFarm ( address ) external view returns ( uint256 amount, uint256 compostedAmount, uint256 blockNumber, uint256 lastHarvestedBlockNumber, address harvesterAddress, uint256 numberOfCollectibles );
  function paused (  ) external view returns ( bool );
  function release (  ) external;
  function releaseCollectible ( uint256 index ) external;
  function renounceOwnership (  ) external;
  function revokeOperator ( address operator ) external;
  function send ( address recipient, uint256 amount, bytes data ) external;
  function setFarmlandAddresses ( address landAddress_, address farmerNFTAddress_, address tractorNFTAddress_ ) external;
  function setFarmlandVariables ( uint256 endMaturityBoost_, uint256 maxGrowthCycle_, uint256 maxGrowthCycleWithFarmer_, uint256 maxCompostBoost_, uint256 maxMaturityBoost_, uint256 maxMaturityCollectibleBoost_, uint256 maxFarmSizeWithoutFarmer_, uint256 maxFarmSizeWithoutTractor_, uint256 bonusCompostBoostWithFarmer_, uint256 bonusCompostBoostWithTractor_ ) external;
  function supportsInterface ( bytes4 interfaceId ) external view returns ( bool );
  function symbol (  ) external view returns ( string );
  function tokensReceived ( address operator, address from, address to, uint256 amount, bytes, bytes ) external;
  function totalSupply (  ) external view returns ( uint256 );
  function transfer ( address recipient, uint256 amount ) external returns ( bool );
  function transferFrom ( address holder, address recipient, uint256 amount ) external returns ( bool );
  function transferOwnership ( address newOwner ) external;
}
*/