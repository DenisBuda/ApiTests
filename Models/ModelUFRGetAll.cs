using Neolant.PFEI.Dto.FrontInt.Out;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsInt.Models
{
    class ModelUFRGetAll
    {
        public DataGetAllUfr data { get; set; }
        public string message { get; set; }
    }

    public class DataGetAllUfr
    {
        public IList<AccountDto> Items { get; set; }
        public int Total { get; set; }
    }
}
