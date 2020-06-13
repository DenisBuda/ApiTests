using Neolant.PFEI.Dto.Front.Out;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsInt.Models
{
    class ModelGetNotifications
    {
        public DataNotifications data { get; set; }
        public string message { get; set; }
    }

    public class DataNotifications
    {
        public List<NotificationPackVm> Items { get; set; }
        public int Total { get; set; }
    }
}
