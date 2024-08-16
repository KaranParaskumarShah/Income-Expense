import { Component, EventEmitter, OnInit, Output } from '@angular/core';


@Component({
  selector: 'page-header',

  templateUrl: './page-header.component.html',
  styleUrls: ['./page-header.component.css']
})
export class PageHeaderComponent implements OnInit {

  @Output()
  onMenuClicked = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

}
