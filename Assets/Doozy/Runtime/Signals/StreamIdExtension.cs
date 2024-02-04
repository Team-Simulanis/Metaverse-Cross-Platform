// Copyright (c) 2015 - 2023 Doozy Entertainment. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

//.........................
//.....Generated Class.....
//.........................
//.......Do not edit.......
//.........................

using UnityEngine;
// ReSharper disable All

namespace Doozy.Runtime.Signals
{
    public static partial class SignalsService
    {
        public static SignalStream GetStream(StreamId.Login id) => GetStream(nameof(StreamId.Login), id.ToString());
   
    }

    public partial class Signal
    {
        public static bool Send(StreamId.Login id, string message = "") => SignalsService.SendSignal(nameof(StreamId.Login), id.ToString(), message);
        public static bool Send(StreamId.Login id, GameObject signalSource, string message = "") => SignalsService.SendSignal(nameof(StreamId.Login), id.ToString(), signalSource, message);
        public static bool Send(StreamId.Login id, SignalProvider signalProvider, string message = "") => SignalsService.SendSignal(nameof(StreamId.Login), id.ToString(), signalProvider, message);
        public static bool Send(StreamId.Login id, Object signalSender, string message = "") => SignalsService.SendSignal(nameof(StreamId.Login), id.ToString(), signalSender, message);
        public static bool Send<T>(StreamId.Login id, T signalValue, string message = "") => SignalsService.SendSignal(nameof(StreamId.Login), id.ToString(), signalValue, message);
        public static bool Send<T>(StreamId.Login id, T signalValue, GameObject signalSource, string message = "") => SignalsService.SendSignal(nameof(StreamId.Login), id.ToString(), signalValue, signalSource, message);
        public static bool Send<T>(StreamId.Login id, T signalValue, SignalProvider signalProvider, string message = "") => SignalsService.SendSignal(nameof(StreamId.Login), id.ToString(), signalValue, signalProvider, message);
        public static bool Send<T>(StreamId.Login id, T signalValue, Object signalSender, string message = "") => SignalsService.SendSignal(nameof(StreamId.Login), id.ToString(), signalValue, signalSender, message);
   
    }

    public partial class SignalStream
    {
        public static SignalStream GetStream(StreamId.Login id) => SignalsService.GetStream(id);
   
    }

    public partial class StreamId
    {
        public enum Login
        {
            LoggingIn,
            LoginFailed,
            Success
        }         
    }
}

