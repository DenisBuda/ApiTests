using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsInt.Helpers
{
    class AuthAdminIbInt
    {
        public static string token;

        static string Host = CustomRequestInt.GetSettingByKey<string>("Host");
        static string LoginAdmin = CustomRequestInt.GetSettingByKey<string>("LoginAdminIB");
        static string PassAdmin = CustomRequestInt.GetSettingByKey<string>("PassAdminIB");

        public static RestClient Client = new RestClient(Host);

        //static string accHost = RefactConfig.GetSettingByKey<string>("accHost");
        //public static RestClient accClient = new RestClient(accHost);

        public static void GetToken()
        {
            RestRequest pRequestLogin = new RestRequest("api/test", Method.POST);

            pRequestLogin.AddJsonBody(new { Login = LoginAdmin, Password = PassAdmin });

            var response = Client.Execute(pRequestLogin);

            JObject output = null;


            try
            {

                output = JObject.Parse(response.Content);
                token = output["data"]["Token"].ToString();
            }
            catch
            {
                token = null;
            }
        }
    }
}
