using PetManage.ViewModels;

namespace PetManage.View;

public partial class FoodPage : ContentPage
{
	public FoodPage(FoodVM VM)
	{
		InitializeComponent();
		BindingContext = VM;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await ((FoodVM)BindingContext).LoadInfoFoodCommand.ExecuteAsync(null);
	}
}