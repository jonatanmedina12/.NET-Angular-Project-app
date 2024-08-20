import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { enviroment } from 'src/env/enviroment.prod';
import { Login } from '../interfaces/login';
import { Observable } from 'rxjs';
import { Sesion } from '../interfaces/sesion';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  baseUrl: string = enviroment.apiUrl+'Usuario/'

  constructor(private http:HttpClient) { }

  iniciarSesion(request:Login):Observable<Sesion>{
    return this.http.post<Sesion>(`${this.baseUrl}login`,request)

  }
}
