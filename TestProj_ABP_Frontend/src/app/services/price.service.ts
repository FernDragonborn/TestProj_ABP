import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { enviroment } from '../enviroments/enviroment';

@Injectable({
  providedIn: 'root'
})
export class PriceTestService {

  baseApiUrl: string = enviroment.baseApiUrl;
  
  constructor(private http: HttpClient) { }

  public GetPrice(): Observable<number>{
    return this.http.get<{key: string, value: number}>(this.baseApiUrl + '/experiment/price')
    .pipe(
      map((response: {key: string, value: number}) => response.value)
    )
  }
}
