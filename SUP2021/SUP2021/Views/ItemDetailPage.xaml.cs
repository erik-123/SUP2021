﻿using SUP2021.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace SUP2021.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}