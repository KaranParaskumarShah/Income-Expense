import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.css']
})
export class SideNavComponent implements OnInit {

  navItems : {name : string; link : string}[] = [
  
    { name : 'maintenance' , link : 'maintenance' },
    { name : 'view data' , link : 'view' }, 
  ];

  constructor() { }

  ngOnInit(): void {
  }

}
