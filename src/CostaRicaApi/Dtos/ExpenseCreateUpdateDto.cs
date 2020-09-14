using CostaRicaApi.Models;

namespace CostaRicaApi.Dtos {
    public class ExpenseCreateUpdateDto {
        public float Amount { get; set; }
        public string Description { get; set; }
    }
}