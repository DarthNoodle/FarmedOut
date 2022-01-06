


using FarmedOut.Console;
using FarmLandSDK;
using Nethereum.Uniswap.Contracts.UniswapV2Router02;
using Nethereum.Web3.Accounts;

string ARBITRUM_PROD_NETWORK = "https://arb1.arbitrum.io/rpc";
string ARBITRUM_TEST_NETWORK = "https://rinkeby.arbitrum.io/rpc";


//environment variable.. put the HEX of your private key
//   web3key
//   
//not sure of a cleaner way of testing this out quickly and easily while also keeping secrets out of github repos :D
//this is quick and dirty... in prod.. replace with something more ROBUST or you will be in trouble!
Account _UserAccount = new Account(ConfigProvider.GetETH_PrivKey());

FarmManager farmManager = new FarmManager(ARBITRUM_PROD_NETWORK, _UserAccount, false);

string accountPubAddress = _UserAccount.Address;


var farmDetails = await farmManager.GetFarm(accountPubAddress);
var landBalance = await farmManager.GetLandBalance(accountPubAddress);
var cornBalance = await farmManager.GetCornBalance(accountPubAddress);
var tokenBosts = await farmManager.GetFarmCollectibleTotals(accountPubAddress);

var landToken = new Nethereum.StandardTokenEIP20.StandardTokenService(farmManager.Web3, "0xFcc0351f3a1ff72409Df66a7589c1F9efBf53386"); //CORN PROD

var uniswapRouter_Arbi = new UniswapV2Router02Service(farmManager.Web3, "0x82af49447d8a07e3bd95bd0d56f35241523fbab1");


Console.ReadKey();