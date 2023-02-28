using Core.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donate.Actions.MyDonations.GetNotDeliveredCount
{
    public class GetNotDeliveredCountResponse
    {
        public int Count { get; set; }
    }
}
