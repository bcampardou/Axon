import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

export class SearchMode {
    public type: string;
    public label: string;
    public selector: string;
}

@Injectable({
    providedIn: 'root'
})
export class SearchService {
    public get label() {
        return this.translateService.instant(this.searchMode.label);
    }
    public get term() {
        return this.searchTerm;
    }
    public set term(value: string) {
        value = value.trim();
        this.availableModes.forEach(m => {
            if (value.startsWith(m.selector)) {
                this.searchMode = m;
                value = value.replace(m.selector, '');
            }
        });
        this.searchTerm = value;
    }
    private searchTerm: string = '';
    private availableModes: Array<SearchMode> = [
        { type: 'any', label: 'Search', selector: 'a:' },
        { type: 'server', label: 'Server', selector: 's:' },
        { type: 'project', label: 'Project', selector: 'p:' },
        { type: 'network', label: 'Network', selector: 'n:' },
        { type: 'user', label: 'User', selector: 'u:' },
    ]
    private searchMode: SearchMode;

    constructor(private translateService: TranslateService) {
        this.reinit();
    }

    public reinit() {
        this.searchMode = this.availableModes[0];
        this.searchTerm = '';
    }
}
