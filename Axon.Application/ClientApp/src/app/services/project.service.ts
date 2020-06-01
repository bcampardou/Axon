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
    private url: string = '/api/projects';

    constructor(private http: HttpClient) { }
  
    public post(project: Project) : Observable<Project> {
      return this.http.post<Project>(`${this.url}`, project).pipe(
        map(res => { this.currentProject$.next(res); return res; })
      );
    }
  
    public get(payload: string) {
      return this.http.get<Project>(`${this.url}/${payload}`).pipe(
        map(res => this.currentProject$.next(res))
      );
    }

    public getAll() {
        return this.http.get<Array<Project>>(`${this.url}`).pipe(
            map(res => this.projects$.next(res))
        );
    }
}
