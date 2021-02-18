import { mapEnumToOptions } from '@abp/ng.core';

export enum BookType {
  DigitalCopy = 0,
  Hardcover = 1,
  Paperback = 2,
}

export const bookTypeOptions = mapEnumToOptions(BookType);
