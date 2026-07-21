using PetManage.ViewModels;

namespace PetManage
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainVM();
        }
    }
}
