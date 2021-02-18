import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import {
  BookService,
  BookDto,
  bookStatusOptions,
  bookLocationOptions,
  BookStatus,
  ReturnBookDto,
  bookTypeOptions,
} from '@proxy/books';
import { FormGroup, FormBuilder, Validators } from '@angular/forms'; // add this
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, Confirmation, ToasterService, Toaster } from '@abp/ng.theme.shared';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.scss'],
  providers: [ListService, { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }],
})
export class BookComponent implements OnInit {
  book = { items: [], totalCount: 0 } as PagedResultDto<BookDto>;

  form: FormGroup;
  commentsForm: FormGroup;

  bookStatuses = bookStatusOptions;
  bookLocations = bookLocationOptions;
  bookTypes = bookTypeOptions;

  isModalOpen = false;
  isDevolutionModalOpen = false;
  selectedBook: BookDto;
  currentUserId: string;

  currentComments: string;

  options: Partial<Toaster.ToastOptions> = {
    life: 10000,
    sticky: false,
    closable: true,
    tapToDismiss: true,
    titleLocalizationParams: [],
  };
  returnigBookId: string;

  constructor(
    public readonly list: ListService,
    private bookService: BookService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private oAuthService: OAuthService,
    private toaster: ToasterService
  ) {
    this.currentUserId = oAuthService.getIdentityClaims()['sub'];
  }

  ngOnInit() {
    const bookStreamCreator = query => this.bookService.getList(query);

    this.list.hookToQuery(bookStreamCreator).subscribe(response => {
      this.book = response;
    });
  }

  createBook() {
    this.selectedBook = {} as BookDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  // add buildForm method
  buildForm() {
    console.log(this.selectedBook);
    this.form = this.fb.group({
      title: [this.selectedBook.title || '', Validators.required],
      authors: [this.selectedBook.authors || '', Validators.required],
      owner: [this.selectedBook.owner || '', Validators.required],
      status: [
        this.selectedBook.status != null ? +this.selectedBook.status : null,
        Validators.required,
      ],
      location: [
        this.selectedBook.location != null ? +this.selectedBook.location : null,
        Validators.required,
      ],
    });
  }

  buildCommentsForm() {
    this.currentComments = null;
    this.commentsForm = this.fb.group({
      comments: [''],
    });
  }

  // add save method
  save() {
    if (this.form.invalid) {
      return;
    }

    const request = this.selectedBook.id
      ? this.bookService.update(this.selectedBook.id, this.form.value)
      : this.bookService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }

  editBook(id: string) {
    this.bookService.get(id).subscribe(book => {
      this.selectedBook = book;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', 'AbpAccount::AreYouSure').subscribe(status => {
      if (status === Confirmation.Status.confirm) {
        this.bookService.delete(id).subscribe(() => this.list.get());
      }
    });
  }

  checkout(id: string) {
    this.toaster.success('::BookCheckedOutSuccessfully', 'AbpUi::Success', this.options);
  }

  return(id: string) {
    this.currentComments = '';
    this.returnigBookId = id;
    this.buildCommentsForm();
    this.isDevolutionModalOpen = true;
  }

  returnBook() {
    let bookReturn = {} as ReturnBookDto;
    bookReturn.bookId = this.returnigBookId;
    bookReturn.comments = this.currentComments;
    this.bookService.return(bookReturn).subscribe(() => {
      this.isDevolutionModalOpen = false;
      this.toaster.success('::BookReturnedSuccessfully', 'AbpUi::Success', this.options);
    });
  }

  public get bookStatus(): typeof BookStatus {
    return BookStatus;
  }
}
