using Core.Database.Models.Auth;
using HashidsNet;

namespace Auth.Models
{
    public class UserDetailsResponse
    {
        public UserDetailsResponse(User user, Hashids hashids)
        {
            Id = hashids.EncodeLong(user.Id);
            WarehouseId = user.WarehouseId != null ? hashids.EncodeLong(user.WarehouseId.Value) : null;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;

            if (user.Auth0Users != null)
                Auth0Identifiers = user.Auth0Users.Select(c => c.Identifier).ToList();
            else
                Auth0Identifiers = new List<string>();

            if (user.Roles != null && user.Roles.All(c => c.Role != null))
                Roles = user.Roles.Select(c => c.Role!.Name.ToString()).ToList();
            else
                Roles = new List<string>();
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? WarehouseId { get; set; }
        public string Email { get; set; }
        public List<string> Auth0Identifiers { get; set; }
        public List<string> Roles { get; set; }
    }
}
