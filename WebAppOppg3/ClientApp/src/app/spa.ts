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
    visSkjema: boolean; //ngIf visSkjema
    visSpm: boolean; // ngIf visSpm
    alleSpm: Array<Spm>; // Liste av alle spm
    skjema: FormGroup;
    laster: boolean;

    constructor(private _http: HttpClient, private fb: FormBuilder) {

    }

    ngOnInit() {
        this.laster = true;
        this.hentAlleSpm();
        this.visSpm = true;
        this.visSkjema = false;
        
    }

    hentAlleSpm() {
        this._http.get<Spm[]>("api/SpmDomene")
            .subscribe(
                spmObjekt => {
                    this.alleSpm = spmObjekt;
                    this.laster = false;
                },
                error => alert(error)
            );
    };
    
}

