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

    internal static Result<float> Register(BrowserFingerprint fingerprint, User user, IConfiguration configuration)
    {
        MyDbContext context = ContextFactory.New(configuration);
        bool isExists = context.Fingerprints.Any(x => x.DeviceToken == fingerprint.DeviceToken);

        float similarityPercent = 0F;
        if (isExists)
        {
            var dbFingerprint = context.Fingerprints.Find(fingerprint.DeviceToken);
            similarityPercent = CompareFingerprints(fingerprint, dbFingerprint);
        }

        fingerprint.Id = user.UserId;
        context.Fingerprints.Add(fingerprint);
        context.SaveChanges();

        return new Result<float>(true, similarityPercent, "");
    }

    internal static Result<BrowserFingerprint> IsSimilarToAny(BrowserFingerprint receievedFingerpint, IConfiguration configuration)
    {
        MyDbContext context = ContextFactory.New(configuration);
        BrowserFingerprint storedFingerprint = new();

        if (receievedFingerpint.Id is not null) storedFingerprint = context.Fingerprints.Find(receievedFingerpint.Id);
        if (receievedFingerpint.DeviceToken is not null) storedFingerprint = context.Fingerprints.FirstOrDefault(x => x.DeviceToken == receievedFingerpint.DeviceToken);
        if (receievedFingerpint.Ip is not null) storedFingerprint = context.Fingerprints.FirstOrDefault(x => x.Ip == receievedFingerpint.Ip);

        float similarity = CompareFingerprints(receievedFingerpint, storedFingerprint);

        const float minSimilarity = 0.90F;
        bool isSimilar = similarity > minSimilarity;
        if (similarity != 1F && isSimilar)
        {
            receievedFingerpint.Id = storedFingerprint.Id;
            context.Fingerprints.Update(receievedFingerpint);
            context.SaveChanges();
        }

        receievedFingerpint.DeviceToken = storedFingerprint.DeviceToken;
        return new Result<BrowserFingerprint>(isSimilar, receievedFingerpint, "");
    }

    internal static float CompareFingerprints(BrowserFingerprint? receivedFingerprint, BrowserFingerprint? storedFingerprint)
    {
        if (receivedFingerprint is null && storedFingerprint is null)
        {
            return 1F;
        }
        else if (receivedFingerprint is null || storedFingerprint is null)
        {
            return 0F;
        }

        var totalProperties = typeof(BrowserFingerprint).GetProperties().Length;
        var matchingProperties = 0;

        int iterations = 1;
        foreach (var property in typeof(BrowserFingerprint).GetProperties())
        {
            var value1 = property.GetValue(receivedFingerprint);
            var value2 = property.GetValue(storedFingerprint);
            var name = property.Name;
            //skip id check, because received would not have an id
            if (name is "User" or "Id" or "DeviceToken")
            {
                iterations++;
                totalProperties--;
                continue;
            }
            iterations++;
            if (value1 is not null && value2 is not null && value1.Equals(value2))
            {
                matchingProperties++;
            }
            //if there are no info about properties, we don't count them
            else if (value1 is null && value2 is null)
            {
                totalProperties--;
            }
            else if (value1 is null || value2 is null)
            {
                //can't match null and not null
                continue;
            }
            //there is an array of plugins and it's block for this exact array
            else if (value1.GetType().IsArray && value2.GetType().IsArray)
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