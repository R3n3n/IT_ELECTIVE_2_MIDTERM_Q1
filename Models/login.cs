using System.ComponentModel.DataAnnotations;

namespace IT_ELECTIVE_2_MIDTERM_Q1.Models
{
    public class Login
    {
        [Required]
        public string Username { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";
    }
}