namespace TestProj_ABP_Backend.DTOs;
public struct UserFingerprintDto
{
    public string? UserAgent { get; set; }
    public string? Language { get; set; }
    public int? ScreenWidth { get; set; }
    public int? ScreenHeight { get; set; }
    public int? ColorDepth { get; set; }
    public int? PixelRatio { get; set; }
    public int? Orintation { get; set; }
    public string? BrowserName { get; set; }
    public string? BrowserVersion { get; set; }
    public string? Os { get; set; }
    public string? TimeZone { get; set; }
    public string? NumberFormat { get; set; }
    public string? OnlineStatus { get; set; }
    public string?[] Plugins { get; set; }
    public bool? IsCookieEnabled { get; set; }
    public string? DefaultLocale { get; set; }
    public string? CollatorInfo { get; set; }
    public string? ListFormatInfo { get; set; }
    public string? RelativeTimeFormatInfo { get; set; }
    public string? PluralRulesInfo { get; set; }
}
