using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiCollectionViewSelectedColor
{
    public partial  class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<object> itemCollection = new();

        [ObservableProperty]
        private string retourValue = "Gute Nacht";

        public MainPageViewModel()
        {
            itemCollection.Add(new RowContent() { DisplayValue = "Hallo", Name = "Roland" });
            itemCollection.Add(new RowContent() { DisplayValue = "Guten Morgen", Name = "Monika" });
            itemCollection.Add(new RowContent() { DisplayValue = "Guten Abend", Name = "Max" });
            itemCollection.Add(new RowContent() { DisplayValue = "Good afternoon", Name = "Robert" });
            itemCollection.Add(new RowContent() { DisplayValue = "Guten Tag", Name = "Lisa" });
        }

        [RelayCommand]
        public async Task Tap(object s )
        {
            RetourValue = ((RowContent)s).DisplayValue;
        }
    }
}
