namespace CostaRicaApi.Models {
    public class Expense {
        public int Id { get; set; }
        public Currency Currency { get; set; }
        public float Amount { get; set; }
        public string Description { get; set; }
        public User Owner { get; set; }
    }

    public enum Currency {
        HUF, EUR, USD
    }
}