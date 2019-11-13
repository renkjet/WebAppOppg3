import { Component, OnInit, Input } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormGroup, FormControl, Validators, FormBuilder, FormsModule } from '@angular/forms';
import { Spm } from "./Spm";
import { InnsendtSpm } from "./InnsendtSpm";

@Component({
    selector: "app-root",
    templateUrl: "SPA.html"
})

export class SPA implements OnInit{
    visSkjema: boolean; //ngIf div visSkjema
    visSpm: boolean; // ngIf div visSpm
    alleSpm: Array<Spm>; 
    skjema: FormGroup; 
    laster: boolean;
    etSpm: Spm;
    spmTilKat: Array<Spm>;
    alleKategorier: Array<String>;
    @Input() state = {};
  
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
        //this.hentEtSpm(1); // Får ikke hentet verken kategorier eller spm hvis ikke denne settes i init?
        this.visSpm = true;
        this.visSkjema = false;
        this.hentAlleKategorier();
    };

    hentAlleSpm() {
        this._http.get<Spm[]>("api/Spm")
            .subscribe(
                spmObjekt => {
                    this.alleSpm = spmObjekt;
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

    // Metode som brukes for å hente spm.svar
    hentEtSpm(id: number) {
        this._http.get<Spm>("api/Spm/" + id)
            .subscribe(
                spm => {
                    this.etSpm = spm;
                    console.log("Ferdig get api/Spm/" + id);
                    
                },
                error => alert(error)
        );
    };

    hentEtSpm2(id: number) {
        const nyState = { ...this.state };
        for (let key of Object.keys(nyState)) {
            nyState[key] = false;
        }
        nyState[id] = !this.state[id];
        this.state = nyState;
        console.log(this.state);
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

