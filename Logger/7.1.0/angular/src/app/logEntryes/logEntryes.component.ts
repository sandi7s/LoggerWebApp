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
  LogStats,
  PagedLogEntryResultRequestDto,
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
  sorting: string = "timestamp desc";

  logStats: LogStats[] = [];

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
      this.getStats();
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
        this.sorting,
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

  getStats() {
    this._logEntryesService
      .getStatsAllStats(
        this.projectId
      )
      .pipe(
        finalize(() => {
          //finishedCallback();
        })
      )
      .subscribe((result: LogStats[]) => {
        this.logStats = result;
      });
  }


  excellExport() {
    let input = new PagedLogEntryResultRequestDto();
    input.keyword = this.keyword;
    input.projectId = this.projectId;
    input.sorting = this.sorting;

    this._logEntryesService
        .createExcelLogs(
          input
        )
        .pipe(
            finalize(() => {
                //finishedCallback();
            })
        )
        .subscribe((result) => {
            this.downloadFile(result);
        });
}

_base64ToArrayBuffer(base64) {
    var binary_string = window.atob(base64);
    var len = binary_string.length;
    var bytes = new Uint8Array(len);
    for (var i = 0; i < len; i++) {
        bytes[i] = binary_string.charCodeAt(i);
    }
    return bytes.buffer;
}

downloadFile(result) {
    let bytes = this._base64ToArrayBuffer(result);
    let file = new Blob([bytes], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;' });
    var fileUrl = URL.createObjectURL(file);
    //window.open(fileUrl,"_blank");
    //window.open(fileUrl,);
    //window.location.assign(fileUrl);
    //URL.revokeObjectURL(fileUrl);
    
    let link = document.createElement('a');
    link.href = fileUrl;
    link.download = this.projectId;
    document.body.appendChild(link);
    link.dispatchEvent(new MouseEvent('click', { bubbles: true, cancelable: true, view: window }));
    link.remove();
    window.URL.revokeObjectURL(link.href);
    document.body.removeChild(link);
}
}
