using System;
using CostaRicaApi.Models;

namespace CostaRicaApi.Dtos {
    public class ShallowExpenseDto {
        public int Id { get; set; }
        public float Amount { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}