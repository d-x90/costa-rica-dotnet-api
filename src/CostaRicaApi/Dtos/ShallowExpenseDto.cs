using CostaRicaApi.Models;

namespace CostaRicaApi.Dtos {
    public class ShallowExpenseDto {
        public int Id { get; set; }
        public Currency Currency { get; set; }
        public float Amount { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }
    }
}