using Allure.Commons;
using Neolant.PFEI.Dto;
using Neolant.PFEI.Dto.Accounts;
using Neolant.PFEI.Dto.Certificates;
using Neolant.PFEI.Dto.Front.Out;
using Neolant.PFEI.Dto.FrontInt.In;
using Neolant.PFEI.Dto.FrontInt.Out;
using Neolant.PFEI.Dto.Messages;
using Neolant.PFEI.Infrastructure;
using Neolant.PFEI.Infrastructure.Abstract;
using Neolant.PFEI.Infrastructure.Enums;
using Neolant.PFEI.Infrastructure.Enums.Front;
using Neolant.PFEI.Neva;
using Neolant.PFEI.WebInt.Application;
using Newtonsoft.Json.Linq;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using RestSharp;
using RestSharp.Extensions;
using RestSharpTestsInt.Helpers;
using RestSharpTestsInt.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CategoryAttribute = NUnit.Framework.CategoryAttribute;
using dto = Neolant.PFEI.Dto.FrontInt.In;


namespace RestSharpTestsInt.Tests
{
    [AllureNUnit]
    [AllureDisplayIgnored] 
    [AllureParentSuite("WEB")]

    class AccountTestsInt
    {
        IRestResponse response = null;
        HttpStatusCode statusCode;
        JObject responseJson = null;
        public static string AuthToken;
        public static string Name;
        PluginHelperInt plugHelp = new PluginHelperInt();
        static string idCreatedUFR;
        public static string message;


        [OneTimeSetUp]
        public void ReturnDirectoryAndTokenForFormPatternTests()
        {
            string currentDirectory = plugHelp.ReturnDirectory();
            CleanAllureResultsInt.ClearFolder(CleanAllureResultsInt.alreadyClear, currentDirectory);
            AuthInt.GetToken();
            AuthAdminIbInt.GetToken();
            ArrayJsonKeys.ReturnJsonKeys();
        }

        [AllureTag("/api/test")]
        [AllureSubSuite("GET")]
        [AllureFeature("")]


        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Получение аватара аккаунта. Параметры: ID аккаунта, статус код"),
        TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.AccountGetAvatarTestData))]
        public void AccountGetAvatar(string accountId, int statusCodeExpectedResult)
        {
            //Принимает ресурс и параметр прокидываемый в ресурс 
            CustomRequestInt.ReturnRequest(RequestHelperInt.gRequestGetAvatar, accountId);

            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeader(RequestHelperInt.gRequestGetAvatar);

            response = AuthInt.Client.Execute(RequestHelperInt.gRequestGetAvatar);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var assertsAccumulator = new AssertAccumulatorInt();

            var targetType = typeof(ModelResult<string>);

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
            () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

            //Проверка на статус код
            assertsAccumulator.Accumulate(
            () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK" + message));

            assertsAccumulator.Release();

        }

        [AllureTag("/api/test")]
        [AllureSubSuite("GET")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Получение отдельного УИО (полная информация). Параметры: ID аккаунта, дата окончания действия, статус код"),
        TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.AccountGetUfrTestData))]
        public void AccountGetUfr(string accountId, string endedAt, int statusCodeExpectedResult)
        {
            //Принимает ресурс и параметр прокидываемый в ресурс 
            CustomRequestInt.ReturnRequest(RequestHelperInt.gRequestGetUfr, accountId);

            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeader(RequestHelperInt.gRequestGetUfr);
            //AddParamsInt.AddParametersToAccountGetUfr(false);

            response = AuthInt.Client.Execute(RequestHelperInt.gRequestGetUfr);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var assertsAccumulator = new AssertAccumulatorInt();

            var targetType = typeof(ModelResult<HistoryAccountDto>);

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
            () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме\n" + message));

            //Проверка на статус код
            assertsAccumulator.Accumulate(
            () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));

            assertsAccumulator.Release();

        }

        [AllureTag("/api/test")]
        [AllureSubSuite("GET")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Получение аватара аккаунта. Параметры: ID аккаунта, дата окончания действия, статус код"),
        TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.AccountGetUfrShortTestData))]
        public void AccountGetUfrShort(string accountId, string endedAt, int statusCodeExpectedResult)
        {
            //Принимает ресурс и параметр прокидываемый в ресурс 
            CustomRequestInt.ReturnRequest(RequestHelperInt.gRequestGetUfrShort, accountId);

            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeader(RequestHelperInt.gRequestGetUfrShort);
            AddParamsInt.AddParametersToAccountGetUfrShort(true);


            response = AuthInt.Client.Execute(RequestHelperInt.gRequestGetUfrShort);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var assertsAccumulator = new AssertAccumulatorInt();

            var targetType = typeof(ModelResult<string>);

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
            () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

            //Проверка на статус код
            assertsAccumulator.Accumulate(
            () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));

            assertsAccumulator.Release();

        }

        [AllureTag("/api/test")]
        [AllureSubSuite("GET")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Получение видов деятельности УИО. Параметры: ID аккаунта, дата окончания действия, статус код"),
        TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.AccountGeRegistryTypesTestData))]
        public void AccountGeRegistryTypes(string accountId, string endedAt, int statusCodeExpectedResult)
        {
            //Принимает ресурс и параметр прокидываемый в ресурс 
            CustomRequestInt.ReturnRequest(RequestHelperInt.gRequestGetRegistryRypes, accountId);

            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeader(RequestHelperInt.gRequestGetRegistryRypes);

            response = AuthInt.Client.Execute(RequestHelperInt.gRequestGetRegistryRypes);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var assertsAccumulator = new AssertAccumulatorInt();

            var targetType = typeof(ModelGetRegistryTypes);

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
            () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

            //Проверка на статус код
            assertsAccumulator.Accumulate(
            () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));

            assertsAccumulator.Release();
        }

        [AllureTag("/api/test")]
        [AllureSubSuite("GET")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Получение конфигурации УИО. Параметры: ID аккаунта, статус код"),
        TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.AccountGetUfrConfigurationTestData))]
        public void AccountGetUfrConfiguration(string accountId, int statusCodeExpectedResult)
        {
            //Принимает ресурс и параметр прокидываемый в ресурс 
            CustomRequestInt.ReturnRequest(RequestHelperInt.gRequestGetUfrConfiguration, accountId);

            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeader(RequestHelperInt.gRequestGetUfrConfiguration);

            response = AuthInt.Client.Execute(RequestHelperInt.gRequestGetUfrConfiguration);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var assertsAccumulator = new AssertAccumulatorInt();

            var targetType = typeof(ModelResult<dto.AccountConfigurationDto>);

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
            () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

            //Проверка на статус код
            assertsAccumulator.Accumulate(
            () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));

            assertsAccumulator.Release();
        }

        [AllureTag("/api/test")]
        [AllureSubSuite("GET")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Получение уведомлений УИО. Параметры: ID аккаунта, номер страницы, кол-во отображаемых записей на странице, статус код"),
        TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.AccountGetNotificationsTestData))]
        public void AccountGetNotifications(string accountId, int page, int pageSize, int statusCodeExpectedResult)
        {
            //Принимает ресурс и параметр прокидываемый в ресурс 
            CustomRequestInt.ReturnRequest(RequestHelperInt.gRequestGetNotifications, accountId);

            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeader(RequestHelperInt.gRequestGetNotifications);
            AddParamsInt.AddParametersToAccountGetNotifications(page, pageSize);


            response = AuthInt.Client.Execute(RequestHelperInt.gRequestGetNotifications);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var assertsAccumulator = new AssertAccumulatorInt();

            var targetType = typeof(ModelGetNotifications);

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
            () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

            //Проверка на статус код
            assertsAccumulator.Accumulate(
            () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));

            assertsAccumulator.Release();
        }

        [AllureTag("/api/test")]
        [AllureSubSuite("GET")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Получение сертификатов УИО. Параметры: ID аккаунта, номер страницы, кол-во отображаемых записей на странице, статус код"),
        TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.AccountGetCertificatesTestData))]
        public void AccountGetCertificates(string accountId, int page, int pageSize, int statusCodeExpectedResult)
        {
            //Принимает ресурс и параметр прокидываемый в ресурс 
            CustomRequestInt.ReturnRequest(RequestHelperInt.gRequestGetCertificates, accountId);

            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeader(RequestHelperInt.gRequestGetCertificates);
            AddParamsInt.AddParametersToAccountGetCertificates(page, pageSize);


            response = AuthInt.Client.Execute(RequestHelperInt.gRequestGetCertificates);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var assertsAccumulator = new AssertAccumulatorInt();

            var targetType = typeof(ModelGetCertificate);

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
            () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

            //Проверка на статус код
            assertsAccumulator.Accumulate(
            () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));

            assertsAccumulator.Release();
        }

        [AllureTag("/api/test")]
        [AllureSubSuite("GET")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Получет все пользовательские справочники данного аккаунта, где он делал хотя бы одну запись. Параметры: ID аккаунта, номер страницы, кол-во отображаемых записей на странице, статус код"),
        TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.AccountGetUserCatalogsTestData))]
        public void AccountGetUserCatalogs(string accountId, int page, int pageSize, int statusCodeExpectedResult)
        {
            //Принимает ресурс и параметр прокидываемый в ресурс 
            CustomRequestInt.ReturnRequest(RequestHelperInt.gRequestGetUserCatalogs, accountId);

            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeader(RequestHelperInt.gRequestGetUserCatalogs);
            AddParamsInt.AddParametersToAccountGetUserCatalogs(page, pageSize);


            response = AuthInt.Client.Execute(RequestHelperInt.gRequestGetUserCatalogs);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var assertsAccumulator = new AssertAccumulatorInt();

            var targetType = typeof(ModelGetUserCatalogs);

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
            () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

            //Проверка на статус код
            assertsAccumulator.Accumulate(
            () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));

            assertsAccumulator.Release();
        }

        [AllureTag("/api/test")]
        [AllureSubSuite("GET")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Получает данные конкретного пользовательского справочника, принадлежащие именно этому аккаунту. Параметры: ID аккаунта, ID справочника, номер страницы, кол-во отображаемых эл-тов на странице, статус код"),
        TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.AccountGetUserCatalogsThisAccountTestData))]
        public void AccountGetUserCatalogsThisAccount(string accountId, string catalogId, int page, int pageSize, int statusCodeExpectedResult)
        {
            //Принимает ресурс и параметр прокидываемый в ресурс 
            CustomRequestInt.ReturnRequest(RequestHelperInt.gRequestGetUserCatalogsThisAccount, accountId, catalogId);


            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeader(RequestHelperInt.gRequestGetUserCatalogsThisAccount);
            AddParamsInt.AddParametersToAccountGetUserCatalogsThisAccount(page, pageSize);


            response = AuthInt.Client.Execute(RequestHelperInt.gRequestGetUserCatalogsThisAccount);

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

            //Проверка на статус код
            assertsAccumulator.Accumulate(
            () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));

            assertsAccumulator.Release();
        }

        [AllureTag("/api/test")]
        [AllureSubSuite("GET")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Генерация логина и пароля для пользователя. Параметры: ID аккаунта, статус код"),
        TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.GetGenerateCredentialsTestData))]
        public void GetGenerateCredentials(string accountId, int statusCodeExpectedResult)
        {
            //Принимает ресурс и параметр прокидываемый в ресурс 
            CustomRequestInt.ReturnRequest(RequestHelperInt.gRequestGenerateCredentials, accountId);


            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeader(RequestHelperInt.gRequestGenerateCredentials);

            response = AuthInt.Client.Execute(RequestHelperInt.gRequestGenerateCredentials);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var assertsAccumulator = new AssertAccumulatorInt();

            var targetType = typeof(ModelResult<CredentialsInfoDto>);

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
            () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

            //Проверка на статус код
            assertsAccumulator.Accumulate(
            () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));

            assertsAccumulator.Release();
        }

        [AllureTag("/api/test")]
        [AllureSubSuite("GET")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Генерация данных для иностранного УИО. Параметры: статус код"),
        TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.GetGenerateAccountInfoTestData))]
        public void GetGenerateAccountInfo(int statusCodeExpectedResult)
        {
            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeader(RequestHelperInt.gRequestGenerateAccountInfo);

            response = AuthInt.Client.Execute(RequestHelperInt.gRequestGenerateAccountInfo);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var assertsAccumulator = new AssertAccumulatorInt();

            var targetType = typeof(ModelGenerateAccountInfo);

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
            () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

            //Проверка на статус код
            assertsAccumulator.Accumulate(
            () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));

            assertsAccumulator.Release();
        }

        [AllureTag("/api/test")]
        [AllureSubSuite("GET")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Возвращает допполя текущего профиля (аккаунта). Параметры: ID аккаунта, статус код"),
        TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.GetAddInfoTestData))]
        public void GetAddInfo(string accountId, int statusCodeExpectedResult)
        {
            //Принимает ресурс и параметр прокидываемый в ресурс 
            CustomRequestInt.ReturnRequest(RequestHelperInt.gRequestGetAdditionalInfo, accountId);

            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeader(RequestHelperInt.gRequestGetAdditionalInfo);


            response = AuthInt.Client.Execute(RequestHelperInt.gRequestGetAdditionalInfo);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var assertsAccumulator = new AssertAccumulatorInt();

            var targetType = typeof(ModelResult<ArraySegment<BlockInfoDto>>);
            
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
            () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

            //Проверка на статус код
            assertsAccumulator.Accumulate(
            () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));

            assertsAccumulator.Release();
        }

        [AllureTag("/api/test")]
        [AllureSubSuite("GET")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Получение кол-ва аккаунтов определенного статуса. Параметры: персноальный статус аккаунта, статус код"),
        TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.GetAccCountTestData))]
        public void GetAccCount(PersonalAccountStatuses randomStatus, int statusCodeExpectedResult)
        {
            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeader(RequestHelperInt.gRequestGetAccountCount);
            AddParamsInt.AddParametersToGetAccCount(randomStatus);

            response = AuthInt.Client.Execute(RequestHelperInt.gRequestGetAccountCount);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var assertsAccumulator = new AssertAccumulatorInt();

            var targetType = typeof(ModelResult<int>);

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
            () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

            //Проверка на статус код
            assertsAccumulator.Accumulate(
            () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));

            assertsAccumulator.Release();
        }

        [AllureTag("/api/test")]
        [AllureSubSuite("POST")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Добавление нового УИО (ЮР лицо). Параметры: полное наименование, короткое наименование, ОГРН, ИНН, БИК, RegNum, Номер выданного бланка свидетельства, дата включения, дата создания, имейл, адрес, телефон, Надзорное подразделение ЦБ РФ, Организационно-правовая форма, персональный статус аккаунта, тип аккаунта, статус аккаунта, статус код"), TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.UFRCreateTestData))]
        public void UFRCreate(string fullName, string shortName, /*string firstName, string secondName, string patronymic,*/ string oGRN, string iNN, string bIC, string regNum, string issuedCertificateNumber, string inclusionDate, string creationDate, string email, string address, string phone, string territorialInstituteId, string orgLegalFormCode, string personalAccountStatus, string enterpriseType, string accountStatus, int statusCodeExpectedResult)
        {
            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeader(RequestHelperInt.pRequestUFRCreate);

            string orgLegalFormName = "Акционерные общества";
            string territorialInstituteName = "Административный отдел";
            IDictionary<string, string> registryTypes = ArrayJsonKeys.registryTypes;
            string code = RandomValuesInt.RandomString(5);

            RequestHelperInt.pRequestUFRCreate.AddJsonBody(new {
                FullName = fullName,
                ShortName = shortName,
                FirstName = "",
                SecondName = "",
                Patronymic = "",
                Code = code,
                OGRN = oGRN,
                INN = iNN,
                BIC = bIC,
                RegNum = regNum,
                IssuedCertificateNumber = issuedCertificateNumber,
                InclusionDate = inclusionDate,
                CreationDate = creationDate,
                Email = email,
                Address = address,
                Phone = phone,
                TerritorialInstituteId = territorialInstituteId,
                OrgLegalFormCode = orgLegalFormCode,
                PersonalAccountStatus = personalAccountStatus,
                EnterpriseType = enterpriseType,
                AccountStatus = accountStatus,
                OrgLegalFormName = orgLegalFormName,
                TerritorialInstituteName = territorialInstituteName,
                RegistryTypes = registryTypes
            });

            response = AuthInt.Client.Execute(RequestHelperInt.pRequestUFRCreate);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            //typeof берём из контроллера (SwaggerResponse)
            var targetType = typeof(Result<string>);

            var assertsAccumulator = new AssertAccumulatorInt();

            try
            {
                responseJson = JObject.Parse(response.Content);
                idCreatedUFR = responseJson["data"].Value<string>();
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
            () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

            //Проверка на статус код
            assertsAccumulator.Accumulate(
            () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));
            assertsAccumulator.Release();
            assertsAccumulator.Release();

            //Удаляем из БД созданный УИО
            DBCleaner.RemoveCreatedUFRFromAccountConfigurationsTable(Guid.Parse(idCreatedUFR));
            DBCleaner.RemoveCreatedUFFromAccountTable(Guid.Parse(idCreatedUFR));
        }

        [AllureTag("/api/test")]
        [AllureSubSuite("POST")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Синхронизация УФРю Параметры: статус код"), TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.SyncUFRTestData))]
        public void SyncUFR(int statusCodeExpectedResult)
        {
            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeader(RequestHelperInt.pRequestUFRsync);

            response = AuthInt.Client.Execute(RequestHelperInt.pRequestUFRsync);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            //typeof берём из контроллера (SwaggerResponse)
            var targetType = typeof(Result<string>);

            var assertsAccumulator = new AssertAccumulatorInt();

            try
            {
                responseJson = JObject.Parse(response.Content);
                idCreatedUFR = responseJson["data"].Value<string>();
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
            () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

            //Проверка на статус код
            assertsAccumulator.Accumulate(
            () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));
            assertsAccumulator.Release();
        }

        [AllureTag("/api/test")]
        [AllureSubSuite("POST")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Отправка актуальной информации в НЕВУ. Параметры: ID аккаунта, статус код"), TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.SendToNevaActualInfoTestData))]
        public void SendToNevaActualInfo(string accountId, int statusCodeExpectedResult)
        {
            //Принимает ресурс и параметр прокидываемый в ресурс 
            CustomRequestInt.ReturnRequest(RequestHelperInt.pRequestSendToNeva, accountId);

            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeader(RequestHelperInt.pRequestSendToNeva);

            response = AuthInt.Client.Execute(RequestHelperInt.pRequestSendToNeva);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            //typeof берём из контроллера (SwaggerResponse)
            var targetType = typeof(Result<string>);

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
            () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

            //Проверка на статус код
            assertsAccumulator.Accumulate(
            () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));
            assertsAccumulator.Release();
            assertsAccumulator.Release();

        }

        [AllureTag("/api/test")]
        [AllureSubSuite("POST")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Редактирование УИО (Юр лицо). Параметры: полное наименование, короткое наименование, ОГРН, ИНН, БИК, RegNum, Номер выданного бланка свидетельства, дата включения, дата создания, имейл, адрес, телефон, Надзорное подразделение ЦБ РФ, Организационно-правовая форма, персональный статус аккаунта, тип аккаунта, статус аккаунта, статус код"), TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.UFREditTestData))]
        public void UFREdit(string accountId, string shortName, /*string firstName, string secondName, string patronymic,*/ string code, string oGRN, string iNN, string bIC, string regNum, string issuedCertificateNumber, string inclusionDate, string creationDate, string email, string address, string phone, string territorialInstituteId, string orgLegalFormCode, string personalAccountStatus, string enterpriseType, string accountStatus, int statusCodeExpectedResult)
        {
            //Принимает ресурс и параметр прокидываемый в ресурс 
            CustomRequestInt.ReturnRequest(RequestHelperInt.pRequestUFREdit, accountId);

            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeader(RequestHelperInt.pRequestUFREdit);

            string orgLegalFormName = "Акционерные общества";
            string territorialInstituteName = "Административный отдел";
            IDictionary<string, string> registryTypes = ArrayJsonKeys.registryTypes;
            string fullName = $"Edited{DateTime.Now}" + RandomValuesInt.RandomString(5);

            RequestHelperInt.pRequestUFREdit.AddJsonBody(new
            {
                FullName = fullName,
                ShortName = shortName,
                FirstName = "",
                SecondName = "",
                Patronymic = "",
                Code = code,
                OGRN = oGRN,
                INN = iNN,
                BIC = bIC,
                RegNum = regNum,
                IssuedCertificateNumber = issuedCertificateNumber,
                InclusionDate = inclusionDate,
                CreationDate = creationDate,
                Email = email,
                Address = address,
                Phone = phone,
                TerritorialInstituteId = territorialInstituteId,
                OrgLegalFormCode = orgLegalFormCode,
                PersonalAccountStatus = personalAccountStatus,
                EnterpriseType = enterpriseType,
                AccountStatus = accountStatus,
                OrgLegalFormName = orgLegalFormName,
                TerritorialInstituteName = territorialInstituteName,
                RegistryTypes = registryTypes
            });

            response = AuthInt.Client.Execute(RequestHelperInt.pRequestUFREdit);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            //typeof берём из контроллера (SwaggerResponse)
            var targetType = typeof(Result<string>);

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
            () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

            //Проверка на статус код
            assertsAccumulator.Accumulate(
            () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));
            assertsAccumulator.Release();
            assertsAccumulator.Release();

        }

        [AllureTag("/api/test")]
        [AllureSubSuite("POST")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Синхронизация истории взаимодействия УИО. Параметры: ID аккаунта, статус код"), TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.MessagesSyncTestData))]
        public void MessagesSync(string accountId, int statusCodeExpectedResult)
        {
            //Принимает ресурс и параметр прокидываемый в ресурс 
            CustomRequestInt.ReturnRequest(RequestHelperInt.pRequestMessagesSync, accountId);

            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeader(RequestHelperInt.pRequestMessagesSync);

            response = AuthInt.Client.Execute(RequestHelperInt.pRequestMessagesSync);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            //typeof берём из контроллера (SwaggerResponse)
            var targetType = typeof(Result<string>);

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
            () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

            //Проверка на статус код
            assertsAccumulator.Accumulate(
            () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));
            assertsAccumulator.Release();
            assertsAccumulator.Release();

        }

        [AllureTag("/api/test")]
        [AllureSubSuite("POST")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Сохранение конфигурации УИО. Параметры: ID аккаунта, синхронизация с РУФР, персональный размер пакета, квота, статус код"), TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.SaveUFRConfigurationTestData))]
        public void SaveUFRConfiguration(string accountId, bool rufrSync,  long personalPacketSize, long personalQuotaSize, int statusCodeExpectedResult)
        {
            //Принимает ресурс и параметр прокидываемый в ресурс 
            CustomRequestInt.ReturnRequest(RequestHelperInt.pRequestSaveUFRConfiguration, accountId);

            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeader(RequestHelperInt.pRequestSaveUFRConfiguration);

            RequestHelperInt.pRequestSaveUFRConfiguration.AddJsonBody(new
            {
               RrufrSync = rufrSync,
               PersonalPacketSize = personalPacketSize,
               PersonalQuotaSize = personalQuotaSize
            });

            response = AuthInt.Client.Execute(RequestHelperInt.pRequestSaveUFRConfiguration);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            //typeof берём из контроллера (SwaggerResponse)
            var targetType = typeof(Result<string>);

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
            () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

            //Проверка на статус код
            assertsAccumulator.Accumulate(
            () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));
            assertsAccumulator.Release();
            assertsAccumulator.Release();

        }

        [AllureTag("/api/test")]
        [AllureSubSuite("POST")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Сохранение блока допинфы. Параметры: ID аккаунта, ID вида деятельности, ID блока допполей, ID даты принятия, ID даты исключения, статус код"), TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.SetUFRAdditionalInfoTestData))]
        public void SetUFRAdditionalInfo(string accountId, string registryTypeToAccountId, string additionalFieldsBlockId, string idDateOfDecide, string idDateEnd, int statusCodeExpectedResult)
        {
            //Создаем аккаунт
            Guid accId = DBInserter.CreateAccountWithFullStrings();
            DBInserter.CreateNoteInAccountConfiguration(accId);

            //Принимает ресурс и параметр прокидываемый в ресурс 
            CustomRequestInt.ReturnRequest(RequestHelperInt.pRequestSetAdditionalInfo, accId.ToString());

            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeader(RequestHelperInt.pRequestSetAdditionalInfo);

            response = AuthInt.Client.Execute(RequestHelperInt.pRequestSetAdditionalInfo);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            //typeof берём из контроллера (SwaggerResponse)
            var targetType = typeof(Result<bool>);

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
            () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

            //Проверка на статус код
            assertsAccumulator.Accumulate(
            () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));
            assertsAccumulator.Release();

            //Проверка что в мессадже нет сообщения об ошибке изменения полей
            assertsAccumulator.Accumulate(
            () => Assert.That(message, Is.EqualTo("Текст сообщения пустой"), $"Данные не изменены. Получено сообщение: {message}"));
            assertsAccumulator.Release();
        }

        [AllureTag("/api/test")]
        [AllureSubSuite("POST")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Экспортирование статсов УИО. Параметры: статус код"), TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.ExportStatusesTestData))]
        public void ExportStatuses(int statusCodeExpectedResult)
        {
            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeader(RequestHelperInt.pRequestExportStatuses);

            response = AuthInt.Client.Execute(RequestHelperInt.pRequestExportStatuses);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var assertsAccumulator = new AssertAccumulatorInt();


            if (response.IsSuccessful)
            {
                var dotNetXMLDesirealizer = new RestSharp.Deserializers.DotNetXmlDeserializer();
                ListAccountStatuses data = dotNetXMLDesirealizer.Deserialize<ListAccountStatuses>(response);
                //Достаем только статус
                List<string> listOfAllPersonStatuses = data.Ufr.Select(e => e.PersonStatus).ToList();
                //Убираем повторяющиеся статусы (тип нам не нужен)
                List<string> listOfPersonStatusesWithoutDuplicates = listOfAllPersonStatuses.Distinct().ToList();

                List<string> listOfStatuses = new List<string>();
                listOfStatuses.Add("Действующий");
                listOfStatuses.Add("Заблокированный");
                listOfStatuses.Add("Создан");
                listOfStatuses.Add("Ожидается активация");
                listOfStatuses.Add("Удален");
                listOfStatuses.Add("Ожидается смена пароля");

                //Смотрим есть ли несоответствия у двух листов
                var dissonanceOfLists = listOfPersonStatusesWithoutDuplicates.Except(listOfStatuses).ToList();

                string randomName = "UFRStatuses" + RandomValuesInt.RandomString(5) + ".xml";

                var pathToSave = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                Directory.CreateDirectory("ExportUFRStatuses");
                AuthInt.Client.DownloadData(RequestHelperInt.pRequestExportStatuses).SaveAs(Path.Combine(pathToSave, $"ExportUFRStatuses\\{randomName}"));

                //Проверка на статус код
                assertsAccumulator.Accumulate(
                () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK"));
                assertsAccumulator.Release();

                //Проверка найдены ли статусы, которых нет в перечислении статусов ЛК PersonalAccountStatuses
                assertsAccumulator.Accumulate(
                () => Assert.IsEmpty(dissonanceOfLists, $"При экспортировании статусов УИО обнаружены некорректные статусы: {dissonanceOfLists}"));
                assertsAccumulator.Release();
            }
            else
            {
                JObject badContent = ResponseHelperInt.ReturnBadRequestAnswer(response.Content);
                //Проверка на статус код
                assertsAccumulator.Accumulate(
                () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n{badContent.ToString()}"));
                assertsAccumulator.Release();
            }
        }

        [AllureTag("/api/test")]
        [AllureSubSuite("POST")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Подтверждение нескольких аккаунтов. Параметры: ID аккаунта, статус код"), TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.AccountsApplyTestData))]
        public void AccountsApply(string accountId, int statusCodeExpectedResult)
        {
            Guid accId = Guid.Parse(accountId);

            RequestHelperInt.pRequestApplyAccounts.AddJsonBody(new Guid [] { accId });

            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeaderAdminIB(RequestHelperInt.pRequestApplyAccounts);

            response = AuthInt.Client.Execute(RequestHelperInt.pRequestApplyAccounts);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var targetType = typeof(Result<string>);

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
            () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

            //Проверка на статус код
            assertsAccumulator.Accumulate(
            () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));
            assertsAccumulator.Release();

            //Изменяем PersonalAccountStatus на Создан ЛК
            DBInserter.ChangePersonalAccountStatus(accId, PersonalAccountStatuses.PersonalAccountCreated);
        }

        [AllureTag("/api/test")]
        [AllureSubSuite("POST")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Отправка причины блокировки ЛК. Параметры: ID аккаунта, статус код"), TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.TestSendBlockingReasonTestData))]
        public void TestSendBlockingReason(string accountId, int statusCodeExpectedResult)
        {
            Guid accId = Guid.Parse(accountId);

            RequestHelperInt.pRequestSendBlockingReason.AddJsonBody(new Guid[] { accId });

            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeaderAdminIB(RequestHelperInt.pRequestSendBlockingReason);

            response = AuthInt.Client.Execute(RequestHelperInt.pRequestSendBlockingReason);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var targetType = typeof(Result<string>);

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
            () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

            //Проверка на статус код
            assertsAccumulator.Accumulate(
            () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));
            assertsAccumulator.Release();

        }

        [AllureTag("/api/test")]
        [AllureSubSuite("POST")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Блокировка УИО. Параметры: причина блокировки, ID аккаунта, статус код"), TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.BlockTestTestData))]
        public void BlockTest(BlockingReasonType type, string accountId, int statusCodeExpectedResult)
        {
            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeaderAdminIB(RequestHelperInt.pRequestBlockUFR);

            AddParamsInt.AddParametersToBlockTest(type);

            Guid accId = Guid.Parse(accountId);

            RequestHelperInt.pRequestBlockUFR.AddJsonBody(new Guid[] { accId });

            response = AuthInt.Client.Execute(RequestHelperInt.pRequestBlockUFR);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var targetType = typeof(Result<string>);

            var assertsAccumulator = new AssertAccumulatorInt();

            if (response.IsSuccessful)
            {
                responseJson = JObject.Parse(response.Content);
                message = ResponseHelperInt.ReturnMessage(responseJson);

                //Проверка на валидность схемы ответа
                assertsAccumulator.Accumulate(
                () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

                //Проверка на статус код
                assertsAccumulator.Accumulate(
                () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));
                assertsAccumulator.Release();

                //Меняем статус на Действующий
                DBInserter.ChangePersonalAccountStatus(accId, PersonalAccountStatuses.Active);

            }
            else
            {
                JObject badContent = ResponseHelperInt.ReturnBadRequestAnswer(response.Content);
                //Проверка на статус код
                assertsAccumulator.Accumulate(
                () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n{badContent.ToString()}"));
                assertsAccumulator.Release();
            }
        }

        [AllureTag("/api/test")]
        [AllureSubSuite("POST")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Разблокировка УИО. Параметры: причина блокировки, ID аккаунта, статус код"), TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.UnBlockTestTestData))]
        public void UnBlockTest(string accountId, int statusCodeExpectedResult)
        {
            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeaderAdminIB(RequestHelperInt.pRequestUnBlockUFR);
            Guid accId = Guid.Parse(accountId);
            AddParamsInt.AddParametersToUnBlockTest(accId);

            response = AuthInt.Client.Execute(RequestHelperInt.pRequestUnBlockUFR);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var targetType = typeof(Result<string>);

            var assertsAccumulator = new AssertAccumulatorInt();

            if (response.IsSuccessful)
            {
                responseJson = JObject.Parse(response.Content);
                message = ResponseHelperInt.ReturnMessage(responseJson);

                //Проверка на валидность схемы ответа
                assertsAccumulator.Accumulate(
                () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

                //Проверка на статус код
                assertsAccumulator.Accumulate(
                () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));
                assertsAccumulator.Release();

                //Меняем статус на Заблокированный
                DBInserter.ChangePersonalAccountStatus(accId, PersonalAccountStatuses.Blocked);
            }
            else
            {
                JObject badContent = ResponseHelperInt.ReturnBadRequestAnswer(response.Content);
                //Проверка на статус код
                assertsAccumulator.Accumulate(
                () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n{badContent.ToString()}"));
                assertsAccumulator.Release();
            }
        }

        [AllureTag("/api/test")]
        [AllureSubSuite("POST")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Генерация логина и пароля для пользователя. Параметры: причина блокировки, ID аккаунта, статус код"), TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.AcceptCredentialsTestTestData))]
        public void AcceptCredentialsTest(string accountId, int statusCodeExpectedResult)
        {
            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeader(RequestHelperInt.pRequestAcceptCredentials);

            Guid accId = Guid.Parse(accountId);

            RequestHelperInt.pRequestAcceptCredentials.AddJsonBody(new CredentialsInfoDto {Id = accId});

            response = AuthInt.Client.Execute(RequestHelperInt.pRequestAcceptCredentials);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var targetType = typeof(Result<string>);

            var assertsAccumulator = new AssertAccumulatorInt();

            if (response.IsSuccessful)
            {
                responseJson = JObject.Parse(response.Content);
                message = ResponseHelperInt.ReturnMessage(responseJson);

                //Проверка на валидность схемы ответа
                assertsAccumulator.Accumulate(
                () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

                //Проверка на статус код
                assertsAccumulator.Accumulate(
                () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));
                assertsAccumulator.Release();

                //Меняем статус на Создан ЛК
                DBInserter.ChangePersonalAccountStatus(accId, PersonalAccountStatuses.PersonalAccountCreated);
            }
            else
            {
                JObject badContent = ResponseHelperInt.ReturnBadRequestAnswer(response.Content);
                //Проверка на статус код
                assertsAccumulator.Accumulate(
                () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n{badContent.ToString()}"));
                assertsAccumulator.Release();
            }
        }

        [AllureTag("/api/test")]
        [AllureSubSuite("POST")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Отклонение нескольких аккаунтов. Параметры: причина отклонения, статус код"), TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.RejectTestTestData))]
        public void RejectTest(string reason, int statusCodeExpectedResult)
        {
            //Создаем аккаунты для удаления
            Guid firstAccountId = DBInserter.CreateAccount();
            Guid SecondAccountId = DBInserter.CreateAccount();

            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeaderAdminIB(RequestHelperInt.pRequestReject);

            List<Guid> listAccountIds = new List<Guid>();
            listAccountIds.Add(firstAccountId);

            List<Guid> listAccountIds2 = new List<Guid>();
            listAccountIds2.Add(SecondAccountId);

            var listOfAccountIds = listAccountIds.Concat(listAccountIds2);

            RequestHelperInt.pRequestReject.AddJsonBody(new RejectAccountsDto  { AccountIds = listOfAccountIds, Reason = reason });

            response = AuthInt.Client.Execute(RequestHelperInt.pRequestReject);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var targetType = typeof(Result<string>);

            var assertsAccumulator = new AssertAccumulatorInt();

            if (response.IsSuccessful)
            {
                responseJson = JObject.Parse(response.Content);
                message = ResponseHelperInt.ReturnMessage(responseJson);

                //Проверка на валидность схемы ответа
                assertsAccumulator.Accumulate(
                () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

                //Проверка на статус код
                assertsAccumulator.Accumulate(
                () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));
                assertsAccumulator.Release();

            }
            else
            {
                JObject badContent = ResponseHelperInt.ReturnBadRequestAnswer(response.Content);
                //Проверка на статус код
                assertsAccumulator.Accumulate(
                () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n{badContent.ToString()}"));
                assertsAccumulator.Release();
            }
        }

        [AllureTag("/api/test")]
        [AllureSubSuite("POST")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Получение списка УИО. Параметры: статус код"), TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.GetAllTestTestData))]
        public void GetAllTest(int statusCodeExpectedResult)
        {
            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeaderAdminIB(RequestHelperInt.pRequestGetAll);

            List<FilterQuery> listFilterQuery = new List<FilterQuery>();

            var searchQuery = new SearchQuery
            {
                Common = listFilterQuery
            };

            var filterQuery = new FilterQuery { };

            List<GroupQuery> listGroupQuery = new List<GroupQuery>();

            List<SortQuery> sortQuery = new List<SortQuery>();

            RequestHelperInt.pRequestGetAll.AddJsonBody(new DataQueryPack {SearchQuery = searchQuery, FilterQuery = filterQuery, GroupQueries = listGroupQuery, SortQueries = sortQuery});

            response = AuthInt.Client.Execute(RequestHelperInt.pRequestGetAll);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var targetType = typeof(ModelUFRGetAll);

            var assertsAccumulator = new AssertAccumulatorInt();

            if (response.IsSuccessful)
            {
                responseJson = JObject.Parse(response.Content);
                message = ResponseHelperInt.ReturnMessage(responseJson);

                //Проверка на валидность схемы ответа
                assertsAccumulator.Accumulate(
                () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

                //Проверка на статус код
                assertsAccumulator.Accumulate(
                () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));
                assertsAccumulator.Release();

            }
            else
            {
                JObject badContent = ResponseHelperInt.ReturnBadRequestAnswer(response.Content);
                //Проверка на статус код
                assertsAccumulator.Accumulate(
                () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n{badContent.ToString()}"));
                assertsAccumulator.Release();
            }
        }

        [AllureTag("/api/test")]
        [AllureSubSuite("POST")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Получение сообщений УИО. Параметры: тип сообщения, ID аккаунта, страница, кол-во отображаемых записей на странице, статус код"), TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.GetMessagesTestTestData))]
        public void GetMessagesTest(MessageVmType type, string accountId, int page, int size, int statusCodeExpectedResult)
        {
            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeaderAdminIB(RequestHelperInt.pRequestGetMessagesUFR);

            CustomRequestInt.ReturnRequest(RequestHelperInt.pRequestGetMessagesUFR, accountId);

            AddParamsInt.AddParametersToGetMessageGroupsTest(type, page, size);

            List<FilterQuery> listFilterQuery = new List<FilterQuery>();

            var searchQuery = new SearchQuery
            {
                Common = listFilterQuery
            };

            var filterQuery = new FilterQuery { };

            List<GroupQuery> listGroupQuery = new List<GroupQuery>();

            List<SortQuery> sortQuery = new List<SortQuery>();

            RequestHelperInt.pRequestGetMessagesUFR.AddJsonBody(new DataQueryPack { SearchQuery = searchQuery, FilterQuery = filterQuery, GroupQueries = listGroupQuery, SortQueries = sortQuery });

            response = AuthInt.Client.Execute(RequestHelperInt.pRequestGetMessagesUFR);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var targetType = typeof(ModelGetMessages);

            var assertsAccumulator = new AssertAccumulatorInt();

            if (response.IsSuccessful)
            {
                responseJson = JObject.Parse(response.Content);
                message = ResponseHelperInt.ReturnMessage(responseJson);

                //Проверка на валидность схемы ответа
                assertsAccumulator.Accumulate(
                () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

                //Проверка на статус код
                assertsAccumulator.Accumulate(
                () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));
                assertsAccumulator.Release();

            }
            else
            {
                JObject badContent = ResponseHelperInt.ReturnBadRequestAnswer(response.Content);
                //Проверка на статус код
                assertsAccumulator.Accumulate(
                () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n{badContent.ToString()}"));
                assertsAccumulator.Release();
            }
        }

        [AllureTag("/api/test")]
        [AllureSubSuite("POST")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Удаление списка УИО. Параметры: статус код"), TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.DeleteTestTestData))]
        public void DeleteTest(int statusCodeExpectedResult)
        {
            //Создаем аккаунты для удаления
            Guid idFirstAccount = DBInserter.CreateAccount();
            Guid idSecondAccount = DBInserter.CreateAccount();

            RequestHelperInt.dRequestDeleteUFR.AddJsonBody(new Guid[] { idFirstAccount, idSecondAccount });

            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeaderAdminIB(RequestHelperInt.dRequestDeleteUFR);

            response = AuthInt.Client.Execute(RequestHelperInt.dRequestDeleteUFR);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var targetType = typeof(Result<string>);

            var assertsAccumulator = new AssertAccumulatorInt();

            if (response.IsSuccessful)
            {
                responseJson = JObject.Parse(response.Content);
                message = ResponseHelperInt.ReturnMessage(responseJson);

                //Проверка на валидность схемы ответа
                assertsAccumulator.Accumulate(
                () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

                //Проверка на статус код
                assertsAccumulator.Accumulate(
                () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));
                assertsAccumulator.Release();
            }
            else
            {
                JObject badContent = ResponseHelperInt.ReturnBadRequestAnswer(response.Content);
                //Проверка на статус код
                assertsAccumulator.Accumulate(
                () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n{badContent.ToString()}"));
                assertsAccumulator.Release();
            }
        }

        [AllureTag("/api/test")]
        [AllureSubSuite("POST")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Получение истории изменений УИО. Параметры: ID аккаунта, страница, кол-во отображаемых записей на странице, статус код"), TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.GetChangesTestTestData))]
        public void GetChangesTest(string accountId, int page, int size, int statusCodeExpectedResult)
        {
            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeaderAdminIB(RequestHelperInt.pRequestGetChangesUFR);

            CustomRequestInt.ReturnRequest(RequestHelperInt.pRequestGetChangesUFR, accountId);

            AddParamsInt.AddParametersToGetChangesTest(page, size);

            List<FilterQuery> listFilterQuery = new List<FilterQuery>();

            var searchQuery = new SearchQuery
            {
                Common = listFilterQuery
            };

            var filterQuery = new FilterQuery { };

            List<GroupQuery> listGroupQuery = new List<GroupQuery>();

            List<SortQuery> sortQuery = new List<SortQuery>();

            RequestHelperInt.pRequestGetChangesUFR.AddJsonBody(new DataQueryPack { SearchQuery = searchQuery, FilterQuery = filterQuery, GroupQueries = listGroupQuery, SortQueries = sortQuery });

            response = AuthInt.Client.Execute(RequestHelperInt.pRequestGetChangesUFR);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var targetType = typeof(ModelGetChangesHistory);

            var assertsAccumulator = new AssertAccumulatorInt();

            if (response.IsSuccessful)
            {
                responseJson = JObject.Parse(response.Content);
                message = ResponseHelperInt.ReturnMessage(responseJson);

                //Проверка на валидность схемы ответа
                assertsAccumulator.Accumulate(
                () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

                //Проверка на статус код
                assertsAccumulator.Accumulate(
                () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));
                assertsAccumulator.Release();

            }
            else
            {
                JObject badContent = ResponseHelperInt.ReturnBadRequestAnswer(response.Content);
                //Проверка на статус код
                assertsAccumulator.Accumulate(
                () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n{badContent.ToString()}"));
                assertsAccumulator.Release();
            }
        }

        [AllureTag("/api/test")]
        [AllureSubSuite("POST")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Получение истории действий УИО. Параметры: ID аккаунта, страница, кол-во отображаемых записей на странице, статус код"), TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.GetActionsTestTestData))]
        public void GetActionsTest(string accountId, int page, int size, int statusCodeExpectedResult)
        {
            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeaderAdminIB(RequestHelperInt.pRequestGetChangesActionsUFR);

            CustomRequestInt.ReturnRequest(RequestHelperInt.pRequestGetChangesActionsUFR, accountId);

            AddParamsInt.AddParametersToGetActionsTest(page, size);

            List<FilterQuery> listFilterQuery = new List<FilterQuery>();

            var searchQuery = new SearchQuery
            {
                Common = listFilterQuery
            };

            var filterQuery = new FilterQuery { };

            List<GroupQuery> listGroupQuery = new List<GroupQuery>();

            List<SortQuery> sortQuery = new List<SortQuery>();

            RequestHelperInt.pRequestGetChangesActionsUFR.AddJsonBody(new DataQueryPack { SearchQuery = searchQuery, FilterQuery = filterQuery, GroupQueries = listGroupQuery, SortQueries = sortQuery });

            response = AuthInt.Client.Execute(RequestHelperInt.pRequestGetChangesActionsUFR);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var targetType = typeof(ModelGetActionsChanges);

            var assertsAccumulator = new AssertAccumulatorInt();

            if (response.IsSuccessful)
            {
                responseJson = JObject.Parse(response.Content);
                message = ResponseHelperInt.ReturnMessage(responseJson);

                //Проверка на валидность схемы ответа
                assertsAccumulator.Accumulate(
                () => Assert.IsTrue(SchemaConstructorInt.ValidationBySchema(responseJson, targetType), "Ответ от сервера не соответствует схеме"));

                //Проверка на статус код
                assertsAccumulator.Accumulate(
                () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n" + message));
                assertsAccumulator.Release();

            }
            else
            {
                JObject badContent = ResponseHelperInt.ReturnBadRequestAnswer(response.Content);
                //Проверка на статус код
                assertsAccumulator.Accumulate(
                () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n{badContent.ToString()}"));
                assertsAccumulator.Release();
            }
        }

        [AllureTag("/api/test")]
        [AllureSubSuite("POST")]
        [AllureFeature("")]

        [Category("RestSharp.INT.Account controller")]
        [Test(Description = "Экспортирование УИО в таблицу Excel. Параметры: актуальность УИО, статус код"), TestCaseSource(typeof(TestDataConstructorInt), nameof(TestDataConstructorInt.ExportTestTestData))]
        public void ExportTest(string isActual, int statusCodeExpectedResult)
        {
            //Добавляем токен авторизации в хедер
            AddParamsInt.AddAuthTokenInRequestHeaderAdminIB(RequestHelperInt.pRequestExportToExcell);
            AddParamsInt.AddParametersToExportTest();

            string[] stringArray = new string[] {"InclusionDate", "RegistryTypeNames", "OGRN", "INN", "FullName", "Address", "PersonalAccountStatus"};

            RequestHelperInt.pRequestExportToExcell.AddJsonBody(stringArray);

            string randomName = "ExcellUFR" + RandomValuesInt.RandomString(5)+".xlsx";

            var pathToSave = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            Directory.CreateDirectory("ExportUFRToExcell");
            AuthInt.Client.DownloadData(RequestHelperInt.pRequestExportToExcell).SaveAs(Path.Combine(pathToSave, $"ExportUFRToExcell\\{randomName}"));

            response = AuthInt.Client.Execute(RequestHelperInt.pRequestExportToExcell);

            statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var targetType = typeof(Result<string>);

            var assertsAccumulator = new AssertAccumulatorInt();

            if (response.IsSuccessful)
            {
                //Проверка на статус код
                assertsAccumulator.Accumulate(
                () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK"));
                assertsAccumulator.Release();
            }
            else
            {
                JObject badContent = ResponseHelperInt.ReturnBadRequestAnswer(response.Content);
                //Проверка на статус код
                assertsAccumulator.Accumulate(
                () => Assert.That(numericStatusCode, Is.EqualTo(statusCodeExpectedResult), $"StatusCode: {numericStatusCode} - is not OK\n{badContent.ToString()}"));
                assertsAccumulator.Release();
            }
        }
    }
}
