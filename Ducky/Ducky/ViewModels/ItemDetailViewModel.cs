using System;

namespace Ducky
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Sigtings Item { get; set; }
        public ItemDetailViewModel(Sigtings item = null)
        {
            Title = item?.Species;
            Item = item;
        }
    }
}
