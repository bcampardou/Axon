import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Tenant } from '../models';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TenantService {
    public currentTenant$: BehaviorSubject<Tenant> = new BehaviorSubject<Tenant>(null);
    public tenants$ = new BehaviorSubject<Array<Tenant>>([]);
    private url: string = '/tenants';

    constructor(private http: HttpClient) { }
  
    public post(tenant: Tenant) : Observable<Tenant> {
      return this.http.post<Tenant>(`${this.url}`, tenant).pipe(
        map(res => { 
          let values = this.tenants$.getValue();
          let index = values.findIndex(s => s.id === res.id);
          if(index > -1) {
            values[index] = res;
          } else {
            values.push(res);
          }
          this.tenants$.next(values);
          this.currentTenant$.next(res); 
          return res; 
        })
      );
    }
  
    public get(payload: string) {
      return this.http.get<Tenant>(`${this.url}/${payload}`).pipe(
        map(res => this.currentTenant$.next(res))
      );
    }

    public getAll(force: boolean) {
      if(force || this.tenants$.getValue().length == 0) {
        return this.http.get<Array<Tenant>>(`${this.url}`).pipe(
            map(res => {
              this.tenants$.next(res);
              return res;
            })
        );
      }
      return this.tenants$.asObservable();
    }

    public delete(payload: string) {
      return this.http.delete(`${this.url}/${payload}`).pipe(
        map(res => {
          let values = this.tenants$.getValue();
          values.splice(values.findIndex(v => v.id === payload), 1);
          this.tenants$.next(values);
        })
      );
    }
}
