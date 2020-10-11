using Capture_Mutiple_Users_Service.Model;
using System.Collections.Generic;
using System.ServiceModel;

namespace Capture_Mutiple_Users_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserService" in both code and config file together.
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        bool AddUsers(List<UserDTO> userDTO);
        [OperationContract]
        List<UserDTO> GetUsers();
        [OperationContract]
        UserDTO GetUserById(int userId);
        [OperationContract]
        bool UpdateUser(UserDTO userDTO);
        [OperationContract]
        bool DeleteUser(int userId);
    }
}
