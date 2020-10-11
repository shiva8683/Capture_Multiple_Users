using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Users_Service_Test.Users;

namespace Users_Service_Test
{
    [TestClass]
    public class Add_Edit_Users_Test
    {
        /// <summary>
        /// Adding user(s) in XML file(in WCF service project) and returing "true" as a success response
        /// </summary>
        [TestMethod]
        public void Test_AddUser_SuccessResponse()
        {
            var userServiceClient = new UserServiceClient();
            var userServiceClientResponse = userServiceClient.AddUsers(new List<UserDTO>
            {
                new UserDTO
                {
                    FirstName = "TEST54",
                    LastName = "TEST54",
                    Cellphone = "1234567890"
                },
                 new UserDTO
                {
                    FirstName = "TEST23",
                    LastName = "TEST23",
                    Cellphone = "0545478545",
                }
            }.ToArray());
            Assert.AreEqual(userServiceClientResponse, true);
        }

        /// <summary>
        /// While adding user wantedly missing cellphone number so service will throw "FaultException" as cellphone cannot be empty
        /// </summary>
        [TestMethod]
        public void Test_AddUser_With_Exception()
        {
            var userServiceClient = new UserServiceClient();
            Assert.ThrowsException<FaultException>(() => userServiceClient.AddUsers(new List<UserDTO>
            {
                new UserDTO
                {
                    FirstName = "TEST_EXCEPTION",
                    LastName = "TEST_EXCEPTION",
                    Cellphone = string.Empty,
                }
            }.ToArray()));
        }

        /// <summary>
        /// Getting list of users from the service(WCF)
        /// </summary>
        [TestMethod]
        public void Test_GetUsersList_SuccessResponse()
        {
            var userServiceClient = new UserServiceClient();
            var userServiceClientResponse = userServiceClient.GetUsers().ToList();
            Assert.AreEqual(userServiceClientResponse.Any(), true);
        }

        /// <summary>
        /// Getting first user from the service and editing the user "FirstName" and "LastName" and resturing "true" as a success response
        /// </summary>
        [TestMethod]
        public void Test_EditUser_SuccessResponse()
        {
            var userServiceClient = new UserServiceClient();
            var userInfo = userServiceClient.GetUsers().ToList().FirstOrDefault();

            var userServiceClientResponse = userServiceClient.UpdateUser(new UserDTO
            {
                Cellphone = "1234567890",
                FirstName = "TEST_EDIT",
                UserId = userInfo.UserId,
                LastName = "TEST_EDIT",
            });
            Assert.AreEqual(userServiceClientResponse, true);
        }

        /// <summary>
        /// Getting first user from the service and passing "FirstName" as empty string so service will throw "FaultException" as Firstname cannot be empty
        /// </summary>
        [TestMethod]
        public void Test_EditUser_With_Exception()
        {
            var userServiceClient = new UserServiceClient();
            var userInfo = userServiceClient.GetUsers().ToList().FirstOrDefault();

            Assert.ThrowsException<FaultException>(() => userServiceClient.UpdateUser(new UserDTO
            {
                Cellphone = "1234567890",
                FirstName = string.Empty,
                UserId = userInfo.UserId,
                LastName = "TEST_EDIT",
            }));
        }
    }
}
