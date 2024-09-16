using MauiApp1.Models;
using MauiApp1.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1.ViewModels
{
    public class BooksViewModel : INotifyPropertyChanged
    {
        private readonly BookService _bookService;
        private Book _selectedBook;
        private string _title;
        private string _author;
        private string _genre;

        public BooksViewModel(BookService bookService)
        {
            _bookService = bookService;
            Books = new ObservableCollection<Book>();
            AddBookCommand = new Command(async () => await AddBookAsync());
            EditBookCommand = new Command<Book>(async (book) => await EditBookAsync(book));
            DeleteBookCommand = new Command<Book>(async (book) => await DeleteBookAsync(book));
            LoadBooks();
        }

        public ObservableCollection<Book> Books { get; }

        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
                _selectedBook = value;
                OnPropertyChanged(nameof(SelectedBook));
                if (_selectedBook != null)
                {
                    Title = _selectedBook.Title;
                    Author = _selectedBook.Author;
                    Genre = _selectedBook.Genre;
                }
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Author
        {
            get => _author;
            set
            {
                _author = value;
                OnPropertyChanged(nameof(Author));
            }
        }

        public string Genre
        {
            get => _genre;
            set
            {
                _genre = value;
                OnPropertyChanged(nameof(Genre));
            }
        }

        public ICommand AddBookCommand { get; }
        public ICommand EditBookCommand { get; }
        public ICommand DeleteBookCommand { get; }

        private async Task LoadBooks()
        {
            var books = await _bookService.GetBooksAsync();
            Books.Clear();
            foreach (var book in books)
            {
                Books.Add(book);
            }
        }

        private async Task AddBookAsync()
        {
            var book = new Book { Title = Title, Author = Author, Genre = Genre };
            await _bookService.AddBookAsync(book);
            await LoadBooks();

            Title = string.Empty;
            Author = string.Empty;
            Genre = string.Empty;
        }

        private async Task EditBookAsync(Book book)
        {
            if (book != null)
            {
                book.Title = Title;
                book.Author = Author;
                book.Genre = Genre;
                await _bookService.UpdateBookAsync(book);
                await LoadBooks();

                Title = string.Empty;
                Author = string.Empty;
                Genre = string.Empty;
            }
        }

        private async Task DeleteBookAsync(Book book)
        {
            if (book != null)
            {
                await _bookService.DeleteBookAsync(book.Id);
                await LoadBooks();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
