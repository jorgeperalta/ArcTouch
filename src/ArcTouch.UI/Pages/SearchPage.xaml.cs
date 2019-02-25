using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ArcTouch.Core.ViewModels.Search;

namespace ArcTouch.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, NoHistory = true, Title = "Search Movie")]
    public partial class SearchPage : MvxContentPage<SearchViewModel>
    {
        public SearchPage()
        {
            InitializeComponent();
        }
    }
}
