using Hng0Task.Entity;
using System.Text.Json;

namespace Hng0Task.Service
{
    public class ProfileService
    {
        private readonly HttpClient _httpClient;

        public ProfileService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<Profile> GetProfileAsync()
        {
            string fact = "Could not load cat fact at the moment.";

            try
            {
                var response = await _httpClient.GetAsync("https://catfact.ninja/fact");

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    using var jsonDoc = JsonDocument.Parse(jsonString);
                    fact = jsonDoc.RootElement.GetProperty("fact").GetString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
            var profile = new Profile
            {
                Status = "success",
                User = new UserInfo
                {
                    Email = "ismailagboola130@gmail.com",
                    Name = "Ibrahim Ismail",
                    Stack = "C#/ASP.NET Core"
                },
                Timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                Fact = fact
            };

            return profile;
        }
    }
}
