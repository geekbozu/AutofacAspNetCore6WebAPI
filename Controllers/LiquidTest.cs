using Microsoft.AspNetCore.Mvc;
using Fluid;

namespace FluidTestBed.Controllers
{
    [ApiController]
    [Route("test")]
    public class LiquidTest : ControllerBase
    {

        private readonly ILogger<LiquidTest> _logger;
        private readonly FluidParser _parser;
        public LiquidTest(ILogger<LiquidTest> logger,FluidParser fluid)
        {
            _logger = logger;
            _parser = fluid;
        }

        [HttpGet("/")]
        public string Get()
        {
            var model = new { Firstname = "Bill", Lastname = "Keller" };
            var source = "Hello {{ Firstname }} {{ Lastname }}";

            if (_parser.TryParse(source, out var template, out var error))
            {
                var context = new TemplateContext(model);

                return template.Render(context);
            }
            else
            {
                return $"Error: {error}";
            }
        }
    }
}