import { Component, OnInit, OnChanges, Input } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { ProjectRequestModel } from '../../models/ProjectModel';
import { ProjectService } from '../../service/ProjectService';
import { Router, ActivatedRoute, convertToParamMap } from '@angular/router';


@Component({
  selector: 'app-projectlistingpublicview',
  templateUrl: './projectlistingpublicview.component.html',
  styleUrls: ['./projectlistingpublicview.component.css']
})
export class ProjectlistingpublicviewComponent {
  string :string;
  length = 50;
  pageSize = 12;
  currentPage = 0;
  pageSizeOptions = [12, 24, 36, 48];
  search = null;
  showPageSizeOptions = true;
  showFirstLastButtons = true;
  pageEvent: PageEvent;
  gridResult: any;
  totalCount: number;
  state: ProjectRequestModel = new ProjectRequestModel();
  technologyGridResult: any[];
  industryGridResult: any[];
  selectedTechnologies = [];
  selectedIndustries = [];

  progressValue: number = 0;
  formsubmit: boolean = false;
  @Input() content: string;
  @Input() maxLength = 150;
  showFullContent = ""

  toggleContent(event, item) {
    console.log(event)
    if (event.target.innerText == "See Less") {
      this.showFullContent = ""
    }
    else {
      this.showFullContent = event.target.name;
    }
  }
  constructor(private projectService: ProjectService, private route: ActivatedRoute,private router:Router) {
    this.getAllTechnologies();
    this.getAllIndustries();
    this.ngOnInit()
    this.getData()
  }
  ngOnInit() {
    this.route.queryParams
      .subscribe(params => {
        console.log(params);
        this.string = params.string;
      }
      );
  }

  handlePageEvent(e: PageEvent) {
    this.pageEvent = e;
    this.pageEvent = e;
    this.length = e.length;
    this.pageSize = e.pageSize;
    this.currentPage = e.pageIndex;
    this.getData();
   
  }
  getData() {
    this.formsubmit = true;
    this.progressValue = 20;
    for (var i = 1; i < 4; i++) {
      setTimeout(() => {

        this.progressValue += 20;
      }, 500);
    }
    this.state.PageSize = this.pageSize;
    this.state.CurrentPage = this.currentPage;
    this.state.Search = this.search;
    this.state.Industries = this.selectedIndustries;
    this.state.Technologies = this.selectedTechnologies;
    this.state.PortfolioString = this.string;
    this.projectService.getAllProjectsPublicViewList(this.state).subscribe(
      (response) => {
        this.progressValue = 140;
       
        if (response.isSuccess) {
          setTimeout(() => {
            this.gridResult = response.result.projectList;
            this.length = response.result.totalCount;
            this.formsubmit = false;
          }, 300);
          //this.gridResult = response.result.projectList;
          //this.length = response.result.totalCount;
        }
        else {
          alert(response.message);
        }
      }
    );
  }

  getAllTechnologies() {
    this.projectService.getAllTechnologies().subscribe(
      (response) => {
        if (response.isSuccess) {
          this.technologyGridResult = response.result.technologies;
        };
      });
  }
  getAllIndustries() {
    this.projectService.getAllIndustries().subscribe(
      (response) => {
        if (response.isSuccess) {
          this.industryGridResult = response.result.industries;
        };
      });
  }
  onTechnologyChange(item: any, event: any) {
    if (event.target.checked) {
      this.selectedTechnologies.push(item.id);
    } else {
      for (var i = 0; i < this.technologyGridResult.length; i++) {
        if (this.selectedTechnologies[i] == item.id) {
          this.selectedTechnologies.splice(i, 1);
        }
      }
    }
    this.getData();
  }
  onIndustryChange(item: any, event: any) {
    if (event.target.checked) {
      this.selectedIndustries.push(item.id);
    } else {
      for (var i = 0; i < this.industryGridResult.length; i++) {
        if (this.selectedIndustries[i] == item.id) {
          this.selectedIndustries.splice(i, 1);
        }
      }
    }
    this.getData();
  }

  viewProjectDetail($event: any) { 
    this.router.navigateByUrl('project-detail/' + $event.id);
  }

}

