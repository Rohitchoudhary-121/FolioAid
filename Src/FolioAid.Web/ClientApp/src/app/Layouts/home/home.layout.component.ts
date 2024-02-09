import { AfterViewInit, Component, OnInit } from '@angular/core';
import { ScriptsLoaderService } from 'src/Providers/script.load.provider';

@Component({
  selector: 'app-home.layout',
  templateUrl: './home.layout.component.html'
})
export class HomeLayoutComponent implements OnInit, AfterViewInit {
  constructor(private scriptLoaderService: ScriptsLoaderService) { }
  ngOnInit(): void {
    // Perform any initialization tasks here
  }

  ngAfterViewInit(): void {
    this.scriptLoaderService.loadScripts().then(() => {
      // Scripts have been loaded and executed
      // Perform any additional actions or bindings here
    });
  }
}
