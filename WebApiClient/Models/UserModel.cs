using System.Text.Json.Serialization;

namespace WebApiClient.Models
{
    public class UserModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("firstName")]

        public string FirstName { get; set; }
        [JsonPropertyName("lastName")]

        public string LastName { get; set; }
        [JsonPropertyName("userName")]

        public string UserName { get; set; }
        [JsonPropertyName("email")]

        public string Email { get; set; }
        [JsonPropertyName("accessToken")]

        public string AccessToken { get; set; }
    }
}
