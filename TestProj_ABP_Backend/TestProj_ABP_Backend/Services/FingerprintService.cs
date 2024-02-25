using TestProj_ABP_Backend.DbContext;
using TestProj_ABP_Backend.Models;

namespace TestProj_ABP_Backend.Services;

internal static class FingerprintService
{
    internal static bool IsExists(BrowserFingerprint fingerprint, IConfiguration configuration)
    {
        MyDbContext context = ContextFactory.New(configuration);

        if (fingerprint.DeviceToken is not null)
        {
            return context.Fingerprints.Any(x => x.DeviceToken == fingerprint.DeviceToken);
        }
        else if (fingerprint.Ip is not null)
        {
            return context.Fingerprints.Any(x => x.Ip == fingerprint.Ip);
        }
        else if (fingerprint.UserAgent is not null)
        {
            return context.Fingerprints.Any(x => x.UserAgent == fingerprint.UserAgent);
        }
        else
        {
            return false;
        }
    }

    internal static Result Register(BrowserFingerprint fingerprint, IConfiguration configuration)
    {
        MyDbContext context = ContextFactory.New(configuration);
        bool isExists = context.Fingerprints.Any(x => x.DeviceToken == fingerprint.DeviceToken);

        if (isExists)
        {
            var dbFingerprint = context.Fingerprints.Find(fingerprint.DeviceToken);
            float similarityPercent = CompareFingerprints(fingerprint, dbFingerprint);
        }

        context.Fingerprints.Add(fingerprint);
        context.SaveChanges();

        return new Result(true, "");
    }

    internal static bool IsSimilarToAny(BrowserFingerprint receievedFingerpint, IConfiguration configuration)
    {
        MyDbContext context = ContextFactory.New(configuration);
        var strodredFingerprint = context.Fingerprints.Find(receievedFingerpint.DeviceToken);

        float similarity = CompareFingerprints(receievedFingerpint, strodredFingerprint);

        const float minSimilarity = 0.90F;
        bool isSimilar = similarity > minSimilarity;
        if (similarity != 1F && isSimilar)
        {
            context.Fingerprints.Update(receievedFingerpint);
            context.SaveChanges();
        }

        return isSimilar;
    }

    internal static float CompareFingerprints(BrowserFingerprint? user1, BrowserFingerprint? user2)
    {
        if (user1 is null && user2 is null)
        {
            return 1F;
        }
        else if (user1 is null || user2 is null)
        {
            return 0F;
        }

        var totalProperties = typeof(BrowserFingerprint).GetProperties().Length;
        var matchingProperties = 0;

        foreach (var property in typeof(BrowserFingerprint).GetProperties())
        {
            var value1 = property.GetValue(user1);
            var value2 = property.GetValue(user2);

            if (value1 is not null && value2 is not null && value1.Equals(value2))
            {
                matchingProperties++;
            }
            //if there are no info about properties, we don't count them
            else if (value1 is null && value2 is null)
            {
                totalProperties--;
            }
            //there is an array of plugins and it's block for this exact array
            else if (value1.GetType().IsArray && value1.GetType().IsArray)
            {
                if (Enumerable.SequenceEqual(
                        (IEnumerable<string>)value1,
                        (IEnumerable<string>)value2))
                {
                    matchingProperties++;
                }
            }
        }

        //TODO needs to rewrite this ifs, vecause I'm not sure of side effects, when both are thiout properties. 
        //Maybe it's more reasonable to update context, then?
        if (matchingProperties == 0 && totalProperties == 0) return 1F;
        if (matchingProperties == 0 || totalProperties == 0) return 0F;

        float similarity = (float)matchingProperties / totalProperties;
        if (similarity > 1F) throw new InvalidOperationException("critical error in fingerprint comparison");

        return similarity;
    }
}