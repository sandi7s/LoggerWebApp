import { Component, Injector } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
  PagedListingComponentBase,
  PagedRequestDto,
} from '@shared/paged-listing-component-base';
import {
  ProjectDtoPagedResultDto,
  ProjectDto,
  ProjectServiceProxy,
} from '@shared/service-proxies/service-proxies';

class PagedProjectsRequestDto extends PagedRequestDto {
  keyword: string;
}

@Component({
  templateUrl: './projects.component.html',
  animations: [appModuleAnimation()]
})
export class ProjectsComponent extends PagedListingComponentBase<ProjectDto> {
  projects: ProjectDto[] = [];
  keyword = '';
  advancedFiltersVisible = false;

  constructor(
    injector: Injector,
    private _projectsService: ProjectServiceProxy,
    private _modalService: BsModalService
  ) {
    super(injector);
  }

  list(
    request: PagedProjectsRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;

    this._projectsService
      .getAll(
        request.keyword,
        request.skipCount,
        request.maxResultCount
      )
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: ProjectDtoPagedResultDto) => {
        this.projects = result.items;
        this.showPaging(result, pageNumber);
      });
  }

  delete(project: ProjectDto): void {
    abp.message.confirm(
      this.l('DeleteWarningMessage', project.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this._projectsService
            .delete(project.id)
            .pipe(
              finalize(() => {
                abp.notify.success(this.l('SuccessfullyDeleted'));
                this.refresh();
              })
            )
            .subscribe(() => {});
        }
      }
    );
  }

  clearFilters(): void {
    this.keyword = '';
    this.getDataPage(1);
  }
}
