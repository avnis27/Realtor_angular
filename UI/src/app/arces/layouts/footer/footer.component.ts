import { Component, OnInit } from '@angular/core';
import { APIsService } from '../../../apis.service';
import { FormGroup , FormControl , Validators } from '@angular/forms';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {
   
   subd = new FormGroup({
    id : new FormControl(''),
    companyid : new FormControl(''),
    FirstName : new FormControl('',Validators.required),
    LastName  : new FormControl('',Validators.required),
    Email     :  new FormControl('',[
      Validators.required,
      Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]),
   });

   get firstname(){ return this.subd.get('FirstName') };
   get lastname(){ return this.subd.get('LastName') };
   get email(){ return this.subd.get('Email') };

  constructor(private http : APIsService) { }

  ngOnInit(): void {
  }
   
  show;
   subcripdata(){
    this.subd.get('id').setValue(1);
    this.subd.get('companyid').setValue(1);
    if(this.subd.get('FirstName').value == "" || this.subd.get('LastName').value =="" || this.subd.get('Email').value == "")
    {
      //console.log(this.subd.value)
      this.show="Empty Field's not allowed..!!";
      // alert("Empty Field's not allowed..!!");
    }else{
      this.http.subcrption(this.subd.value).subscribe(data=>{
      // alert("Congratulation Your subcribtion is Done..!!");
      this.show="Congratulation Your subcribtion is Done..!!";
      })
    } 
  }

 

}
