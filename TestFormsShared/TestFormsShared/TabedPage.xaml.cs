using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TestFormsShared
{
	public partial class MainTabedPage : TabbedPage
	{
		public MainTabedPage ()
		{
			InitializeComponent ();
            Children.Add(new HighScores());
            Children.Add(new Setings());
        }

	}
}
