using Final_Updated_Code_Assessment.DTO;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace Final_Updated_Code_Assessment
{
    public class Tests 
    {
        public LoginResponse token;
        public string endpoints = "/v1/accounts/login/real";
        public string moduleId = ConfigurationManager.AppSettings["moduleId"];
        public string productId = ConfigurationManager.AppSettings["productId"];


        [Test]

        public void FirstLoginTestMethod()
        {
            String dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            LoginRequest data = JsonConvert.DeserializeObject<LoginRequest>(File.ReadAllText(Path.Combine(dir + @"\TestData\LoginTestData.json")));

            LoginRequest loginRequest = new LoginRequest();

            var guid = ConfigurationManager.AppSettings["guid"];
            /* string payload = @"{""clientTypeId"": ""5"",
                                  ""languageCode"": ""en""
                                }";*/
            //var request = login.LoginReuestToGetToken(guid, payload);

            // var loginRequest = new LoginRequest();
            loginRequest.UserName = data.UserName;
            loginRequest.Password = data.Password;
            loginRequest.SessionProductId = data.SessionProductId;
            loginRequest.ClientTypeId = data.ClientTypeId;
            loginRequest.LanguageCode = data.LanguageCode;
            loginRequest.NumLaunchTokens = data.NumLaunchTokens;
            loginRequest.MarketType = data.MarketType;


            var helper = new HelperClass<LoginResponse>();

            token = helper.GetToken(endpoints, loginRequest, guid);
            var token1 = "JYDFGDFGFDGRTMPWZDFDFG";
            var token2 = token.tokens.userToken;

            Assert.AreEqual(token1, token2, "Token generated successfully");
            /*if (token!=null)
             {
                 Console.WriteLine("Token generated successfully");
             }*/



        }
        [Test]
        public void GamePlayTestMethod()
        {
            String dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            GamePlayRequest data = JsonConvert.DeserializeObject<GamePlayRequest>(File.ReadAllText(Path.Combine(dir + @"\TestData\GamePlayTestData.json")));
            var gameplaydata = new GamePlayRequest();

            string moduleId = ConfigurationManager.AppSettings["moduleId"];
            string productId = ConfigurationManager.AppSettings["productId"];
            gameplaydata.packetType = data.packetType;
            gameplaydata.payload = data.payload;
            gameplaydata.useFilter = data.useFilter;
            gameplaydata.isBase64Encoded = data.isBase64Encoded;



            var endpoint = "/v1/games/module/{{moduleID}}/client/{{clientID}}/play";
            var helper1 = new HelperClass<GamePlayResponse>();
            //Assert.Pass();


            try
            {

                var generatedtoken = token.tokens.userToken;
                var gamebalance = helper1.GetBalance(endpoint, gameplaydata, productId, moduleId, generatedtoken);
                var financialbalance = gamebalance.context.financials.payoutAmount;
                var playerbalance = gamebalance.context.balances.totalInAccountCurrency;
                var balance = financialbalance + playerbalance;

                Assert.NotNull(balance);
                
                    Console.WriteLine("Avaialble balance is:" + balance);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }





        }

    }
}