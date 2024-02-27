using TestProj_ABP_Backend.DbContext;
using TestProj_ABP_Backend.DTOs;
using TestProj_ABP_Backend.Models;
using TestProj_ABP_Backend.Services;

namespace TestProj_ABP_Backend.AB_Tests;

public static class ColorTest
{
    /// <summary>
    /// Groups of test summared to enum
    /// </summary>
    public enum ColorTestEnum
    {
        Red,
        Green,
        Blue,
    }
    private static readonly Random rand = new Random();
    private static int assignedCount = 0;
    /// <summary>
    /// Assigns number for color AB test
    /// </summary>
    /// <param name="deviceToken"></param>
    /// <param name="configuration"></param>
    /// <returns>true if success, false if user is absent in db</returns>
    private static bool AssignColor(string deviceToken, IConfiguration configuration)
    {
        if (assignedCount is 0 or > 2000000000)
        {
            assignedCount = rand.Next(1, 3);
        }
        MyDbContext context = ContextFactory.New(configuration);
        User? user = context.Users.FirstOrDefault(x => x.DeviceToken == deviceToken);

        try
        {
            if (user is null || user.CreatedAt < DateTime.Parse(configuration["ColorTestStart"]))
            {
                return false;
            }
        }
        catch (Exception ex)
        {

        }

        ColorTestModel colorTest = new()
        {
            Id = user.UserId,
            DeviceToken = deviceToken,
        };

        int modulo = assignedCount % 3;
        colorTest.Group = modulo switch
        {
            0 => ColorTestEnum.Red,
            1 => ColorTestEnum.Green,
            2 => ColorTestEnum.Blue,
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
    internal static Result<string> GetColor(string deviceToken, IConfiguration configuration)
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
            ColorTestEnum.Red => "FF0000",
            ColorTestEnum.Green => "00FF00",
            ColorTestEnum.Blue => "0000FF",
        };

        return new Result<string>(
            true,
            color,
            "User and color founded"
        );
    }

    private static bool IsAssigned(string DeviceToken, IConfiguration configuration)
    {
        MyDbContext context = ContextFactory.New(configuration);
        return context.ColorTest.Any(x => x.DeviceToken == DeviceToken);
    }

    internal static Result<string?> GetColorViaFingerprint(BrowserFingerprintDto fingerprintDto, IConfiguration configuration, HttpContext httpContext)
    {
        BrowserFingerprint fingerprint = new(fingerprintDto.DeviceToken, fingerprintDto, httpContext);

        if (FingerprintService.IsExists(fingerprint, configuration))
        {
            Result<BrowserFingerprint> res = FingerprintService.IsSimilarToAnyIfYesUpdate(fingerprint, configuration);
            if (!IsAssigned(res.Data.DeviceToken, configuration))
            {
                AssignColor(res.Data.DeviceToken, configuration);
            }
            if (res.IsSuccess)
            {
                var color2 = GetColor(res.Data.DeviceToken, configuration).Data;
                return new Result<string?>(true, color2, "all ok");
            }
        }

        User user = UserService.Register(configuration);
        if (!IsAssigned(user.DeviceToken, configuration))
        {
            ColorTest.IsAssigned(user.DeviceToken, configuration);
        }
        fingerprint.DeviceToken = user.DeviceToken;
        FingerprintService.Register(fingerprint, user, configuration);
        var res2 = GetColor(fingerprint.DeviceToken, configuration);
        var color = res2.Data;
        if (!res2.IsSuccess)
        {
            throw new Exception();
        }
        return new Result<string>(true, color, "all ok");
    }

}
