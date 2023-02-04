import { Injectable } from '@angular/core'
import { Step1Service } from './step-1.service';
import { Step2Service } from './step-2.service';

@Injectable()
export class StepperService {

    constructor(
        public readonly step1: Step1Service,
        public readonly step2: Step2Service,
    ) { }

    public currentIndex = 0;

}