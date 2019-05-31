import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AddTaskComponent } from './add-task/add-task.component';
import { ProjectComponent } from './project/project.component';
import { UserComponent } from './user/user.component';
import { ViewTaskComponent } from './view-task/view-task.component';
import { userFilterPipe } from './pipes/user-filter-pipe';
import { HttpModule } from '@angular/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDatepickerModule, ModalModule } from 'ngx-bootstrap';
import { ToastrModule } from 'ngx-toastr';
import { Ng5SliderModule } from 'ng5-slider';
import { UserService } from './services/user.service';
import { EventService } from './services/event.service';
import { BaseService } from './services/base.service';
import { ProjectService } from './services/project.service';
import { TaskService } from './services/task.service';

@NgModule({
  declarations: [
    AppComponent,
    AddTaskComponent,
    ProjectComponent,
    UserComponent,
    ViewTaskComponent,
    userFilterPipe
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    BrowserAnimationsModule,
    BsDatepickerModule.forRoot(),
    ModalModule.forRoot(),
    AppRoutingModule,
    ToastrModule.forRoot({
      timeOut: 3000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true
    }),
    Ng5SliderModule
  ],
  providers: [UserService, EventService, BaseService, ProjectService,TaskService],
  bootstrap: [AppComponent]
})
export class AppModule { }
