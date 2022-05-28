import { Component, Injector, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { LogEntryServiceProxy, LogStats } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';

@Component({
  templateUrl: './home.component.html',
  animations: [appModuleAnimation()],
  changeDetection: ChangeDetectionStrategy.Default
})
export class HomeComponent extends AppComponentBase implements OnInit {

  logStats: LogStats[] = [];
  projectId;

  constructor(
    injector: Injector,
    private _logEntryesService: LogEntryServiceProxy,
    ) {
    super(injector);
  }

  ngOnInit(): void {
      this.getStats();
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
}
