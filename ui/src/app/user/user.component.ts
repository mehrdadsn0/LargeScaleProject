import { Component, OnInit } from '@angular/core';
import { Service } from '../service';
import { SignUpDto } from '../models/signupDto';
import { SignInDto } from '../models/signinDto';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  signUpDto: SignUpDto;
  signInDto: SignInDto;
  constructor(
    private service: Service
  ) { 
    this.signUpDto = new SignUpDto("", "", "");
    this.signInDto = new SignInDto("", "");
  }

  ngOnInit(): void {

  }

  signup() {
    this.service.signup(this.signUpDto).then(s => {
      console.log(s)
      localStorage.setItem("acc", s.accessToken!)
      localStorage.setItem("ref", s.refreshToken!)
    })
  }

  signin() {
    this.service.signin(this.signInDto).then(s => {
      console.log(s)
      localStorage.setItem("acc", s.accessToken!)
      localStorage.setItem("ref", s.refreshToken!)
    })
  }

}
