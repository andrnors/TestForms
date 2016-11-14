using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFormsShared.Models;
using TestFormsShared.Service;
using Xamarin.Forms;

namespace TestFormsShared
{
	public partial class AddNewScorePage : ContentPage
	{
	    private int recordId;

	    public int RecordId
	    {
	        get { return recordId; }
	        set
	        {
	            if (value != 0)
	            {
                    recordId = value;
                    OnPropertyChanged();
                }

	        }
	    }

        private string gameName;

        public string GameName
        {
            get { return gameName; }
            set
            {
                gameName = value;
                OnPropertyChanged();
            }  
        }

       
        public AddNewScorePage (int recordId, string gameName)
		{
			InitializeComponent ();
		    RecordId = recordId;
		    GameName = gameName;

		}

	    private async  void SubmitBtn_OnClicked(object sender, EventArgs e)
	    {

	        if (Score.Text != null)
	        {
                var scoreConvert = Int32.Parse(Score.Text);
                RecordId += 1;
                var amosti = new AmostiService();
                var newScore = new GameScoreBoard() { gameName = GameName, playerName = "Batman", recordId = RecordId, score = scoreConvert };
                await amosti.AddHighScore(newScore, true);
                Navigation.RemovePage(this);
            }
	        else
	        {
	            await DisplayAlert("Score not filled", "Seems like you forgot to add a score", "Try Again");
            }


        }
	}
}
