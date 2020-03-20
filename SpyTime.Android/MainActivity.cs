using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android;
using Android.Support.V4.App;
using System.Threading.Tasks;
using Android.Content;
using Android.Database;
using Android.Provider;
using Android.Telephony;
using SpyTime.Droid.Services;
using Xamarin.Forms;
using SpyTime.Services;

[assembly: UsesPermission( Manifest.Permission.ReadCallLog )]
[assembly: UsesPermission( Manifest.Permission.WriteCallLog )]
namespace SpyTime.Droid
{
    [Activity(Label = "SpyTime", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            //Ask permission for read logs on emulator
            ActivityCompat.RequestPermissions( this, new string[] { Manifest.Permission.ReadCallLog }, 0 );
            ActivityCompat.RequestPermissions( this, new string[] { Manifest.Permission.WriteCallLog }, 0 );

            DependencyService.Register<CallInfoService>( );

            LoadApplication( new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}