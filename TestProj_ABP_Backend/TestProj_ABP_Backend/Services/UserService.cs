using System.Text;
using TestProj_ABP_Backend.DbContext;
using TestProj_ABP_Backend.Models;

namespace TestProj_ABP_Backend.Services;

public class UserService
{
    private static readonly Random random = new();

    public static User Register(IConfiguration configuration)
    {
        MyDbContext context = ContextFactory.New(configuration);
        User user = new User()
        {
            UserId = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            DeviceToken = GenerateRandomString(6),
        };

        context.Users.Add(user);
        context.SaveChanges();

        return user;
    }

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

    public static string? GetUserAgent(HttpContext httpContext)
    {
        return httpContext.Request.Headers.UserAgent;
    }

    public static string? GetUserIp(HttpContext httpContext)
    {
        return httpContext.Connection.RemoteIpAddress?.ToString();
    }

    internal static bool IsExists(User user, IConfiguration configuration)
    {
        MyDbContext context = ContextFactory.New(configuration);

        if (user.DeviceToken is not null)
        {
            return context.Users.Any(x => x.DeviceToken == user.DeviceToken);
        }

        return context.Users.Any(x => x.UserId == user.UserId);
    }

}
