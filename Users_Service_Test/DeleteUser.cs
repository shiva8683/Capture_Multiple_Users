using System.Linq;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Users_Service_Test.Users;

namespace Users_Service_Test
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class DeleteUser
    {
        /// <summary>
        /// Deleting the firstuser in the XML file and returing the true as success response
        /// </summary>
        [TestMethod]
        public void Test_DeleteUser_SuccessResponse()
        {
            var userServiceClient = new UserServiceClient();
            var userInfo = userServiceClient.GetUsers().ToList().FirstOrDefault();
            var deleteUserResponse = userServiceClient.DeleteUser(userInfo.UserId);
            Assert.AreEqual(deleteUserResponse, true);
        }

        /// <summary>
        /// Deleting the non existing user resulting "FaultException"
        /// </summary>
        [TestMethod]
        public void Test_DeleteUser_With_Exception()
        {
            var userServiceClient = new UserServiceClient();
            Assert.ThrowsException<FaultException>(() => userServiceClient.DeleteUser(0));
        }
    }
}
