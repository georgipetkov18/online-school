import { Component, OnInit } from '@angular/core';
import { UtilityService } from '../../../services/utility.service';

@Component({
  selector: 'app-create-subject',
  templateUrl: './create-subject.component.html',
  styleUrls: ['./create-subject.component.css']
})
export class CreateSubjectComponent implements OnInit {
  public errorMessage!: string;

  constructor(public utilityService: UtilityService) { }

  ngOnInit(): void {
  }

}
