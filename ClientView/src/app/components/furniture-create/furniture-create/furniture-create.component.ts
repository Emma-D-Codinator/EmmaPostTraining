import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Furniture } from 'src/app/models/Furniture';
import { FurnitureListService } from 'src/app/services/furniture-list/furniture-list.service';

@Component({
  selector: 'app-furniture-create',
  templateUrl: './furniture-create.component.html',
  styleUrls: ['./furniture-create.component.css']
})
export class FurnitureCreateComponent {


  furniture: any;
  fpostmessage: any;
  furnitureImage = null; 
  addFurnitureForm!: FormGroup;


   
  constructor( 
    private FLS : FurnitureListService,
    private route: ActivatedRoute,
    private formBuilder : FormBuilder
    ) { }




  ngOnInit() : void {
    this.addFurnitureForm = this.formBuilder.group({
      furnitureID: ['', Validators.required],
      furniture_name_type: ['', Validators.required],
      furniturePrice: ['', Validators.required],
      furnitureImage: ['', Validators.required],
      furnitureFootage: ['', Validators.required],
      houseID: ['', Validators.required],

    })
    
  }

   
  onUpLoad(event: any){
    this.furnitureImage = event.target.files[0];
    this.addFurnitureForm.get('furnitureImage')?.setValue(this.furnitureImage);
  }


  //Create new furniture
  createNewFurniture(): void {
    const formData = new FormData();
    formData.append('furnitureID', this.addFurnitureForm.get('furnitureID')?.value)
    formData.append('furniture_name_type', this.addFurnitureForm.get('furniture_name_type')?.value)
    formData.append('furniturePrice', this.addFurnitureForm.get('furniturePrice')?.value)
    formData.append('furnitureImage', this.addFurnitureForm.get('furnitureImage')?.value)
    formData.append('furnitureFootage', this.addFurnitureForm.get('furnitureFootage')?.value)
    formData.append('houseID', this.addFurnitureForm.get('houseID')?.value)
    console.log(formData);
    this.FLS.createFurniture(formData).subscribe((data: any) => {    
      this.fpostmessage = data.message;
      console.log(this.fpostmessage);
    })
  }

}
