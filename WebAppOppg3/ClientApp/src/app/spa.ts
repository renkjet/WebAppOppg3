import { Component } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
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
    etSpm: Spm; // Fungerer ikke enda
  
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

    tilSkjema() {
        this.visSpm = false;
        this.visSkjema = true;
    };

    tilSpm() {
        this.visSpm = true;
        this.visSkjema = false;
    };

    lagreInnSendtSpm() {
        
        var lagretSpm = new InnsendtSpm();

        lagretSpm.navn = this.skjema.value.navn;
        lagretSpm.epost = this.skjema.value.epost;
        lagretSpm.spm = this.skjema.value.spm;

        const body: string = JSON.stringify(lagretSpm);
        const headers = new HttpHeaders({ "Content-Type": "application/json" });

        this._http.post("api/InnsendtSpmDomene", body, { headers: headers })
            .subscribe(
                () => {
                    console.log("Ferdig post api/InnsendtSpmDomene");
                },
                error => alert(error)
            );
    };

    // TESTMNETODER SOM IKKE FUNGERER ......

    okVote($event) {
        console.log("Save button is clicked!", $event);
    }    
    //Usikker om denne virker
    hentEtSpm(id: number) {
        this._http.get<Spm>("api/Spm/" + id)
            .subscribe(
                spm => {
                    this.etSpm = spm;
                },
                error => alert(error)
            );
    };

    // Øker 
    okUpvote() {

        const endretSpm = new Spm();
        var tommel = 0;
        endretSpm.tommelOpp = this.skjema.value.tommelOpp++;

        const body: string = JSON.stringify(endretSpm);
        const headers = new HttpHeaders({ "Content-Type": "application/json" });
        this._http.put("api/Spm/" + this.skjema.value.id, body, { headers: headers })
            .subscribe(
                () => {
                    this.hentAlleSpm(); // Oppdaterer da med en gang?
                    console.log("Klikket på upvote");
                },
                error => alert(error),
         );
    };
    
}

