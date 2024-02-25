namespace TestProj_ABP_Backend_Tests;

using TestProj_ABP_Backend.Models;
using TestProj_ABP_Backend.Services;
using Xunit;

public class BrowserFingerprintTests
{
    [Fact]
    public void CompareBrowserFingerprints_BothNull_Returns100Percent()
    {
        // Arrange
        BrowserFingerprint? user1 = null;
        BrowserFingerprint? user2 = null;

        // Act
        var result = FingerprintService.CompareFingerprints(user1, user2);

        // Assert
        Assert.Equal(1F, result);
    }

    [Fact]
    public void CompareBrowserFingerprints_OneNull_Returns0Percent()
    {
        // Arrange
        var user1 = new BrowserFingerprint();
        BrowserFingerprint? user2 = null;

        // Act
        var result = FingerprintService.CompareFingerprints(user1, user2);

        // Assert
        Assert.Equal(0F, result);
    }

    [Fact]
    public void CompareBrowserFingerprints_AllPropertiesMatch_Returns100Percent()
    {
        // Arrange
        var user1 = new BrowserFingerprint
        {
            DeviceToken = "token1",
            Ip = "127.0.0.1",
            UserAgent = "Mozilla/5.0",
            UserAgent2 = "Chrome/91.0",
            Language = "en-US",
            ScreenWidth = 1920,
            ScreenHeight = 1080,
            ColorDepth = 32,
            PixelRatio = 2,
            Orintation = 0,
            BrowserName = "Chrome",
            BrowserVersion = "91.0",
            Os = "Windows",
            TimeZone = "UTC",
            NumberFormat = "decimal",
            OnlineStatus = "online",
            Plugins = new[] { "Plugin1", "Plugin2" },
            IsCookieEnabled = true,
            DefaultLocale = "en-US",
            CollatorInfo = "collatorInfo",
            ListFormatInfo = "listFormatInfo",
            RelativeTimeFormatInfo = "relativeTimeFormatInfo",
            PluralRulesInfo = "pluralRulesInfo",
        };

        var user2 = new BrowserFingerprint
        {
            DeviceToken = "token1",
            Ip = "127.0.0.1",
            UserAgent = "Mozilla/5.0",
            UserAgent2 = "Chrome/91.0",
            Language = "en-US",
            ScreenWidth = 1920,
            ScreenHeight = 1080,
            ColorDepth = 32,
            PixelRatio = 2,
            Orintation = 0,
            BrowserName = "Chrome",
            BrowserVersion = "91.0",
            Os = "Windows",
            TimeZone = "UTC",
            NumberFormat = "decimal",
            OnlineStatus = "online",
            Plugins = new[] { "Plugin1", "Plugin2" },
            IsCookieEnabled = true,
            DefaultLocale = "en-US",
            CollatorInfo = "collatorInfo",
            ListFormatInfo = "listFormatInfo",
            RelativeTimeFormatInfo = "relativeTimeFormatInfo",
            PluralRulesInfo = "pluralRulesInfo",
        };

        // Act
        var result = FingerprintService.CompareFingerprints(user1, user2);

        // Assert
        Assert.Equal(1F, result);
    }

    [Fact]
    public void CompareBrowserFingerprints_NoPropertiesMatch_Returns0Percent()
    {
        // Arrange
        var user1 = new BrowserFingerprint
        {
            DeviceToken = "token1",
            Ip = "127.0.0.1",
            UserAgent = "Mozilla/5.0",
            // Set other properties
        };

        var user2 = new BrowserFingerprint
        {
            DeviceToken = "token2",
            Ip = "192.168.1.1",
            UserAgent = "Chrome/91.0",
            // Set different properties
        };

        // Act
        var result = FingerprintService.CompareFingerprints(user1, user2);

        // Assert
        Assert.Equal(0F, result);
    }
}
