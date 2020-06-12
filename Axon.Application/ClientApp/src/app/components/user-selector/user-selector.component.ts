import { Component, OnInit, OnDestroy, Input, EventEmitter, Output } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ServerService, NetworkService, AuthenticationService } from '@app/services';
import { BehaviorSubject, Subscription } from 'rxjs';
import { take } from 'rxjs/operators'
import { Server, Network, User } from '@app/models';
import { OperatingSystems } from '@app/models/operating-systems.model';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'app-user-selector',
    templateUrl: './user-selector.component.html',
    styleUrls: ['./user-selector.component.scss']
})
export class UserSelectorComponent implements OnInit, OnDestroy {
    public isCollapsed = true;
    @Input() public selectedList: Array<User> = [];
    @Output() public canceled = new EventEmitter<boolean>();
    @Output() public selectUser = new EventEmitter<User>();
    public user: User;
    public users: Array<User> = [];
    private subscriptions = new Array<Subscription>();

    constructor(private route: ActivatedRoute,
        private router: Router,
        private toastr: ToastrService,
        private translateService: TranslateService,
        private authService: AuthenticationService,
        private networkService: NetworkService) {
        this.subscriptions.push(this.authService.getAll(false).subscribe(us => {
            this.users = us.filter((u) => {
                return this.selectedList.findIndex(su => su.id === u.id) < 0
            });
        }));
    }

    ngOnInit() {
    }

    ngOnDestroy() {
        this.subscriptions.forEach(sub => sub.unsubscribe());
    }

    public save() {
        this.selectUser.next(this.user);
    }

    public setUser(event: User) {
        this.user = event;
    }

    public cancel() {
        this.canceled.next(true);
    }
}
