import { mapEnumToOptions } from '@abp/ng.core';

export enum BookStatus {
  Available = 0,
  CheckedOut = 1,
  Damaged = 2,
  Lost = 3,
}

export const bookStatusOptions = mapEnumToOptions(BookStatus);
