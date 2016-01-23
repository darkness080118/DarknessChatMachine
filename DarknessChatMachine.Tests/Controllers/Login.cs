using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebQQ;

namespace DarknessChatMachine.Tests.Controllers
{
    [TestClass]
    public class Login
    {
        [TestMethod]
        public void GetConfirmPic()
        {
            try
            {
                Client client = new Client();
                client.getQRCode();
            }
            catch (Exception e)
            {
                Assert.Fail();
            }

            
        }
    }
}
