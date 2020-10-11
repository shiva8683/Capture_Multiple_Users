using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Xml.Linq;
using Capture_Mutiple_Users_Service.Model;
using DevTrends.WCFDataAnnotations;

namespace Capture_Mutiple_Users_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UserService.svc or UserService.svc.cs at the Solution Explorer and start debugging.
    [ValidateDataAnnotationsBehavior]
    public class UserService : IUserService
    {
        private static readonly string _strAppPath = AppDomain.CurrentDomain.BaseDirectory;
        private readonly string _xmlPath = $"{_strAppPath}XMLFile\\UsersInformation.xml";
        private readonly string _appRootElement = "UsersInfo";
        private readonly string _userRootElement = "User";

        /// <summary>
        /// Adding List of user in XML file
        /// </summary>
        /// <param name="userListDTO"></param>
        /// <returns></returns>
        public bool AddUsers(List<UserDTO> userListDTO)
        {
            CheckRootElementExistsInXMLFile();
            var doc = XDocument.Load(_xmlPath);
            var usersList = GetUsers();
            var userId = 0;
            if (usersList.Any())
            {
                userId = usersList.LastOrDefault().UserId;
            }
            foreach (var userDTO in userListDTO)
            {
                userId = userId + 1;
                var root = new XElement(_userRootElement);
                root.Add(new XAttribute(nameof(UserDTO.UserId), userId));
                root.Add(new XElement(nameof(UserDTO.FirstName), userDTO.FirstName));
                root.Add(new XElement(nameof(UserDTO.LastName), userDTO.LastName));
                root.Add(new XElement(nameof(UserDTO.Cellphone), userDTO.Cellphone));
                doc.Element(_appRootElement).Add(root);
            }
            doc.Save(_xmlPath);
            return true;
        }

        /// <summary>
        /// Deleting the user based on supplied userId, if no user found the service will throw fault exception
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteUser(int userId)
        {
            var xmlDoc = XDocument.Load(_xmlPath);
            var item = (from userInfo in xmlDoc.Descendants(_userRootElement)
                        where userInfo.Attribute(nameof(UserDTO.UserId)).Value == userId.ToString()
                        select userInfo);
            if (item.Any())
                item.FirstOrDefault().Remove();
            else
                throw new FaultException($"No user found with the userId: {userId}");
            xmlDoc.Save(_xmlPath);
            return true;
        }

        /// <summary>
        /// Getting the user based on supplied userId, if no user found the service will throw fault exception
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserDTO GetUserById(int userId)
        {
            var userInfo = XDocument.Load(_xmlPath).Root.Descendants(_userRootElement).Select(node => new UserDTO
            {
                FirstName = node.Element(nameof(UserDTO.FirstName)).Value,
                LastName = node.Element(nameof(UserDTO.LastName)).Value,
                Cellphone = node.Element(nameof(UserDTO.Cellphone)).Value,
                UserId = Convert.ToInt32(node.Attribute(nameof(UserDTO.UserId)).Value)
            }).Where(x => x.UserId == userId);
            if (userInfo.Any())
                return userInfo.FirstOrDefault();
            else
                throw new FaultException($"No user found with the userId: {userId}");
        }

        /// <summary>
        /// Getting the List of all users from the service file
        /// </summary>
        /// <returns></returns>
        public List<UserDTO> GetUsers()
        {
            var itemsList = XDocument.Load(_xmlPath).Root.Descendants(_userRootElement).Select(node => new UserDTO
            {
                FirstName = node.Element(nameof(UserDTO.FirstName)).Value,
                LastName = node.Element(nameof(UserDTO.LastName)).Value,
                Cellphone = node.Element(nameof(UserDTO.Cellphone)).Value,
                UserId = Convert.ToInt32(node.Attribute(nameof(UserDTO.UserId)).Value)
            }).ToList();
            return itemsList;
        }

        /// <summary>
        /// Updating the user details based on user DTO
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        public bool UpdateUser(UserDTO userDTO)
        {
            var xmlDoc = XDocument.Load(_xmlPath);
            var item = (from userInfo in xmlDoc.Descendants(_userRootElement)
                        where userInfo.Attribute(nameof(UserDTO.UserId)).Value == userDTO.UserId.ToString()
                        select userInfo).FirstOrDefault();
            item.Element(nameof(UserDTO.FirstName)).Value = userDTO.FirstName;
            item.Element(nameof(UserDTO.LastName)).Value = userDTO.LastName;
            item.Element(nameof(UserDTO.Cellphone)).Value = userDTO.Cellphone;
            xmlDoc.Save(_xmlPath);
            return true;
        }

        /// <summary>
        /// Cheking the root element exists or not in the XML file.
        /// If root element is found then it will create it.
        /// </summary>
        private void CheckRootElementExistsInXMLFile()
        {
            XDocument doc;
            try
            {
                doc = XDocument.Load(_xmlPath);
            }
            catch
            {
                doc = new XDocument(new XElement(_appRootElement));
            }
            doc.Save(_xmlPath);
        }
    }
}
