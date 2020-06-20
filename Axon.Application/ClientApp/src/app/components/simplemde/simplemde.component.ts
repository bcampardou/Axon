import {
    Component,
    Input,
    forwardRef,
    ElementRef,
    OnDestroy,
    ChangeDetectionStrategy,
    AfterViewInit,
    OnChanges,
    SimpleChanges,
    NgZone,
    ViewChild,
    SimpleChange,
    Output,
    EventEmitter,
} from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from '@angular/forms';

import { SimplemdeConfig, SimplemdeOptions } from './simplemde.config';

declare const SimpleMDE: any;

@Component({
    selector: 'simplemde',
    template: `
      <textarea #con [name]="name" [(ngModel)]="value"></textarea>
    `,
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useClass: forwardRef(() => SimplemdeComponent),
            multi: true,
        },
    ],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SimplemdeComponent
    implements AfterViewInit, OnChanges, OnDestroy {
    @ViewChild('con', { static: true }) private con: ElementRef;
    private instance: any;
    @Input() value: string;
    @Output() public changed: EventEmitter<string> = new EventEmitter<string>();

    @Input() name: string;
    @Input() options: SimplemdeOptions;
    @Input() style: 'default' | 'antd';
    @Input() delay: number;
    @Input() disabled: boolean;

    get Instance(): any {
        return this.instance;
    }

    /**
     * Call [setOption](https://codemirror.net/doc/manual.html#setOption) method of Codemirror.
     */
    setOptions(option: string, value: any): void {
        if (!this.instance) return;
        this.instance.codemirror.setOption(option, value);
    }

    constructor(private cog: SimplemdeConfig, private zone: NgZone) {
        cog = { ...new SimplemdeConfig(), ...cog };
        this.style = cog.style;
        this.delay = cog.delay || 0;
    }

    private initDelay() {
        if (this.delay > 0) {
            setTimeout(() => this.init(), this.delay);
        } else {
            this.init();
        }
    }

    private init() {
        if (typeof SimpleMDE === 'undefined') {
            throw new Error(`Could not find SimpleMDE object.`);
        }
        this.destroy();
        const config = Object.assign(
            {},
            this.cog,
            this.options,
            this.style === 'antd'
                ? {
                    spellChecker: false,
                    autoDownloadFontAwesome: false,
                }
                : {},
        );
        config.element = this.con.nativeElement;
        this.zone.runOutsideAngular(() => {
            this.instance = new SimpleMDE(config);
            if (this.value) {
                this.instance.value(this.value);
            }
            this.instance.codemirror.on('change', () => {
                this.value = this.instance.value();
                this.zone.run(() => this.changed.next(this.value));
            });
            this.setDisable();
        });
    }

    private destroy() {
        if (this.instance) {
            this.instance.toTextArea();
            this.instance = null;
        }
    }

    private setDisable() {
        if (this.instance) {
            this.zone.runOutsideAngular(
                () => (this.instance.codemirror.options.readOnly = this.disabled),
            );
        }
    }

    ngAfterViewInit(): void {
        this.initDelay();
    }

    ngOnChanges(
        changes: { [P in keyof this]?: SimpleChange } & SimpleChanges,
    ): void {
        if (changes.options && !changes.options.firstChange) this.initDelay();
    }

    ngOnDestroy() {
        this.destroy();
    }

    setDisabledState(isDisabled: boolean): void {
        this.disabled = isDisabled;
        this.setDisable();
    }
}