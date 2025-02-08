using GoldInvestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldInvestApp.Repository.Interface
{
    public interface IGoldInvest
    {
        public Task<GoldInvestModelResponse> getGoldPrice(GoldInvestModelRequest request);
    }
}
