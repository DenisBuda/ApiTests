using Neolant.PFEI.Infrastructure.Enums;
using Neolant.PFEI.Infrastructure.Enums.Front;
using RestSharp;
using RestSharpTestsInt.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsInt.Helpers
{
    class AddParamsInt
    {
        public static void AddParametersToAccountGetUfr(bool value)
        {
            RequestHelperInt.gRequestGetUfr
                .AddParameter("short", value, ParameterType.QueryString);
        }

        public static void AddParametersToAccountGetUfrShort(bool value)
        {
            RequestHelperInt.gRequestGetUfrShort
                .AddParameter("short", value, ParameterType.QueryString);
        }
        public static void AddParametersToGetLastVersionTest(Guid reportFormId)
        {
            RequestHelperInt.gRequestGetLastVersion
                .AddParameter("reportFormId", reportFormId, ParameterType.QueryString);
        }
        public static void AddParametersToGetNewVersionTest(FormType type)
        {
            RequestHelperInt.gRequestGetNewVersion
                .AddParameter("type", type, ParameterType.QueryString);
        }
        public static void AddParametersToGetAllTest(FormPatternTypeVm type)
        {
            RequestHelperInt.pRequestGetAllFormPattern
                .AddParameter("type", type, ParameterType.QueryString);
        }
        public static void AddParametersToGenerateFormTest(bool removeFiles)
        {
            RequestHelperInt.pRequestGenerateForm
                .AddParameter("removeFiles", removeFiles, ParameterType.QueryString);
        }
        
        public static void AddParametersToSetStatusItemTest(PatternStatus status)
        {
            RequestHelperInt.gRequestSetStatusItem
                .AddParameter("status", status, ParameterType.QueryString);
        }
        
        public static void AddParametersToGetItemVersionsTest(FormPatternTypeVm type, string name, Guid periodId)
        {
            RequestHelperInt.gRequestGetItemVersions
                .AddParameter("type", type, ParameterType.QueryString)
                .AddParameter("name", name, ParameterType.QueryString)
                .AddParameter("periodId", periodId, ParameterType.QueryString);
        }
        public static void AddParametersToGetReportPatternPeriodsTest(Guid? reportId)
        {
            RequestHelperInt.gRequestGetReportPatternPeriods
                .AddParameter("reportId", reportId, ParameterType.QueryString);
        }
        

        public static void AddParametersToAccountGetNotifications(int page, int pageSize)
        {
            RequestHelperInt.gRequestGetNotifications
                .AddParameter("page", page, ParameterType.QueryString)
                .AddParameter("pageSize", pageSize, ParameterType.QueryString);
        }

        public static void AddParametersToAccountGetCertificates(int page, int pageSize)
        {
            RequestHelperInt.gRequestGetCertificates
                .AddParameter("page", page, ParameterType.QueryString)
                .AddParameter("pageSize", pageSize, ParameterType.QueryString);
        }

        public static void AddParametersToAccountGetUserCatalogs(int page, int pageSize)
        {
            RequestHelperInt.gRequestGetUserCatalogs
                .AddParameter("page", page, ParameterType.QueryString)
                .AddParameter("pageSize", pageSize, ParameterType.QueryString);
        }

        public static void AddParametersToAccountGetUserCatalogsThisAccount(int page, int pageSize)
        {
            RequestHelperInt.gRequestGetUserCatalogsThisAccount
                .AddParameter("page", page, ParameterType.QueryString)
                .AddParameter("pageSize", pageSize, ParameterType.QueryString);
        }
        public static void AddParametersToGetAccCount(PersonalAccountStatuses randomStatus)
        {
            RequestHelperInt.gRequestGetAccountCount
                .AddParameter("status", randomStatus, ParameterType.QueryString);
        }

        public static void AddParametersToBlockTest(BlockingReasonType randomType)
        {
            RequestHelperInt.pRequestBlockUFR
                .AddParameter("reason", randomType, ParameterType.QueryString);
        }
        public static void AddParametersToUnBlockTest(Guid accountId)
        {
            RequestHelperInt.pRequestUnBlockUFR
                .AddParameter("id", accountId, ParameterType.QueryString);
        }
        public static void AddParametersToGetMessageGroupsTest(MessageVmType type, int page, int size)
        {
            RequestHelperInt.pRequestGetMessagesUFR
                .AddParameter("type", type, ParameterType.QueryString)
                .AddParameter("page", page, ParameterType.QueryString)
                .AddParameter("size", size, ParameterType.QueryString);
        }
        public static void AddParametersToGetChangesTest(int page, int size)
        {
            RequestHelperInt.pRequestGetChangesUFR
                .AddParameter("page", page, ParameterType.QueryString)
                .AddParameter("size", size, ParameterType.QueryString)
                .AddParameter("eventName", null, ParameterType.QueryString);
        }
        public static void AddParametersToGetActionsTest(int page, int size)
        {
            RequestHelperInt.pRequestGetChangesActionsUFR
                .AddParameter("page", page, ParameterType.QueryString)
                .AddParameter("size", size, ParameterType.QueryString);
        }
        public static void AddParametersToExportTest(Guid? registryTypeId = null,
            bool areActual = true)
        {
            RequestHelperInt.pRequestExportToExcell
                .AddParameter("registrytype", registryTypeId, ParameterType.QueryString)
                .AddParameter("isactual", areActual, ParameterType.QueryString);
        }
        public static void AddParametersToActualizeTest(string value)
        {
            RequestHelperInt.pRequestActualize
                .AddParameter("fileName", value, ParameterType.QueryString);
        }

        //Добавление токена авторизации в хедер 
        public static void AddAuthTokenInRequestHeader(RestRequest request)
        {
            request.AddParameter(nameof(HttpRequestHeader.Authorization), AuthInt.token, ParameterType.HttpHeader);
        }

        //Добавление токена авторизации под Админом ИБ в хедер 
        public static void AddAuthTokenInRequestHeaderAdminIB(RestRequest request)
        {
            request.AddParameter(nameof(HttpRequestHeader.Authorization), AuthAdminIbInt.token, ParameterType.HttpHeader);
        }
    }
}
