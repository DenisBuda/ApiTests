using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsInt.Helpers
{
    class CustomRequestInt
    {
        public static T GetSettingByKey<T>(string key)
        {
            var jsonFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "DataFileInt.json");

            if (!File.Exists(jsonFile))
                throw new FileNotFoundException("Не найден файл с данными!");

            //парсим файл
            string dataString = File.ReadAllText(jsonFile);

            var Json = JObject.Parse(dataString);

            var valueJson = Json[key];

            var value = Json[key].Value<T>();
            return value;
        }

        public static void ReturnRequest(RestRequest request, string value1 = null, string value2 = null, string value3 = null)
        {
            if (value1 != null & value2 == null)
            {
                request.Resource = request.Resource.Replace("{accountId}", value1);
            }

            if (value1 != null & value2 != null)
            {
                request.Resource = request.Resource.Replace("{accountId}", value1);
                request.Resource = request.Resource.Replace("{catalogId}", value2);
            }

            if (value3 != null & value1 == null & value2 == null)
            {
                request.Resource = request.Resource.Replace("{fileId}", value3);
            }
        }

        public static void ReturnModifiedRequest(RestRequest request, string oldValue, string newValue)
        {
            if (oldValue != null & newValue != null)
            {
                request.Resource = request.Resource.Replace($"{oldValue}", newValue);
            }
        }
    }
}
