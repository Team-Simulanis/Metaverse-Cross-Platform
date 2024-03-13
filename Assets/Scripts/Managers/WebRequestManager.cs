using System.IO;
using System.Threading.Tasks;
using Simulanis;
using UnityEngine;
using UnityEngine.Networking;
using Sirenix.OdinInspector;
using Unity.VectorGraphics;

public class WebRequestManager : MonoBehaviour
{
    [TitleGroup("Web Request Calls")]
    [Space(10)]
    [ShowInInspector]
    [HideLabel]
    [ReadOnly]
    [HorizontalGroup("Web Request Calls/Split", 0.5f)]
    [BoxGroup("Web Request Calls/Split/GET")]
    public static int Get;

    [Space(10)] [ShowInInspector] [HideLabel] [ReadOnly] [BoxGroup("Web Request Calls/Split/POST")]
    public static int Set;

    public static WebRequestManager Instance;

    public string domain;

    private void Awake()
    {
        Instance = this;
    }

    public async Task<string> WebRequest(string endPoint)
    {
        var request = UnityWebRequest.Get(domain + endPoint);
        var result = await request.SendWebRequestAsync();
        Get++;
        if (result.IsSuccess)
        {
            DebugManager.Log(DebugType.ServerResponse, "Received: " + result.Result);
            return result.Result;
        }
        else
        {
            DebugManager.Log(DebugType.ServerResponse, "Error: " + result.Error);
        }

        return null;
    }

    public static async Task<string> WebRequestWithAuthorization(string customDomain, string endPoint, string token)
    {
        var request = UnityWebRequest.Get(customDomain + endPoint);
        request.SetRequestHeader("Authorization", "Bearer " + token);
        Get++;
        var result = await request.SendWebRequestAsync();

        if (result.IsSuccess)
        {
            DebugManager.Log(DebugType.ServerResponse, "Received: " + result.Result);
            return result.Result;
        }
        else
        {
            DebugManager.Log(DebugType.ServerResponseError, "Error: " + result.Error);
        }

        return null;
    }

    public static async Task<Sprite> ImageDownloadRequest(string imageUrl)
    {
        using var webRequest = UnityWebRequestTexture.GetTexture(imageUrl);
        // Send the request asynchronously
        var asyncOperation = webRequest.SendWebRequest();
        while (!asyncOperation.isDone)
        {
            await Task.Yield(); // Yield to the main Unity thread to avoid blocking it
        }

        // Check for errors
        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error downloading image: " + webRequest.error);
            return null; // Return null if there's an error
        }
        else
        {
            // Convert downloaded texture to sprite
            var texture = DownloadHandlerTexture.GetContent(webRequest);
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            return sprite;
        }
    }

    public static async Task<Sprite> DownloadSVG(string _url)
    {
        string url = _url;

        UnityWebRequest www = UnityWebRequest.Get(url);

        var asyncOperation = www.SendWebRequest();
        while (!asyncOperation.isDone)
        {
            await Task.Yield(); // Yield to the main Unity thread to avoid blocking it
        }
        if (www.isHttpError || www.isNetworkError)
        {
            Debug.Log("Error while Receiving: " + www.error);
            return null;
        }
        else
        {
            return SVGConverter.ConvertSVGToSprite(www);
        }
    }

    public static async Task<Texture> TextureDownloadRequest(string imageUrl)
    {
        using var webRequest = UnityWebRequestTexture.GetTexture(imageUrl);
        // Send the request asynchronously
        var asyncOperation = webRequest.SendWebRequest();
        while (!asyncOperation.isDone)
        {
            await Task.Yield(); // Yield to the main Unity thread to avoid blocking it
        }

        // Check for errors
        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error downloading image: " + webRequest.error);
            return null; // Return null if there's an error
        }
        else
        {
            // Convert downloaded texture to sprite
            Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);

            return texture;
        }
    }
}

public static class UnityWebRequestExtensions
{
    public static async Task<UnityWebRequestResult> SendWebRequestAsync(this UnityWebRequest request)
    {
        var asyncOperation = request.SendWebRequest();
        while (!asyncOperation.isDone)
        {
            await Task.Yield(); // Yield to the main Unity thread to avoid blocking it
        }

        return request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError
            ? new UnityWebRequestResult(false, request.error)
            : new UnityWebRequestResult(true, request.downloadHandler.text);
    }
}

public class UnityWebRequestResult
{
    public bool IsSuccess { get; private set; }
    public string Result { get; private set; }
    public string Error { get; private set; }

    public UnityWebRequestResult(bool isSuccess, string result = null, string error = null)
    {
        IsSuccess = isSuccess;
        Result = result;
        Error = error;
    }
}