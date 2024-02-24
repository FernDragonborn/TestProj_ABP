using System.ComponentModel.DataAnnotations;
using TestProj_ABP_Backend.DTOs;
using TestProj_ABP_Backend.Services;

namespace TestProj_ABP_Backend.Models;
public class UserFingerprint
{
    public UserFingerprint() { }

    public UserFingerprint(string deviceToken, UserFingerprintDto dto, IHttpContextAccessor httpContextAccessor)
    {
        DeviceToken = deviceToken;
        UserAgent2 = UserService.GetUserAgent(httpContextAccessor);
        Ip = UserService.GetUserIp(httpContextAccessor);
        UserAgent = dto.UserAgent;
        Language = dto.Language;
        ScreenWidth = dto.ScreenWidth;
        ScreenHeight = dto.ScreenHeight;
        ColorDepth = dto.ColorDepth;
        PixelRatio = dto.PixelRatio;
        Orintation = dto.Orintation;
        BrowserName = dto.BrowserName;
        BrowserVersion = dto.BrowserVersion;
        Os = dto.Os;
        TimeZone = dto.TimeZone;
        NumberFormat = dto.NumberFormat;
        OnlineStatus = dto.OnlineStatus;
        Plugins = dto.Plugins;
        IsCookieEnabled = dto.IsCookieEnabled;
        DefaultLocale = dto.DefaultLocale;
        CollatorInfo = dto.CollatorInfo;
        ListFormatInfo = dto.ListFormatInfo;
        RelativeTimeFormatInfo = dto.RelativeTimeFormatInfo;
        PluralRulesInfo = dto.PluralRulesInfo;
    }
    [Key]
    public string DeviceToken { get; set; }
    public string? Ip { get; set; }
    public string? UserAgent { get; set; }
    public string? UserAgent2 { get; set; }
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
