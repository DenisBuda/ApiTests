using Neolant.PFEI.Dto.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsInt.Models
{
    class ModelFormPatternGetAllTest
    {
        public DataFormPatternGetAllTest data { get; set; }
        public string message { get; set; }
    }

    public class DataFormPatternGetAllTest
    {
        public IList<BaseCodeDto> Items { get; set; }
        public int Total { get; set; }
    }
}
