import { Component } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Spm } from "./Spm";
import { InnsendtSpm } from "./InnsendtSpm";

@Component({
    selector: "app-root",
    templateUrl: "SPA.html"
})

export class SPA {
    visSkjema: boolean;
    visSpm: boolean;
    skjema: FormGroup;
    laster: boolean;

    ngOnInit() {
        this.laster = true;

    }
}

