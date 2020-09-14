using System.Threading.Tasks;

namespace CostaRicaApi.Services {
    public interface IStatisticsService {
        float GetSumOfExpenses();
        float GetSumOfExpensesOfThisMonth();
        float GetSumOfExpensesOfThisYear();
    }
}