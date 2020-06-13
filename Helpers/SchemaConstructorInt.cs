using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharpTestsInt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsInt.Helpers
{
    class SchemaConstructorInt
    {
        private static JsonSchemaGenerator generator = new JsonSchemaGenerator();

        public static bool ValidationBySchema(JObject content,
        Type targetType)
        {

            generator.UndefinedSchemaIdHandling = UndefinedSchemaIdHandling.UseTypeName;
            var schema = generator.Generate(targetType);
            schema.AllowAdditionalProperties = false;
            // сравниваем JSON объект полученный из JSON строки присланной сервером, относительно JSON схемы целевого типа (модели из кода)
            if (content == null)
                return false;
            else
                return content.IsValid(schema);
        }

        //Схема для пользователя
        public static JsonSchema SchemaGeneratorForLogin()
        {
            JsonSchema schema = generator.Generate(typeof(ModelAuthLoginInt));
            return schema;
        }
        //Разлогинизация
        public static JsonSchema SchemaGeneratorForLogout()
        {
            JsonSchema schema = generator.Generate(typeof(ModelAuthLogoutInt));
            return schema;
        }
    }
}
