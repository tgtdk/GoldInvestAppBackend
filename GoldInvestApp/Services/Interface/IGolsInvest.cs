using GoldInvestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldInvestApp.Services.Interface
{
    public interface IGolsInvest
    {
        public Task<GoldInvestModelResponse> getGoldPrice(GoldInvestModelRequest request);
    }
}
