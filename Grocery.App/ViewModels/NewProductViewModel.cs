using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System;

namespace Grocery.App.ViewModels
{
    public partial class NewProductViewModel : BaseViewModel
    {
        private readonly IProductService _productService;

        [ObservableProperty]
        string productName = string.Empty;

        [ObservableProperty]
        string stockText = string.Empty;

        [ObservableProperty]
        string priceText = string.Empty;

        [ObservableProperty]
        DateTime shelfLifeDate = DateTime.Today.AddMonths(1);

        public NewProductViewModel(IProductService productService)
        {
            _productService = productService;
            Title = "Nieuw Product";
        }

        [RelayCommand]
        public async Task SaveProduct()
        {
            int.TryParse(StockText, out int stock);
            bool priceParsed = decimal.TryParse(PriceText, out decimal price);

            if (stock == 0 || string.IsNullOrWhiteSpace(ProductName) || !priceParsed)
            {
                return;
            }
            var product = new Product(
                id: 0,
                name: ProductName,
                stock: stock,
                shelfLife: DateOnly.FromDateTime(ShelfLifeDate),
                price: price
            );

            _productService.Add(product);

            ClearInputFields();
        }

        private void ClearInputFields()
        {
            ProductName = string.Empty;
            StockText = string.Empty;
            PriceText = string.Empty;
            ShelfLifeDate = DateTime.Today.AddMonths(1);
        }
    }
}
