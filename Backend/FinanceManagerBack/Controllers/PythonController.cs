using FinanceManagerBack.Python;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManagerBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PythonController : ControllerBase
    {
        IPythonScriptExecutor _pythonScriptExectutor;

        public PythonController(IPythonScriptExecutor pythonScriptExectutor)
        {
            _pythonScriptExectutor = pythonScriptExectutor;
        }

        [HttpGet("prediction")]
        public IActionResult GetPrediction()
        {
            var test = "";
            test = _pythonScriptExectutor.RunPredictionScript("car", -15000);

            return Ok(test);
        }
    }
}