import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ClassResponse } from 'src/app/models/response/class-response.model';
import { ClassesService } from 'src/app/services/classes.service';

@Component({
  selector: 'app-classes-list',
  templateUrl: './classes-list.component.html',
  styleUrls: ['./classes-list.component.css']
})
export class ClassesListComponent implements OnInit {
  public classes: ClassResponse[] = [];

  constructor(
    private classesService: ClassesService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.classesService.getAllClasses('').subscribe(classes => {
      this.classes = classes;
    });
  }

  onDelete(id: string) {
    this.classesService.deleteClass(id).subscribe(() => {
      this.toastr.success('Класът е изтрит успешно');
      const removedElement = this.classes.find(c => c.id === id)!;
      const index = this.classes.indexOf(removedElement);
      this.classes.splice(index, 1);
    });
  }

}
