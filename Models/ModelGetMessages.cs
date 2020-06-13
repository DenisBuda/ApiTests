using Neolant.PFEI.Dto.Front.Out;
using Neolant.PFEI.Dto.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsInt.Models
{
    class ModelGetMessages
    {
        public DataGetMessages data { get; set; }
        public string message { get; set; }
    }

    public class DataGetMessages
    {
        public IList<MessagePackVm> Items { get; set; }
        public int Total { get; set; }
    }
}
