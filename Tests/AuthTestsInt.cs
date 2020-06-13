using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using RestSharp;
using RestSharpTestsInt.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsInt.Tests
{
    [AllureNUnit]
    [AllureDisplayIgnored] 
    [AllureParentSuite("WEB")]

    class AuthTestsInt
    {
        IRestResponse response = null;
        HttpStatusCode statusCode;
        JObject responseJson = null;
        public static string AuthToken;
        public static string Name;
        public static string message;

        PluginHelperInt plugHelp = new PluginHelperInt();

        //Прокидываем токен в хедер запросов 
        [OneTimeSetUp]
        public void ReturnDirectoryAndTokenForFormPatternTests()
        {
            string currentDirectory = plugHelp.ReturnDirectory();
            CleanAllureResultsInt.ClearFolder(CleanAllureResultsInt.alreadyClear, currentDirectory);
            AuthInt.GetToken();
        }

        [AllureTag("/auth")]
        [AllureSubSuite("POST")]

        [Category("RestSharp.INT.Auth controller")]
        [Test(Description = "Вход в систему под Админом. Параметры: логин, пароль, статус код"), TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.LoginTestData))]
        public void Login(string login, string pass, int statusCodeExpectedResult)
        {
            //Логинимся под AdminTest
            Name = "Admin";

            JsonSchema validSchema = SchemaConstructorInt.SchemaGeneratorForLogin();

            RequestHelperInt.pRequestLogin.AddJsonBody(new { Login = login, Password = pass });

            response = AuthInt.Client.Execute(RequestHelperInt.pRequestLogin);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var assertsAccumulator = new AssertAccumulatorInt();
            try
            {
                responseJson = JObject.Parse(response.Content);
                message = ResponseHelperInt.ReturnMessage(responseJson);
            }

            catch
            {
                JObject badContent = ResponseHelperInt.ReturnBadRequestAnswer(response.Content);
                //Проверка на статус код
                assertsAccumulator.Accumulate(
                () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n{badContent.ToString()}"));
                assertsAccumulator.Release();
            }

            //Проверка на валидность схемы ответа
            assertsAccumulator.Accumulate(
            () => Assert.That(responseJson.IsValid(validSchema), "Schema is invalid"));

            //Проверка на статус код
            assertsAccumulator.Accumulate(
            () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));
            assertsAccumulator.Release();

            //Проверка что залогинились под AdminTest
            string firstName = responseJson["data"]["User"]["Roles"][0].ToString();
            assertsAccumulator.Accumulate(
            () => Assert.That(firstName, Is.EqualTo(Name), $"FirstName {firstName} is invalid"));
        }

        [AllureTag("/auth")]
        [AllureSubSuite("POST")]

        [Category("RestSharp.INT.Auth controller")]
        [Test(Description = "Вход в систему под Админом ИБ. Параметры: логин, пароль, статус код"), TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.LoginAdminIBTestData))]
        public void LoginAdminIB(string login, string pass, int statusCodeExpectedResult)
        {
            //Логинимся под AdminTest
            Name = "AdminIb";

            JsonSchema validSchema = SchemaConstructorInt.SchemaGeneratorForLogin();

            RequestHelperInt.pRequestLoginAdminIB.AddJsonBody(new { Login = login, Password = pass });

            response = AuthInt.Client.Execute(RequestHelperInt.pRequestLoginAdminIB);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var assertsAccumulator = new AssertAccumulatorInt();
            try
            {
                responseJson = JObject.Parse(response.Content);
                message = ResponseHelperInt.ReturnMessage(responseJson);
            }

            catch
            {
                JObject badContent = ResponseHelperInt.ReturnBadRequestAnswer(response.Content);
                //Проверка на статус код
                assertsAccumulator.Accumulate(
                () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n{badContent.ToString()}"));
                assertsAccumulator.Release();
            }

            //Проверка на валидность схемы ответа
            assertsAccumulator.Accumulate(
            () => Assert.That(responseJson.IsValid(validSchema), "Schema is invalid"));

            //Проверка на статус код
            assertsAccumulator.Accumulate(
            () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));
            assertsAccumulator.Release();

            //Проверка что залогинились под AdminTest
            string firstName = responseJson["data"]["User"]["Roles"][0].ToString();
            assertsAccumulator.Accumulate(
            () => Assert.That(firstName, Is.EqualTo(Name), $"FirstName {firstName} is invalid"));
        }

        [AllureTag("/auth")]
        [AllureSubSuite("POST")]

        [Category("RestSharp.INT.Auth controller")]
        [Test(Description = "Выход пользователя из системы, нужен для логирования действий. Параметры: статус код"), TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.LogoutTestData))]
        public void Logout(int statusCodeExpectedResult)
        {
            AuthInt.GetToken();
            AuthToken = AuthInt.token;

            JsonSchema validSchema = SchemaConstructorInt.SchemaGeneratorForLogout();

            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeader(RequestHelperInt.pRequestLogout);

            response = AuthInt.Client.Execute(RequestHelperInt.pRequestLogout);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var assertsAccumulator = new AssertAccumulatorInt();
            try
            {
                responseJson = JObject.Parse(response.Content);
                message = ResponseHelperInt.ReturnMessage(responseJson);
            }
            catch
            {
                JObject badContent = ResponseHelperInt.ReturnBadRequestAnswer(response.Content);
                //Проверка на статус код
                assertsAccumulator.Accumulate(
                () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n{badContent.ToString()}"));
                assertsAccumulator.Release();
            }

            ////Проверка на валидность схемы ответа
            assertsAccumulator.Accumulate(
            () => Assert.IsTrue(responseJson.IsValid(validSchema), "Schema is incorrect"));

            //Проверка на статус код
            assertsAccumulator.Accumulate(
            () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));
            assertsAccumulator.Release();
        }
    }
}
