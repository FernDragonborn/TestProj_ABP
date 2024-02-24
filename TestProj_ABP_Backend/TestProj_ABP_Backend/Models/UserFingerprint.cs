namespace TestProj_ABP_Backend.Models;
public class UserFingerprint
{
    public string? userAgent;
    public string? language;
    public int? screenWidth;
    public int? screenHeight;
    public int? colorDepth;
    public int? pixelRatio;
    public int? orintation;
    public string? browserName;
    public string? browserVersion;
    public string? os;
    public string? timeZone;
    public string? numberFormat;
    public string? onlineStatus;
    public string?[] plugins;
    public bool? isCookieEnabled;
    public string? defaultLocale;
    public string? collatorInfo;
    public string? listFormatInfo;
    public string? relativeTimeFormatInfo;
    public string? pluralRulesInfo;
}
