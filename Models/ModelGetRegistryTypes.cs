using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neolant.PFEI.Dto.FrontInt.Out;

namespace RestSharpTestsInt.Models
{
    class ModelGetRegistryTypes
    {
        public DataRegistryTypes data { get; set; }
        public string message { get; set; }
    }
    public class DataRegistryTypes
    {
        public List<AccountToRegistryTypeDto> Items { get; set; }
        public int Total { get; set; }
    }
}




