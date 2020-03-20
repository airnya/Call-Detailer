using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using SpyTime.Model.Types;
using SpyTime.Services;
using Xamarin.Forms;
using Application = Android.App.Application;

[assembly: Xamarin.Forms.Dependency( typeof( SpyTime.Droid.Services.CallInfoService ) )]
namespace SpyTime.Droid.Services
{
    public class CallInfoService : PhoneStateListener, IDeviceState
    {
        public event StateHandler CallFinished;
        bool IsNewCall { get; set; } = false;
        public CallInfoService( )
        {
            var telephonyService = (TelephonyManager)Application.Context.GetSystemService( Context.TelephonyService );
            telephonyService.Listen( this, PhoneStateListenerFlags.CallState );
        }

        public override void OnCallStateChanged( [GeneratedEnum] CallState state, string phoneNumber )
        {
            base.OnCallStateChanged( state, phoneNumber );

            switch( state )
            {
                case CallState.Idle:

                    if( IsNewCall ) CallFinished?.Invoke( GetCallLogs( ) );
                    IsNewCall = false;
                    break;

                case CallState.Ringing:

                    IsNewCall = true;
                    break;
                case CallState.Offhook:

                    IsNewCall = true;
                    break;
                default:
                    break;
            }
        }

        private Call GetCallLogs( )
        {
            string querySorter = string.Format( "{0} desc ", CallLog.Calls.Date );

            ICursor data = Application.Context.ContentResolver
                .Query( CallLog.Calls.ContentUri, null, null, null, querySorter );

            int number = data.GetColumnIndex( CallLog.Calls.Number );
            int duration = data.GetColumnIndex( CallLog.Calls.Duration );
            int date = data.GetColumnIndex( CallLog.Calls.Date );

            if( data.MoveToFirst( ) == true )
            {
                long dateTime = data.GetLong( date );
                return new Call
                {
                    CallDuratation = data.GetInt( duration ),
                    DialedNumber = data.GetString( number ),
                    CallDateTime = new DateTime( dateTime )
                };
            }

            return null;
        }
    }
}
