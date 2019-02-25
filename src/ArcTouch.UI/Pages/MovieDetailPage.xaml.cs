using ArcTouch.Core.ViewModels.Home;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace ArcTouch.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, NoHistory = true)]
    public partial class MovieDetailPage : MvxContentPage<MovieDetailViewModel>
    {
        public MovieDetailPage()
        {
            InitializeComponent();
        }
    }
}
