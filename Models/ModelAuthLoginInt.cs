using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsInt.Models
{
    class ModelAuthLoginInt
    {
        public DataUser data { get; set; }
        public string message { get; set; }
    }

    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public object SecondName { get; set; }
        public object Patronymic { get; set; }
        public List<string> Roles { get; set; }
    }

    public class DataUser
    {
        public User User { get; set; }
        public string Token { get; set; }
    }
}
