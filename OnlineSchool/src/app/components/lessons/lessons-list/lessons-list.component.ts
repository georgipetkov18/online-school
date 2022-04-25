import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { LessonResponse } from 'src/app/models/response/lesson-response.model';
import { LessonsService } from 'src/app/services/lessons.service';

@Component({
  selector: 'app-lessons-list',
  templateUrl: './lessons-list.component.html',
  styleUrls: ['./lessons-list.component.css']
})
export class LessonsListComponent implements OnInit {
  public lessons: LessonResponse[] = [];

  constructor(
    private lessonsService: LessonsService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.lessonsService.getAllLessons('').subscribe(lessons => {
      this.lessons = lessons;
    });
  }

  onDelete(id: string) {
    this.lessonsService.deleteLesson(id).subscribe(() => {
      this.toastr.success('Часът е изтрит успешно');
      const removedElement = this.lessons.find(c => c.id === id)!;
      const index = this.lessons.indexOf(removedElement);
      this.lessons.splice(index, 1);
    });
  }

}
