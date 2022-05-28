import { Component, Injector, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
  PagedListingComponentBase,
  PagedRequestDto,
} from '@shared/paged-listing-component-base';
import {
  LogEntryDtoPagedResultDto,
  LogEntryDto,
  LogEntryServiceProxy,
} from '@shared/service-proxies/service-proxies';
import { ActivatedRoute, Params } from '@angular/router';

class PagedLogEntryesRequestDto extends PagedRequestDto {
  keyword: string;
}

@Component({
  templateUrl: './logEntryes.component.html',
  animations: [appModuleAnimation()]
})
export class LogEntryesComponent extends PagedListingComponentBase<LogEntryDto> implements OnInit {
  logEntryes: LogEntryDto[] = [];
  keyword = '';
  advancedFiltersVisible = false;

  projectId;

  constructor(
    injector: Injector,
    private _logEntryesService: LogEntryServiceProxy,
    private _activatedRoute: ActivatedRoute,
    private _modalService: BsModalService
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._activatedRoute.params.subscribe((params: Params) => {
      this.projectId = params['id'];
      //console.log(this.projectId);
      this.getDataPage(1);
    });
  }

  list(
    request: PagedLogEntryesRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;

    this._logEntryesService
      .getAllPagedAndFiltered(
        request.keyword,
        this.projectId,
        request.skipCount,
        request.maxResultCount
      )
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: LogEntryDtoPagedResultDto) => {
        this.logEntryes = result.items;
        this.showPaging(result, pageNumber);
      });
  }

  delete(logEntry: LogEntryDto): void {
    abp.message.confirm(
      this.l('DeleteWarningMessage', logEntry.log),
      undefined,
      (result: boolean) => {
        if (result) {
          this._logEntryesService
            .delete(logEntry.id)
            .pipe(
              finalize(() => {
                abp.notify.success(this.l('SuccessfullyDeleted'));
                this.refresh();
              })
            )
            .subscribe(() => { });
        }
      }
    );
  }

  clearFilters(): void {
    this.keyword = '';
    this.getDataPage(1);
  }
}
