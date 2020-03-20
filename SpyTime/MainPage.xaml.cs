using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SpyTime.Model.Types;
using SpyTime.Services;
using Xamarin.Forms;

namespace SpyTime
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible( false )]
    public partial class MainPage : ContentPage
    {
        public string Duratation { get; set; }
        private readonly IDeviceState _deviceState = DependencyService.Get<IDeviceState>( DependencyFetchTarget.GlobalInstance );
        public MainPage( )
        {
            InitializeComponent( );
            _deviceState.CallFinished += CallFinishedHandler;
        }

        private async void CallFinishedHandler( ICall callInfo )
        {
            await DisplayAlert( "Info send", Newtonsoft.Json.JsonConvert
                .SerializeObject( callInfo ), "OK");
        }
    }
}
