using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Event_Calendar_WebApi.Models
{
    public class User
    {
        public int UserId { get; set; }
        [StringLength(20)]
        public string FirstName { get; set; }
        [StringLength(20)]
        public string LastName { get; set; }
        public string Email { get; set; }
        [StringLength(15)]
        public string Username { get; set; }
        [StringLength(20)]
        public string Password { get; set; }
        [JsonIgnore]
        public virtual List<UserRole>? UserRoles { get; set; }
        [JsonIgnore]
        public virtual Schedule? Schedule { get; set; }
    }
}
