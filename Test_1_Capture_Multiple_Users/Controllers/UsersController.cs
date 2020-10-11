using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Test_1_Capture_Multiple_Users.Helpers;
using Test_1_Capture_Multiple_Users.Users;

namespace Test_1_Capture_Multiple_Users.Controllers
{
    public class UsersController : Controller
    {
        /// <summary>
        /// Getting list of users and showing in the index view
        /// Set Users/Index as default view in RouteConfig.cs file.
        /// Functionality to get the list of users from service file has written in UsersMVCGrid.cs file
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Returning the create view.
        /// Passing Default List of UserDTO to the view because view will check and display if it has any users list else it will diplay empty controls to create users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateUser()
        {
            return View(new List<UserDTO>());
        }

        /// <summary>
        /// Getting list of users from the "FormCollection" and creating the user(s)
        /// On any exception "view" will display the exception message.
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateUser(FormCollection form)
        {
            var firstNameList = form[nameof(UserDTO.FirstName)].Split(char.Parse(","));
            var lastNameList = form[nameof(UserDTO.LastName)].Split(char.Parse(","));
            var cellphoneList = form[nameof(UserDTO.Cellphone)].Split(char.Parse(","));
            var lstUserDTO = new List<UserDTO>();
            for (int i = 0; i < firstNameList.Length; i++)
            {
                lstUserDTO.Add(new UserDTO
                {
                    FirstName = firstNameList[i].Trim(),
                    LastName = lastNameList[i].Trim(),
                    Cellphone = cellphoneList[i].Trim()
                });
            }
            var addUserResponse = ServiceWrapperHelper.AddUsers(lstUserDTO);
            if (!addUserResponse.HasExceptions)
            {
                TempData.ToastSuccess("User(s) Added Successfully!");
                return RedirectToAction("Index");
            }
            else
            {
                TempData.ToastError(addUserResponse.ExceptionMessage);
                ModelState.AddModelError("CreateUserError", addUserResponse.ExceptionMessage);
                return View(lstUserDTO);
            }
        }

        /// <summary>
        /// Getting the user details based on the userId from the service and returing the "PartialView" which will display on the popup to edit the details
        /// On Exception returning the exception view with exception information in it.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditUser(string userId)
        {
            var convertedUserId = Convert.ToInt32(Security.Decrypt(userId));
            var getUserByIdServiceResponse = ServiceWrapperHelper.GetUserById(convertedUserId);
            var userDTODeserializeObject = JsonConvert.DeserializeObject<UserDTO>(getUserByIdServiceResponse.Data.ToString());
            if (!getUserByIdServiceResponse.HasExceptions)
                return PartialView("~/Views/Users/_EditUserDetails.cshtml", userDTODeserializeObject);
            else
                return PartialView("~/Views/Users/_ExceptionView.cshtml", getUserByIdServiceResponse.ExceptionMessage);
        }

        /// <summary>
        /// Editing the user details based on passing UserDTO using service(WCF) and returning to the INDEX view
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditUser(UserDTO userDto)
        {
            var updateUserResponse = ServiceWrapperHelper.UpdateUser(new UserDTO
            {
                Cellphone = userDto.Cellphone.Trim(),
                FirstName = userDto.FirstName.Trim(),
                LastName = userDto.LastName.Trim(),
                UserId = userDto.UserId
            });
            if (updateUserResponse.HasExceptions)
            {
                TempData.ToastError(updateUserResponse.ExceptionMessage);
                ModelState.AddModelError("UpdateUser", updateUserResponse.ExceptionMessage);
            }
            else
            {
                TempData.ToastSuccess("User Details Updated Successfully!");
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Getting the user details based on the userId from the service and returing the "PartialView" which will display on the popup to delete the details
        /// On Exception returning the exception view with exception information in it.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteUser(string userId)
        {
            var convertedUserId = Convert.ToInt32(Security.Decrypt(userId.Trim()));
            var getUserByIdServiceResponse = ServiceWrapperHelper.GetUserById(convertedUserId);
            var userDTODeserializeObject = JsonConvert.DeserializeObject<UserDTO>(getUserByIdServiceResponse.Data.ToString());
            if (!getUserByIdServiceResponse.HasExceptions)
                return PartialView("~/Views/Users/_DeleteUserDetails.cshtml", userDTODeserializeObject);
            else
                return PartialView("~/Views/Users/_ExceptionView.cshtml", getUserByIdServiceResponse.ExceptionMessage);
        }

        /// <summary>
        /// Deleting the user details based in UserDTO using service(WCF) and returning to the INDEX view
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteUser(UserDTO userDto)
        {
            var getUserByIdServiceResponse = ServiceWrapperHelper.DeleteUser(userDto.UserId);
            if (getUserByIdServiceResponse.HasExceptions)
            {
                TempData.ToastError(getUserByIdServiceResponse.ExceptionMessage);
                ModelState.AddModelError("UpdateUser", getUserByIdServiceResponse.ExceptionMessage);
            }
            else
                TempData.ToastSuccess("User Deleted Successfully.");
            return RedirectToAction("Index");
        }

    }
}