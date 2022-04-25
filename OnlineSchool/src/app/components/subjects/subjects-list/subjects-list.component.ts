import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { SubjectResponse } from 'src/app/models/response/subject-response.model';
import { SubjectsService } from 'src/app/services/subjects.service';

@Component({
  selector: 'app-subjects-list',
  templateUrl: './subjects-list.component.html',
  styleUrls: ['./subjects-list.component.css']
})
export class SubjectsListComponent implements OnInit {
  public subjects: SubjectResponse[] = [];

  constructor(
    private subjectsService: SubjectsService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.subjectsService.getAllSubjects('').subscribe(subjects => {
      this.subjects = subjects;
    });
  }

  onDelete(id: string) {
    this.subjectsService.deleteSubject(id).subscribe(() => {
      this.toastr.success('Предметът е изтрит успешно');
      const removedElement = this.subjects.find(c => c.id === id)!;
      const index = this.subjects.indexOf(removedElement);
      this.subjects.splice(index, 1);
    });
  }

}
