using System;
using System.Collections.Generic;
using System.Text;
using SpyTime.Model.Types;

namespace SpyTime.Services
{
    public enum DeviceStates
    {
        Idle,
        Ringing,
        Offhook
    }

    public delegate void StateHandler( ICallInfo callInfo );

    public interface IDeviceState
    {
        event StateHandler StateHandler;
    }
}
