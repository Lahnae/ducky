using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Ducky
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;
        bool sorted = false;
        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Sightings;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewItemPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        private void Button_Clicked_Jarjesta(object sender, EventArgs e)
        {
            
            if (sorted == false)
            {
                Button_Jarjesta.Text = "Järjestä (Vanhin ensin)";
                ObservableCollection<Sightings> dateSort = new ObservableCollection<Sightings>(viewModel.Items.OrderBy(x => x.DateTime));
                ItemsListView.ItemsSource = dateSort;
                sorted = true;
            }
            else
            {
                Button_Jarjesta.Text = "Järjestä (Uusin ensin)";
                ObservableCollection<Sightings> dateSort = new ObservableCollection<Sightings>(viewModel.Items.OrderByDescending(x => x.DateTime));
                ItemsListView.ItemsSource = dateSort;
                sorted = false;
            }
        }
    }
}