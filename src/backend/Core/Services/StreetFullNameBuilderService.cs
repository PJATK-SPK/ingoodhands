using Core.Database.Models.Core;
using System.Text;

namespace Core.Services
{
    public static class StreetFullNameBuilderService
    {
        public static string Build(Address address)
            => Build(address.Street, address.StreetNumber, address.Apartment);

        public static string Build(string? street = null, string? number = null, string? apartment = null)
        {
            var sb = new StringBuilder();

            if (street != null)
            {
                sb.Append(street);

                if (number != null || apartment != null)
                    sb.Append(' ');
            }

            if (number != null)
            {
                sb.Append(number);
            }
            else if (apartment != null)
            {
                sb.Append("?/");
            }

            if (number != null && apartment != null)
            {
                sb.Append('/');
            }

            if (apartment != null)
            {
                sb.Append(apartment);
            }

            return sb.ToString();
        }
    }
}
