using Neolant.PFEI.Dto.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsInt.Models
{
    class ModelGetChangesHistory
    {
        public DataGetChangesHistory data { get; set; }
        public string message { get; set; }
    }
    public class DataGetChangesHistory
    {
        public IList<AccountChangesHistoryDto> Items { get; set; }
        public int Total { get; set; }
    }
}
