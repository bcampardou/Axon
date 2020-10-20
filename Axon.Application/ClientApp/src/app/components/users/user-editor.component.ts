import { Component, OnInit, OnDestroy, Input, EventEmitter, Output } from '@angular/core';
import { AuthenticationService } from '@app/services';
import { Subscription } from 'rxjs';
import { User } from '@app/models';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'app-user-editor',
    templateUrl: './user-editor.component.html',
    styleUrls: ['./user-editor.component.scss']
})
export class UserEditorComponent implements OnInit, OnDestroy {
    public isCollapsed = true;
    @Input() public userId: string;
    @Input() public edition: boolean = true;
    @Output() public canceled = new EventEmitter<boolean>();
    public user = new User();
    private subscriptions = new Array<Subscription>();

    constructor(
        private toastr: ToastrService,
        private translateService: TranslateService,
        private authService: AuthenticationService) {
            this.subscriptions.push(this.authService.currentUser$.subscribe(ser => this.user = ser));
    }

    ngOnInit() {
    }

    ngOnDestroy() {
        this.subscriptions.forEach(sub => sub.unsubscribe());
    }

    public onSubmit() {
        this.authService.post(this.user).subscribe(res => 
            {
                this.toastr.success(this.translateService.instant('Successfully saved', 'Success'));
            });
    }

    public cancel() {
        this.canceled.next(true);
    }

    public activateAccount(value: boolean) {
        this.authService.activate(this.user.id, value).subscribe(() => {
            this.toastr.success(this.translateService.instant('Successfully saved', 'Success'));
        });
    }
}
