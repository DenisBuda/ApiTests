using Neolant.PFEI.Dto.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsInt.Models
{
    class ModelGetPatternsByTask
    {
        public IList<BaseCodeDto> data { get; set; }
        public string message { get; set; }
    }
}
