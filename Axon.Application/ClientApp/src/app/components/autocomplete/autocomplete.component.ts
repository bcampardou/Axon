import { Component, Input } from '@angular/core';
import { Observable } from 'rxjs';
import { debounceTime, map, distinctUntilChanged } from 'rxjs/operators';
import { IdentifiedModel } from '@app/models';

@Component({
    selector: 'app-autocomplete',
    templateUrl: './autocomplete.component.html',
    styleUrls: ['./autocomplete.component.scss']
})
export class AutocompleteComponent {
    @Input() public name: string;
    @Input() public options: { filter: string, value: string }
    @Input() public model: any;
    @Input() public source: Array<any>
    search = (text$: Observable<string>) =>
        text$.pipe(
            debounceTime(200),
            distinctUntilChanged(),
            map(term => term.length < 2 ? []
                : this.source.filter(v => v[this.options.filter].toLowerCase().indexOf(term.toLowerCase()) > -1).slice(0, 10))
        );

    formatter = (x: any) => x[this.options.filter];
}