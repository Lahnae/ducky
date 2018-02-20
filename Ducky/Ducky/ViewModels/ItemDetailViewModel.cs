using System;

namespace Ducky
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Sightings Item { get; set; }
        public ItemDetailViewModel(Sightings item = null)
        {
            Title = item?.Species;
            Item = item;
        }
    }
}
