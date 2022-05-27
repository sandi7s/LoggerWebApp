import {
    Component,
    Injector,
    OnInit,
    Output,
    EventEmitter
  } from '@angular/core';
  import { BsModalRef } from 'ngx-bootstrap/modal';
  import { AppComponentBase } from '@shared/app-component-base';
  import {
    CreateProjectDto,
    ProjectServiceProxy
  } from '@shared/service-proxies/service-proxies';
  
  @Component({
    templateUrl: 'create-project-dialog.component.html'
  })
  export class CreateProjectDialogComponent extends AppComponentBase
    implements OnInit {
    saving = false;
    project: CreateProjectDto = new CreateProjectDto();
  
    @Output() onSave = new EventEmitter<any>();
  
    constructor(
      injector: Injector,
      public _projectService: ProjectServiceProxy,
      public bsModalRef: BsModalRef
    ) {
      super(injector);
    }
  
    ngOnInit(): void {
      //this.project.isActive = true;
    }
  
    save(): void {
      this.saving = true;
  
      this._projectService.create(this.project).subscribe(
        () => {
          this.notify.info(this.l('SavedSuccessfully'));
          this.bsModalRef.hide();
          this.onSave.emit();
        },
        () => {
          this.saving = false;
        }
      );
    }
  }
  