using System.Text.Json.Serialization;

namespace Core.Auth0
{
    public class CurrentUserInfo
    {
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("email_verified")]
        public bool EmailVerified { get; set; }
        [JsonPropertyName("family_name")]
        public string? FamilyName { get; set; }
        [JsonPropertyName("given_name")]
        public string? GivenName { get; set; }
        [JsonPropertyName("locale")]
        public string? Locale { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("nickname")]
        public string? Nickname { get; set; }
        [JsonPropertyName("picture")]
        public string? PictureURL { get; set; }
        [JsonPropertyName("sub")]
        public string? Identifier { get; set; }
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}