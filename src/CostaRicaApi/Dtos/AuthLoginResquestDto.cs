using System.ComponentModel.DataAnnotations;

namespace CostaRicaApi.Dtos {
    public class AuthLoginResquestDto {

        [Required]
        public string usernameOrEmail { get; set; }

        [Required]
        public string password { get; set; }
    }
}