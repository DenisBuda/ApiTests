using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsInt.Models
{
    class ModelGetItemVersions
    {
        public Data data { get; set; }
        public string message { get; set; }
    }

    public class Data
    {
        public IList<object> Items { get; set; }
        public int Total { get; set; }
    }
}
