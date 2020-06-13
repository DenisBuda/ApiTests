using Neolant.PFEI.Dto.Certificates;
using Neolant.PFEI.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsInt.Models
{
    class ModelGetCertificate
    {
        public DataCertificate data { get; set; }
        public string message { get; set; }
    }

    public class DataCertificate
    {
        public List<CertificateAccountDto> Items { get; set; }
        public int Total { get; set; }
    }

}
