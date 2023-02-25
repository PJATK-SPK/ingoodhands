using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donate.Actions.DonateForm.PerformDonate
{
    public class PerformDonateProductPayload
    {
        public string Id { get; set; } = default!;
        public int Quantity { get; set; }
    }
}
