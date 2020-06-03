import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators'
import { Project } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
    public currentProject$: BehaviorSubject<Project> = new BehaviorSubject<Project>(null);
    public projects$ = new BehaviorSubject<Array<Project>>([]);
    private url: string = '/projects';

    constructor(private http: HttpClient) { }
  
    public post(project: Project) : Observable<Project> {
      return this.http.post<Project>(`${this.url}`, project).pipe(
        map(res => { 
          let values = this.projects$.getValue();
          let index = values.findIndex(s => s.id === res.id);
          if(index > -1) {
            values[index] = res;
          } else {
            values.push(res);
          }
          this.projects$.next(values);
          this.currentProject$.next(res); 
          return res; 
        })
      );
    }
  
    public get(payload: string) {
      return this.http.get<Project>(`${this.url}/${payload}`).pipe(
        map(res => this.currentProject$.next(res))
      );
    }

    public getAll(force: boolean) {
      if(force || this.projects$.getValue().length == 0) {
        return this.http.get<Array<Project>>(`${this.url}`).pipe(
            map(res => {
              this.projects$.next(res);
              return res;
            })
        );
      }
      return this.projects$.asObservable();
    }

    public delete(payload: string) {
      return this.http.delete(`${this.url}/${payload}`).pipe(
        map(res => {
          let values = this.projects$.getValue();
          values.splice(values.findIndex(v => v.id === payload), 1);
          this.projects$.next(values);
        })
      );
    }
}
