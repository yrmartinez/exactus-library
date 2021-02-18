import type { SmallUserDto } from '../users/models';

export interface CommentDto {
  comment?: string;
  user: SmallUserDto;
  id?: string;
}

export interface CreateUpdateCommentDto {
  comment?: string;
  bookId?: string;
}
