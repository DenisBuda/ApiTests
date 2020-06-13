using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsInt.Helpers
{
    class PluginHelperInt
    {
        public string ReturnDirectory()
        {
            Environment.CurrentDirectory = Path.GetDirectoryName(GetType().Assembly.Location);
            //Directory.CreateDirectory("allure-results");
            return Environment.CurrentDirectory;
        }
    }
}
