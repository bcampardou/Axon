import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Network } from '../models';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class NetworkService {
    public currentNetwork$: BehaviorSubject<Network> = new BehaviorSubject<Network>(null);
    public networks$ = new BehaviorSubject<Array<Network>>([]);
    private url: string = '/api/networks';

    constructor(private http: HttpClient) { }
  
    public post(network: Network) : Observable<Network> {
      return this.http.post<Network>(`${this.url}`, network).pipe(
        map(res => { this.currentNetwork$.next(res); return res; })
      );
    }
  
    public get(payload: string) {
      return this.http.get<Network>(`${this.url}/${payload}`).pipe(
        map(res => this.currentNetwork$.next(res))
      );
    }

    public getAll(force: boolean) {
      if(force || this.networks$.getValue().length == 0) {
        return this.http.get<Array<Network>>(`${this.url}`).pipe(
            map(res => {
              this.networks$.next(res);
              return res;
            })
        );
      }
      return this.networks$.asObservable();
    }
}
