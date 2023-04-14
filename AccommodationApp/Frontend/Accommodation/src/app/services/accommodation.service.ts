import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateAccommodationDTO } from '../model/create-accommodation';

@Injectable({
  providedIn: 'root'
})
export class AccommodationService {
  
  private accommUrl = 'http://localhost:5000/gateway/';
  
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'multipart/form-data' })
  };

  constructor(private http: HttpClient) { }
 
  
  Post(accommDto: CreateAccommodationDTO, images: File[]) {
    const form = new FormData;
    
    for(let image of images){
      form.append('file', image);
    }
    form.append('accomm-data', JSON.stringify(accommDto));

    return this.http.post(this.accommUrl+'create-accommodation', form);
  }
}