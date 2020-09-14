using System.Threading.Tasks;
using CostaRicaApi.Dtos;
using CostaRicaApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CostaRicaApi.Controllers {

    [Authorize]
    [Route("api/v1/statistics")]
    [ApiController]
    public class StatisticsController : ControllerBase {
        
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet("dashboard")]
        public ActionResult<DashboardStatisticsDto> GetDashboardStats() {
            var responseDto = new DashboardStatisticsDto() {
                SumOfExpenses = _statisticsService.GetSumOfExpenses(),
                SumOfExpensesThisMonth = _statisticsService.GetSumOfExpensesOfThisMonth(),
                SumOfExpensesThisYear = _statisticsService.GetSumOfExpensesOfThisYear()
            };
            
            return Ok(responseDto);
        }
    }
}