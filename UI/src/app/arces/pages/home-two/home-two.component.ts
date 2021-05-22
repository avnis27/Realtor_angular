import { Component, OnInit } from '@angular/core';
import { APIsService } from '../../../apis.service';
import { FormGroup , FormControl ,Validators } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home-two',
  templateUrl: './home-two.component.html',
  styleUrls: ['./home-two.component.css'],
  providers: [DatePipe]
})
export class HomeTwoComponent implements OnInit {
  
  appointformdetails = new FormGroup({
    id : new FormControl(''),
    companyid : new FormControl(''),
    preferredDate : new FormControl('',Validators.required),
    preferredTime : new FormControl('',Validators.required),
    referenceNumber : new FormControl(''),
    propertyID : new FormControl(''),
    userId : new FormControl('')
  })
  get datereq(){ return this.appointformdetails.get('preferredDate') };
  get timereq(){ return this.appointformdetails.get('preferredTime') };

  constructor(private http : APIsService , private datePipe: DatePipe,private router : Router) {
    var userdata = JSON.parse(localStorage.getItem('userdetails'));
    this.loginid = userdata.id;
    this.isadmin=userdata.isAdmin;
    if(userdata.isAdmin){
      this.http.appointmentdetails(userdata.companyId).subscribe(data=>{
      this.appointment=data;
      this.totalrecords = this.appointment.length;
     //console.log(data);
      },(error)=>{
          this.errorstates=true;
          this.adminappoinmetviewerror=error;
      })
    }
  }

   loginid : any ;

   errorstates=false;
   adminappoinmetviewerror:any = [];
   appointment:any=[];
   isadmin:any=[];
   totalrecords="";
   page = 1;

  ngOnInit(): void {
  }
  myDate = new Date();

  addapoinment(){
    if(this.appointformdetails.get('preferredTime').value == ""){
      alert("Enter The Time Please..!!");
    }
    else{
     
    var myDate = this.datePipe.transform(this.myDate, 'yyyy-MM-dd');

    this.appointment.preferredDate
    this.appointformdetails.get('id').setValue(1);
    this.appointformdetails.get('companyid').setValue(1);
    this.appointformdetails.get('preferredDate').setValue(myDate);
    this.appointformdetails.get('userId').setValue(this.loginid);
    console.log(this.appointformdetails.value);

   this.http.makeappointments(this.appointformdetails.value).subscribe(data=>{
    alert("Thanks !! Your Appointment is Registered.");
    window.location.reload();
   },error=>{
    alert("Something Went Wrong..TryAgain Please!!");
    window.location.reload();

   })

    }
  }

  deleteappointment(id:any){

    this.http.deleteappoinment(id).subscribe(data=>{
      alert("Appointment Is Deleted..!!");
      window.location.reload();
    },error=>{
      alert("Appointment Is Not Deleted..!!");
      window.location.reload();
    })
  
  }

}
