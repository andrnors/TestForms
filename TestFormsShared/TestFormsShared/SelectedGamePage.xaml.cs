using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Database;
using TestFormsShared.Models;
using TestFormsShared.Service;
using Xamarin.Forms;

namespace TestFormsShared
{
	public partial class SelectedGamePage : ContentPage
	{
	    private ObservableCollection<SingleGameHighScorecs> gameHighScores = new ObservableCollection<SingleGameHighScorecs>();

	    public ObservableCollection<SingleGameHighScorecs> GameHighScores
	    {
	        get { return gameHighScores; }
	        set
	        {
	            if (value != null)
	            {
	                gameHighScores = value;
                    OnPropertyChanged();
	            }
	        }
	    }

	    private string gamename;
	    public string GameName { get { return gamename; }
	        set
	        {
	            if (value != null)
	            {
	                gamename = value;
	            }        
	        }
        }

        private int recordId;
        public int RecordId
        {
            get { return recordId; }
            set
            {
                if (value != 0)
                {
                    recordId = value;
                }
            }
        }

	    protected virtual void OnResume()
	    {
	        GetGameScores(GameName);
	    } 
        public SelectedGamePage (string gameName)
		{
			InitializeComponent ();
		    GameName = gameName;
		    Title = gameName;
            GetGameScores(gameName);
		    BindingContext = this;
		    HighScoresListView.ItemsSource = GameHighScores;
		}

	    private async void GetGameScores(string gamename)
	    {
            AmostiService amostiService = new AmostiService();
            GameHighScores.Clear();
            var scores = await amostiService.GetGameScores(gamename);
	        RecordId = scores.First().recordId; // Finner id til verdien med høyest score
            foreach (var score in scores)
            {
                var username = score.playerName;
                var points = score.score;
                GameHighScores.Add(new SingleGameHighScorecs() {HighScore = points, Username = username});
            }
	    }

	    private async void AddNewScoreBtn_OnClicked(object sender, EventArgs e)
	    {
            // Playername skulle vært sendt inn også, men har ingen bruker..
            await Navigation.PushAsync(new AddNewScorePage(RecordId, GameName));

        }
    }
}
