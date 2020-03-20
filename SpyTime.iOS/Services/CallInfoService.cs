using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using SpyTime.Services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency( typeof( SpyTime.iOS.Services.CallInfoService ) )]
namespace SpyTime.iOS.Services
{
    public class CallInfoService : IDeviceState
    {
        public event StateHandler StateHandler;
    }
}