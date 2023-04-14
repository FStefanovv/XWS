import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private jwtHelper: JwtHelperService) { }

  isLoggedIn() {
    const token = this.getToken();
    if (token && !this.jwtHelper.isTokenExpired(token)){
      return true;
    }
    return false;
  }

  public decodeToken() {
    const token = this.getToken();
    if(token)
      return this.jwtHelper.decodeToken(token);
  }

  getRole(){
    const decodedToken = this.decodeToken();
    return decodedToken['Role'];
  }

  getUsername(){
    const decodedToken = this.decodeToken();
    return decodedToken['Username'];
  }

  getId(){
    const decodedToken = this.decodeToken()
    return decodedToken['UserId'];
  }

  getToken(){
    return localStorage.getItem("jwt");
  }

  storeToken(token: string){
    localStorage.setItem("jwt", token); 
  }

  logOut(){
    localStorage.removeItem("jwt");
  }
}