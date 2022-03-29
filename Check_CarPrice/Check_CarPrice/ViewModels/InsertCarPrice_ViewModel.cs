using Check_CarPrice.Persistence;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Check_CarPrice.Model.Appz_Model;

namespace Check_CarPrice.ViewModels
{
    public class InsertCarPrice_ViewModel : BaseViewModel
    {
        [Obsolete]
        public ICommand ClickCommand => new Command<string>((url) =>
        {
            Device.OpenUri(new System.Uri(url));
        });

        public InsertCarPrice_ViewModel()
        {
            Title = "เพิ่มรายการรถ";
        }


        //public async  Task<ObservableCollection<DataAccessories>>  SetAll()
        //{
        //    _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

        //    var recipes = await _connection.Table<DataAccessories>().ToListAsync();
        //    _accessories = new ObservableCollection<DataAccessories>(recipes);

        //    return _accessories;
        //}
    }
}
