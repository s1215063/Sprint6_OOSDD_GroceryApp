using Grocery.Core.Models;
using Grocery.App.ViewModels;

namespace Grocery.App.Views;

public partial class NewProductView : ContentPage
{
    private readonly NewProductViewModel _viewModel;

    public NewProductView(NewProductViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

}