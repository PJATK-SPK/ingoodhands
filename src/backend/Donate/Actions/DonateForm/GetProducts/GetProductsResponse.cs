using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donate.Actions.DonateForm.GetProducts
{
    public class GetProductsResponse
    {
        public string Id { get; set; } = default!; // ProductId
        public string Name { get; set; } = default!;
        public string Unit { get; set; } = default!;
    }
}
