import { Component, OnInit, TemplateRef } from '@angular/core';
import { Project } from '../models/project';
import * as moment from 'moment';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { User } from '../models/user';
import { EventService } from '../services/event.service';
import { ProjectService } from '../services/project.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css']
})
export class ProjectComponent implements OnInit {

  projectToAdd: Project;
  buttonName: string;
  startEndDateEnable: boolean;
  minStartDate: Date;
  minEndDate: Date;
  modalRef: BsModalRef;
  selectedIndexUser: number;
  selectedUser: string;
  users: Array<User>;
  searchText: string;
  searchTextUser: string;
  projects: Array<Project>;
  isStartDateAsc: boolean;
  isEndDateAsc: boolean;
  isPriorityAsc: boolean;
  isCompletedAsc: boolean;

  constructor(private eventService: EventService, private projectService: ProjectService,
    private userService: UserService, private modalService: BsModalService) {
    this.users = new Array<User>();
    this.projects = new Array<Project>();
  }

  ngOnInit() {
    this.projectToAdd = new Project();
    this.buttonName = 'Add';
    this.projectToAdd.Priority = 0;
    this.minStartDate = new Date();
    this.minEndDate = new Date();
    this.minEndDate.setDate(this.minStartDate.getDate() + 1);
    this.eventService.showLoading(true);
    this.userService.getUser().subscribe((user) => {
      this.users = user;

      this.eventService.showLoading(false);
    },
      (error) => {
        this.eventService.showError(error);
        this.eventService.showLoading(false);
      });
    this.eventService.showLoading(true);
    this.projectService.getProject().subscribe((project) => {
      this.projects = project;
      this.eventService.showLoading(false);
    },
      (error) => {
        this.eventService.showError(error);
        this.eventService.showLoading(false);
      });
  }
  setStartEndDateChange($event) {
    if (this.startEndDateEnable) {
      this.projectToAdd.ProjectStartDate = this.projectToAdd.ProjectStartDate ?
        this.projectToAdd.ProjectStartDate : moment(new Date()).format('MM-DD-YYYY').toString();
      this.projectToAdd.ProjectEndDate = this.projectToAdd.ProjectEndDate ? this.projectToAdd.ProjectEndDate :
        moment(new Date()).add(1, 'days').format('MM-DD-YYYY').toString();
    } else {
      this.projectToAdd.ProjectStartDate = null;
      this.projectToAdd.ProjectEndDate = null;
    }
  }
  setMinEndDate($event) {
    this.minEndDate = moment(this.projectToAdd.ProjectStartDate).add(1, 'days').toDate();
    if (moment(this.projectToAdd.ProjectEndDate) <= moment(this.projectToAdd.ProjectStartDate)) {
      this.projectToAdd.ProjectEndDate = moment(this.minEndDate).format('MM-DD-YYYY').toString();
    }
  }


  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);


  }
  setIndex(index: number) {
    this.selectedIndexUser = index;
  }
  selectUser() {
    this.projectToAdd.User.UserId = +this.users[this.selectedIndexUser].UserId;
    this.selectedUser = this.users[this.selectedIndexUser].FirstName;
    this.selectedIndexUser = null;
    this.modalRef.hide();
  }

  addProject() {
    if (!this.projectToAdd.ProjectName || this.projectToAdd.ProjectName === '') {
      this.eventService.showWarning('Please add project name ');
      return;
    }
    if (!this.projectToAdd.Priority) {
      this.eventService.showWarning('Please set priority ');
      return;
    }
    if (this.startEndDateEnable && (!this.projectToAdd.ProjectStartDate || this.projectToAdd.ProjectStartDate.toString() === '')) {
      this.eventService.showWarning('Please select start date ');
      return;
    }
    if (this.startEndDateEnable && (!this.projectToAdd.ProjectEndDate || this.projectToAdd.ProjectEndDate.toString() === '')) {
      this.eventService.showWarning('Please select end date ');
      return;
    }
    if (!this.projectToAdd.User.UserId || this.projectToAdd.User.UserId.toString() === '') {
      this.eventService.showWarning('Please select userId ');
      return;
    }
    if (!this.startEndDateEnable) {
      this.projectToAdd.ProjectStartDate = null;
      this.projectToAdd.ProjectEndDate = null;
    }
    if (this.buttonName === 'Add') {
      this.eventService.showLoading(true);
      this.projectService.addProject(this.projectToAdd).subscribe((data) => {
        this.eventService.showSuccess('Saved successfully');
        this.resetProject();
        this.ngOnInit();
        this.eventService.showLoading(false);
      },
        (error) => {
          this.eventService.showError(error);
          this.eventService.showLoading(false);
        });
    }
    if (this.buttonName === 'Update') {
      this.eventService.showLoading(true);
      this.projectService.updateProject(this.projectToAdd).subscribe((data) => {
        this.eventService.showSuccess('Update successfully')
        this.ngOnInit();
        this.eventService.showLoading(false);
      },
        (error) => {
          this.eventService.showError(error);
          this.eventService.showLoading(false);
        });
    }

  }
  resetProject() {
    this.projectToAdd = new Project();
    this.startEndDateEnable = false;
    this.buttonName = 'Add';
    this.projectToAdd.Priority = 0;
    this.minStartDate = new Date();
    this.minEndDate = new Date();
    this.minEndDate.setDate(this.minStartDate.getDate() + 1);
    this.selectedUser = null;
    this.selectedIndexUser = null;
  }

  editProject(project) {
    this.buttonName = 'Update';
    this.selectedUser = this.users.find(x => x.UserId === project.User.UserId).FirstName;
    this.projectToAdd = project;
    if (project.ProjectStartDate && project.ProjectEndDate) {
      this.projectToAdd.ProjectStartDate = moment(this.projectToAdd.ProjectStartDate).format('MM-DD-YYYY').toString();
      this.projectToAdd.ProjectEndDate = moment(this.projectToAdd.ProjectEndDate).format('MM-DD-YYYY').toString();
      this.startEndDateEnable = true;
    } else {
      this.startEndDateEnable = false;
    }
  }

  deleteProject(project) {
    this.eventService.showLoading(true);
    this.projectService.deleteProject(project).subscribe((data) => {
      this.eventService.showSuccess('Project suspended successfully')
      this.ngOnInit();
      this.eventService.showLoading(false);
    },
      (error) => {
        this.eventService.showError(error);
        this.eventService.showLoading(false);
      });
  }

  sortProject(type: number) {
    if (type === 1) {
      this.isStartDateAsc = !this.isStartDateAsc;
      const direction = this.isStartDateAsc ? 1 : -1;
      this.projects.sort((a, b) => (a.ProjectStartDate > b.ProjectStartDate) ? 1 * direction
        : ((b.ProjectStartDate > a.ProjectStartDate) ? -1 * direction : 0));
    }
    if (type === 2) {
      this.isEndDateAsc = !this.isEndDateAsc;
      const direction = this.isEndDateAsc ? 1 : -1;
      this.projects.sort((a, b) => (a.ProjectEndDate > b.ProjectEndDate) ? 1 * direction :
        ((b.ProjectEndDate > a.ProjectEndDate) ? -1 * direction : 0));
    }
    if (type === 3) {
      this.isPriorityAsc = !this.isPriorityAsc;
      const direction = this.isPriorityAsc ? 1 : -1;
      this.projects.sort((a, b) => (a.Priority > b.Priority) ? 1 * direction :
        ((b.Priority > a.Priority) ? -1 * direction : 0));
    }
    if (type === 4) {
      this.isCompletedAsc = !this.isCompletedAsc;
      const direction = this.isCompletedAsc ? 1 : -1;
      this.projects.sort((a, b) => (a.NoOfCompletedTasks > b.NoOfCompletedTasks) ? 1 * direction :
        ((b.NoOfCompletedTasks > a.NoOfCompletedTasks) ? -1 * direction : 0));
    }
    this.projects = [...this.projects];
  }
}
