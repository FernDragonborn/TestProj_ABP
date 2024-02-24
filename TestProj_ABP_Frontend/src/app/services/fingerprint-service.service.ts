import { UserFingerprintDto } from '../models/userFingerprint.model';
import { Injectable} from '@angular/core';
@Injectable({
  providedIn: 'root'
})
export class FingerprintService {
  
  public getFingerprint(): UserFingerprintDto  {
    const fingerprintData: UserFingerprintDto = {
      userAgent: navigator.userAgent,
      language: navigator.language,
      screenWidth: window.screen.width,
      screenHeight: window.screen.height,
      colorDepth: window.screen.colorDepth,
      pixelRatio: window.devicePixelRatio,
      orintation: window.screen.orientation.angle,
      browserName: this.getBrowserName(),
      browserVersion: this.getBrowserVersion(),
      os: this.getOperatingSystem(),
      timeZone: Intl.DateTimeFormat().resolvedOptions().timeZone,
      numberFormat: window.Intl.NumberFormat.name,
      onlineStatus: navigator.onLine,
      plugins: this.getInstalledPlugins(),
      isCookieEnabled: navigator.cookieEnabled,
      defaultLocale: window.Intl.DateTimeFormat().resolvedOptions().locale,
      collatorInfo:  Intl.Collator().resolvedOptions().locale,
      listFormatInfo: new Intl.ListFormat().resolvedOptions().locale,
      relativeTimeFormatInfo: new Intl.RelativeTimeFormat().resolvedOptions().locale,
      pluralRulesInfo: new Intl.PluralRules().resolvedOptions().locale,
    };

    return fingerprintData;
  }

  getBrowserName(): string {
    const userAgent = navigator.userAgent;
    if (userAgent.includes('Chrome')) {
      return 'Chrome';
    } else if (userAgent.includes('Firefox')) {
      return 'Firefox';
    } else if (userAgent.includes('Safari')) {
      return 'Safari';
    } else if (userAgent.includes('Edge')) {
      return 'Edge';
    } else {
      return 'Unknown';
    }
  }

  getBrowserVersion(): string {
    const userAgent = navigator.userAgent;
    const match = userAgent.match(/(?:Chrome|Firefox|Safari|Edge)\/([\d.]+)/);
    return match ? match[1] : 'Unknown';
  }

  getOperatingSystem(): string {
    const userAgent = navigator.userAgent;
    if (userAgent.includes('Windows')) {
      return 'Windows';
    } else if (userAgent.includes('Mac OS')) {
      return 'Mac OS';
    } else if (userAgent.includes('Linux')) {
      return 'Linux';
    } else {
      return 'Unknown';
    }
  }

  getInstalledPlugins(): string[] {
    const plugins = [];
    for (let i = 0; i < navigator.plugins.length; i++) {
      plugins.push(navigator.plugins[i].name);
    }
    return plugins;
  }
}
