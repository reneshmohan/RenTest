using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace ClientTest
{
    public class ApiTests
    {
        HelperMethods helper;

        private string _token;

        [SetUp]
        public void Setup()
        {
            // Runs before every [Test] method
            helper = new HelperMethods();
            _token = helper.GetLoginDetails();
            Assert.IsNotNull(_token, "Access token should not be null");
        }

        [Test]
        public void TestLogin()
        {
            // Just verify login worked
            Assert.That(_token, Is.Not.Empty);
        }

        [Test]
        public void TestReset()
        {
            // Use token obtained in [SetUp]
            helper.ResetFirst(_token);
            Assert.Pass("Reset endpoint called successfully");
        }  

    }
}