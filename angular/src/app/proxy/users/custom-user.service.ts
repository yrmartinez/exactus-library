import type { SmallUserDto } from './models';
import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CustomUserService {
  apiName = 'Default';

  getUserByName = (name: string) =>
    this.restService.request<any, SmallUserDto[]>({
      method: 'GET',
      url: `/api/app/custom-user/user-by-name`,
      params: { name: name },
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
