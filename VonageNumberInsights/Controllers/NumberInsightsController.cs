using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using Vonage;

namespace VonageNumberInsights.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NumberInsightsController : ControllerBase
    {
        private readonly ILogger<NumberInsightsController> _logger;
        private VonageClient _vonageClient;

        //We inject and get a reference to the VonageClient in the constructor
        public NumberInsightsController(ILogger<NumberInsightsController> logger, VonageClient vonageClient)
        {
            _logger = logger;
            _vonageClient = vonageClient;
        }

        //This is a simple endpoint that returns the index.html file from the wwwroot folder for our UI
        [HttpGet("Index")]
        public IActionResult Index()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html");
            return PhysicalFile(filePath, "text/html");
        }

        //For simplicity we have split the 3 types of Number Insight lookups into separate endpoints,
        //but as their behaviour is similar you could combine them into one endpoint with a parameter to specify the type of lookup

        //This is the endpoint that will handle the Basic Lookup
        [HttpGet("BasicLookup")]
        public async Task<ActionResult<string>> BasicLookup(string phoneNumber)
        {
            if(string.IsNullOrEmpty(phoneNumber))
            {
                return BadRequest("Phone Number is required");
            }

            try
            {
                var result = await _vonageClient.NumberInsightClient.GetNumberInsightBasicAsync(new Vonage.NumberInsights.BasicNumberInsightRequest()
                {
                    Number = phoneNumber
                });

                var stringRes = JsonConvert.SerializeObject(result);

                return Ok(stringRes);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //This is the endpoint that will handle the Standard Lookup
        [HttpGet("StandardLookup")]
        public async Task<ActionResult<string>> StandardLookup(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return BadRequest("Phone Number is required");
            }

            try
            {
                var result = await _vonageClient.NumberInsightClient.GetNumberInsightStandardAsync(new Vonage.NumberInsights.StandardNumberInsightRequest()
                {
                    Number = phoneNumber
                });

                var stringRes = JsonConvert.SerializeObject(result);

                return Ok(stringRes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //This is the endpoint that will handle the Advanced Lookup
        [HttpGet("AdvancedLookup")]
        public async Task<ActionResult<string>> AdvancedLookup(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return BadRequest("Phone Number is required");
            }

            try
            {
                var result = await _vonageClient.NumberInsightClient.GetNumberInsightAdvancedAsync(new Vonage.NumberInsights.AdvancedNumberInsightRequest()
                {
                    Number = phoneNumber
                });

                var stringRes = JsonConvert.SerializeObject(result);

                return Ok(stringRes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
