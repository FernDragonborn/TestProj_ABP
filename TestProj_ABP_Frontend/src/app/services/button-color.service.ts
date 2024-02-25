import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map} from 'rxjs';
import { enviroment } from '../enviroments/enviroment';

@Injectable({
  providedIn: 'root'
})
export class ButtonColorService {

  baseApiUrl: string = enviroment.baseApiUrl;

  constructor(private http: HttpClient) {}

  public GetButtonColor(): Observable<string>{
    return this.http.get<{ key: string, value: string }>(this.baseApiUrl + '/experiment/button-color')
      .pipe(
        map((response: { key: string, value: string }) => response.value)
      );
  }

}
