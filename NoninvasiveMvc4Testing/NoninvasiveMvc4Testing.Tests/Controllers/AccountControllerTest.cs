using System;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Security.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoninvasiveMvc4Testing.Controllers;
using NoninvasiveMvc4Testing.Models;

namespace NoninvasiveMvc4Testing.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void TestLogOff()
        {
            var accountController = new AccountController();
            RedirectToRouteResult redirectToRouteResult;

            //Scope the detours we're creating
            using (ShimsContext.Create())
            {
                //Detours FormsAuthentication.SignOut() to an empty implementation
                ShimFormsAuthentication.SignOut = () => { };
                redirectToRouteResult = accountController.LogOff() as RedirectToRouteResult;
            }

            Assert.IsNotNull(redirectToRouteResult);
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
            Assert.AreEqual("Home", redirectToRouteResult.RouteValues["controller"]);
        }

        [TestMethod]
        public void TestJsonLogin()
        {
            const string testUserName = "TestUserName";
            const string testPassword = "TestPassword";
            const bool testRememberMe = false;
            const string testUrl = "TestUrl";

            var loginModel = new LoginModel 
            { 
                Password = testPassword, 
                RememberMe = testRememberMe,
                UserName = testUserName
            };

            var controller = new AccountController();
            JsonResult jsonResult;

            using (ShimsContext.Create())
            {
                ShimMembership.ValidateUserStringString = (userName, password) =>
                {
                    Assert.AreEqual(testUserName, userName);
                    Assert.AreEqual(testPassword, password);
                    return true;
                };

                //Sets up a detour for FormsAuthentication.SetAuthCookie to our mocked implementation
                ShimFormsAuthentication.SetAuthCookieStringBoolean = (userName, rememberMe) =>
                {
                    Assert.AreEqual(testUserName, userName);
                    Assert.AreEqual(testRememberMe, rememberMe);
                };

                jsonResult = controller.JsonLogin(loginModel, testUrl);
                dynamic data = jsonResult.Data;
                Assert.AreEqual(true, data.success);
                Assert.AreEqual(testUrl, data.redirect);
            }
        }
    }
}
