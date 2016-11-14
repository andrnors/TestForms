using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Hardware.Camera2;
using TestFormsShared.Models;
using TestFormsShared.Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestFormsShared
{
	public partial class HighScores : ContentPage
	{
	    private List<Game> _gameNames = new List<Game>();
	    private Game _gameName;

	    public List<Game> GameNames
	    {
	        get { return _gameNames;}
            set
            {
                if (value != null)
                {
                    _gameNames = value;
                    OnPropertyChanged();
                }  
            }
        }

	    public Game GameName
	    {
	        get { return _gameName; }
	        set
	        {
	            _gameName = value; 
	            OnPropertyChanged();
	        }
	    }

	    public HighScores ()
		{
			InitializeComponent ();
            GetAllGames();
		    GamesListView.ItemsSource = GameNames;
		}

	    public async void GetAllGames()
	    {
	        AmostiService amostiService = new AmostiService();
            GameNames.Clear();
	        var gameResults = await amostiService.GetGames();
            
	        foreach (var item in gameResults)
	        {
	            if (item.GameName != null && item.GameName != "null")
	            {
	                GameNames.Add(item);
                }
            }
        }

        private async void GamesListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        var selection = e.SelectedItem as Game;
	        if (selection != null)
	        {
	            await Navigation.PushAsync(new SelectedGamePage(selection.GameName));
                ((ListView)sender).SelectedItem = null;
            }
        }
    }
}
