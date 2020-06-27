import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Intervention } from '../models/intervention.model';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class InterventionService {
    public currentIntervention$: BehaviorSubject<Intervention> = new BehaviorSubject<Intervention>(null);
    public interventions$ = new BehaviorSubject<Array<Intervention>>([]);
    private url: string = '/interventions';

    constructor(private http: HttpClient) { }
  
    public post(intervention: Intervention) : Observable<Intervention> {
      return this.http.post<Intervention>(`${this.url}/${intervention.type}`, intervention).pipe(
        map(res => { 
          let values = this.interventions$.getValue();
          let index = values.findIndex(s => s.id === res.id);
          if(index > -1) {
            values[index] = res;
          } else {
            values.push(res);
          }
          this.interventions$.next(values);
          this.currentIntervention$.next(res); 
          return res; 
        })
      );
    }
  
    public get(type: string, payload: string) {
      return this.http.get<Intervention>(`${this.url}/${type}/${payload}`).pipe(
        map(res => this.currentIntervention$.next(res))
      );
    }

    public getAll(type: string, force: boolean) {
      if(force || this.interventions$.getValue().length == 0) {
        return this.http.get<Array<Intervention>>(`${this.url}/${type}`).pipe(
            map(res => {
              this.interventions$.next(res);
              return res;
            })
        );
      }
      return this.interventions$.asObservable();
    }

    public delete(type: string, payload: string) {
      return this.http.delete(`${this.url}/${type}/${payload}`).pipe(
        map(res => {
          let values = this.interventions$.getValue();
          values.splice(values.findIndex(v => v.id === payload), 1);
          this.interventions$.next(values);
        })
      );
    }
}
