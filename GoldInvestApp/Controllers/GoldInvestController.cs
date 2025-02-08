using GoldInvestApp.Models;
using GoldInvestApp.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldInvestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoldInvestController : ControllerBase
    {
        public readonly IGolsInvest _golsInvest;
        public readonly ILogger<GoldInvestController> _logger;

        public GoldInvestController(IGolsInvest golsInvest, ILogger<GoldInvestController> logger)
        {
            _golsInvest = golsInvest;
            _logger = logger;

        }

        [HttpPost]
        public async Task<IActionResult> getGoldPrice(GoldInvestModelRequest goldInvestModelRequest)
        {
            GoldInvestModelResponse response = new GoldInvestModelResponse();
            //_logger.LogInformation(message: $"ReadAllInformation API Calling in Controller....{JsonConvert.SerializeObject(request)}");
            _logger.LogInformation(message: $"ReadAllInformation API Calling in Controller....");

            try
            {
                response = await _golsInvest.getGoldPrice(goldInvestModelRequest);

                if (!response.IsSuccess)
                {
                    return BadRequest(new { IsSuccess = response.IsSuccess, Message = response.Message, Data = response.cPrice });
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                _logger.LogError($"ReadAllInformation API Error Occur : Message {ex.Message}");
                return BadRequest(new { IsSuccess = response.IsSuccess, Message = response.Message });
            }
            return Ok(new { IsSuccess = response.IsSuccess, Message = response.Message, Data = response.cPrice });

        }

    }
}
