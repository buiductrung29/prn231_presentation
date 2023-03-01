using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Identity_Sample.Models
{
    public class AppUser : IdentityUser
    {
        [MaxLength(100)]
        public string FullName { get; set; }
        [MaxLength(255)]
        public string Address { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }
    }
}