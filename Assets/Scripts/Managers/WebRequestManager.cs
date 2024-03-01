using System.Threading.Tasks;
using Simulanis;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequestManager : MonoBehaviour
{
    
    public static WebRequestManager Instance;

    public string domain;

    private void Awake()
    {
        Instance = this;
    }

    public async Task<string> WebRequest(string endPoint) 
    {
        var request = UnityWebRequest.Get(domain+endPoint);
        var result = await request.SendWebRequestAsync();

        if (result.IsSuccess)
        {
            DebugManager.Log(DebugManager.DebugType.ServerResponse ,"Received: " + result.Result);
            return result.Result;
        }
        else
        {
            DebugManager.Log(DebugManager.DebugType.ServerResponse,"Error: " + result.Error);
        }

        return null;
    }
    
    public static async Task<string> WebRequestWithAuthorization(string customDomain,string endPoint,string token) 
    {
        var request = UnityWebRequest.Get(customDomain+endPoint);
        request.SetRequestHeader("Authorization", "Bearer " + token); 
        
        var result = await request.SendWebRequestAsync();

        if (result.IsSuccess)
        {
            DebugManager.Log(DebugManager.DebugType.ServerResponse ,"Received: " + result.Result);
            return result.Result;
        }
        else
        {
            DebugManager.Log(DebugManager.DebugType.ServerResponseError,"Error: " + result.Error);
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