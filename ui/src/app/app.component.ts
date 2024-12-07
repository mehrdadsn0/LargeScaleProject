import { Component, OnInit } from '@angular/core';
import { UserResult } from './models/userResult';
import { Service } from './service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'shop';

  user: UserResult;

  constructor(private service: Service, private router: RouterModule) {
    this.user = new UserResult();
  }
  ngOnInit(): void {
    this.service.getUser().then(u => {
      this.user = u;
    })
  }




}
