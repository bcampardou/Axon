import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';

export class SearchMode {
    public type: string;
    public label: string;
    public selector: string;
    public url: string;
}

@Injectable({
    providedIn: 'root'
})
export class SearchService {

    public get label() {
        return this.translateService.instant(this.searchMode.label);
    }
    public get term() {
        return this.searchTerm$.getValue();
    }
    public set term(value: string) {
        value = value.trim();
        this.availableModes.forEach(m => {
            if (value.startsWith(m.selector)) {
                this.searchMode = m;
                value = value.replace(m.selector, '');
            }
        });
        this.searchTerm$.next(value);
    }
    public searchTerm$ = new BehaviorSubject<string>('');
    private availableModes: Array<SearchMode> = [
        { type: 'any', label: 'Search', selector: 'a:', url: '/' },
        { type: 'server', label: 'Server', selector: 's:', url: '/servers' },
        { type: 'project', label: 'Project', selector: 'p:', url: '/projects' },
        { type: 'network', label: 'Network', selector: 'n:', url: '/networks' },
        { type: 'user', label: 'User', selector: 'u:', url: '/users' },
    ]
    private searchMode: SearchMode;

    constructor(
        private router: Router,
        private translateService: TranslateService
    ) {
        this.reinit();
    }

    public reinit() {
        this.searchMode = this.availableModes[0];
        this.searchTerm$.next('');
    }

    public search() {
        this.router.navigateByUrl(this.searchMode.url, {
            queryParams: {
                filter: this.term.toLowerCase().trim()
            },
            replaceUrl: true
        }).then(res => {
            this.searchMode = this.availableModes[0];
        });
    }
}
