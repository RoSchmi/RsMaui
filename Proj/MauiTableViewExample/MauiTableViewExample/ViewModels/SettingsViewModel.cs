using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiTableViewExample.Cells;

namespace MauiTableViewExample.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        private int itemCounter = 0;
        private static TableSection section1 = new();
        private static List<string> names = new();

        private static SwitchCellSource switchCellSource = new SwitchCellSource(names, section1);

        [ObservableProperty]
        private string actItem = string.Empty;

        // Binding is accomplished through:
        // 'TableViewAccounts.Root = vm.AccountsTableRoot;' in 'SettingPagexaml.cs'
        [ObservableProperty]
        private TableRoot accountsTableRoot = new();

        public SettingsViewModel()
        {
            switchCellSource.SwitchCellSourceSend += SwitchCellSource_SwitchCellSourceSend;
        }

        private async void SwitchCellSource_SwitchCellSourceSend(SwitchCellSource sender, SwitchCellSource.SwitchCellSourceEventArgs e)
        {
            switch (e.Action)
            {
                case SwitchCellSource.CellAction.select:
                    {
                        ActItem = e.ItemName;

                        if (names.Contains(ActItem))
                        {
                            names.Remove(ActItem);
                        }

                        names.Remove(ActItem);
                        names.Insert(0, ActItem);                      
                        switchCellSource.Populate(names);                   
                    }
                    break;

                case SwitchCellSource.CellAction.delete:
                    {
                        var OkCancelResult = await Application.Current.MainPage.DisplayAlert("Alert", "Delete Item ?", "OK", "Cancel");
                        if (OkCancelResult == true)
                        {
                            string theItem = "";

                            if (e.ItemName != null)
                            {
                                if (e.ItemName.Length > 6)
                                {
                                    theItem = e.ItemName.Substring(0, e.ItemName.Length - 6);
                                }
                            }
                            try
                            {
                            }
                            catch (Exception ex)
                            {
#if DEBUG
                                Console.WriteLine(ex.Message);
#endif
                            }

                            int index = names.FindIndex(x => x == e.ItemName);

                            if ((index < names.Count) && (index != -1))
                            {
                                names.RemoveAt(index);
                            }
                            
                            switchCellSource.Populate(names);
                            ActItem = names.First();
                        }
                    }
                    break;

                case SwitchCellSource.CellAction.leave:
                    { /* do nothing */ }
                    break;

                default:
                    { /* do nothing */ }
                      
                    break;
            }
        }

        [RelayCommand]
        public void Add_TableView_Item()
        {
            itemCounter++;
            names.Add("Item_" + itemCounter.ToString());
            ActItem = names.First();

            AccountsTableRoot.Clear();          
            switchCellSource.Populate(names);
            AccountsTableRoot.Add(section1);
        }
    }
}
