using System.Text;

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
    }
}
