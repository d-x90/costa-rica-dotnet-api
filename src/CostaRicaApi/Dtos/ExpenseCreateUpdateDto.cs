using CostaRicaApi.Models;

namespace CostaRicaApi.Dtos {
    public class ExpenseCreateUpdateDto {
        public Currency Currency { get; set; }
        public float Amount { get; set; }
        public string Description { get; set; }
    }
}