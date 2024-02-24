import { Component, OnInit } from '@angular/core';
import { FingerprintService } from '../../services/fingerprint-service.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrl: '../../css/app.component.min.css'
})

export class ProductsComponent {
  public fingerprint: any;

  constructor(private fingerprintService: FingerprintService) {  }

  ngOnInit(): void {
    this.fingerprintService.getFingerprint()
    // Use the fingerprint as needed
  }

}
