import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Technology } from '../models';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TechnologyService {
    public currentTechnology$: BehaviorSubject<Technology> = new BehaviorSubject<Technology>(null);
    public technologies$ = new BehaviorSubject<Array<Technology>>([]);
    private url: string = '/technologies';

    constructor(private http: HttpClient) { }
  
    public post(technology: Technology) : Observable<Technology> {
      return this.http.post<Technology>(`${this.url}`, technology).pipe(
        map(res => { 
          let values = this.technologies$.getValue();
          let index = values.findIndex(s => s.id === res.id);
          if(index > -1) {
            values[index] = res;
          } else {
            values.push(res);
          }
          this.technologies$.next(values);
          this.currentTechnology$.next(res); 
          return res; 
        })
      );
    }
  
    public get(payload: string) {
      return this.http.get<Technology>(`${this.url}/${payload}`).pipe(
        map(res => this.currentTechnology$.next(res))
      );
    }

    public getAll(force: boolean) {
      if(force || this.technologies$.getValue().length == 0) {
        return this.http.get<Array<Technology>>(`${this.url}`).pipe(
            map(res => {
              this.technologies$.next(res);
              return res;
            })
        );
      }
      return this.technologies$.asObservable();
    }

    public delete(payload: string) {
      return this.http.delete(`${this.url}/${payload}`).pipe(
        map(res => {
          let values = this.technologies$.getValue();
          values.splice(values.findIndex(v => v.id === payload), 1);
          this.technologies$.next(values);
        })
      );
    }
}
