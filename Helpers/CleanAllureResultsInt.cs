using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsInt.Helpers
{
    class CleanAllureResultsInt
    {
        /// <summary>
        /// Флаг принимающий булевское значение, если false чистим директорию, в противном случае нет
        /// </summary>
        public static bool alreadyClear = false;

        public static void ClearFolder(bool flag, string directory)
        {

            if (!flag)
            {
                try
                {
                    //Чистим папку с отчётами
                    foreach (FileInfo file in new DirectoryInfo(Path.Combine(directory, "allure-results")).GetFiles())
                    {
                        file.Delete();
                    }
                    alreadyClear = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }
    }
}
