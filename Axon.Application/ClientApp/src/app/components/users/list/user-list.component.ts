import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from '@app/services';
import { BehaviorSubject, Subscription } from 'rxjs';
import { User } from '@app/models';
import { OperatingSystems } from '@app/models/operating-systems.model';

@Component({
    selector: 'app-user-list',
    templateUrl: './user-list.component.html',
    styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit, OnDestroy {
    public isCollapsed = true;
    public page: number = 0;
    public pageSize: number = 10;
    public osOptions = OperatingSystems;
    @Input() public users: Array<User>;
    @Input() public edition: boolean = true;
    @Output() public userSelected = new EventEmitter<string>();
    private subscriptions = new Array<Subscription>();

    constructor(
        private authService: AuthenticationService) {
    }

    ngOnInit() {
    }

    ngOnDestroy() {
        this.subscriptions.forEach(sub => sub.unsubscribe());
    }

    public select(user: User) {
        this.userSelected.next(user.id);
    }
}
