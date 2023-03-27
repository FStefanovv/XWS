import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';


import { Credentials } from '../model/credentials';
import { catchError, Observable, throwError } from 'rxjs';
import { TokenDTO } from '../model/tokenDto';



@Injectable({
  providedIn: 'root'
})
export class UserService {

  private usersUrl = 'http://localhost:5000/api/Users/';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  
  constructor(private http: HttpClient) { }


  LogIn(credentials: Credentials) : Observable<TokenDTO> {
    
    return this.http.post<TokenDTO>(this.usersUrl + 'login', credentials, this.httpOptions);
  }
}