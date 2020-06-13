using Neolant.PFEI.DataAccess.EF.Context;
using Neolant.PFEI.Database;
using Neolant.PFEI.Database.Accounts;
using Neolant.PFEI.Database.Files;
using Neolant.PFEI.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsInt.Helpers
{
    class DBInserter
    {
        public static void ChangePersonalAccountStatus(Guid accountId, PersonalAccountStatuses status)
        {
            using (var context = new DatabaseContext())
            {
                var account = context.Accounts
                .Where(c => c.Id == accountId)
                .FirstOrDefault();

                if (account != null)
                {
                    account.PersonalAccountStatus = status;
                    context.SaveChanges();
                }
            }
        }

        public static Guid CreateAccount()
        {
            Account acc = new Account();
            Guid idCreatedAcc = Guid.NewGuid();

            acc.Id = idCreatedAcc;
            acc.AccountStatus = AccountStatuses.Active;
            acc.EnterpriseType = AccountEnterpriseType.LegalPerson;
            acc.PersonalAccountStatus = PersonalAccountStatuses.PersonalAccountCreated;
            acc.CreatedAt = DateTime.Now;
            acc.EndedAt = DateTime.Now.AddDays(15);
            acc.FullName = "CreatedForDelete";

            try
            {
                using (var context = new DatabaseContext())
                {
                    context.Accounts.Add(acc);
                    context.SaveChanges();
                    return idCreatedAcc;
                }
            }
            catch
            {
                Console.WriteLine($"Не удалось сохранить аккаунт в БД, ID {idCreatedAcc}");
                return idCreatedAcc = Guid.Empty;
            }

        }
        /// <summary>
        /// Создаем аккаунт со всеми заполненными полями
        /// </summary>
        /// <returns></returns>
        public static Guid CreateAccountWithFullStrings()
        {
            Account acc = new Account();
            Guid idCreatedAcc = Guid.NewGuid();

            acc.Id = idCreatedAcc;
            acc.INN = RandomValuesInt.RandomInt(10);
            acc.BIC = RandomValuesInt.RandomInt(11);
            acc.OGRN = RandomValuesInt.RandomInt(13);
            acc.FullName = "ООО ТЕСТОВЫЙ АК" + DateTime.Now;
            acc.Address = "Тестовый";
            acc.AccountStatus = AccountStatuses.Active;
            acc.EnterpriseType = AccountEnterpriseType.LegalPerson;
            acc.PersonalAccountStatus = PersonalAccountStatuses.PersonalAccountCreated;
            acc.Code = "0000";
            acc.InclusionDate = DateTime.Now;
            acc.RegNum = RandomValuesInt.RandomInt(6);
            acc.ActivationDate = DateTime.Now;
            acc.CreationDate = DateTime.Now;
            acc.CreatedAt = DateTime.Now;
            acc.EndedAt = DateTime.Now.AddDays(15);


            try
            {
                using (var context = new DatabaseContext())
                {
                    context.Accounts.Add(acc);
                    context.SaveChanges();
                    return idCreatedAcc;
                }
            }
            catch
            {
                Console.WriteLine($"Не удалось сохранить аккаунт в БД, ID {idCreatedAcc}");
                return idCreatedAcc = Guid.Empty;
            }
        }

        /// <summary>
        /// Создаем запись в таблице AccountConfiguration
        /// </summary>
        /// <returns></returns>
        public static void CreateNoteInAccountConfiguration(Guid accId)
        {
            AccountConfiguration accConf = new AccountConfiguration();

            accConf.Id = accId;
            accConf.LastTspTestDate = DateTime.Now;
            accConf.TspTested = false;
            accConf.TspService = "test";
            accConf.RufrSynchronization = true;
            accConf.UsedDiscSpace = 10234494;
            accConf.PersonalAccountPacketSize = 9999999;
            accConf.TotalPacketSize = 2147483648;
            accConf.PersonalAccountQuota = 9999999;
            accConf.TotalQuota = 5368709120;
            accConf.CryptographyType = CryptographyTypes.Russian;

            try
            {
                using (var context = new DatabaseContext())
                {
                    context.Set<AccountConfiguration>().Add(accConf);
                    context.SaveChanges();
                }
            }
            catch
            {
                Console.WriteLine($"Не удалось сохранить аккаунт в БД");
            }
        }


        //Меняем статус на первоначальный
        public static void ChangeFormPatternStatus(Guid formPatternId, PatternStatus status)
        {
            using (var context = new DatabaseContext())
            {
                var form = context.Set<FormPattern>()
                .Where(c => c.Id == formPatternId)
                .FirstOrDefault();

                if (form != null)
                {
                    form.Status = status;
                    context.SaveChanges();
                }
            }
        }
        //Изменяем версию FormPattern
        public static void ChangeFormPatternVersion(Guid formPatternId, string version)
        {
            using (var context = new DatabaseContext())
            {
                var form = context.Set<FormPattern>()
                .Where(c => c.Id == formPatternId)
                .FirstOrDefault();

                if (form != null)
                {
                    form.Version = version;
                    context.SaveChanges();
                }
            }
        }

        //Создаем FormPattern
        public static Guid CreateFormPattern()
        {
            FormPattern form = new FormPattern();

            form.Id = Guid.NewGuid();
            form.Name = "CreatedByAutotest" + DateTime.Now;
            form.IsDefaultVersion = false;
            form.CreatedAt = DateTime.Now;
            form.Status = PatternStatus.Draft;
            form.DurationFrom = DateTime.Now;
            form.CanSkipValidation = false;
            form.SkipEventValidation = false;

            try
            {
                using (var context = new DatabaseContext())
                {
                    context.Set<FormPattern>().Add(form);
                    context.SaveChanges();
                    return form.Id;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не удалось создать форму {form.Id}");
                return Guid.Empty;
            }

        }

        //БейзФайл без всяких там
        public static Guid CreateBaseFile(string contentType, byte[] content, string name)
        {
            string connectionString = $"Data Source={CustomRequestInt.GetSettingByKey<string>("DataSource")},{CustomRequestInt.GetSettingByKey<string>("Port")};Initial Catalog={CustomRequestInt.GetSettingByKey<string>("InitialCatalog")};Persist Security Info=True;User ID={CustomRequestInt.GetSettingByKey<string>("UserId")};Password={CustomRequestInt.GetSettingByKey<string>("Password")}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                //string Name = "main.xsd";
                Guid id = Guid.NewGuid();
                DateTime createDate = DateTime.Now;

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@content", content);
                command.Parameters.AddWithValue("@contentType", contentType);
                command.Parameters.AddWithValue("@createDate", createDate);

                command.CommandText = $"INSERT INTO [BaseFiles] (Id, CreateDate, Name, ContentType, Content) VALUES(@id, @createDate, @name, @contentType, @content)";

                try
                {
                    connection.Open();

                    command.ExecuteNonQuery();

                    connection.Close();
                    return id;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Не удалось обновить данные в БД", ex.InnerException.Message);
                    return Guid.Empty;
                }
            }
        }

    }
}
