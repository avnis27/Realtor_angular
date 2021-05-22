import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(private router : Router) {
    var alreadylogin = JSON.parse(localStorage.getItem('userdetails'));
    if(alreadylogin !== null){
      this.loginstates = false;
    }else{
      this.loginstates = true;
    }
  }
  
   loginstates :any;

  ngOnInit(): void {
   
  }

   }

