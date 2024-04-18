using FinanceManagerBack.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FinanceManagerBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [Route("{period}/{walletId:int}/{categoryId:int}")]
        [HttpGet]
        public ActionResult<int[]> GetStatisticsForPeriod(string period, int walletId, int categoryId)
        {
            int[] result;
            
            try
            {
                result = _statisticsService.GetStatisticsForPeriod(period, walletId, categoryId);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            return Ok(result);
        }
    }
}
