import type { CommentDto, CreateUpdateCommentDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CommentsService {
  apiName = 'Default';

  create = (input: CreateUpdateCommentDto) =>
    this.restService.request<any, CommentDto>({
      method: 'POST',
      url: `/api/app/comments`,
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/comments/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, CommentDto>({
      method: 'GET',
      url: `/api/app/comments/${id}`,
    },
    { apiName: this.apiName });

  getCommentsByBook = (bookId: string) =>
    this.restService.request<any, CommentDto[]>({
      method: 'GET',
      url: `/api/app/comments/comments-by-book/${bookId}`,
    },
    { apiName: this.apiName });

  getList = (input: PagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<CommentDto>>({
      method: 'GET',
      url: `/api/app/comments`,
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount, sorting: input.sorting },
    },
    { apiName: this.apiName });

  update = (id: string, input: CreateUpdateCommentDto) =>
    this.restService.request<any, CommentDto>({
      method: 'PUT',
      url: `/api/app/comments/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
