using HowToImplementTests.Api.DAL;
using Microsoft.AspNetCore.Mvc;

namespace HowToImplementTests.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        internal readonly ICalculatorModel _calculator;

        public CalculatorController(ICalculatorModel calculator)
        {
            _calculator = calculator;
        }

        [HttpGet("add")]
        public ActionResult<double> Add(double a, double b)
        {
            double result = _calculator.Add(a, b);
            return Ok(result);
        }

        [HttpGet("subtract")]
        public ActionResult<double> Subtract(double a, double b)
        {
            double result = _calculator.Subtract(a, b);
            return Ok(result);
        }

        [HttpGet("multiply")]
        public ActionResult<double> Multiply(double a, double b)
        {
            double result = _calculator.Multiply(a, b);
            return Ok(result);
        }

        [HttpGet("divide")]
        public ActionResult<double> Divide(double a, double b)
        {
            try
            {
                return Ok(_calculator.Divide(a, b));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}