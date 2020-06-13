using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsInt.Helpers
{
    class RequestHelperInt
    {
        //Auth
        //Post
        public static RestRequest pRequestLogin = new RestRequest("api/test", Method.POST);
        public static RestRequest pRequestLoginAdminIB = new RestRequest("api/test", Method.POST);
        public static RestRequest pRequestLogout = new RestRequest("/api/test", Method.POST);

        //Account
        //Get
        public static RestRequest gRequestGetAvatar = new RestRequest("/api/test", Method.GET);
        public static RestRequest gRequestGetUfr = new RestRequest("/api/test", Method.GET);
        public static RestRequest gRequestGetUfrShort = new RestRequest("/api/test", Method.GET);
        public static RestRequest gRequestGetRegistryRypes = new RestRequest("/api/test", Method.GET);
        public static RestRequest gRequestGetUfrConfiguration = new RestRequest("/api/test", Method.GET);
        public static RestRequest gRequestGetNotifications = new RestRequest("/api/test", Method.GET);
        public static RestRequest gRequestGetCertificates = new RestRequest("/api/test", Method.GET);
        public static RestRequest gRequestGetUserCatalogs = new RestRequest("/api/test", Method.GET);
        public static RestRequest gRequestGetUserCatalogsThisAccount = new RestRequest("/api/test", Method.GET);
        public static RestRequest gRequestGenerateCredentials = new RestRequest("/api/test", Method.GET);
        public static RestRequest gRequestGenerateAccountInfo = new RestRequest("/api/test", Method.GET);
        public static RestRequest gRequestGetAdditionalInfo = new RestRequest("/api/test", Method.GET);
        public static RestRequest gRequestGetAccountCount = new RestRequest("/test", Method.GET);
        //Post
        public static RestRequest pRequestUFRCreate = new RestRequest("/api/test", Method.POST);
        public static RestRequest pRequestUFREdit = new RestRequest("/api/test", Method.POST);
        public static RestRequest pRequestUFRsync = new RestRequest("/api/test", Method.POST);
        public static RestRequest pRequestSendToNeva = new RestRequest("/api/test", Method.POST);
        public static RestRequest pRequestMessagesSync = new RestRequest("/api/test", Method.POST);
        public static RestRequest pRequestSaveUFRConfiguration = new RestRequest("/api/test", Method.POST);
        public static RestRequest pRequestSetAdditionalInfo = new RestRequest("/api/test", Method.POST);
        public static RestRequest pRequestExportStatuses = new RestRequest("/api/test", Method.POST);
        public static RestRequest pRequestApplyAccounts = new RestRequest("/api/test", Method.POST);
        public static RestRequest pRequestSendBlockingReason = new RestRequest("/api/test", Method.POST);
        public static RestRequest pRequestBlockUFR = new RestRequest("/api/test", Method.POST);
        public static RestRequest pRequestUnBlockUFR = new RestRequest("/api/test", Method.POST);
        public static RestRequest pRequestAcceptCredentials = new RestRequest("/api/test", Method.POST);
        public static RestRequest pRequestReject = new RestRequest("/api/test", Method.POST);
        public static RestRequest pRequestGetAll = new RestRequest("/api/test", Method.POST);
        public static RestRequest pRequestGetMessagesUFR = new RestRequest("/api/test", Method.POST);
        public static RestRequest pRequestGetChangesUFR = new RestRequest("/api/test", Method.POST);
        public static RestRequest pRequestGetChangesActionsUFR = new RestRequest("/api/test", Method.POST);
        public static RestRequest pRequestExportToExcell = new RestRequest("/api/test", Method.POST);
        //Delete
        public static RestRequest dRequestDeleteUFR = new RestRequest("/api/test", Method.DELETE);

    }
}
