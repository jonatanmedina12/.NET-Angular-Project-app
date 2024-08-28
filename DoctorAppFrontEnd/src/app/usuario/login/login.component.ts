import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Route, Router } from '@angular/router';
import { CompartidoService } from 'src/app/compartido/compartido.service';
import { Login } from '../interfaces/login';
import { UsuarioService } from '../servicios/usuario.service';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {


formLogin:FormGroup;

ocultarPaswword: boolean=true;
mostrarLoading:boolean=false;
constructor(private fb :FormBuilder,private router:Router, private usuarioServicio:UsuarioService ,private compartidoService:CompartidoService,
  private cookieService:CookieService
){


  this.formLogin=this.fb.group({
    username:['',Validators.required],
    password:['',Validators.required]
  })
}

iniciarSesion(){
  this.mostrarLoading=true;
  const request:Login={
    username:this.formLogin.value.username,
    password:this.formLogin.value.password
  };
  this.usuarioServicio.iniciarSesion(request).subscribe({
    next:(response)=>{
      this.compartidoService.guardarSesion(response)
      
      this.cookieService.set(
        'Authorization',
        `Bearer ${response.token}`,
        undefined,
        '/',
        undefined,
        true,
        'Strict'
      )


      this.router.navigate(['layout'])
    },
    complete:()=>{
      this.mostrarLoading=false;
    },
    error:(error)=>{
      this.compartidoService.mostrarAlerta(error.error,'Error!');
      this.mostrarLoading=false;
    }
  })
}





}
