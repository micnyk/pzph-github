using System.Text.Json.Serialization;

namespace Pzph.ServiceLayer.Users.Models
{
    public class TokenResponse
    {
        [JsonPropertyName("access_token")] public string AccessToken { get; set; } = default!;

        [JsonPropertyName("expires_in")] public int ExpiresIn { get; set; }

        [JsonPropertyName("token_type")] public string TokenType { get; set; } = default!;

        [JsonPropertyName("refresh_token")] public string RefreshToken { get; set; } = default!;

        [JsonPropertyName("scope")] public string Scope { get; set; } = default!;

        [JsonPropertyName("error")] public string Error { get; set; } = default!;

        [JsonPropertyName("error_description")]
        public string ErrorDescription { get; set; } = default!;

        [JsonIgnore] public bool IsError => !string.IsNullOrEmpty(Error);
    }
}