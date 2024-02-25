using TestProj_ABP_Backend.DbContext;
using TestProj_ABP_Backend.Models;
using TestProj_ABP_Backend.Services;

namespace TestProj_ABP_Backend.AB_Tests;

public static class PriceTest
{
    public enum PriceTestEnum
    {
        ten,
        twenty,
        fifty,
        five,
    }
    private static readonly Random rand = new Random();
    private static int assignedCount = 0;
    /// <summary>
    /// Assigns number for color AB test
    /// </summary>
    /// <param name="deviceToken"></param>
    /// <param name="configuration"></param>
    /// <returns>true if success, false if user is absent in db</returns>
    public static bool AssignPrice(string deviceToken, IConfiguration configuration)
    {
        if (assignedCount is 0 or > 2000000000)
        {
            assignedCount = rand.Next(1, 3);
        }
        MyDbContext context = ContextFactory.New(configuration);
        User? user = context.Users.FirstOrDefault(x => x.DeviceToken == deviceToken);

        if (user.CreatedAt < DateTime.Parse(configuration["ColorTestStart"]))
        {
            return false;
        }

        if (user is null)
        {
            return false;
        }

        ColorTestModel colorTest = new()
        {
            Id = user.UserId,
            DeviceToken = deviceToken,
        };

        int modulo = assignedCount % 3;
        colorTest.Group = modulo switch
        {

        };
        context.ColorTest.Add(colorTest);
        context.SaveChanges();
        assignedCount++;

        return true;
    }


    /// <summary>
    /// Gets user experiment variable and converts it to color to be assigned on frontend
    /// </summary>
    /// <param name="deviceToken"></param>
    /// <param name="configuration"></param>
    /// <returns>Result with color to send</returns>
    /// <exception cref="ArgumentException">user.Experiment[0] is missing</exception>
    public static Result<string> GetPrice(string deviceToken, IConfiguration configuration)
    {
        if (deviceToken is null)
        {
            return new Result<string>(false, "", "device token is null");
        }
        MyDbContext context = ContextFactory.New(configuration);

        User? user = context.Users.FirstOrDefault(x => x.DeviceToken == deviceToken);

        //if user is old, he don't know about test
        if (user.CreatedAt < DateTime.Parse(configuration["ColorTestStart"]))
        {
            return new Result<string>(false, null, "Test started after user registered");
        }

        ColorTestModel? colorTest = context.ColorTest.FirstOrDefault(x => x.User.DeviceToken == deviceToken);

        if (user is null)
        {
            return new Result<string>(false, "", "user is missing");
        }
        //TODO rewrite (?)
        if (colorTest is null)
        {
            return new Result<string>(false, "", "colorTest is missing");
        }

        string color = colorTest.Group switch
        {
            ColorTest.ColorTestEnum.Red => "FF0000",
            ColorTest.ColorTestEnum.Green => "00FF00",
            ColorTest.ColorTestEnum.Blue => "0000FF",
            _ => throw new ArgumentException("user.Experiment[0] is missing")
        };

        return new Result<string>(
            true,
            color,
            "User and color founded"
        );
    }

}
