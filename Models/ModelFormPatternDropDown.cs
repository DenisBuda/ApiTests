using Neolant.PFEI.Dto.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsInt.Models
{
    class ModelFormPatternDropDown
    {
        public DataModelFormPatternDropDown data { get; set; }
        public string message { get; set; }
    }

    public class DataModelFormPatternDropDown
    {
        public IList<BaseNameDto> Items { get; set; }
        public int Total { get; set; }
    }


}
