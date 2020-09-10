using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CostaRicaApi.Models {
    public class User {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public byte[] PasswordHash { get; set; }
        
        public byte[] PasswordSalt { get; set; }

        public List<Expense> Expenses { get; set; }
    }
}