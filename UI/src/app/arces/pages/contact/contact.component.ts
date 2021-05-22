import { Component, OnInit } from '@angular/core';
import { APIsService } from '../../../apis.service';
import { FormGroup , FormControl ,Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {
   
  vistordata = new FormGroup({
    companyId : new FormControl('1',Validators.required),
    FirstName : new FormControl('',Validators.required),
    LastName : new FormControl('',Validators.required),
    Email :  new FormControl('',[
      Validators.required,
      Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]),
    PhoneNo :  new FormControl('',[Validators.required,Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]),
    Subject : new FormControl('',Validators.required),
    Message : new FormControl('',Validators.required),
    
  })

  constructor(private http : APIsService,private router : Router) { 
    var userdata = JSON.parse(localStorage.getItem('userdetails'));
     if(userdata !== null){
       if(userdata.isAdmin){
        this.contectblock = true;
        this.http.visitorsdetails().subscribe(data=>{
          // console.log(data)
          this.contectquerydata = data;
          this.totalrecords = this.contectquerydata.length;
        })
       }
     }
  }

  get firstname(){ return this.vistordata.get('FirstName') };
  get lastname(){ return this.vistordata.get('LastName') };
  get email(){ return this.vistordata.get('Email') };
  get phoneno(){ return this.vistordata.get('PhoneNo') };
  get subject(){ return this.vistordata.get('Subject') };
  get msg(){ return this.vistordata.get('Message') };

  ngOnInit(): void {
  }

  isadmin:any=[];
  contectblock = false;
  contectquerydata :any =[];
  totalrecords="";
  page = 1;
  Message;
  Email=  /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/;
  vistdata(){
    if( this.vistordata.get('FirstName').value == "" || this.vistordata.get('LastName').value == "" || this.vistordata.get('PhoneNo').value == "" || this.vistordata.get('Message').value == "" || this.vistordata.get('Subject').value == "")
    {
       this.Message="No Empty Space Allowed..!!";
    }else{
      this.http.visitorsrequest(this.vistordata.value).subscribe(data=>{
        // alert("Your Query's Has Been Send ThankYou..!!")
        // this.routerLink['/listing-grid'];
        //  this.router.url; 
        this.Message="Your Query's Has Been Send ThankYou..!!";
       
         window.location.reload();
        
       },(error)=>{
        this.Message="Your Query's Is Not Submitted Try Later..!!";
       })
    }
     }
  show;
  vistdelete(id:any){
    this.http.visitorsdatadelete(id).subscribe(data=>{
      this.show="Data Is Deleted..!!";
      // alert("Data Is Deleted..!!");
      window.location.reload();
    },error=>{
      this.show="Data Is Not Deleted..!!";
      // alert("Data Is Not Deleted..!!");
      window.location.reload();
    })
  }
}
