using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestFormsShared
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

	    private async void LoginBtn_OnClicked(object sender, EventArgs e)
	    {
	        string username = Username.Text;
	        string password = Password.Text;

	        if (username == null && password == null)
	        {
	            await DisplayAlert("Whoops", "Username or password seems to be wrong", "Try Again");

	        }
	        else
	        {
                await Navigation.PushAsync(new MainTabedPage());
                Navigation.RemovePage(this);
            }
        }
	}
}
