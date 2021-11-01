using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// The Class that contains the information needed for each user or 'player'.
/// </summary>
namespace Milestone.Models
{
    public class PlayerInfo
    {


        public int PlayerID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 8)]
        public string Password { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Sex { get; set; }

        [Required]
        [Range(10, 150, ErrorMessage = "You have to be over the age of " +
            "10 to signup.")]
        public int Age { get; set; }

        [Required]
        [RegularExpression("[A-Z]{2}")]
        public string State { get; set; }
    }

}
