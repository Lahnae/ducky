using System;

using Xamarin.Forms;

namespace Ducky
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            Page itemsPage, aboutPage = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    itemsPage = new NavigationPage(new ItemsPage())
                    {
                        Title = "Havainnot"
                    };

                    aboutPage = new NavigationPage(new AboutPage())
                    {
                        Title = "Tietoja"
                    };
                    itemsPage.Icon = "tab_feed.png";
                    aboutPage.Icon = "tab_about.png";
                    break;
                default:
                    itemsPage = new ItemsPage()
                    {
                        Title = "Havainnot"
                    };

                    aboutPage = new AboutPage()
                    {
                        Title = "Tietoja"
                    };
                    break;
            }

            Children.Add(itemsPage);
            Children.Add(aboutPage);

            Title = Children[0].Title;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
