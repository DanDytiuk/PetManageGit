using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PetManage.Infrastructure;
using PetManage.Models;
using PetManage.Models.ModelViewPicker;
using PetManage.Services;
using System.Collections.ObjectModel;

namespace PetManage.ViewModels
{
    public partial class AddHealthInfoVM : ObservableObject
    {
        private readonly DatabaseService _database;

        #region Observable Property

        [ObservableProperty]
        public partial 

        #endregion

        #region Observable Collection



        #endregion

        #region Functions



        #endregion

        #region Commands



        #endregion

        #region Constructor

        public AddHealthInfoVM(DatabaseService database)
        {
            _database = database;
        }

        #endregion

    }
}
