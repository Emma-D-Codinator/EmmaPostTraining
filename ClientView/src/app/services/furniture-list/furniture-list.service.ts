import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Furniture } from 'src/app/models/Furniture';
import { baseURL } from '../base-url';

@Injectable({
  providedIn: 'root'
})
export class FurnitureListService {

  constructor(private http: HttpClient) { }


  //Display furniture by House
  public getFurnitureByHouse( House_name_type : string): Observable<Furniture> {
    
    return this.http.get<Furniture>(`${baseURL}/get-allfurnituresbyhouse?House_name_type=` + House_name_type)
    
  }

  //Get furniture count by type
  public getFurnitureCountByType( furniture_name_type : string) : Observable<number> {
    
    return this.http.get<number>(`${baseURL}/Count-furniturebytype?Furniture_name_type=` + furniture_name_type)
    
  }


  //Create furniture 
  public createFurniture( furniture : any) : Observable<any> {
    
    return this.http.post<Furniture>(`${baseURL}/Create-Furniture`, furniture)
    
  }



}
