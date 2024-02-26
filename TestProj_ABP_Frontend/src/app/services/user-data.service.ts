import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserData } from '../models/userData.model';
import { enviroment } from '../enviroments/enviroment';

@Injectable({
  providedIn: 'root'
})
export class UserDataService {

  baseApiUrl: string = enviroment.baseApiUrl;

  constructor(private http: HttpClient) { }

  public GetData(): Observable<UserData[]>{
    return this.http.get<UserData[]>(this.baseApiUrl + '/experiment/data');
  }
}
