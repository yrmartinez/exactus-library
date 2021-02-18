import type { EntityDto } from '@abp/ng.core';

export interface SmallUserDto extends EntityDto<string> {
  name?: string;
}
