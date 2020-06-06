import { BehaviorSubject } from "rxjs";
import { License } from "@app/models";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class LicenseService {
    public currentLicense$: BehaviorSubject<License> = new BehaviorSubject<License>(null);

    constructor() { }
}