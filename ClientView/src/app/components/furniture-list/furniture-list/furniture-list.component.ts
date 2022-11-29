import { Furniture } from 'src/app/models/Furniture';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FurnitureListService } from 'src/app/services/furniture-list/furniture-list.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';


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
  clickindex: any;
  
  furniture: any;

  addFurnitureForm = new FormGroup({
    furnitureID: new FormControl('', [Validators.required]),
    furniture_name_type: new FormControl('', [Validators.required]),
    furniturePrice: new FormControl('', [Validators.required]),
    furnitureImage: new FormControl('', [Validators.required]),
    furnitureFootage: new FormControl('', [Validators.required]),
    houseID : new FormControl('', [Validators.required]),
  });
  
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
    console.log(furniture_name_type);
    this.FLS.getFurnitureCountByType(furniture_name_type).subscribe((data: any) => {    
      this.furniturecountbytype = data;
      console.log(this.furniturecountbytype);
    })
  }






}
