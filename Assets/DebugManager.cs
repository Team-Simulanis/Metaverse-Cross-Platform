using UnityEngine;

namespace Simulanis
{
    public class DebugManager : MonoBehaviour
    {
        public enum DebugType
        {
            Msg,
            Error,
            Network,
            Env,
            Data,
            DataGet,
            DataPost,
            ServerResponse,
            ServerResponseError,
            DataError,
            Session,
            Analytics
        }

        public static void Log(DebugType debugType, string message)
        {
            switch (debugType)
            {
                case DebugType.Error:
                    LogError(message);
                    break;
                case DebugType.Analytics:
                    AnalyticsLog(message);
                    break;
                case DebugType.Network:
                    NetworkLog(message);
                    break;
                case DebugType.Env:
                    EnvironmentLog(message);
                    break;
                case DebugType.Data:
                    DataLog(message);
                    break;
                case DebugType.DataGet:
                    DataGetLog(message);
                    break;
                case DebugType.DataPost:
                    DataPostLog(message);
                    break;
                case DebugType.ServerResponse:
                    ServerResponseLog(message);
                    break;
                case DebugType.ServerResponseError:
                    ServerResponseLog(message);
                    break;
                case DebugType.DataError:
                    DataLogError(message);
                    break;
                case DebugType.Session:
                    SessionLog(message);
                    break;
                case DebugType.Msg:
                    MessageLog(message);
                    break;
            }
        }

        public static void LogError(string message)
        {
            Debug.Log("<color=red>[error] " + message + "</color>");
        }

        public static void MessageLog(string message)
        {
            Debug.Log("<color=red>[error] " + message + "</color>");
        }

        public static void NetworkLog(string message)
        {
            Debug.Log("<color=yellow>[network] " + message + "</color>");
        }

        public static void EnvironmentLog(string message)
        {
            Debug.Log("<color=cyan>[env] " + message + "</color>");
        }

        public static void DataLog(string message)
        {
            Debug.Log("<color=pink>[data] " + message + "</color>");
        }

        public static void DataGetLog(string message)
        {
            Debug.Log("<color=#008080ff>[data-get] " + message + "</color>");
        }

        public static void DataGetLog(string message, bool result)
        {
            if (result == false)
            {
                Debug.Log("<color=red>[data-get] " + message + "</color>");
            }
            else
            {
                Debug.Log("<color=green>[data-get] </color><color=#008080ff>" + message + "</color>");
            }
        }

        public static void DataPostLog(string message)
        {
            Debug.Log("<color=#008080ff>[data-post] " + message + "</color>");
        }

        public static void ServerResponseLog(string message)
        {
            Debug.Log("<color=green>[data] " + message + "</color>");
        }
        
        public static void ServerResponseErrorLog(string message)
        {
            Debug.Log("<color=red>[dataError] " + message + "</color>");
        }

        public static void DataLogError(string message)
        {
            Debug.Log("<color=red><b>[data][error] " + message + "</b></color>");
        }

        public static void SessionLog(string message)
        {
            Debug.Log("<color=blue>[session] " + message + "</color>");
        }

        public static void AnalyticsLog(string message)
        {
            Debug.Log("<color=cyan>[Analytics] " + message + "</color>");
        }
    }
}