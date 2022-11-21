import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { House } from 'src/app/models/House';
import { baseURL } from '../base-url';



@Injectable({
  providedIn: 'root'
})
export class HouseListService {

  constructor(private http: HttpClient) { }


   //Display all Houses
  public getHouse(): Observable<House[]> {

    return this.http.get<House[]>(baseURL + '/get-allhouses')

  }


}
