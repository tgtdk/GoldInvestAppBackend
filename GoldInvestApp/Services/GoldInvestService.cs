using GoldInvestApp.Models;
using GoldInvestApp.Repository.Interface;
using GoldInvestApp.Services.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldInvestApp.Services
{
    public class GoldInvestService : IGolsInvest
    {

        public readonly IGoldInvest _golsInvestRepo;
        public readonly ILogger<IGoldInvest> _logger;

        public GoldInvestService(IGoldInvest golsInvestRepo, ILogger<IGoldInvest> logger)
        {
            _golsInvestRepo = golsInvestRepo;
            _logger = logger;

        }


        public async Task<GoldInvestModelResponse> getGoldPrice(GoldInvestModelRequest request)
        {
            _logger.LogInformation("ReadInformationById Method Calling In Service Layer");
            return await _golsInvestRepo.getGoldPrice(request);
        }
    }
}
