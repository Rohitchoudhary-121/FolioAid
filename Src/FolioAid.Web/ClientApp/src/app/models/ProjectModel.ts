export class ProjectRequestModel {
  PageSize!: number;
  CurrentPage!: number;
  Search!: string;
  Technologies!: string[];
  PortfolioString!: string;
  Industries!: string[];
}
export class ResultModel<T> {
  result: T;
  isSuccess: boolean = false;
  message: string = "";
  errors: string[] = [];

  constructor(result: T) {
    this.result = result;
  }
}
export class LoginUserCommand {
  id: any;
  Email: any;
  Password: any;
}
export class GetProjectRequestModel{
  id: any;
}

