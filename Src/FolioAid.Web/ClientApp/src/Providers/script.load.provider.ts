import {  Injectable } from '@angular/core';
import * as $ from "jquery";

@Injectable({
  providedIn: 'root'
})
export class ScriptsLoaderService {
  private scriptUrls: string[] = [    
    "../assets/js/owl-carousel.js",
    "../assets/js/animation.js",
    "../assets/js/imagesloaded.js",
    "../assets/js/custom.js",
    "../assets/js/jquery.uploader.min.js"
    // Add more script URLs as needed
  ];

  loadScripts(): Promise<void> {
    const promises = this.scriptUrls.map(url => this.loadScript(url));
    return Promise.all(promises).then(() => { });
  }

  public loadScript(url: string): Promise<void> {
    return new Promise<void>((resolve: any, reject) => {
      const scriptElement = document.createElement('script');
      scriptElement.src = url;
      scriptElement.onload = resolve;
      scriptElement.onerror = reject;
      document.body.appendChild(scriptElement);
    });
  }
}
