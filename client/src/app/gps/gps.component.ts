import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HelpCall } from '../shared/models/helpCall.model';
import { HelpCallService } from '../shared/services/help-call.service';

@Component({
  selector: 'app-gps',
  templateUrl: './gps.component.html',
  styleUrls: ['./gps.component.scss'],
})
export class GpsComponent implements OnInit {
  constructor(private router: Router,private helpCallService:HelpCallService) { }
  map: google.maps.Map
  infoWindow: google.maps.InfoWindow;

  hc:HelpCall=new HelpCall()
  ngOnInit() { }

  getCurrentLocation() {
    if (navigator.geolocation) 
    {
      navigator.geolocation.getCurrentPosition(position => 
      {
        const pos =
        {
          lat: position.coords.latitude,
          lng: position.coords.longitude,
        };
        console.log(pos)
        var today = new Date();

var date = today.getFullYear()+'-'+(today.getMonth()+1)+'-'+today.getDate();

        this.hc.date=new Date(Date.now())
        this.hc.addressX=pos.lat;
        this.hc.addressY=pos.lng
        this.helpCallService.AddHelpCall(this.hc)
      });
    }
    else {
      alert("Geolocation is not supported by this browser.");
    }
    this.router.navigateByUrl('microphone')
  }



  initMap(): void {
    this.map = new google.maps.Map(document.getElementById("map") as HTMLElement, {
      center: { lat: -34.397, lng: 150.644 },
      zoom: 6,
    });
    this.infoWindow = new google.maps.InfoWindow();

    const locationButton = document.createElement("button");
    locationButton.textContent = "Pan to Current Location";
    locationButton.classList.add("custom-map-control-button");

    this.map.controls[google.maps.ControlPosition.TOP_CENTER].push(locationButton);

    locationButton.addEventListener("click", () => {
      // Try HTML5 geolocation.
      if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
          (position) => {
            const pos = {
              lat: position.coords.latitude,
              lng: position.coords.longitude,
            };

            this.infoWindow.setPosition(pos);
            this.infoWindow.setContent("Location found.");
            this.infoWindow.open(this.map);
            this.map.setCenter(pos);
          },
          () => {
            this.handleLocationError(true, this.infoWindow, this.map.getCenter());
          }
        );
      } else {
        // Browser doesn't support Geolocation
        this.handleLocationError(false, this.infoWindow, this.map.getCenter());
      }
    });
  }

  handleLocationError(browserHasGeolocation: boolean, infoWindow: google.maps.InfoWindow, pos: google.maps.LatLng) {
    infoWindow.setPosition(pos);
    infoWindow.setContent(
      browserHasGeolocation
        ? "Error: The Geolocation service failed."
        : "Error: Your browser doesn't support geolocation."
    );
    infoWindow.open(this.map);
  }

}
