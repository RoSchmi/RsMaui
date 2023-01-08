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
                        ActItem = e.Account;

                        if (names.Contains(ActItem))
                        {
                            names.Remove(ActItem);
                        }

                        names.Remove(ActItem);
                        names.Insert(0, ActItem);

                        //AccountsTableRoot.Clear();
                        //section1.Clear();
                        //switchCellSource.SwitchCellSourceSend += SwitchCellSource_SwitchCellSourceSend;
                        switchCellSource.Populate(names);
                        //AccountsTableRoot.Add(section1); 
                    }
                    break;

                case SwitchCellSource.CellAction.delete:
                    {
                        var OkCancelResult = await Application.Current.MainPage.DisplayAlert("Alert", "Delete Credentials ?", "OK", "Cancel");
                        if (OkCancelResult == true)
                        {

                            string theAccount = "";

                            if (e.Account != null)
                            {
                                if (e.Account.Length > 6)
                                {
                                    theAccount = e.Account.Substring(0, e.Account.Length - 6);
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

                            int index = names.FindIndex(x => x == e.Account);

                            if ((index < names.Count) && (index != -1))
                            {
                                names.RemoveAt(index);
                            }
                            //AccountsTableRoot.Clear();
                            //section1.Clear();
                            switchCellSource.Populate(names);
                            ActItem = names.First();
                            //AccountsTableRoot.Add(section1);


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
