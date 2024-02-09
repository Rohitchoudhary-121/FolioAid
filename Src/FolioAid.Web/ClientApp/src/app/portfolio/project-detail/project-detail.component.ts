import { Component, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbCarousel } from '@ng-bootstrap/ng-bootstrap';
import { GetProjectRequestModel } from 'src/app/models/ProjectModel';
import { ProjectService } from 'src/app/service/ProjectService';
export interface videoObject {
  image:any;
  thumbImage:any;
 };
@Component({
  selector: 'app-project-detail',
  templateUrl: './project-detail.component.html',
  styleUrls: ['./project-detail.component.css']
})

export class ProjectDetailComponent {
  progressValue: number = 0;
  formsubmit: boolean = false;
  @ViewChild('myCarousel') myCarousel: NgbCarousel;
  @ViewChild('myCarousel') myCarouselVideo: NgbCarousel;              
customOptionsGallery={items:0, dots:false, margin:5, autoWidth: true};
payload=new GetProjectRequestModel(); 
activeSliderId: string;
videoObjects:videoObject[]=[];
constructor (private projectService:ProjectService, private route: ActivatedRoute){}
projectDetail:any;
selectedImage: string;
  changeimage(image: string){
    this.selectedImage = image;
  }
  cycleToSlide(id) {
    this.myCarousel.select(''+ id);
  }
  cycleToVideo(id)
  {
    this.myCarouselVideo.select(''+ id);
  }


  ngOnInit() {
    this.projectdetails();
  }
  projectdetails() {
    this.formsubmit = true;
    this.progressValue = 20;
    for (var i = 1; i < 4; i++) {
      setTimeout(() => {
        this.progressValue += 20;
      }, 500);
    }
    this.payload.id = this.route.snapshot.params.id;
      this.projectService.getProjectById(this.payload).subscribe(response => {
        this.progressValue = 140;
        if (response.isSuccess) {
          setTimeout(() => {
            this.projectDetail = response.result;
            this.customOptionsGallery.items = response.result.imagesPath.length;
            this.projectDetail.videosPath.forEach(x => {
              this.videoObjects.push({ image: x, thumbImage: x })
            });
          }, 300);
        }
      });
  }
}

