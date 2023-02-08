using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class SeedingCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "core",
                table: "countries",
                columns: new[] { "id", "alpha2iso_code", "alpha3iso_code", "english_name", "status", "update_user_id", "updated_at" },
                values: new object[,]
                {
                    { 1L, "AF", "AFG", "Afghanistan", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2L, "AX", "ALA", "Aland Islands", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3L, "AL", "ALB", "Albania", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4L, "DZ", "DZA", "Algeria", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5L, "AS", "ASM", "American Samoa", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6L, "AD", "AND", "Andorra", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7L, "AO", "AGO", "Angola", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 8L, "AI", "AIA", "Anguilla", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 9L, "AQ", "ATA", "Antarctica", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 10L, "AG", "ATG", "Antigua and Barbuda", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 11L, "AR", "ARG", "Argentina", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 12L, "AM", "ARM", "Armenia", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 13L, "AW", "ABW", "Aruba", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 14L, "AU", "AUS", "Australia", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 15L, "AT", "AUT", "Austria", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 16L, "AZ", "AZE", "Azerbaijan", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 17L, "BS", "BHS", "Bahamas", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 18L, "BH", "BHR", "Bahrain", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 19L, "BD", "BGD", "Bangladesh", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 20L, "BB", "BRB", "Barbados", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 21L, "BY", "BLR", "Belarus", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 22L, "BE", "BEL", "Belgium", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 23L, "BZ", "BLZ", "Belize", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 24L, "BJ", "BEN", "Benin", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 25L, "BM", "BMU", "Bermuda", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 26L, "BT", "BTN", "Bhutan", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 27L, "BO", "BOL", "Bolivia", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 28L, "BA", "BIH", "Bosnia and Herzegovina", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 29L, "BW", "BWA", "Botswana", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 30L, "BV", "BVT", "Bouvet Island", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 31L, "BR", "BRA", "Brazil", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 32L, "VG", "VGB", "British Virgin Islands", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 33L, "IO", "IOT", "British Indian Ocean Territory", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 34L, "BN", "BRN", "Brunei Darussalam", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 35L, "BG", "BGR", "Bulgaria", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 36L, "BF", "BFA", "Burkina Faso", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 37L, "BI", "BDI", "Burundi", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 38L, "KH", "KHM", "Cambodia", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 39L, "CM", "CMR", "Cameroon", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 40L, "CA", "CAN", "Canada", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 41L, "CV", "CPV", "Cape Verde", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 42L, "KY", "CYM", "Cayman Islands", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 43L, "CF", "CAF", "Central African Republic", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 44L, "TD", "TCD", "Chad", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 45L, "CL", "CHL", "Chile", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 46L, "CN", "CHN", "China", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 47L, "HK", "HKG", "Hong Kong, SAR China", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 48L, "MO", "MAC", "Macao, SAR China", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 49L, "CX", "CXR", "Christmas Island", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 50L, "CC", "CCK", "Cocos (Keeling) Islands", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 51L, "CO", "COL", "Colombia", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 52L, "KM", "COM", "Comoros", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 53L, "CG", "COG", "Congo (Brazzaville)", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 54L, "CD", "COD", "Congo, (Kinshasa)", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 55L, "CK", "COK", "Cook Islands", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 56L, "CR", "CRI", "Costa Rica", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 57L, "CI", "CIV", "Côte d'Ivoire", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 58L, "HR", "HRV", "Croatia", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 59L, "CU", "CUB", "Cuba", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 60L, "CY", "CYP", "Cyprus", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 61L, "CZ", "CZE", "Czech Republic", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 62L, "DK", "DNK", "Denmark", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 63L, "DJ", "DJI", "Djibouti", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 64L, "DM", "DMA", "Dominica", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 65L, "DO", "DOM", "Dominican Republic", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 66L, "EC", "ECU", "Ecuador", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 67L, "EG", "EGY", "Egypt", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 68L, "SV", "SLV", "El Salvador", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 69L, "GQ", "GNQ", "Equatorial Guinea", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 70L, "ER", "ERI", "Eritrea", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 71L, "EE", "EST", "Estonia", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 72L, "ET", "ETH", "Ethiopia", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 73L, "FK", "FLK", "Falkland Islands (Malvinas)", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 74L, "FO", "FRO", "Faroe Islands", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 75L, "FJ", "FJI", "Fiji", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 76L, "FI", "FIN", "Finland", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 77L, "FR", "FRA", "France", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 78L, "GF", "GUF", "French Guiana", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 79L, "PF", "PYF", "French Polynesia", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 80L, "TF", "ATF", "French Southern Territories", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 81L, "GA", "GAB", "Gabon", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 82L, "GM", "GMB", "Gambia", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 83L, "GE", "GEO", "Georgia", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 84L, "DE", "DEU", "Germany", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 85L, "GH", "GHA", "Ghana", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 86L, "GI", "GIB", "Gibraltar", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 87L, "GR", "GRC", "Greece", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 88L, "GL", "GRL", "Greenland", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 89L, "GD", "GRD", "Grenada", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 90L, "GP", "GLP", "Guadeloupe", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 91L, "GU", "GUM", "Guam", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 92L, "GT", "GTM", "Guatemala", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 93L, "GG", "GGY", "Guernsey", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 94L, "GN", "GIN", "Guinea", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 95L, "GW", "GNB", "Guinea-Bissau", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 96L, "GY", "GUY", "Guyana", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 97L, "HT", "HTI", "Haiti", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 98L, "HM", "HMD", "Heard and Mcdonald Islands", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 99L, "VA", "VAT", "Holy See (Vatican City State)", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 100L, "HN", "HND", "Honduras", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 101L, "HU", "HUN", "Hungary", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 102L, "IS", "ISL", "Iceland", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 103L, "IN", "IND", "India", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 104L, "ID", "IDN", "Indonesia", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 105L, "IR", "IRN", "Iran, Islamic Republic of", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 106L, "IQ", "IRQ", "Iraq", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 107L, "IE", "IRL", "Ireland", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 108L, "IM", "IMN", "Isle of Man", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 109L, "IL", "ISR", "Israel", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 110L, "IT", "ITA", "Italy", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 111L, "JM", "JAM", "Jamaica", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 112L, "JP", "JPN", "Japan", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 113L, "JE", "JEY", "Jersey", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 114L, "JO", "JOR", "Jordan", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 115L, "KZ", "KAZ", "Kazakhstan", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 116L, "KE", "KEN", "Kenya", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 117L, "KI", "KIR", "Kiribati", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 118L, "KP", "PRK", "Korea (North)", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 119L, "KR", "KOR", "Korea (South)", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 120L, "KW", "KWT", "Kuwait", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 121L, "KG", "KGZ", "Kyrgyzstan", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 122L, "LA", "LAO", "Lao PDR", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 123L, "LV", "LVA", "Latvia", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 124L, "LB", "LBN", "Lebanon", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 125L, "LS", "LSO", "Lesotho", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 126L, "LR", "LBR", "Liberia", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 127L, "LY", "LBY", "Libya", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 128L, "LI", "LIE", "Liechtenstein", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 129L, "LT", "LTU", "Lithuania", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 130L, "LU", "LUX", "Luxembourg", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 131L, "MK", "MKD", "Macedonia, Republic of", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 132L, "MG", "MDG", "Madagascar", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 133L, "MW", "MWI", "Malawi", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 134L, "MY", "MYS", "Malaysia", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 135L, "MV", "MDV", "Maldives", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 136L, "ML", "MLI", "Mali", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 137L, "MT", "MLT", "Malta", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 138L, "MH", "MHL", "Marshall Islands", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 139L, "MQ", "MTQ", "Martinique", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 140L, "MR", "MRT", "Mauritania", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 141L, "MU", "MUS", "Mauritius", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 142L, "YT", "MYT", "Mayotte", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 143L, "MX", "MEX", "Mexico", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 144L, "FM", "FSM", "Micronesia, Federated States of", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 145L, "MD", "MDA", "Moldova", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 146L, "MC", "MCO", "Monaco", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 147L, "MN", "MNG", "Mongolia", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 148L, "ME", "MNE", "Montenegro", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 149L, "MS", "MSR", "Montserrat", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 150L, "MA", "MAR", "Morocco", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 151L, "MZ", "MOZ", "Mozambique", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 152L, "MM", "MMR", "Myanmar", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 153L, "NA", "NAM", "Namibia", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 154L, "NR", "NRU", "Nauru", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 155L, "NP", "NPL", "Nepal", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 156L, "NL", "NLD", "Netherlands", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 157L, "AN", "ANT", "Netherlands Antilles", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 158L, "NC", "NCL", "New Caledonia", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 159L, "NZ", "NZL", "New Zealand", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 160L, "NI", "NIC", "Nicaragua", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 161L, "NE", "NER", "Niger", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 162L, "NG", "NGA", "Nigeria", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 163L, "NU", "NIU", "Niue", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 164L, "NF", "NFK", "Norfolk Island", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 165L, "MP", "MNP", "Northern Mariana Islands", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 166L, "NO", "NOR", "Norway", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 167L, "OM", "OMN", "Oman", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 168L, "PK", "PAK", "Pakistan", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 169L, "PW", "PLW", "Palau", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 170L, "PS", "PSE", "Palestinian Territory", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 171L, "PA", "PAN", "Panama", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 172L, "PG", "PNG", "Papua New Guinea", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 173L, "PY", "PRY", "Paraguay", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 174L, "PE", "PER", "Peru", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 175L, "PH", "PHL", "Philippines", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 176L, "PN", "PCN", "Pitcairn", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 177L, "PL", "POL", "Poland", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 178L, "PT", "PRT", "Portugal", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 179L, "PR", "PRI", "Puerto Rico", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 180L, "QA", "QAT", "Qatar", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 181L, "RE", "REU", "Réunion", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 182L, "RO", "ROU", "Romania", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 183L, "RU", "RUS", "Russian Federation", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 184L, "RW", "RWA", "Rwanda", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 185L, "BL", "BLM", "Saint-Barthélemy", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 186L, "SH", "SHN", "Saint Helena", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 187L, "KN", "KNA", "Saint Kitts and Nevis", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 188L, "LC", "LCA", "Saint Lucia", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 189L, "MF", "MAF", "Saint-Martin (French part)", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 190L, "PM", "SPM", "Saint Pierre and Miquelon", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 191L, "VC", "VCT", "Saint Vincent and Grenadines", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 192L, "WS", "WSM", "Samoa", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 193L, "SM", "SMR", "San Marino", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 194L, "ST", "STP", "Sao Tome and Principe", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 195L, "SA", "SAU", "Saudi Arabia", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 196L, "SN", "SEN", "Senegal", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 197L, "RS", "SRB", "Serbia", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 198L, "SC", "SYC", "Seychelles", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 199L, "SL", "SLE", "Sierra Leone", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 200L, "SG", "SGP", "Singapore", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 201L, "SK", "SVK", "Slovakia", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 202L, "SI", "SVN", "Slovenia", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 203L, "SB", "SLB", "Solomon Islands", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 204L, "SO", "SOM", "Somalia", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 205L, "ZA", "ZAF", "South Africa", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 206L, "GS", "SGS", "South Georgia and the South Sandwich Islands", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 207L, "SS", "SSD", "South Sudan", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 208L, "ES", "ESP", "Spain", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 209L, "LK", "LKA", "Sri Lanka", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 210L, "SD", "SDN", "Sudan", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 211L, "SR", "SUR", "Suriname", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 212L, "SJ", "SJM", "Svalbard and Jan Mayen Islands", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 213L, "SZ", "SWZ", "Swaziland", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 214L, "SE", "SWE", "Sweden", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 215L, "CH", "CHE", "Switzerland", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 216L, "SY", "SYR", "Syrian Arab Republic (Syria)", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 217L, "TW", "TWN", "Taiwan, Republic of China", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 218L, "TJ", "TJK", "Tajikistan", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 219L, "TZ", "TZA", "Tanzania, United Republic of", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 220L, "TH", "THA", "Thailand", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 221L, "TL", "TLS", "Timor-Leste", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 222L, "TG", "TGO", "Togo", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 223L, "TK", "TKL", "Tokelau", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 224L, "TO", "TON", "Tonga", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 225L, "TT", "TTO", "Trinidad and Tobago", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 226L, "TN", "TUN", "Tunisia", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 227L, "TR", "TUR", "Turkey", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 228L, "TM", "TKM", "Turkmenistan", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 229L, "TC", "TCA", "Turks and Caicos Islands", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 230L, "TV", "TUV", "Tuvalu", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 231L, "UG", "UGA", "Uganda", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 232L, "UA", "UKR", "Ukraine", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 233L, "AE", "ARE", "United Arab Emirates", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 234L, "GB", "GBR", "United Kingdom", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 235L, "US", "USA", "United States of America", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 236L, "UM", "UMI", "US Minor Outlying Islands", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 237L, "UY", "URY", "Uruguay", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 238L, "UZ", "UZB", "Uzbekistan", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 239L, "VU", "VUT", "Vanuatu", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 240L, "VE", "VEN", "Venezuela (Bolivarian Republic)", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 241L, "VN", "VNM", "Viet Nam", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 242L, "VI", "VIR", "Virgin Islands, US", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 243L, "WF", "WLF", "Wallis and Futuna Islands", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 244L, "EH", "ESH", "Western Sahara", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 245L, "YE", "YEM", "Yemen", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 246L, "ZM", "ZMB", "Zambia", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 247L, "ZW", "ZWE", "Zimbabwe", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "roles",
                keyColumn: "id",
                keyValue: 1L,
                column: "updated_at",
                value: new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "roles",
                keyColumn: "id",
                keyValue: 2L,
                column: "updated_at",
                value: new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "roles",
                keyColumn: "id",
                keyValue: 3L,
                column: "updated_at",
                value: new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "roles",
                keyColumn: "id",
                keyValue: 4L,
                column: "updated_at",
                value: new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "roles",
                keyColumn: "id",
                keyValue: 5L,
                column: "updated_at",
                value: new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 39L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 40L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 42L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 44L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 45L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 46L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 47L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 48L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 49L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 50L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 51L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 52L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 53L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 54L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 55L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 56L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 57L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 58L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 59L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 60L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 61L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 62L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 63L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 64L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 65L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 66L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 67L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 68L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 69L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 70L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 71L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 72L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 73L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 74L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 75L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 76L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 77L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 78L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 79L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 80L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 81L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 82L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 83L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 84L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 85L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 86L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 87L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 88L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 89L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 90L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 91L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 92L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 93L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 94L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 95L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 96L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 97L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 98L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 99L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 100L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 101L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 102L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 103L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 104L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 105L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 106L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 107L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 108L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 109L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 110L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 111L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 112L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 113L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 114L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 115L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 116L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 117L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 118L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 119L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 120L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 121L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 122L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 123L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 124L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 125L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 126L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 127L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 128L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 129L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 130L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 131L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 132L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 133L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 134L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 135L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 136L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 137L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 138L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 139L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 140L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 141L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 142L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 143L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 144L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 145L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 146L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 147L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 148L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 149L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 150L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 151L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 152L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 153L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 154L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 155L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 156L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 157L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 158L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 159L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 160L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 161L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 162L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 163L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 164L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 165L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 166L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 167L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 168L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 169L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 170L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 171L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 172L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 173L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 174L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 175L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 176L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 177L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 178L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 179L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 180L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 181L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 182L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 183L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 184L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 185L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 186L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 187L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 188L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 189L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 190L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 191L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 192L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 193L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 194L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 195L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 196L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 197L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 198L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 199L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 200L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 201L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 202L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 203L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 204L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 205L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 206L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 207L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 208L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 209L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 210L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 211L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 212L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 213L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 214L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 215L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 216L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 217L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 218L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 219L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 220L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 221L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 222L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 223L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 224L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 225L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 226L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 227L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 228L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 229L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 230L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 231L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 232L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 233L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 234L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 235L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 236L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 237L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 238L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 239L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 240L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 241L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 242L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 243L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 244L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 245L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 246L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "countries",
                keyColumn: "id",
                keyValue: 247L);

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "roles",
                keyColumn: "id",
                keyValue: 1L,
                column: "updated_at",
                value: new DateTime(2023, 2, 8, 18, 41, 57, 344, DateTimeKind.Utc).AddTicks(2570));

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "roles",
                keyColumn: "id",
                keyValue: 2L,
                column: "updated_at",
                value: new DateTime(2023, 2, 8, 18, 41, 57, 344, DateTimeKind.Utc).AddTicks(2590));

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "roles",
                keyColumn: "id",
                keyValue: 3L,
                column: "updated_at",
                value: new DateTime(2023, 2, 8, 18, 41, 57, 344, DateTimeKind.Utc).AddTicks(2600));

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "roles",
                keyColumn: "id",
                keyValue: 4L,
                column: "updated_at",
                value: new DateTime(2023, 2, 8, 18, 41, 57, 344, DateTimeKind.Utc).AddTicks(2610));

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "roles",
                keyColumn: "id",
                keyValue: 5L,
                column: "updated_at",
                value: new DateTime(2023, 2, 8, 18, 41, 57, 344, DateTimeKind.Utc).AddTicks(2620));
        }
    }
}
