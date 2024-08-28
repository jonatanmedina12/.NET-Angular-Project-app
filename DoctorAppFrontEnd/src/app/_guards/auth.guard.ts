import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { CompartidoService } from '../compartido/compartido.service';
import jwtDecode from 'jwt-decode';
import { CookieService } from 'ngx-cookie-service';
export const authGuard: CanActivateFn = (route, state) => {
 
  const compartidoService = inject(CompartidoService);
  const router = inject(Router);
  const usuario = compartidoService.obtenerSesion();
  const cookieService = inject(CookieService)
  let token = cookieService.get('Authorization')

  if( token &&usuario){
    token = token.replace('Bearer','');
    const decodedToken: any = jwtDecode(token) 
    const  fechaExperiacion = decodedToken.exp * 1000;
    const fecha_ACTUAL = new Date().getTime();
    if(fechaExperiacion<fecha_ACTUAL){
      router.navigate(['login']);
      return false;
    }
    return true;
  }else{
    router.navigate(['login']);
    return false;
  }
  return true;
};
