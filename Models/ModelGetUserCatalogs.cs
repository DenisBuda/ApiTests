using Neolant.PFEI.Dto.FrontInt.Out;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsInt.Models
{
    class ModelGetUserCatalogs
    {
        public DataUserCatalogs data { get; set; }
        public string message { get; set; }
    }


    public class DataUserCatalogs
    {
        public List<CatalogLinkDto> Items { get; set; }
        public int Total { get; set; }
    }
}
