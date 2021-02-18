import { mapEnumToOptions } from '@abp/ng.core';

export enum BookLocation {
  ExactusOffice = 0,
  OwnersHome = 1,
  Matrix = 2,
}

export const bookLocationOptions = mapEnumToOptions(BookLocation);
