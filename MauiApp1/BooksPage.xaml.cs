using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;
using MauiApp1.ViewModels;
using MauiApp1.Services;

namespace MauiApp1
{
    public partial class BooksPage : ContentPage
    {
        public BooksPage()
        {
            InitializeComponent();
            BindingContext = new BooksViewModel(new BookService());
        }
        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            // Handle item selection if needed
        }
    }
}