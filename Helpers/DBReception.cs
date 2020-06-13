using Neolant.PFEI.DataAccess.EF.Context;
using Neolant.PFEI.Database;
using Neolant.PFEI.Database.Dictionaries;
using Neolant.PFEI.Dto.Base;
using Neolant.PFEI.Infrastructure.Abstract;
using Neolant.PFEI.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsInt.Helpers
{
    class DBReception
    {


        //Получение ReportFormId из таблицы FormPattern
        public static Guid? ReturnReportId()
        {
            using (var context = new DatabaseContext())
            {
                var form = context.Set<FormPattern>()
                .Where(c => c.ReportFormId != null)
                .FirstOrDefault();

                if (form != null)
                {
                    return form.ReportFormId;
                }
                else
                    return Guid.Empty;
            }
        }

        //Берем из таблицы FormPatterns
        //TargetNamespace, Version, RootElementName, PeriodId, Id

        public static Tuple<string, string, string, Guid?, Guid> ReturnFormPatternItems()
        {
            using (var context = new DatabaseContext())
            {
                var form = context.Set<FormPattern>()
                .Where(c => c.Id != null)
                .FirstOrDefault();

                return Tuple.Create(form.TargetNamespace, form.Version, form.RootElementName, form.PeriodId, form.Id);
            }
        }

        //Вытягиваем айди активной формы
        public static Guid ReturnActiveFormPatternId()
        {
            using (var context = new DatabaseContext())
            {
                var form = context.Set<FormPattern>()
                .Where(c => c.Status == PatternStatus.Active & c.RootElementName == "Form_Request")
                .FirstOrDefault();

                if (form != null)
                {
                    return form.Id;
                }
                else
                    return Guid.Empty;
            }
        }

        /// <summary>
        /// Получение айди задачи для получения шаблона отчетности
        /// </summary>
        /// <returns></returns>
        public static string ReturnTaskId()
        {
            string conString = $"Data Source={CustomRequestInt.GetSettingByKey<string>("DataSource")},{CustomRequestInt.GetSettingByKey<string>("Port")};Initial Catalog={CustomRequestInt.GetSettingByKey<string>("InitialCatalog")};Persist Security Info=True;User ID={CustomRequestInt.GetSettingByKey<string>("UserId")};Password={CustomRequestInt.GetSettingByKey<string>("Password")}";
            
            //string conString = "Data Source=localhost;Initial Catalog=LoginScreen;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);

            string selectSql = $"SELECT TOP (1) CatalogTaskId from [{CustomRequestInt.GetSettingByKey<string>("InitialCatalog")}].[dbo].[CatalogTasksToReportForms]";
            SqlCommand com = new SqlCommand(selectSql, con);
            string item = null;

            try
            {
                con.Open();

                using (SqlDataReader read = com.ExecuteReader())
                {
                    while (read.Read())
                    {
                        item = (read["CatalogTaskId"].ToString());

                    }
                }
                return item;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return item;
            }
            finally
            {
                con.Close();
            }

        }

        public static Guid ReturnFormPatternId()
        {
            using (var context = new DatabaseContext())
            {
                try
                {
                    var form = context.Set<FormPattern>()
                    .Where(c => c.Version != null)
                    .FirstOrDefault();
                    return form.Id;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return Guid.Empty;
                }
            }

        }

    }
}
