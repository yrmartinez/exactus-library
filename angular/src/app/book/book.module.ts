import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BookRoutingModule } from './book-routing.module';
import { BookComponent } from './book.component';
import { SharedModule } from '../shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { BookDetailComponent } from './book-detail/book-detail.component';

@NgModule({
  declarations: [BookComponent, BookDetailComponent],
  imports: [CommonModule, BookRoutingModule, SharedModule, NgbDatepickerModule],
})
export class BookModule {}
