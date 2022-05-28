import { Component, Injector } from '@angular/core';
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

class PagedLogEntryesRequestDto extends PagedRequestDto {
  keyword: string;
}

@Component({
  templateUrl: './logEntryes.component.html',
  animations: [appModuleAnimation()]
})
export class LogEntryesComponent extends PagedListingComponentBase<LogEntryDto> {
  logEntryes: LogEntryDto[] = [];
  keyword = '';
  advancedFiltersVisible = false;

  constructor(
    injector: Injector,
    private _logEntryesService: LogEntryServiceProxy,
    private _modalService: BsModalService
  ) {
    super(injector);
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
