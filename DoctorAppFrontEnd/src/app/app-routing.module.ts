import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './usuario/login/login.component';
import {} from '../app/especialidad/especialidad.module'
const routes: Routes = [
  {
    path:'',component:LoginComponent,pathMatch:'full'
  },
  {

    path:'login',component:LoginComponent,pathMatch:'full'

  },
  {
    path:'layout',loadChildren:()=> import('./compartido/compartido.module').then(m=>m.CompartidoModule) //layout/dasboard
  },
  {
    path:'**',
    redirectTo:'',
    pathMatch:'full'
  }


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
