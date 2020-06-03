import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Server } from '../models';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ServerService {
    public currentServer$: BehaviorSubject<Server> = new BehaviorSubject<Server>(null);
    public servers$ = new BehaviorSubject<Array<Server>>([]);
    private url: string = '/servers';

    constructor(private http: HttpClient) { }
  
    public post(server: Server) : Observable<Server> {
      return this.http.post<Server>(`${this.url}`, server).pipe(
        map(res => { 
          let servers = this.servers$.getValue();
          let index = servers.findIndex(s => s.id === res.id);
          if(index > -1) {
            servers[index] = res;
          } else {
            servers.push(res);
          }
          this.servers$.next(servers);
          this.currentServer$.next(res); 
          return res; })
      );
    }
  
    public get(payload: string) {
      return this.http.get<Server>(`${this.url}/${payload}`).pipe(
        map(res => this.currentServer$.next(res))
      );
    }

    public getAll(force: boolean) {
      if(force || this.servers$.getValue().length == 0) {
        return this.http.get<Array<Server>>(`${this.url}`).pipe(
            map(res => {
              this.servers$.next(res);
              return res;
            })
        );
      }
      return this.servers$.asObservable();
    }

    public delete(payload: string) {
      return this.http.delete(`${this.url}/${payload}`).pipe(
        map(res => {
          let values = this.servers$.getValue();
          values.splice(values.findIndex(v => v.id === payload), 1);
          this.servers$.next(values);
        })
      );
    }
}
