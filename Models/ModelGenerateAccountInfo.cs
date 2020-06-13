using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsInt.Models
{
    class ModelGenerateAccountInfo
    {
        public DataGenerateAccountInfo data { get; set; }
        public string message { get; set; }
    }

    public class DataGenerateAccountInfo
    {
        public string INN { get; set; }
        public string OGRN { get; set; }
        public string ActivationCode { get; set; }
    }
}
