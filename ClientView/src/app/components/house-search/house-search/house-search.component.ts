import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HouseListService } from 'src/app/services/house-list/house-list.service';

@Component({
  selector: 'app-house-search',
  templateUrl: './house-search.component.html',
  styleUrls: ['./house-search.component.css']
})
export class HouseSearchComponent {

   house : any;
   housereturn: any;

  constructor( 
    private HLS : HouseListService
    
    ) { }

    FindHouseForm = new FormGroup({
      id : new FormControl('', [Validators.required]),
    });
  
 

  displayHouseByID(id : string): void {
    const idint = parseInt(id);
    console.log(idint );
    this.HLS.getHouseByID(idint).subscribe((data: any) => {    
      this.housereturn = data;
      console.log(this.housereturn);
    })
  }


}
