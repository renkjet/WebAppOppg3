import { Component, Input } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Spm } from "./Spm";
import { InnsendtSpm } from "./InnsendtSpm";

@Component({
    selector: "app-root",
    templateUrl: "SPA.html"
})

export class SPA  {
    visSkjema: boolean; //ngIf div visSkjema
    visSpm: boolean; // ngIf div visSpm
    alleSpm: Array<Spm>; 
    skjema: FormGroup; 
    laster: boolean;
    spmTilKat: Array<Spm>;
    alleKategorier: Array<String>;
    @Input() state = {};
    bekreftelse: boolean;

  
    constructor(private _http: HttpClient, private fb: FormBuilder) {
        this.skjema = fb.group({
            id: [""],
            navn: [null, Validators.compose([Validators.required, Validators.pattern("^[a-zæøåA-ZÆØÅ. \\-]{2,50}$")])],
            epost: [null, Validators.compose([Validators.required, Validators.pattern("^[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,4}$")])],
            spm: [null, Validators.compose([Validators.required, Validators.pattern("^[a-zøæåA-ZØÆÅ.0-9!?.,;.() \\-]{2,250}$")])]
        });
    }

    ngOnInit() {
        this.laster = true;
        this.hentAlleSpm();
        this.hentAlleKategorier();
        this.visSpm = true;
        this.visSkjema = false;
    };

    hentAlleSpm() {
        this._http.get<Spm[]>("api/Spm")
            .subscribe(
                spm => {
                    this.alleSpm = spm;
                    this.laster = false;
                    console.log(this.alleSpm);
                },
                error => alert(error)
        );
    };

    hentAlleKategorier() {
        this._http.get<String[]>("api/Kategori")
            .subscribe(
                kategori => {
                    this.alleKategorier = kategori;
                    this.laster = false;
                    console.log(kategori);
                },
                error => alert(error)
            );
    };

    hentEtSpm(id: number) {
        const nyState = { ...this.state };
        for (let key of Object.keys(nyState)) {
            nyState[key] = false;
        }
        nyState[id] = !this.state[id];
        this.state = nyState;
    };

    // endrede spm blir sendt til kontroller, brukes til up- og downvotes
    okTommelOpp(id: Number, innSpm: Spm) {
        var nyVerdiOpp = innSpm.tommelOpp + 1;
        innSpm.tommelOpp = nyVerdiOpp;

        this._http.put("api/Spm/" + id, innSpm)
            .subscribe(
                () => {
                    console.log("ferdig put-api/Spm" + id, innSpm);
                },
                error => alert(error),
            );
    }

    okTommelNed(id: Number, innSpm: Spm) {
        var nyVerdiOpp = innSpm.tommelNed + 1;
        innSpm.tommelNed = nyVerdiOpp;

        this._http.put("api/Spm/" + id, innSpm)
            .subscribe(
                () => {
                    console.log("ferdig put-api/Spm" + id, innSpm);
                },
                error => alert(error),
            );
    }

    lagreInnSendtSpm() {
        var lagretSpm = new InnsendtSpm();

        lagretSpm.navn = this.skjema.value.navn;
        lagretSpm.epost = this.skjema.value.epost;
        lagretSpm.spm = this.skjema.value.spm;

        this._http.post("api/Spm", lagretSpm)
            .subscribe(
                () => {
                    console.log("Ferdig post api/Spm");
                },
                error => alert(error)
        );
        this.skjema.setValue({
            id: "",
            navn: "",
            epost: "",
            spm: ""
        });
        this.skjema.markAsPristine();
    };

    // Tilbakemelding til bruker når henvendelse er sendt inn
    settBekreftelse() {
        this.bekreftelse = true;
    };

    // Metoder som bytter div
    tilSkjema() {
        this.visSpm = false;
        this.visSkjema = true;
        this.bekreftelse = false; 
    };

    tilSpm() {
        this.visSpm = true;
        this.visSkjema = false;
    };
}

