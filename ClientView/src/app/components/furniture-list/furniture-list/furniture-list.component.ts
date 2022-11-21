import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FurnitureListService } from 'src/app/services/furniture-list/furniture-list.service';

@Component({
  selector: 'app-furniture-list',
  templateUrl: './furniture-list.component.html',
  styleUrls: ['./furniture-list.component.css']
})
export class FurnitureListComponent implements OnInit {

  furnituresbyhouses: any;
  furniturecountbytype: any;
  private House_name_type : any;
  private furniture_name_type : any;

  constructor( 
    private FLS : FurnitureListService,
    private route: ActivatedRoute
    ) { }

  ngOnInit() : void {
      
    this.House_name_type = this.route.snapshot.paramMap.get("furniture");
    this.FLS.getFurnitureByHouse(this.House_name_type)   
    this.displayFurnituresByHouse(this.House_name_type)
    this.displayFurniturecountsByType(this.furniture_name_type)
  }


  //Displays all furnitures by houses
  displayFurnituresByHouse(House_name_type : string): void {
    this.FLS.getFurnitureByHouse(House_name_type).subscribe((data: any) => {
      this.furnituresbyhouses = data;
    })
  }



  //Displays all furnitures Count by type
  displayFurniturecountsByType(furniture_name_type : string): void {
    this.FLS.getFurnitureByHouse(furniture_name_type).subscribe((data: any) => {
      this.furniturecountbytype = data;
    })
  }



}
