using Neolant.PFEI.Infrastructure.Enums;
using Neolant.PFEI.Infrastructure.Enums.Front;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTestsInt.Helpers
{
    class TestDataConstructorInt
    {
        #region Auth controller
        //Для AdminTest
        public static IEnumerable<TestCaseData> LoginTestData()
        {
            yield return new TestCaseData(CustomRequestInt.GetSettingByKey<string>("LoginAdmin"), CustomRequestInt.GetSettingByKey<string>("PassAdmin"), 200);
        }

        //Для AdminIB
        public static IEnumerable<TestCaseData> LoginAdminIBTestData()
        {
            yield return new TestCaseData(CustomRequestInt.GetSettingByKey<string>("LoginAdminIB"), CustomRequestInt.GetSettingByKey<string>("PassAdminIB"), 200);
        }

        public static IEnumerable<TestCaseData> LogoutTestData()
        {
            yield return new TestCaseData(200);
        }
        #endregion

        #region Account controller
        public static IEnumerable<TestCaseData> AccountGetAvatarTestData()
        {
            yield return new TestCaseData(CustomRequestInt.GetSettingByKey<string>("AccountId"), 200);
        }
        public static IEnumerable<TestCaseData> AccountGetUfrTestData()
        {
            yield return new TestCaseData(CustomRequestInt.GetSettingByKey<string>("AccountIdGetUfr"), CustomRequestInt.GetSettingByKey<string>("EndedAtGetUfr"), 200);
        }
        public static IEnumerable<TestCaseData> AccountGetUfrShortTestData()
        {
            yield return new TestCaseData(CustomRequestInt.GetSettingByKey<string>("AccountIdGetUfr"), CustomRequestInt.GetSettingByKey<string>("EndedAtGetUfr"), 200);
        }
        public static IEnumerable<TestCaseData> AccountGeRegistryTypesTestData()
        {
            yield return new TestCaseData(CustomRequestInt.GetSettingByKey<string>("AccountIdGetUfr"), CustomRequestInt.GetSettingByKey<string>("EndedAtGetUfr"), 200);
        }
        public static IEnumerable<TestCaseData> AccountGetUfrConfigurationTestData()
        {
            yield return new TestCaseData(CustomRequestInt.GetSettingByKey<string>("AccountIdGetUfr"), 200);
        }
        public static IEnumerable<TestCaseData> AccountGetNotificationsTestData()
        {
            yield return new TestCaseData(CustomRequestInt.GetSettingByKey<string>("AccountIdGetUfr"), CustomRequestInt.GetSettingByKey<int>("page"), CustomRequestInt.GetSettingByKey<int>("pageSize"), 200);
        }
        public static IEnumerable<TestCaseData> AccountGetCertificatesTestData()
        {
            yield return new TestCaseData(CustomRequestInt.GetSettingByKey<string>("AccountIdGetUfr"), CustomRequestInt.GetSettingByKey<int>("page"), CustomRequestInt.GetSettingByKey<int>("pageSize"), 200);
        }
        public static IEnumerable<TestCaseData> AccountGetUserCatalogsTestData()
        {
            yield return new TestCaseData(CustomRequestInt.GetSettingByKey<string>("AccountIdGetUfr"), CustomRequestInt.GetSettingByKey<int>("page"), CustomRequestInt.GetSettingByKey<int>("pageSize"), 200);
        }
        public static IEnumerable<TestCaseData> AccountGetUserCatalogsThisAccountTestData()
        {
            yield return new TestCaseData(CustomRequestInt.GetSettingByKey<string>("AccountIdGetUfr"), CustomRequestInt.GetSettingByKey<string>("AccountCatalogId"), CustomRequestInt.GetSettingByKey<int>("page"), CustomRequestInt.GetSettingByKey<int>("pageSize"), 200);
        }
        public static IEnumerable<TestCaseData> GetGenerateCredentialsTestData()
        {
            yield return new TestCaseData(CustomRequestInt.GetSettingByKey<string>("GenerateCredentialsAccountId"), 200);
        }
        public static IEnumerable<TestCaseData> GetGenerateAccountInfoTestData()
        {
            yield return new TestCaseData(200);
        }
        public static IEnumerable<TestCaseData> GetAddInfoTestData()
        {
            yield return new TestCaseData(CustomRequestInt.GetSettingByKey<string>("GenerateCredentialsAccountId"), 200);
        }
        public static IEnumerable<TestCaseData> GetAccCountTestData()
        {
            Random rnd = new Random();
            //Создаем массив из перечисления
            Array values = Enum.GetValues(typeof(PersonalAccountStatuses));
            //Вытягиваем рандомно элемент 
            PersonalAccountStatuses randomStatus = (PersonalAccountStatuses)values.GetValue(rnd.Next(values.Length));         
            //PersonalAccountStatuses randomStatus = PersonalAccountStatuses.Active;     

            yield return new TestCaseData(randomStatus, 200);
        }
        public static IEnumerable<TestCaseData> UFRCreateTestData()
        {
            //yield return new TestCaseData(CustomRequestInt.GetSettingByKey<string>("FullName"), CustomRequestInt.GetSettingByKey<string>("ShortName"), CustomRequestInt.GetSettingByKey<string>("FirstName"), CustomRequestInt.GetSettingByKey<string>("SecondName"), CustomRequestInt.GetSettingByKey<string>("Patronymic"), CustomRequestInt.GetSettingByKey<string>("OGRN"), CustomRequestInt.GetSettingByKey<string>("INN"), CustomRequestInt.GetSettingByKey<string>("BIC"), CustomRequestInt.GetSettingByKey<string>("RegNum"), CustomRequestInt.GetSettingByKey<string>("IssuedCertificateNumber"), CustomRequestInt.GetSettingByKey<string>("InclusionDate"), CustomRequestInt.GetSettingByKey<string>("CreationDate"), CustomRequestInt.GetSettingByKey<string>("Email"), CustomRequestInt.GetSettingByKey<string>("Address"), CustomRequestInt.GetSettingByKey<string>("Phone"), CustomRequestInt.GetSettingByKey<string>("TerritorialInstituteId"), CustomRequestInt.GetSettingByKey<string>("OrgLegalFormCode"), CustomRequestInt.GetSettingByKey<string>("PersonalAccountStatus"), CustomRequestInt.GetSettingByKey<string>("EnterpriseType"), CustomRequestInt.GetSettingByKey<string>("AccountStatus"), 200);

            yield return new TestCaseData(CustomRequestInt.GetSettingByKey<string>("FullName"), CustomRequestInt.GetSettingByKey<string>("ShortName"),  CustomRequestInt.GetSettingByKey<string>("OGRN"), CustomRequestInt.GetSettingByKey<string>("INN"), CustomRequestInt.GetSettingByKey<string>("BIC"), CustomRequestInt.GetSettingByKey<string>("RegNum"), CustomRequestInt.GetSettingByKey<string>("IssuedCertificateNumber"), CustomRequestInt.GetSettingByKey<string>("InclusionDate"), CustomRequestInt.GetSettingByKey<string>("CreationDate"), CustomRequestInt.GetSettingByKey<string>("Email"), CustomRequestInt.GetSettingByKey<string>("Address"), CustomRequestInt.GetSettingByKey<string>("Phone"), CustomRequestInt.GetSettingByKey<string>("TerritorialInstituteId"), CustomRequestInt.GetSettingByKey<string>("OrgLegalFormCode"), CustomRequestInt.GetSettingByKey<string>("PersonalAccountStatus"), CustomRequestInt.GetSettingByKey<string>("EnterpriseType"), CustomRequestInt.GetSettingByKey<string>("AccountStatus"), 200);


        }
        public static IEnumerable<TestCaseData> UFREditTestData()
        {
            yield return new TestCaseData(CustomRequestInt.GetSettingByKey<string>("idUfrForEdit"), CustomRequestInt.GetSettingByKey<string>("ShortName"), /*CustomRequestInt.GetSettingByKey<string>("FirstName"), CustomRequestInt.GetSettingByKey<string>("SecondName"), CustomRequestInt.GetSettingByKey<string>("Patronymic"),*/ CustomRequestInt.GetSettingByKey<string>("Code"), CustomRequestInt.GetSettingByKey<string>("OGRN"), CustomRequestInt.GetSettingByKey<string>("INN"), CustomRequestInt.GetSettingByKey<string>("BIC"), CustomRequestInt.GetSettingByKey<string>("RegNum"), CustomRequestInt.GetSettingByKey<string>("IssuedCertificateNumber"), CustomRequestInt.GetSettingByKey<string>("InclusionDate"), CustomRequestInt.GetSettingByKey<string>("CreationDate"), CustomRequestInt.GetSettingByKey<string>("Email"), CustomRequestInt.GetSettingByKey<string>("Address"), CustomRequestInt.GetSettingByKey<string>("Phone"), CustomRequestInt.GetSettingByKey<string>("TerritorialInstituteId"), CustomRequestInt.GetSettingByKey<string>("OrgLegalFormCode"), CustomRequestInt.GetSettingByKey<string>("PersonalAccountStatus"), CustomRequestInt.GetSettingByKey<string>("EnterpriseType"), CustomRequestInt.GetSettingByKey<string>("AccountStatus"), 200);
        }
        
        public static IEnumerable<TestCaseData> SyncUFRTestData()
        {
            yield return new TestCaseData(200);
        }
        public static IEnumerable<TestCaseData> SendToNevaActualInfoTestData()
        {
            yield return new TestCaseData(CustomRequestInt.GetSettingByKey<string>("AccountId"), 200);
        }
        public static IEnumerable<TestCaseData> MessagesSyncTestData()
        {
            yield return new TestCaseData(CustomRequestInt.GetSettingByKey<string>("AccountId"), 200);
        }
        public static IEnumerable<TestCaseData> ExportStatusesTestData()
        {
            yield return new TestCaseData(200);
        }
        public static IEnumerable<TestCaseData> AccountsApplyTestData()
        {
            yield return new TestCaseData(CustomRequestInt.GetSettingByKey<string>("AccountIdApply"), 200);
        }
        public static IEnumerable<TestCaseData> DeleteTestTestData()
        {
            yield return new TestCaseData(200);
        }
        public static IEnumerable<TestCaseData> ExportTestTestData()
        {
            yield return new TestCaseData(CustomRequestInt.GetSettingByKey<string>("isactual"), 200);
        }
        public static IEnumerable<TestCaseData> TestSendBlockingReasonTestData()
        {
            yield return new TestCaseData(CustomRequestInt.GetSettingByKey<string>("AccountIdBlock"), 200);
        }
        public static IEnumerable<TestCaseData> BlockTestTestData()
        {
            Random rnd = new Random();
            //Создаем массив из перечисления
            Array values = Enum.GetValues(typeof(BlockingReasonType));
            //Вытягиваем рандомно элемент 
            BlockingReasonType randomType = (BlockingReasonType)values.GetValue(rnd.Next(values.Length));

            yield return new TestCaseData(randomType, CustomRequestInt.GetSettingByKey<string>("AccountIdForBlock"), 200);
        }
        public static IEnumerable<TestCaseData> UnBlockTestTestData()
        {
            yield return new TestCaseData(CustomRequestInt.GetSettingByKey<string>("AccountIdForUnBlock"), 200);
        }
        public static IEnumerable<TestCaseData> AcceptCredentialsTestTestData()
        {
            yield return new TestCaseData(CustomRequestInt.GetSettingByKey<string>("AccountIdForAccept"), 200);
        }
        public static IEnumerable<TestCaseData> RejectTestTestData()
        {
            yield return new TestCaseData(CustomRequestInt.GetSettingByKey<string>("RejectReason"), 200);
        }
        
        public static IEnumerable<TestCaseData> GetAllTestTestData()
        {
            yield return new TestCaseData(200);
        }
        public static IEnumerable<TestCaseData> GetMessagesTestTestData()
        {
            MessageVmType type = MessageVmType.All;

            yield return new TestCaseData(type, CustomRequestInt.GetSettingByKey<string>("AccountId"), CustomRequestInt.GetSettingByKey<int>("pageForGetMessages"), CustomRequestInt.GetSettingByKey<int>("size"), 200);
        }
        public static IEnumerable<TestCaseData> GetChangesTestTestData()
        {
            yield return new TestCaseData(CustomRequestInt.GetSettingByKey<string>("AccountId"), CustomRequestInt.GetSettingByKey<int>("pageForGetChangesUFR"), CustomRequestInt.GetSettingByKey<int>("size"), 200);
        }
        public static IEnumerable<TestCaseData> GetActionsTestTestData()
        {
            yield return new TestCaseData(CustomRequestInt.GetSettingByKey<string>("AccountId"), CustomRequestInt.GetSettingByKey<int>("pageForGetActionsUFR"), CustomRequestInt.GetSettingByKey<int>("size"), 200);
        }
        

        public static IEnumerable<TestCaseData> SaveUFRConfigurationTestData()
        {
            yield return new TestCaseData(CustomRequestInt.GetSettingByKey<string>("SaveConfigSettingsAccountId"), CustomRequestInt.GetSettingByKey<bool>("RufrSync"), CustomRequestInt.GetSettingByKey<long>("PersonalPacketSize"), CustomRequestInt.GetSettingByKey<long>("PersonalQuotaSize"), 200);
        }
        public static IEnumerable<TestCaseData> SetUFRAdditionalInfoTestData()
        {
            yield return new TestCaseData(CustomRequestInt.GetSettingByKey<string>("SetAdditionalInfoAccountId"), CustomRequestInt.GetSettingByKey<string>("AdditionalInfoRegistryTypeToAccountId"), CustomRequestInt.GetSettingByKey<string>("AdditionalInfoAdditionalFieldsBlockId"), CustomRequestInt.GetSettingByKey<string>("IdDateOfDecide"), CustomRequestInt.GetSettingByKey<string>("idDateEnd"), 200);

            
        }

        #endregion

    }
}
