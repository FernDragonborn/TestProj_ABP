import { UserDataService } from './../../services/user-data.service';
import { Component } from '@angular/core';
import { UserData } from '../../models/userData.model';
@Component({
  selector: 'app-results-table',
  templateUrl: './results-table.component.html',
  styleUrl: './results-table.component.scss'
})
export class ResultsTableComponent {
  
  users: UserData[] | undefined;
  user: UserData | undefined;
  constructor(private userDataService: UserDataService) { }

  ngOnInit(): void {
    this.userDataService.GetData()
      .subscribe(
        (data) => {
          this.users = data
        },
        (error) => console.log()
      )
  }
}
