using PetManage.ViewModels;

namespace PetManage.View;

public partial class FoodPage : ContentPage
{
	public FoodPage(FoodVM VM)
	{
		InitializeComponent();
		BindingContext = VM;
	}
}