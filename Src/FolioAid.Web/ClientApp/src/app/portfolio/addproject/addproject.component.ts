import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ProjectService } from 'src/app/service/ProjectService';
import { MessageService } from 'src/app/service/message.service';
//import  ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { Subscriber, Observable } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { GetProjectRequestModel } from 'src/app/models/ProjectModel';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-addproject',
  templateUrl: './addproject.component.html',
  styleUrls: ['./addproject.component.css']
})
export class AddprojectComponent {
  technologyGridResult: any[];
  industryGridResult: any[];
  projectForm: FormGroup
  selectedTechnologies:any;
  selectedIndustries:any;
  active:boolean=true;
  inactive:boolean=false;
  showIcon:boolean;
  showVideoIcon: boolean;
 // public Editor = ClassicEditor;
  images: string[] = [];
  videoUrls:any[]=[];
  videoUrl:any;
  format:any;
  payload=new GetProjectRequestModel();
  isEdit:boolean=false;
  bannerImage: any;

  progressValue: number = 0;
  formsubmit: boolean = false;

  constructor(private projectService: ProjectService, private messageService: MessageService, private router: Router, private route: ActivatedRoute, private toastr: ToastrService){
    this.projectForm = new FormGroup({
      bannerImage: new FormControl(null,Validators.required),
      title: new FormControl('',[Validators.required,Validators.maxLength(150)]),
      technologies: new FormControl(null, Validators.required),
      industries: new FormControl( null, Validators.required),
      summary: new FormControl(null,Validators.required),
      solutions: new FormControl(null,Validators.required),
      challenges: new FormControl(null,Validators.required),
      status: new FormControl(null,Validators.required),
      publicURL: new FormControl(null),
      demoURL: new FormControl(null),
      androidURL:new FormControl(null),
      appleURL: new FormControl(null),
      imagesPath: new FormControl(null,Validators.required),
      videosPath: new FormControl( null,Validators.required),
    })
   
  }

  ngOnInit() {
    this.payload.id=this.route.snapshot.params.id;
    if(this.payload.id !=null || this.payload.id!= undefined)
    {
      this.formsubmit = true;
      this.progressValue = 20;
      for (var i = 1; i < 4; i++) {
        setTimeout(() => {
          this.progressValue += 20;
        }, 100);
      }
      this.isEdit = true;
      
      this.projectService.getProjectById(this.payload).subscribe(res => {
        this.progressValue = 160;
        this.setProjectDetails(res.result)
        this.formsubmit = false;

      });
    
    }
    else
       this.isEdit=false;
    this.getAllTechnologies();
    this.getAllIndustries();
  }
  getAllTechnologies(){
    this.projectService.getAllTechnologies().subscribe(
    (response)=>{
    if(response.isSuccess)
    {
      this.technologyGridResult=response.result.technologies;
    };
   });
  }
  getAllIndustries(){
    this.projectService.getAllIndustries().subscribe(
    (response)=>{
    if(response.isSuccess)
    {
      this.industryGridResult=response.result.industries;
    };
   });
  }

  addNewProject()
  {
    if (this.projectForm.valid) {
      this.formsubmit = true;
      this.progressValue = 20;
      for (var i = 1; i < 4; i++) {
        setTimeout(() => {

          this.progressValue += 20;
        }, 100);
      }
      this.projectService.addNewProject(this.projectForm.value).subscribe(
        (response) => {
          this.progressValue = 140;
          this.formsubmit = false;
          if(response.isSuccess)
          {
           
            this.toastr.success('Project added successfully', '', {
              closeButton: true,
            });
            this.router.navigateByUrl('/project-listing');
          };
        }
      )
    }
    else {
      this.projectForm.get('bannerImage').markAsDirty();
      this.projectForm.get('imagesPath').markAsDirty();
      this.projectForm.get('videosPath').markAsDirty();
      this.projectForm.markAllAsTouched();
      return;
    }
  }

  reset(){
    this.projectForm.reset();
    this.images=[];
    this.videoUrls=[];
  }


  fileUpload(event) {
    let reader = new FileReader();
    if (event.target.files && event.target.files.length > 0){
      let file=event.target.files[0];
      let img = new Image();
      img.src = window.URL.createObjectURL( file );
      reader.readAsDataURL(file);
      var filename = event.target.files[0].name;
      reader.onload = () => {
        setTimeout(() => {
        const width = img.naturalWidth;
        const height = img.naturalHeight;
        console.log(width + '*' + height);
          if (width != 1200 && height != 800) {
            this.toastr.error('Banner size should be 1200 x 800', '', {
              closeButton: true,
            });
            this.projectForm.get('bannerImage').setValue(null);
            return;
          }
          else
          {
            this.projectForm.get('bannerImage').setValue(reader.result);
          }
          }  , 2000);}
   
      }
    else 
    this.projectForm.get('bannerImage').setValue(null);
  }

  onSelectImage(event) {
    if (event.target.files) {
        for (let i = 0; i < event.target.files.length; i++) {
                if(event.target.files[i].size> 10000000){
                  this.toastr.error('Size cannot exceed 10Mb', '', {
                    closeButton: true,
                  });
                   return;}
          if (this.images.length > 9) {
                  this.toastr.error('Maximum 10 images can be uploaded', '', {
                    closeButton: true,
                  });
                    return;
                  }
                var reader = new FileReader();
                reader.readAsDataURL(event.target.files[i]);
                reader.onload = (event:any) => {
                  this.images.push(event.target.result);
                  this.projectForm.patchValue({
                    imagesPath: this.images
                 });
                  
                }
        }
        this.showIcon=true;
    }
    else
      this.showIcon=false;

  }
 
  onSelectVideo($event: Event) {
    const target =$event.target as HTMLInputElement;
    if (target.files) {
      for (let i = 0; i < target.files.length; i++) {
           if(target.files[i].type.indexOf('video')> -1){
              if (target.files[i].size > 15000000){
                this.toastr.error('Size cannot exceed 150Mb', '', {
                  closeButton: true,
                });
                 return;}
            this.format = 'video';
            if(this.videoUrls.length>9)
             {
              this.toastr.error('Maximum 10 videos can be uploaded', '', {
                closeButton: true,
              });
                return; }
            }
            if(target.files[i].type.indexOf('image')> -1){
              this.toastr.error('Please upload a video', '', {
                closeButton: true,
              });
               return;
            }
      var reader = new FileReader();
      reader.readAsDataURL(target.files[i]);
      reader.onload = (event) => {
        this.videoUrl=(<FileReader>event.target).result;
        this.videoUrls.push(this.videoUrl);
        this.projectForm.patchValue({
           videosPath: this.videoUrls
        });
      }
    }
     this.showVideoIcon=true;
    }
    else  
    this.showVideoIcon=false;
  }

  deleteImg(img:any){
    var index=this.images.findIndex(x=>x==img);
    this.images.splice(index,1);
    if(this.images.length==0)
      this.projectForm.get('imagesPath').setValue(null);
   }

   deleteVideo(video :any)
   {
    var index=this.videoUrls.findIndex(x=>x==video);
    this.videoUrls.splice(index,1);
    if(this.videoUrls.length==0)
       this.projectForm.get('videosPath').setValue(null);
   }
   
   setProjectDetails(model:any)
   { 
    this.bannerImage=model.bannerImage;
    this.projectForm.get('bannerImage').setValue(model.bannerImage);
    this.projectForm.get('title').setValue(model.title);
    this.projectForm.get('summary').setValue(model.summary);
    this.projectForm.get('challenges').setValue(model.challenges);
    this.projectForm.get('solutions').setValue(model.solutions);
    this.projectForm.get('status').setValue(model.status);
    this.projectForm.get('publicURL').setValue(model.publicURL);
    this.projectForm.get('demoURL').setValue(model.demoURL);
    this.projectForm.get('androidURL').setValue(model.androidURL);
    this.projectForm.get('appleURL').setValue(model.appleURL);
    this.projectForm.get('technologies').setValue(model.technologies);
    this.projectForm.get('industries').setValue(model.industries);
    this.projectForm.get('imagesPath').setValue(model.imagesPath);
    this.projectForm.get('videosPath').setValue(model.videosPath);
    this.images = model.imagesPath;
    this.videoUrls = model.videosPath;
    if(model.imagesPath.length !=0)
       this.showIcon=true;
    else  this.showIcon=false;
    if(model.videosPath.length !=0)
       this.showVideoIcon =true;
    else  this.showVideoIcon=false;
   }

  updateProject(){
    if (this.projectForm.valid) {
      this.formsubmit = true;
      this.progressValue = 20;
      for (var i = 1; i < 4; i++) {
        setTimeout(() => {

          this.progressValue += 20;
        }, 100);
      }
    var projectData=this.projectForm.value;
    projectData.id=this.payload.id;
      this.projectService.updateProject(projectData).subscribe(res => {
        this.progressValue = 140;
        this.formsubmit = false;
        if (res.isSuccess)
        {
          setTimeout(() => {
            this.toastr.success('Project updated successfully', '', {
              closeButton: true,
            });
            this.router.navigateByUrl('project-listing');
          }, 300);
        }
     
    });
  } 
  else {
    this.projectForm.get('bannerImage').markAsDirty();
    this.projectForm.markAllAsTouched();
    return;
  }
}

}


