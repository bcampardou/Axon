import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { KnowledgeSheet } from '../models';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class KnowledgeSheetService {
    public currentKnowledgeSheet$: BehaviorSubject<KnowledgeSheet> = new BehaviorSubject<KnowledgeSheet>(new KnowledgeSheet());
    public knowledgeSheets$ = new BehaviorSubject<Array<KnowledgeSheet>>([]);
    public knowledgeBase$ = new BehaviorSubject<Array<KnowledgeSheet>>([]);
    private url: string = '/knowledgeSheets';

    constructor(private http: HttpClient) { }
  
    public post(knowledgeSheet: KnowledgeSheet) : Observable<KnowledgeSheet> {
      return this.http.post<KnowledgeSheet>(`${this.url}`, knowledgeSheet).pipe(
        map(res => { 
          let values = this.knowledgeSheets$.getValue();
          let index = values.findIndex(s => s.id === res.id);
          if(index > -1) {
            values[index] = res;
          } else {
            values.push(res);
          }
          this.knowledgeSheets$.next(values);
          this.currentKnowledgeSheet$.next(res); 
          return res; 
        })
      );
    }
  
    public get(payload: string) {
      return this.http.get<KnowledgeSheet>(`${this.url}/${payload}`).pipe(
        map(res => this.currentKnowledgeSheet$.next(res))
      );
    }

    public getAll(force: boolean) {
      if(force || this.knowledgeSheets$.getValue().length == 0) {
        return this.http.get<Array<KnowledgeSheet>>(`${this.url}`).pipe(
            map(res => {
              this.knowledgeSheets$.next(res);
              return res;
            })
        );
      }
      return this.knowledgeSheets$.asObservable();
    }

    public getBase(force: boolean) {
      if(force || this.knowledgeBase$.getValue().length == 0) {
        return this.http.get<Array<KnowledgeSheet>>(`${this.url}/base?force=${force}`).pipe(
            map(res => {
              this.knowledgeBase$.next(res);
              return res;
            })
        );
      }
      return this.knowledgeBase$.asObservable();
    }

    public delete(payload: string) {
      return this.http.delete(`${this.url}/${payload}`).pipe(
        map(res => {
          let values = this.knowledgeSheets$.getValue();
          values.splice(values.findIndex(v => v.id === payload), 1);
          this.knowledgeSheets$.next(values);
        })
      );
    }
}
