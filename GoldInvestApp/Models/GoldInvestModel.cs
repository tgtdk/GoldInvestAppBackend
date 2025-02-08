using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoldInvestApp.Models
{
    public class GoldInvestModelRequest
    {
        [Required(ErrorMessage = "Year Is Required")]
        public string cYear { get; set; }
    }
    public class GoldInvestModelResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string cPrice { get; set; }
    }
}
