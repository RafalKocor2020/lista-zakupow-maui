using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace ListaZakupow
{
    public partial class MainPage : ContentPage
    {
        private readonly DatabaseService _database;
        public ObservableCollection<ShoppingItem> Items { get; } = new();

        public MainPage(DatabaseService database)
        {
            InitializeComponent();
            _database = database;
            BindingContext = this;
            LoadItems();
        }

        private async void LoadItems()
        {
            var items = await _database.GetItems();
            Items.Clear();
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }

        private async void OnEntryCompleted(object sender, EventArgs e)
        {
            await AddItemToList();
        }

        private async Task AddItemToList()
        {
            if (!string.IsNullOrWhiteSpace(newItemEntry.Text))
            {
                var newItem = new ShoppingItem { Name = newItemEntry.Text };
                await _database.AddItem(newItem);
                Items.Add(newItem);
                newItemEntry.Text = string.Empty;
            }
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is ShoppingItem item)
            {
                await _database.DeleteItem(item.Id);
                Items.Remove(item);
            }
        }
        private async void OnAddButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(newItemEntry.Text))
            {
                var newItem = new ShoppingItem { Name = newItemEntry.Text };
                await _database.AddItem(newItem);
                Items.Add(newItem);
                newItemEntry.Text = string.Empty;
            }
        }
        private async void OnClearListClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Potwierdzenie",
                                           "Czy na pewno chcesz usunąć WSZYSTKIE produkty z listy?",
                                           "Tak", "Nie");

            if (confirm && Items.Count > 0)
            {
                await _database.ClearAllItems();
                Items.Clear();
                await DisplayAlert("Sukces", "Lista została wyczyszczona", "OK");
            }
        }

    }
}