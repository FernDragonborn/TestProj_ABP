namespace TestProj_ABP_Backend.DTOs;
public struct BrowserFingerprintDto
{
    public string? DeviceToken { get; set; }
    public string? Language { get; set; }
    public int? ScreenWidth { get; set; }
    public int? ScreenHeight { get; set; }
    public int? ColorDepth { get; set; }
    public float? PixelRatio { get; set; }
    public int? Orientation { get; set; }
    public string? BrowserName { get; set; }
    public string? BrowserVersion { get; set; }
    public string? Os { get; set; }
    public string? TimeZone { get; set; }
    public string? NumberFormat { get; set; }
    public bool? OnlineStatus { get; set; }
    public string?[] Plugins { get; set; }
    public bool? IsCookieEnabled { get; set; }
    public string? DefaultLocale { get; set; }
    public string? CollatorInfo { get; set; }
    public string? ListFormatInfo { get; set; }
    public string? RelativeTimeFormatInfo { get; set; }
    public string? PluralRulesInfo { get; set; }
}
