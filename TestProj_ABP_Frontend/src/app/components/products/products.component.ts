import { Component, OnInit } from '@angular/core';
import { FingerprintService } from '../../services/fingerprint-service.service';
import { ButtonColorService } from '../../services/button-color.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrl: '../../css/app.component.min.css'
})

export class ProductsComponent {
  public fingerprint: any;

  constructor(
    private fingerprintService: FingerprintService,
    private buttonColorService: ButtonColorService) 
  {  }

  public buttonColor: string | undefined;
  ngOnInit(): void {
    this.buttonColorService.GetButtonColor()
    .subscribe((color) => {
      this.buttonColor = color
      console.log(color);
    },
    (error) =>{
      this.fingerprintService.GetColorViaFingerprint()
      .subscribe((color) => {
        this.buttonColor = color
        console.log(color);
      },
      (error) =>{
        console.error('Error occured on get color via fingerprint', error)
      }
      )
    })
    this.fingerprintService.GetColorViaFingerprint()
    // Use the fingerprint as needed
  }

}
