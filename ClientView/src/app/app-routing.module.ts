import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FurnitureCreateComponent } from './components/furniture-create/furniture-create/furniture-create.component';
import { FurnitureListComponent } from './components/furniture-list/furniture-list/furniture-list.component';
import { HouseCreateComponent } from './components/house-create/house-create/house-create.component';
import { HouseListComponent } from './components/house-list/house-list/house-list.component';
import { HouseSearchComponent } from './components/house-search/house-search/house-search.component';

const routes: Routes = [
  {path: '', component: HouseListComponent},
  {path: 'house/:furniture', component: FurnitureListComponent},
  {path: 'createfurniture', component: FurnitureCreateComponent},
  {path: 'findhouse', component: HouseSearchComponent},
  {path: 'createhouse', component: HouseCreateComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
