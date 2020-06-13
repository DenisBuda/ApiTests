using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsInt.Helpers
{
    class ArrayJsonKeys
    {
        private static JObject Json;
        public static IDictionary<string, string> registryTypes { get; set; }
        public static object additionalInfoFileDTOs { get; set; }


        public static void ReturnJsonKeys()
        {
            var jsonFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "DataFileInt.json");

            if (!File.Exists(jsonFile))
                throw new FileNotFoundException("Не найден файл с данными!");

            //парсим файл
            string dataString = File.ReadAllText(jsonFile);

            Json = JObject.Parse(dataString);

            try
            {
                registryTypes = Json["RegistryTypes"].ToObject<IDictionary<string, string>>();
                //additionalInfoFileDTOs = Json["AdditionalInfoFieldDtos"].
            }
            catch
            {
                Console.WriteLine("Не удалось достать данные из джейсона");
            }
        }
    }
}
