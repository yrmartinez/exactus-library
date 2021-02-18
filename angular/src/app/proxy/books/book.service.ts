import type { BookDto, CreateUpdateBookDto, ReturnBookDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class BookService {
  apiName = 'Default';

  checkout = (bookGuid: string) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/book/checkout`,
      params: { bookGuid: bookGuid },
    },
    { apiName: this.apiName });

  create = (input: CreateUpdateBookDto) =>
    this.restService.request<any, BookDto>({
      method: 'POST',
      url: `/api/app/book`,
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/book/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, BookDto>({
      method: 'GET',
      url: `/api/app/book/${id}`,
    },
    { apiName: this.apiName });

  getList = (input: PagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<BookDto>>({
      method: 'GET',
      url: `/api/app/book`,
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount, sorting: input.sorting },
    },
    { apiName: this.apiName });

  return = (input: ReturnBookDto) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/book/return`,
      body: input,
    },
    { apiName: this.apiName });

  update = (id: string, input: CreateUpdateBookDto) =>
    this.restService.request<any, BookDto>({
      method: 'PUT',
      url: `/api/app/book/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
