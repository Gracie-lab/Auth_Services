using Danfohq.Models;
using Danfohq.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanfoHqTests
{
    [TestClass]
    class AuthorizationTest
    {
        [TestMethod]
        public void Test_can_generate_token()
        {
            AccountService accountService = new AccountService();
            Account account = new Account(4L, "Joshua", "Ola", "09087654321");

           string token = accountService.generateJwtToken(account);

            Assert.IsNotNull(token);
        }
       
    }
}
