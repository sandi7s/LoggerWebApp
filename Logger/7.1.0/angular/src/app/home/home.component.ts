import { Component, Injector, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { LogEntryServiceProxy, LogStats, ProjectDto, ProjectServiceProxy } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';
import { Router } from '@angular/router';

@Component({
  templateUrl: './home.component.html',
  animations: [appModuleAnimation()],
  changeDetection: ChangeDetectionStrategy.Default
})
export class HomeComponent extends AppComponentBase implements OnInit {

  logStats: LogStats[] = [];
  projects: ProjectDto[] = [];
  projectId;

  constructor(
    injector: Injector,
    private _logEntryesService: LogEntryServiceProxy,
    private _projectServiceProxy: ProjectServiceProxy,
    private _router: Router
    ) {
    super(injector);
  }

  ngOnInit(): void {
      this.getStats();
      this.getProjects();
  }

  goToProject(id: number){
    this._router.navigate(['/app/logEntryes', id]);
  }

  getProjects() {
    this._projectServiceProxy
      .getAllForFrontPage()
      .pipe(
        finalize(() => {
          //finishedCallback();
        })
      )
      .subscribe((result: ProjectDto[]) => {
        this.projects = result;
      });
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
