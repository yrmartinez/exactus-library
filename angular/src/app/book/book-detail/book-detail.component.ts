import { Toaster, ToasterService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BookDto, BookService } from '@proxy/books';
import { CommentDto, CommentsService, CreateUpdateCommentDto } from '@proxy/comments';

@Component({
  selector: 'app-book-detail',
  templateUrl: './book-detail.component.html',
  styleUrls: ['./book-detail.component.scss'],
})
export class BookDetailComponent implements OnInit {
  bookId: string;
  book: BookDto;
  comments: CommentDto[] = [];
  comment: string;
  createComment = {} as CreateUpdateCommentDto;
  options: Partial<Toaster.ToastOptions> = {
    life: 10000,
    sticky: false,
    closable: true,
    tapToDismiss: true,
    titleLocalizationParams: [],
  };
  
  constructor(
    private route: ActivatedRoute,
    private _bookService: BookService,
    private _commentsService: CommentsService,
    private toaster: ToasterService
  ) {}

  ngOnInit(): void {
    this.bookId = this.route.snapshot.paramMap.get('id');
    this.getBook();
  }

  getBook() {
    this._bookService.get(this.bookId).subscribe(book => {
      this.book = book;
      this.getComments();
    });
  }

  getComments() {
    this._commentsService.getCommentsByBook(this.bookId).subscribe(comments => {
      this.comments = comments;
    });
  }

  postComment() {
    this.createComment.comment = this.comment;
    this.createComment.bookId = this.bookId;
    this._commentsService.create(this.createComment).subscribe(() => {
      this.comment = null;
      this.toaster.success('::BookReturnedSuccessfully', 'AbpUi::Success', this.options);
      this.getComments();
    });
  }
}
