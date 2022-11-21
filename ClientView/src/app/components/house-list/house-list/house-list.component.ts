import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FurnitureListService } from 'src/app/services/furniture-list/furniture-list.service';
import { HouseListService } from 'src/app/services/house-list/house-list.service';

@Component({
  selector: 'app-house-list',
  templateUrl: './house-list.component.html',
  styleUrls: ['./house-list.component.css']
})


export class HouseListComponent implements OnInit {


  houses: any;
 


  constructor( 
    private HLS : HouseListService
    
    ) { }


  ngOnInit() : void {

    this.displayHouses();

  }

  //Displays all houses
  displayHouses(): void {
    this.HLS.getHouse().subscribe((data: any) => {
      this.houses = data;
    })
  }


   



}
