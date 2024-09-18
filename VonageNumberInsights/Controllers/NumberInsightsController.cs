using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using Vonage;
using Vonage.NumberInsights;

namespace VonageNumberInsights.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NumberInsightsController : ControllerBase
    {
        private INumberInsightClient _numberInsightClient;

        //We inject and get a reference to the VonageClient in the constructor
        public NumberInsightsController(INumberInsightClient numberInsightClient)
        {
            _numberInsightClient = numberInsightClient;
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
                var result = await _numberInsightClient.GetNumberInsightBasicAsync(new Vonage.NumberInsights.BasicNumberInsightRequest()
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
                var result = await _numberInsightClient.GetNumberInsightStandardAsync(new Vonage.NumberInsights.StandardNumberInsightRequest()
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
                var result = await _numberInsightClient.GetNumberInsightAdvancedAsync(new Vonage.NumberInsights.AdvancedNumberInsightRequest()
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
