using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Test_1_Capture_Multiple_Users.Models;
using Test_1_Capture_Multiple_Users.Users;

namespace Test_1_Capture_Multiple_Users.Helpers
{
    //BINDING ALL SERVICE CALLS IN A STANDARD FORMAT BEFORE EXPOSING TO UI
    public static class ServiceWrapperHelper
    {
        public static ServiceWrapper AddUsers(List<UserDTO> lstUserDTO)
        {
            try
            {
                var userServiceClient = new UserServiceClient();
                var usersInserted = userServiceClient.AddUsers(lstUserDTO.ToArray());
                return WarpServiceCall(usersInserted, false, string.Empty);
            }
            catch (Exception ex)
            {
                return WarpServiceCall(string.Empty, true, ex.Message);
            }
        }

        public static ServiceWrapper GetUserById(int userId)
        {
            try
            {
                var userServiceClient = new UserServiceClient();
                var getUserByIdResponse = userServiceClient.GetUserById(userId);
                return WarpServiceCall(JsonConvert.SerializeObject(getUserByIdResponse), false, string.Empty);
            }
            catch (Exception ex)
            {
                return WarpServiceCall(string.Empty, true, ex.Message);
            }
        }

        public static ServiceWrapper UpdateUser(UserDTO userDTO)
        {
            try
            {
                var userServiceClient = new UserServiceClient();
                var updateUserResponse = userServiceClient.UpdateUser(userDTO);
                return WarpServiceCall(updateUserResponse, false, string.Empty);
            }
            catch (Exception ex)
            {
                return WarpServiceCall(string.Empty, true, ex.Message);
            }
        }

        public static ServiceWrapper DeleteUser(int userId)
        {
            try
            {
                var userServiceClient = new UserServiceClient();
                bool deleteUserResponse = userServiceClient.DeleteUser(userId);
                return WarpServiceCall(deleteUserResponse, false, string.Empty);
            }
            catch (Exception ex)
            {
                return WarpServiceCall(string.Empty, true, ex.Message);
            }
        }


        private static ServiceWrapper WarpServiceCall<T>(T obj, bool hasExceptions, string exceptionMessage)
        {
            return new ServiceWrapper()
            {
                Data = obj,
                ExceptionMessage = exceptionMessage,
                HasExceptions = hasExceptions
            };

        }
    }
}