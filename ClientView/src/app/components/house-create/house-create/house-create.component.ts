import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { HouseListService } from 'src/app/services/house-list/house-list.service';

@Component({
  selector: 'app-house-create',
  templateUrl: './house-create.component.html',
  styleUrls: ['./house-create.component.css']
})
export class HouseCreateComponent {


  house: any;
  hpostmessage: any;
  houseImage = null; 
  addHouseForm!: FormGroup;


   
  constructor( 
    private HLS : HouseListService,
    private route: ActivatedRoute,
    private formBuilder : FormBuilder
    ) { }




  ngOnInit() : void {
    this.addHouseForm = this.formBuilder.group({
      id: ['', Validators.required],
      house_Name_Type: ['', Validators.required],
      address: ['', Validators.required],
      footage: ['', Validators.required],
      houseImage: ['', Validators.required],
      houseCost: ['', Validators.required],

    })
    
  }

   
  onUpLoad(event: any){
    this.houseImage = event.target.files[0];
    this.addHouseForm.get('houseImage')?.setValue(this.houseImage);
  }


  //Create new house
  createNewHouse(): void {
    const formData = new FormData();
    formData.append('id', this.addHouseForm.get('id')?.value)
    formData.append('house_Name_Type', this.addHouseForm.get('house_Name_Type')?.value)
    formData.append('address', this.addHouseForm.get('address')?.value)
    formData.append('footage', this.addHouseForm.get('footage')?.value)
    formData.append('houseImage', this.addHouseForm.get('houseImage')?.value)
    formData.append('houseCost', this.addHouseForm.get('houseCost')?.value)
    console.log(formData);
    this.HLS.createHouse(formData).subscribe((data: any) => {    
      this.hpostmessage = data.message;
      console.log(this.hpostmessage);
    })
  }


}
