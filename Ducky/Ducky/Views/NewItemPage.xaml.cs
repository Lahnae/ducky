using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;

namespace Ducky
{
    public partial class NewItemPage : ContentPage
    {
        public Sightings Sightings { get; set; }
        List<Species> Species { get; set; }
        public NewItemPage()
        {
            InitializeComponent();
            Species = new List<Species>();
            Sightings = new Sightings
            {
                Species = "",
                Description = "",
                Count = 0,
                DateTime = DateTime.Now
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (Sightings.Count > 0 && Sightings.Species != "")
            {
                MessagingCenter.Send(this, "AddItem", Sightings);
                await Navigation.PopToRootAsync();
            }
            if(Sightings.Species == "")
            {
                await DisplayAlert("Virhe!", "Valitse listalta ankka laji", "OK");
            }
            if(Sightings.Count < 1)
            {
                await DisplayAlert("Virhe!", "Anna lintujen havainto määräksi enemmän kuin 0", "OK");
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var client = new HttpClient()
            {
                BaseAddress = new Uri(Constants.RestUrl)
            };
            var response = await client.GetAsync("/species");
            if (response.IsSuccessStatusCode)
            {
                var usersJson = await response.Content.ReadAsStringAsync();
                var rootobject = JsonConvert.DeserializeObject<List<Species>>(usersJson);
                foreach (var user_json in rootobject)
                {
                    Species.Add(user_json);
                }
                picker.ItemsSource = Species;
            }
    }

        private void Lajit_Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                Valittu_Laji_Label.Text = picker.Items[selectedIndex];
            }
        }
    }
}
