using Neolant.PFEI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsInt.Models
{
    class ModelGetActionsChanges
    {
        public DataGetActionsChanges data { get; set; }
        public string message { get; set; }
    }

    public class DataGetActionsChanges
    {
        public IList<JournalRecord> Items { get; set; }
        public int Total { get; set; }
    }

}
