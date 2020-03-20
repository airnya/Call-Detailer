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
        public event StateHandler StateHandler;
        public bool IsNewCall { get; set; } = false;
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

                    if( IsNewCall ) StateChanged( );
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

        public void StateChanged( )
        {
            StateHandler?.Invoke( GetCallLogs( ) );
        }

        private CallInfo GetCallLogs( )
        {
            //string queryFilter = String.Format( "{0}={1}", CallLog.Calls.Type, (int)CallType.Outgoing );
            string querySorter = string.Format( "{0} desc ", CallLog.Calls.Date );

            var Columns = new string[]
            {
                CallLog.Calls.PhoneAccountId,
                CallLog.Calls.Number,
                CallLog.Calls.Date,
                CallLog.Calls.Duration,
                CallLog.Calls.Type
            };

            ICursor queryData1 = Android.App.Application.Context.ContentResolver.Query( CallLog.Calls.ContentUri, null, null, null, querySorter );

            int number = queryData1.GetColumnIndex( CallLog.Calls.Number );
            int duration1 = queryData1.GetColumnIndex( CallLog.Calls.Duration );
            int date = queryData1.GetColumnIndex( CallLog.Calls.Date );

            if( queryData1.MoveToFirst( ) == true )
            {
                //var dialed = queryData1.GetLong( queryData1.GetColumnIndex( CallLog.Calls.Date ) );                
                //to get phNumber
                //string phNumber = queryData1.GetString( number );
                //string callDuration = queryData1.GetString( duration1 );
                long dateTime = queryData1.GetLong( date );
                return new CallInfo
                {
                    CallDuratation = queryData1.GetInt( duration1 ),
                    DialedNumber = queryData1.GetString( number ),
                    CallDateTime = new DateTime( ) + TimeSpan.FromTicks( dateTime )
                };
            }
            return null;
        }
    }
}
