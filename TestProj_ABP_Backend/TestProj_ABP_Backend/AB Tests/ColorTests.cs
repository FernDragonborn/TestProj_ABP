using TestProj_ABP_Backend.DbContext;
using TestProj_ABP_Backend.Models;
using TestProj_ABP_Backend.Services;

namespace TestProj_ABP_Backend.AB_Tests;

public static class ColorTest
{
    private static readonly Random rand = new Random();
    private static int assignedCount = 0;
    /// <summary>
    /// Assigns number for color AB test
    /// </summary>
    /// <param name="deviceToken"></param>
    /// <param name="configuration"></param>
    /// <returns>true if success, false if user is absent in db</returns>
    public static bool AssignColor(string deviceToken, IConfiguration configuration)
    {
        if (assignedCount is 0 or > 2000000000)
        {
            assignedCount = rand.Next(1, 3);
        }
        MyDbContext context = ContextFactory.New(configuration);
        User? user = context.Users.FirstOrDefault(x => x.DeviceToken == deviceToken);
        if (user is null)
        {
            return false;
        }

        int modulo = assignedCount % 3;
        user.Experiment[0] = modulo switch
        {
            1 => '1',
            2 => '2',
            3 => '3',
        };

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
    public static Result<string> GetColor(string deviceToken, IConfiguration configuration)
    {
        if (deviceToken is null)
        {
            return new Result<string>("", false, "device token is null");
        }
        MyDbContext context = ContextFactory.New(configuration);
        User? user = context.Users.FirstOrDefault(x => x.DeviceToken == deviceToken);

        if (user is null)
        {
            return new Result<string>("", false, "user is missing");
        }


        //I used string in sql for multiple experiments.
        //In such case, it's first experiment, so we watch first char of string and assign value
        //There's a lot variant of chars, so I assumed that would be okay varian for tests
        //P.S.: and we can contain many tests, for each user in this field

        //P.P.S.: if value is 0, than user do not take part in AB test
        string color = user.Experiment[0] switch
        {
            '1' => "FF0000",
            '2' => "00FF00",
            '3' => "0000FF",
            '0' => "",
            _ => throw new ArgumentException("user.Experiment[0] is missing")
        };

        return new Result<string>(
            color,
            true,
            "User founded"
        );
    }

}
