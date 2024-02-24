export class UserFingerprintDto{
    userAgent: string | undefined = undefined;
    language: string | undefined = undefined;
    screenWidth: number | undefined = undefined;
    screenHeight: number | undefined = undefined;
    colorDepth: number | undefined = undefined;
    pixelRatio: number | undefined = undefined;
    orintation: number | undefined = undefined;
    browserName: string | undefined = undefined;
    browserVersion: string | undefined = undefined;
    os: string | undefined = undefined;
    timeZone: string | undefined = undefined;
    numberFormat: string | undefined = undefined;
    onlineStatus: boolean | undefined = undefined;
    plugins: string[] | undefined = undefined;
    isCookieEnabled: boolean | undefined = undefined;
    defaultLocale: string | undefined = undefined;
    collatorInfo: string | undefined = undefined;
    listFormatInfo: string | undefined = undefined;
    relativeTimeFormatInfo: string | undefined = undefined;
    pluralRulesInfo: string | undefined = undefined;
}