import { Component, OnInit } from '@angular/core';
import { APIsService } from '../../../apis.service';
import { Router } from '@angular/router';
import { FormGroup , FormControl  } from '@angular/forms';
declare var jQuery: any;
import { SlickCarouselComponent } from 'ngx-slick-carousel';
import { ViewChild } from '@angular/core';
@Component({
  selector: 'app-listing-map',
  templateUrl: './listing-map.component.html',
  styleUrls: ['./listing-map.component.css']
})
export class ListingMapComponent implements OnInit {

  searchdata = new FormGroup({
    Mlsmo : new FormControl(''),
    cityname : new FormControl(''),
    minprice : new FormControl('0'),
    maxprice :new FormControl('0'),
    beds : new FormControl('0'),
    bath : new FormControl('0'),
    propertytype : new FormControl('0'),
    transectiontype :new FormControl('0'),

  })

  constructor(private http : APIsService ,  private router : Router ) {
   
    this.http.dropdowndata().subscribe(data=>{
      this.dropdowndata = data;
      
      //  console.log(data)
    })
    this.userinfo = JSON.parse(localStorage.getItem('userdetails'));
  }
 
  ngOnInit(): void {
    (function ($) {
      var $buttons = jQuery('button');
$buttons.on('click',function() {
  jQuery(this).toggleClass('active').siblings('button').removeClass('active');
})
    })(jQuery);
    }
    
  
  dropdowndata :any =[];
  search=false;
  searchbyMlsno = false;
  dropstates = false;
  mlsresult:any = [];
  residential = "";
  commercial = "";
  dropdownsearchdetails : any =[];
  totalrecords="";
  page = 1;
  users:object;
  loading=true;
  slideConfig = {
   slidesToShow: 3,
   slidesToScroll: 1,
   infinite: false,
   autoplay:true,
   
   responsive: [
   {
   breakpoint: 800,
   settings: {
   slidesToShow: 2
   }
   },
   {
   breakpoint: 600,
   settings: {
   slidesToShow: 1
   }
   }
   ]
   };
   searchfirst()
   {
    //Router.reload()
     
    // $route.reload();
    
    // location.reload();
    this.searchquerys();
   }
 
  searchquerys(){
    
    if(this.searchdata.get('Mlsmo').value !== "" && this.searchdata.get('cityname').value == ""){
      this.search = true;
      this.searchbyMlsno = true;
      this.dropstates=false;
      this.http.searchMlsno(this.searchdata.get('Mlsmo').value).subscribe(data=>{
        this.loading = false;
        this.mlsresult = data;
        this.totalrecords = this.mlsresult.length;
         //console.log("search data :"+this.mlsresult)
       },error=>{
         alert("Somthing Went Wrong Try Later..!!");
       })
    } else if(this.searchdata.get('Mlsmo').value == "" && this.searchdata.get('cityname').value !== ""){
      this.dropstates=true;
      this.search = false;
      this.searchbyMlsno = false;
      this.dropdowndata.forEach(element => {
        if(element.name == this.searchdata.get('cityname').value){
          if(this.residential !== ""){
            let dropsearchobj = {
              Type:this.residential,
              CurrentPage:1,
              LatitudeMin:element.latitudeMin,
              LongitudeMax:element.longitudeMax,
              RecordsPerPage:5,
              LongitudeMin:element.longitudeMin,
              LatitudeMax:element.latitudeMax,
              BedRange:this.searchdata.get('beds').value,
              BathRange:this.searchdata.get('bath').value,
              NumberOfDays:0,
              SortBy:0,
              BuildingTypeId:this.searchdata.get('propertytype').value,
              PriceMax:this.searchdata.get('maxprice').value,
              PriceMin:this.searchdata.get('minprice').value,
              TransactionTypeId : this.searchdata.get('transectiontype').value
             }
            //console.log(dropsearchobj);
            this.http.searchbyproptype(dropsearchobj).subscribe(data=>{
              this.loading = false;
              //console.log("dropdown"+data);
              
              this.dropdownsearchdetails = data;
              this.totalrecords =  this.dropdownsearchdetails.length;
             },error=>{
               console.log(error)
             })
          }else if(this.commercial !== ""){
            let dropsearchobj = {
              Type:this.commercial,
              CurrentPage:1,
              LatitudeMin:element.latitudeMin,
              LongitudeMax:element.longitudeMax,
              RecordsPerPage:5,
              LongitudeMin:element.longitudeMin,
              LatitudeMax:element.longitudeMax,
              BedRange:this.searchdata.get('beds').value,
              BathRange:this.searchdata.get('bath').value,
              NumberOfDays:0,
              SortBy:0,
              BuildingTypeId:this.searchdata.get('propertytype').value,
              PriceMax:this.searchdata.get('maxprice').value,
              PriceMin:this.searchdata.get('minprice').value,
              TransactionTypeId : this.searchdata.get('transectiontype').value
             }
           // console.log(dropsearchobj);
            this.http.searchbyproptype(dropsearchobj).subscribe(data=>{
              //console.log(data);
              this.dropdownsearchdetails = data;
              this.totalrecords =  this.dropdownsearchdetails.length;
             },error=>{
               console.log(error)
             })
          }else{
            let dropsearchobj = {
              Type:"residential",
              CurrentPage:1,
              LatitudeMin:element.latitudeMin,
              LongitudeMax:element.longitudeMax,
              RecordsPerPage:5,
              LongitudeMin:element.longitudeMin,
              LatitudeMax:element.latitudeMax,
              BedRange:this.searchdata.get('beds').value,
              BathRange:this.searchdata.get('bath').value,
              NumberOfDays:0,
              SortBy:0,
              BuildingTypeId:this.searchdata.get('propertytype').value,
              PriceMax:this.searchdata.get('maxprice').value,
              PriceMin:this.searchdata.get('minprice').value,
              TransactionTypeId : this.searchdata.get('transectiontype').value
             }
           // console.log(dropsearchobj);
            this.http.searchbyproptype(dropsearchobj).subscribe(data=>{
              this.loading = false;
             console.log(data);
              this.dropdownsearchdetails = data;
              this.totalrecords =  this.dropdownsearchdetails.length;
              
             },error=>{
               console.log(error)
             })
          }
        }
      })
    }      else if(this.searchdata.get('Mlsmo').value !== "" && this.searchdata.get('cityname').value !== ""){
       alert("Please Select Any One Input Field..!!");
    }
    else{
      alert("NO Empty Space Allowed..!!")
    }
    // 
   
  }
  resi(){
   this.residential = "residential";
  }

  com(){
    this.commercial = "commercial";
   }
    
   userinfo :any = [];

     saveproperty(propertyid :any){
      //console.log(propertyid);
      //saveprorperty in login
      if(this.userinfo == null){
        alert("To Save The Property Please Login..!!")
      }else{
       let obj = {
         id : 1,
         companyId : 1,
         userId : this.userinfo.id,
         referenceNumber : propertyid,
         propertyID : propertyid
       }
      this.http.addsavelist(obj).subscribe(data=>{
        alert("Property Saved..!")
      },error=>{
       alert("To Save The Property Please Login..!")
     })
      }
     }
   @ViewChild('slickModal') slickModal: SlickCarouselComponent;
   next() {
    this.slickModal.slickNext();
    }
    
   prev() {
     this.slickModal.slickPrev();
   }
  
}
