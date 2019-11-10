import { Component } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormGroup, FormControl, Validators, FormBuilder, FormsModule } from '@angular/forms';
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
    skjema: FormGroup; // Må definere skjema
    laster: boolean;
    spmTilKat: Array<Spm>;
    alleKategorier: Array<String>;
    etSpm: Spm;
  
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
        this.visSpm = true;
        this.visSkjema = false;
        this.hentAlleKategorier();
    }

    hentAlleSpm() {
        this._http.get<Spm[]>("api/Spm")
            .subscribe(
                spmObjekt => {
                    this.alleSpm = spmObjekt;
                    this.laster = false;
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
                },
                error => alert(error)
            );
    };

    hentSpmTilKategori(kategori: String) {
        this._http.get<Spm[]>("api/Kategori/" + kategori)
            .subscribe(
                spmKat => {
                    this.spmTilKat = spmKat;
                    console.log("Ferdig get api/Kategori/" + kategori);
                },
                error => alert(error)
            );
    };

    hentEtSpm(id: number) {
        this._http.get<Spm>("api/Spm/" + id)
            .subscribe(
                spm => {
                    this.etSpm = spm;
                    console.log("Ferdig get api/Spm/" + spm);
                },
                error => alert(error)
            );
    };


    tilSkjema() {
        this.visSpm = false;
        this.visSkjema = true;
    };

    tilSpm() {
        this.visSpm = true;
        this.visSkjema = false;
    };


    // Lagrer nytt spm 
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
    };

    // her blir det endrede spm lagret, brukes til up- og downvotes
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
                    //this.hentAlleSpm();
                    console.log("ferdig put-api/Spm" + id, innSpm);
                },
                error => alert(error),
            );
    } 
}

