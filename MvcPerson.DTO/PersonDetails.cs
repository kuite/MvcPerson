using System.ComponentModel.DataAnnotations;

namespace MvcPerson.DTO
{
    public class PersonDetails
    {
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Length should be between 3-30")]
        public string Firstname { get; set; }
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Length should be between 3-30")]
        public string Lastname { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string Comment { get; set; }
    }
}