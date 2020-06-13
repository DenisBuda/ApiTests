using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsInt.Helpers
{
    class ResponseHelperInt
    {
        /// <summary>
        /// Метод возвращает текст из message. Если текст пустой - возвращается сообщение "Текст сообщения пустой"
        /// </summary>
        /// <param name="jsonFromResponse"></param>
        /// <returns></returns>
        public static string ReturnMessage(JObject jsonFromResponse)
        {
            string messageFromParsingResponseJson = jsonFromResponse["message"].Value<string>();
            string message = messageFromParsingResponseJson != string.Empty ? messageFromParsingResponseJson : "Текст сообщения пустой";
            return message;
        }

        public static string ReturnData(JObject jsonFromResponse)
        {
            string dataFromParsingResponseJson = jsonFromResponse["data"].Value<string>();
            string data = dataFromParsingResponseJson != string.Empty ? dataFromParsingResponseJson : "Значение параметра data пустой";
            return data;
        }

        public static JObject ReturnBadRequestAnswer(string content)
        {
            dynamic data = JObject.Parse(content);
            return data;
        }
    }
}
