import { OnInit, OnDestroy, Component, Input, EventEmitter, Output, ViewChild } from "@angular/core";
import dayGridPlugin from '@fullcalendar/daygrid';
import bootstrapPlugin from '@fullcalendar/bootstrap';
import { Intervention } from "@app/models/intervention.model";
import { NetworkService, ServerService, ProjectService, AuthenticationService } from "@app/services";
import { InterventionService } from "@app/services/intervention.service";
import { BehaviorSubject, Subscription } from "rxjs";
import { User } from "@app/models";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from "@ngx-translate/core";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";

@Component({
    selector: 'app-intervention-calendar',
    templateUrl: './intervention-calendar.component.html',
    styleUrls: ['./intervention-calendar.component.scss']
})
export class InterventionCalendarComponent implements OnInit, OnDestroy {
    calendarPlugins = [
        dayGridPlugin,
        bootstrapPlugin
    ];
    @Input() public startDate: Date = (() => {
        var date = new Date();
        date.setDate(1);
        return date;
    })();
    @Input() public endDate: Date = (() => {
        var date = new Date();
        date.setMonth(this.startDate.getMonth() + 1);
        date.setDate(-1);
        return date;
    })();
    @Input() public datas: Array<Intervention> = [];
    private type$ = new BehaviorSubject<'all' | 'server' | 'network' | 'project'>('project');
    @Input() public set type(value: 'all' | 'server' | 'network' | 'project') {
        this.type$.next(value);
    }
    public get type() {
        return this.type$.getValue();
    }
    @Output() public onSelect = new EventEmitter<Intervention>();
    // public users: Array<User> = [];
    public get events() {
        return this.datas.map(intervention => {
            return {
                title: intervention.data["name"],
                id: intervention.id,
                start: intervention.start,
                end: intervention.end,
                backgroundColor: (() => {
                    switch (intervention.type) {
                        case 'network': return 'var(--danger)';
                        case 'server': return 'var(--warning)';
                        case 'project': return 'var(--yellow)';
                    }
                })()
            }
        })
    }
    public selectedIntervention: Intervention;
    @ViewChild('interventionModal', { static: false }) interventionModal: any;

    private _subscriptions = new Array<Subscription>();

    constructor(private modalService: NgbModal) {

    }

    ngOnInit() {
    }

    ngOnDestroy() {
        this._subscriptions.forEach(sub => sub.unsubscribe());
    }

    selectEvent(event: any) {
        this.selectedIntervention = this.datas.find(i => i.id === event.event.id);
        const ref = this.modalService.open(this.interventionModal, { centered: true, size: 'lg', backdrop: 'static' });
    }
}