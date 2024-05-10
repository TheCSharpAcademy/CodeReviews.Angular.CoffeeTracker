import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { SmokeService } from '../../services/smoke.service';
import { SmokeLog } from '../../models/SmokeLog';
import { BehaviorSubject, Subject, map } from 'rxjs';
import { AsyncPipe, formatDate } from '@angular/common';
import { AddModalComponent } from '../add-modal/add-modal.component';
import { EditModalComponent } from '../edit-modal/edit-modal.component';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css',
  imports: [RouterLink, AsyncPipe, AddModalComponent, EditModalComponent],
})
export class DashboardComponent implements OnInit {
  logsSubject: Subject<SmokeLog[] | null> = new BehaviorSubject<
    SmokeLog[] | null
  >(null);
  logs$ = this.logsSubject.asObservable();
  isAddOpen = false;
  isEditOpen = false;
  logToUpdate: SmokeLog | null = null;

  constructor(private smokeService: SmokeService) {}

  ngOnInit(): void {
    this.getSmokeLogs();
  }

  getSmokeLogs() {
    this.smokeService.getLogs().subscribe((logs) => {
      this.logsSubject.next(logs);
    });
  }

  openAddModal() {
    this.isAddOpen = true;
  }

  closeAddModal() {
    this.isAddOpen = false;
    this.getSmokeLogs();
  }

  openEditModal(log: SmokeLog) {
    this.isEditOpen = true;
    this.setLogToUpdate(log);
  }

  private setLogToUpdate(log: SmokeLog) {
    this.logToUpdate = log;
  }

  closeEditModal() {
    this.isEditOpen = false;
    this.getSmokeLogs();
  }

  formatDate(dateStr: string, format: string, locale: string): string {
    return formatDate(dateStr, format, locale);
  }

  search(input: string) {
    this.smokeService
      .getLogs()
      .pipe(map((logs) => logs!.filter((log) => log.date.includes(input))))
      .subscribe((filteredLogs) => {
        this.logsSubject.next(filteredLogs);
      });
  }

  deleteLog(id: number) {
    this.smokeService.deleteLog(id).subscribe(() => {
      this.getSmokeLogs();
    });
  }
}
