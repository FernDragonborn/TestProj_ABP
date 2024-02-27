import { Component, OnInit } from '@angular/core';
import { FingerprintService } from '../../services/fingerprint-service.service';
import { ButtonColorService } from '../../services/button-color.service';
import { PriceTestService } from '../../services/price.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})

export class ProductsComponent {
  public fingerprint: any;
  public price: number | undefined;

  constructor(
    private fingerprintService: FingerprintService,
    private buttonColorService: ButtonColorService,
    private priceService: PriceTestService) 
  {  }
  public buttonColor: string | undefined = '';

  public buttonStyle: any = {
    'background-color': '#' + this.buttonColor,
    'border-color':  this.buttonColor
  }
  
  ngOnInit(): void {
    this.buttonColorService.GetButtonColor()
    .subscribe((resp: string) => {
      this.buttonColor = resp
      console.log(resp);
    },
    (error) =>{
      console.log(error);
      this.fingerprintService.GetColorViaFingerprint()
      .subscribe((resp : string) => {
        this.buttonColor = resp;
        console.log(resp);
        },
        (error) =>{
          console.error('Error occured on get color via fingerprint', error)
        }
      )
    })

    this.priceService.GetPrice()
      .subscribe((resp: number) =>
        {
          this.price = resp
        },
        (error) => {
          console.log(error);
          this.fingerprintService.GetPriceViaFingerprint()
            .subscribe((resp : number) => {
              this.price = resp;
              console.log(resp);
              },
              (error) => console.error('Error occured on get price via fingerprint', error)
            )
        }
        )
  }

}
