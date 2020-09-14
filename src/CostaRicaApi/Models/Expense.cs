using System;
using System.ComponentModel.DataAnnotations;

namespace CostaRicaApi.Models {
    public class Expense {

        [Key]
        public int Id { get; set; }

        [Required]
        public float Amount { get; set; }
        public string Description { get; set; }
        public User Owner { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}