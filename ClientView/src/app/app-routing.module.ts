import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FurnitureListComponent } from './components/furniture-list/furniture-list/furniture-list.component';
import { HouseListComponent } from './components/house-list/house-list/house-list.component';

const routes: Routes = [
  {path: '', component: HouseListComponent},
  {path: 'house/:furniture', component: FurnitureListComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
