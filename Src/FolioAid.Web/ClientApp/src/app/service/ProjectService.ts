import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Uri } from '../shared/url';
import { ProjectRequestModel, ResultModel } from '../models/ProjectModel';

@Injectable({
  providedIn: 'root'
})

export class ProjectService {
  constructor(private http: HttpClient) { }

  getAllProjects(payload:ProjectRequestModel) {
    return this.http.post<ResultModel<any>>(Uri.getAllProjects, payload);
  }
  getImage(base64String) {
    var imageSrc = 'data:image/*;base64,' + base64String; // Replace 'image/png' with the actual image format
    return imageSrc;
  }

  getAllTechnologies()
  {
    return this.http.get<ResultModel<any>>(Uri.getAllTechnologies);
  }
  getAllIndustries()
  {
    return this.http.get<ResultModel<any>>(Uri.getAllIndustries);
  }
  addNewProject(payload:any)
  {
    return this.http.post<ResultModel<any>>(Uri.addProject, payload);
  }



  getAllProjectsPublicViewList(payload:ProjectRequestModel) {
    return this.http.post<ResultModel<any>>(Uri.getAllProjectsByPortolioString, payload);
  }

  getProjectById(payload:any)
  {
    return this.http.post<ResultModel<any>>(Uri.getProjectById, payload);
  }
  
  updateProject(payload :any)
  {
    return this.http.post<ResultModel<any>>(Uri.updateProject,payload);
  }

  deleteProject(payload :any){
    return this.http.post<ResultModel<any>>(Uri.deleteProjectById,payload);
  }

}
