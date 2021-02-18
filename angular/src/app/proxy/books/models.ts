import type { AuditedEntityDto } from '@abp/ng.core';
import type { BookStatus } from './book-status.enum';
import type { BookLocation } from './book-location.enum';
import type { BookType } from './book-type.enum';

export interface BookDto extends AuditedEntityDto<string> {
  title?: string;
  status: BookStatus;
  location: BookLocation;
  type: BookType;
  owner?: string;
  authors?: string;
  checkedOutById?: string;
  checkOutDate?: string;
}

export interface CreateUpdateBookDto {
  title: string;
  owner: string;
  type: BookType;
  status: BookStatus;
  location: BookLocation;
  authors?: string;
}

export interface ReturnBookDto {
  bookId: string;
  comments?: string;
}
