import { Component, OnInit } from '@angular/core';
import { FingerprintService } from '../../services/fingerprint-service.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrl: '../../css/app.component.min.css'
})

export class ProductsComponent {
  public fingerprint: string = "";

  constructor(private fingerprintService: FingerprintService) {  }

  ngOnInit(): void {
    this.fingerprint = this.fingerprintService.getFingerprint();
    console.log('User Fingerprint:', this.fingerprint);
    // Use the fingerprint as needed
  }
}
