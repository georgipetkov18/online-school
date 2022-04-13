import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';

import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  {
    path: 'subjects',
    loadChildren: () =>
      import('./components/subjects/subjects-routing.module')
        .then(module => module.SubjectsRoutingModule)
  },
  {
    path: 'classes',
    loadChildren: () =>
      import('./components/classes/classes-routing.module')
        .then(module => module.ClassesRoutingModule)
  },
  {
    path: 'lessons',
    loadChildren: () =>
      import('./components/lessons/lessons-routing.module')
        .then(module => module.LessonsRoutingModule)
  },
  {
    path: 'timetable',
    loadChildren: () =>
      import('./components/timetable/timetable-routing.module')
        .then(module => module.TimetableRoutingModule)
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
