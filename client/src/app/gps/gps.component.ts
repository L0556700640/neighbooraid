import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-gps',
  templateUrl: './gps.component.html',
  styleUrls: ['./gps.component.scss'],
})
export class GpsComponent implements OnInit {
  constructor(private router: Router) { }

  ngOnInit() { }

  getCurrentLocation() {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(position => {
        console.log(position)
        // this.currLat = position.coords.latitude;
        // this.currLng = position.coords.longitude;


      });
    }
    else {
      alert("Geolocation is not supported by this browser.");
    }
    this.router.navigateByUrl('microphone')
  }

}
