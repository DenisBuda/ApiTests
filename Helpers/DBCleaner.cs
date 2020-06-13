using Neolant.PFEI.DataAccess.EF.Context;
using Neolant.PFEI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsInt.Helpers
{
    class DBCleaner
    {
        /// <summary>
        /// Удаляем созданный УИО из табл AccountConfigurations
        /// </summary>
        /// <param name="accountId"></param>
        public static void RemoveCreatedUFRFromAccountConfigurationsTable(Guid accountId)
        {
            using (var db = new DatabaseContext())
            {
                var results = db.AccountsConfigurations
                .Where(b => b.Id == accountId)
                .FirstOrDefault();
                db.AccountsConfigurations.Remove(results);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// Удаляем созданный УИО из табл IdentityCredentials
        /// </summary>
        /// <param name="accountId"></param>
        public static void RemoveCreatedUFRFromIdentityCredentialsTable(Guid accountId)
        {
            using (var db = new DatabaseContext())
            {
                var results = db.IdentitiesCredentials
                .Where(b => b.Id == accountId)
                .FirstOrDefault();
                db.IdentitiesCredentials.Remove(results);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Удаляем созданный УИО из табл Accounts
        /// </summary>
        /// <param name="accountId"></param>
        public static void RemoveCreatedUFFromAccountTable(Guid accountId)
        {
            using (var db = new DatabaseContext())
            {
                var results = db.Accounts
                .Where(b => b.Id == accountId)
                .FirstOrDefault();
                db.Accounts.Remove(results);
                db.SaveChanges();
            }
        }

        //Находим УИО по Inn в табл. Accounts
        public static Guid ReturnUfrId(string ufrInn)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    var results = db.Accounts
                    .Where(b => b.INN == ufrInn)
                    .FirstOrDefault();
                    return results.Id;
                }
            }
            catch
            {
                Console.WriteLine("УИО не найден");
                return Guid.Empty;
            }
        }
        //Удаляем FormPattern
        public static void RemoveFormPattern(Guid idFormPattern)
        {
            using (var context = new DatabaseContext())
            {
                var form = context.Set<FormPattern>()
                .Where(c => c.Id == idFormPattern)
                .FirstOrDefault();

                try
                {
                    context.Set<FormPattern>().Remove(form);
                    context.SaveChanges();
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Не удалось удалить FormPattern", ex.Message);
                }

            }
        }


    }
}
