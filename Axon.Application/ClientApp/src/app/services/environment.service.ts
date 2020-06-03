import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Environment } from '../models';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class EnvironmentService {
    public currentEnvironment$: BehaviorSubject<Environment> = new BehaviorSubject<Environment>(null);
    public environments$ = new BehaviorSubject<Array<Environment>>([]);
    private url: string = '/environments';

    constructor(private http: HttpClient) { }
  
    public post(environment: Environment) : Observable<Environment> {
      return this.http.post<Environment>(`${this.url}`, environment).pipe(
        map(res => { 
          let values = this.environments$.getValue();
          let index = values.findIndex(s => s.name === res.name && s.projectId === s.projectId);
          if(index > -1) {
            values[index] = res;
          } else {
            values.push(res);
          }
          this.environments$.next(values);
          this.currentEnvironment$.next(res); 
          return res; 
        })
      );
    }
  
    public get(payload: string) {
      return this.http.get<Environment>(`${this.url}/${payload}`).pipe(
        map(res => this.currentEnvironment$.next(res))
      );
    }

    public getAll(force: boolean) {
      if(force || this.environments$.getValue().length == 0) {
        return this.http.get<Array<Environment>>(`${this.url}`).pipe(
            map(res => {
              this.environments$.next(res);
              return res;
            })
        );
      }
      return this.environments$.asObservable();
    }
}
