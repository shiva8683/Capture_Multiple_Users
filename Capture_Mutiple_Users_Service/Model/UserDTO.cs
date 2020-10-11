using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Capture_Mutiple_Users_Service.Model
{
    [DataContract]
    public class UserDTO //: IValidatableObject
    {
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Cell Number is required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Cell number")]
        public string Cellphone { get; set; }

    }
}