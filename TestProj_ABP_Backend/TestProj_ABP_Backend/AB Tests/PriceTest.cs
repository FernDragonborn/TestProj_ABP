using TestProj_ABP_Backend.DbContext;
using TestProj_ABP_Backend.DTOs;
using TestProj_ABP_Backend.Models;
using TestProj_ABP_Backend.Services;

namespace TestProj_ABP_Backend.AB_Tests;

public static class PriceTest
{

    /// <summary>
    /// Groups of test summared to enum
    /// </summary>
    public enum PriceTestEnum
    {
        Ten,
        Twenty,
        Fifty,
        Five,
    }
    private static readonly Random rand = new();
    /// <summary>
    /// Assigns number for color AB test
    /// </summary>
    /// <param name="deviceToken"></param>
    /// <param name="configuration"></param>
    /// <returns>true if success, false if user is absent in db</returns>
    private static bool AssignPrice(string deviceToken, IConfiguration configuration)
    {
        MyDbContext context = ContextFactory.New(configuration);
        User? user = context.Users.FirstOrDefault(x => x.DeviceToken == deviceToken);

        if (user is null || user.CreatedAt < DateTime.Parse(configuration["PriceTestStart"]))
        {
            return false;
        }

        PriceTestModel priceTest = new()
        {
            Id = user.UserId,
            DeviceToken = deviceToken,
        };

        int groupInt = rand.Next(0, 100);
        priceTest.Group = groupInt switch
        {
            <= 5 => PriceTestEnum.Fifty,
            <= 15 => PriceTestEnum.Twenty,
            <= 25 => PriceTestEnum.Five,
            _ => PriceTestEnum.Ten
        };

        context.PriceTest.Add(priceTest);
        context.SaveChanges();

        return true;
    }


    /// <summary>
    /// Gets user experiment variable and converts it to price to be assigned on frontend
    /// </summary>
    /// <param name="deviceToken"></param>
    /// <param name="configuration"></param>
    /// <returns>Result with price to send</returns>
    internal static Result<int?> GetPrice(string deviceToken, IConfiguration configuration)
    {
        if (deviceToken is null)
        {
            return new Result<int?>(false, null, "deviceToken is null");
        }
        MyDbContext context = ContextFactory.New(configuration);

        User? user = context.Users.FirstOrDefault(x => x.DeviceToken == deviceToken);

        //if user is old, he don't know about test
        if (user.CreatedAt < DateTime.Parse(configuration["PriceTestStart"]))
        {
            return new Result<int?>(false, null, "Test started after user registered");
        }

        PriceTestModel? priceTest = context.PriceTest.FirstOrDefault(x => x.User.DeviceToken == deviceToken);

        if (user is null)
        {
            return new Result<int?>(false, null, "user is missing");
        }
        //TODO rewrite (?)
        if (priceTest is null)
        {
            return new Result<int?>(false, null, "priceTest is missing");
        }

        int price = priceTest.Group switch
        {
            PriceTestEnum.Fifty => 50,
            PriceTestEnum.Twenty => 20,
            PriceTestEnum.Ten => 10,
            PriceTestEnum.Five => 5,
        };

        return new Result<int?>(
            true,
            price,
            "User and price founded"
        );
    }

    private static bool IsAssigned(string DeviceToken, IConfiguration configuration)
    {
        MyDbContext context = ContextFactory.New(configuration);
        return context.PriceTest.Any(x => x.DeviceToken == DeviceToken);
    }

    internal static Result<int?> GetPriceViaFingerprint(BrowserFingerprintDto fingerprintDto, IConfiguration configuration, HttpContext httpContext)
    {
        BrowserFingerprint fingerprint = new(fingerprintDto.DeviceToken, fingerprintDto, httpContext);

        if (FingerprintService.IsExists(fingerprint, configuration))
        {
            Result<BrowserFingerprint> res = FingerprintService.IsSimilarToAnyIfYesUpdate(fingerprint, configuration);
            if (!IsAssigned(res.Data.DeviceToken, configuration))
            {
                AssignPrice(res.Data.DeviceToken, configuration);
            }
            if (res.IsSuccess)
            {
                var price2 = GetPrice(res.Data.DeviceToken, configuration).Data;
                return new Result<int?>(true, price2, "all ok");
            }
        }

        User user = UserService.Register(configuration);
        if (!IsAssigned(user.DeviceToken, configuration))
        {
            PriceTest.AssignPrice(user.DeviceToken, configuration);
        }
        fingerprint.DeviceToken = user.DeviceToken;
        FingerprintService.Register(fingerprint, user, configuration);
        var price = GetPrice(fingerprint.DeviceToken, configuration).Data;

        return new Result<int?>(true, price, "all ok");
    }

}
