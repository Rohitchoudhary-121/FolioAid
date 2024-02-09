import { Injectable } from '@angular/core';
import { NotificationService } from '@progress/kendo-angular-notification';
import { DialogService, DialogCloseResult } from '@progress/kendo-angular-dialog';
@Injectable({
  providedIn: 'root'
})
export class MessageService {
 

  constructor( private notificationService: NotificationService) { }
  public showWarning(message: string): void {
    this.notificationService.show({
      content: message,
      hideAfter: 3000,
      position: { horizontal: 'right', vertical: 'bottom' },
      animation: { type: 'fade', duration: 1000 },
      type: { style: 'warning', icon: true }
    });
  }
  public success(message: string): void {
    this.notificationService.show({
      content: message,
      hideAfter: 3000,
      position: { horizontal: 'right', vertical: 'bottom' },
      animation: { type: 'slide', duration: 1000 },
      type: { style: 'success', icon: true },
      width: 400,
      height: 50 
      
    });
  }
  public error(message: string): void {
    this.notificationService.show({
      content: message,
      hideAfter: 3000,
      position: { horizontal: 'right', vertical: 'bottom' },
      animation: { type: 'slide', duration: 1000 },
      type: { style: 'error', icon: true },
      width: 400,
      height: 50 
    });
  }
  public info(message: string): void {
    this.notificationService.show({
      content: message,
      hideAfter: 3000,
      position: { horizontal: 'right', vertical: 'bottom' },
      animation: { type: 'slide', duration: 1000 },
      type: { style: 'info', icon: true }
    });
  }
 
}
