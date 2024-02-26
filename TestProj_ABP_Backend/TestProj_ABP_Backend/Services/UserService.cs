using System.Text;
using TestProj_ABP_Backend.DbContext;
using TestProj_ABP_Backend.DTOs;
using TestProj_ABP_Backend.Models;

namespace TestProj_ABP_Backend.Services;

public class UserService
{
    private static readonly Random random = new();

    internal static User Register(IConfiguration configuration)
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
    internal static string GenerateRandomString(int length)
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

    internal static string? GetUserAgent(HttpContext httpContext)
    {
        return httpContext.Request.Headers.UserAgent;
    }

    internal static string? GetUserIp(HttpContext httpContext)
    {
        return httpContext.Connection.RemoteIpAddress?.ToString();
    }

    internal static UserDataDto[] GetUserData(IConfiguration configuration)
    {
        MyDbContext context = ContextFactory.New(configuration);
        User[] users = context.Users.ToArray();
        ColorTestModel[] colorTests = context.ColorTest.ToArray();
        PriceTestModel[] priceTests = context.PriceTest.ToArray();

        List<UserDataDto> data = new();
        for (int i = 0; i < users.Length; i++)
        {
            User? user = users[i];
            data.Add(new()
            {
                deviceToken = user.DeviceToken,
                createdAt = user.CreatedAt,
            });
        }


        for (int i = 0; i < colorTests.Length; i++)
        {
            if (data[i].deviceToken == colorTests[i].DeviceToken)
                data[i].colorTest = colorTests[i].Group.ToString();
        }

        for (int i = 0; i < priceTests.Length; i++)
        {
            if (data[i].deviceToken == priceTests[i].DeviceToken)
                data[i].priceTest = priceTests[i].Group.ToString();
        }

        return data.ToArray();
    }
}
