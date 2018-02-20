using System;

using Xamarin.Forms;

namespace Ducky
{
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public ItemDetailPage()
        {
            InitializeComponent();

            var sighting = new Sightings
            {
                Species = "Ankka laji",
                Description = "",
                Count = 0,
                DateTime = DateTime.Now
            };

            viewModel = new ItemDetailViewModel(sighting);
            BindingContext = viewModel;
        }

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
    }
}
