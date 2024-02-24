using System.Text;
using TestProj_ABP_Backend.Models;

namespace TestProj_ABP_Backend.Services
{
    public class UserService
    {
        private static readonly Random random = new Random();

        /// <summary>
        /// Generates random string from numbers, latin lower and upper case letters of needed lenght.
        /// </summary>
        /// <param name="length">Lenght of string needed.</param>
        /// <returns>Random string.</returns>
        public static string GenerateRandomString(int length)
        {
            const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder stringBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                int randomIndex = random.Next(chars.Length);
                char randomChar = chars[randomIndex];
                stringBuilder.Append(randomChar);
            }

            return stringBuilder.ToString();
        }


        public string?[] GetUserCredentials(IHttpContextAccessor httpContextAccessor)
        {
            var userCredentials = new string?[2];
            userCredentials[0] = httpContextAccessor.HttpContext?.Request.Headers.UserAgent;
            userCredentials[1] = httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
            return userCredentials;
        }

        public static float CompareBrowserFingerprints(UserFingerprint? user1, UserFingerprint? user2)
        {
            if (user1 is null && user2 is null)
            {
                return 1F;
            }
            else if (user1 == null || user2 == null)
            {
                return 0F;
            }

            var totalProperties = typeof(UserFingerprint).GetProperties().Length;
            var matchingProperties = 0;

            foreach (var property in typeof(UserFingerprint).GetProperties())
            {
                var value1 = property.GetValue(user1);
                var value2 = property.GetValue(user2);

                if (value1 is not null && value2 is not null && value1.Equals(value2))
                {
                    matchingProperties++;
                }
            }

            return (float)matchingProperties / totalProperties * 100;
        }
    }
}
