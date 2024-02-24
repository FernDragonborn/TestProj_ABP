import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FingerprintService {
  private clientJS: any;
  
  constructor() {
    const ClientJS = require('clientjs');
    this.clientJS = new ClientJS();
  }

  public getFingerprint(): string {
    // Get fingerprint components
    const fingerprintComponents = this.clientJS.getFingerprint();

    // Combine fingerprint components into a string
    const fingerprint = fingerprintComponents.join('_');

    return fingerprint;
  }
}
