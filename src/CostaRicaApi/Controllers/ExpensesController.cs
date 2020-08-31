using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace CostaRicaApi.Controllers {

    [Route("api/v1/[controller]")]
    [ApiController]
    public class ExpensesController: ControllerBase {
        
        [HttpGet]
        public ActionResult<IEnumerable<int>> Get() {
            return new int[] {1,2,3,4};
        }
    }
}