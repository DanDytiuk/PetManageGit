using PetManage.ViewModels;

namespace PetManage.View;

public partial class AddInfoFoodPage : ContentPage
{
	public AddInfoFoodPage(AddInfoFoodVM VM)
	{
		InitializeComponent();
        BindingContext = VM;
    }
}