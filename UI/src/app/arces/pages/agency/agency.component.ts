import { Component, OnInit } from '@angular/core';
import { FormGroup,FormControl , Validators } from '@angular/forms';
import { APIsService } from '../../../apis.service';

@Component({
  selector: 'app-agency',
  templateUrl: './agency.component.html',
  styleUrls: ['./agency.component.css']
})
export class AgencyComponent implements OnInit {

  morgrageappdata = new FormGroup({
    id :new FormControl('',Validators.required),
    companyId :new FormControl('',Validators.required),
    firstName :new FormControl('',Validators.required),
    lastName :new FormControl('',Validators.required),
    phoneNo :new FormControl('',[Validators.required,Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]),
    email :new FormControl('',[
      Validators.required,
      Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]),
    subject :new FormControl('',Validators.required),
    message :new FormControl('',Validators.required),
  })

  constructor(private http : APIsService ) {
    this.userdetails =JSON.parse(localStorage.getItem('userdetails'));
   }

  userdetails :any = [];

  ngOnInit(): void {
    
  }

  get fname(){ return this.morgrageappdata.get('firstName')}
  get lname(){ return this.morgrageappdata.get('lastName')}
  get pno(){ return this.morgrageappdata.get('phoneNo')}
  get email(){ return this.morgrageappdata.get('email')}
  get sbj(){ return this.morgrageappdata.get('subject')}
  get msg(){ return this.morgrageappdata.get('message')}

   Message;
   showerr;
  morgarageformdata(){
   this.morgrageappdata.get('id').setValue(1);
   this.morgrageappdata.get('companyId').setValue(1);
   if(this.morgrageappdata.get('firstName').value == "" || this.morgrageappdata.get('lastName').value == "" || this.morgrageappdata.get('phoneNo').value == "" || this.morgrageappdata.get('email').value == "" || this.morgrageappdata.get('subject').value == "" || this.morgrageappdata.get('message').value == ""){
    // alert("No Blank's Allowed..!!")
    this.Message="No Blank's Allowed..!!";
   }else{
     console.log(this.morgrageappdata.value)
   this.http.mortgagepreapproval(this.morgrageappdata.value).subscribe(data=>{
    // alert("Your MortgagePreApproval Is Send..!!");
    this.Message="Your Mortgage Pre Approval Is Send..!!";
    
   },error=>{
    // alert("Your MortgagePreApproval Is Not Send..!!");
    this.showerr="Your Mortgage PreApproval Is Not Send..!!";
   })

       }
   
  
  }
}
