using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Danfohq;
using Danfohq.Models;
using Danfohq.DataContexts;

namespace DanfoHqTests
{
    [TestClass]
    class ContextTest
    {
        [TestMethod]
        public void Test_Can_Save_An_Account()
        {
            AccountContext accountContext = new AccountContext();

            long Id = 4L;
            string FirstName = "Grace";
            string PhoneNumber = "09078654326";
            string LastName = "Ojo";

            Account account = new Account(Id, FirstName, LastName, PhoneNumber);
            accountContext.SaveChanges();

            Assert.AreEqual(1, accountContext.Accounts.ToList().Count);
            
        }

        [TestMethod]
        public void Test_can_generate_otp()
        {
            OTPContext oTP = new OTPContext();

            string phoneNumber = "09087654321";
            bool verificationStatus = true;

            OTP otp = new OTP(phoneNumber, verificationStatus);
            oTP.SaveChanges();

            Assert.AreEqual(1, oTP.Otps.ToList().Count);
        }

        [TestMethod]
        public void Test_get_all_Accounts()
        {
            AccountContext accountContext = new AccountContext();

            long Id = 4L;
            string FirstName = "Grace";
            string PhoneNumber = "09078654326";
            string LastName = "Ojo";

            Account account = new Account(Id, FirstName, LastName, PhoneNumber);
            accountContext.SaveChanges();

            List<Account> allAccounts = accountContext.GetAccounts();

            Assert.AreEqual(1, allAccounts.Count());

        }
    }
}
