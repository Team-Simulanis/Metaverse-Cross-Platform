using System.Threading.Tasks;
using Simulanis;
using UnityEngine;
using UnityEngine.Networking;
using Sirenix.OdinInspector;

public class WebRequestManager : MonoBehaviour
{
    #region Static Variables

    [TitleGroup("Web Request Calls", Alignment = TitleAlignments.Centered)]
    [Space(10)]
    [ShowInInspector]
    [HideLabel]
    [ReadOnly]
    [HorizontalGroup("Web Request Calls/Split", 0.5f)]
    [BoxGroup("Web Request Calls/Split/GET")]
    public static int Get;

    [Space(10)] [ShowInInspector] [HideLabel] [ReadOnly] [BoxGroup("Web Request Calls/Split/POST")]
    public static int Post;

    public static WebRequestManager Instance;

    [ShowInInspector] public static Sprite ImageDownloadFailed;
    [ShowInInspector] [ReadOnly] public static string Domain;

    [ShowInInspector]
    [ReadOnly]
    [Multiline(5)]
    [HideLabel]
    [TitleGroup("Bearer Token", boldTitle: true, Alignment = TitleAlignments.Centered)]
    public static string Token =
        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1dWlkIjoiZmM5MjgwYTUtZjcxNy00MDg0LWI2Y2UtYWVkNjQ1NmE0NWZhIiwiY2hhbm5lbCI6ImVmMjFlMWUwLWI4MGUtNGQwMy04MGRiLTJjN2E0OThmNTJlYiIsImlhdCI6MTcxMDMxNzA5MiwiZXhwIjoxNzEyOTA5MDkyfQ.ojLr0qxSaWUtmIBP2mZ86FKh0kuNs6kNE7HbExQmic0";

    #endregion

    private void Awake()
    {
        Instance = this;
        Get = 0;
        Post = 0;
    }

    public async Task<string> WebRequest(string endPoint)
    {

        var request = UnityWebRequest.Get(Domain + endPoint);
        DebugManager.Log(DebugType.ServerResponse, "Sending request to: " + Domain + endPoint);

        var result = await request.SendWebRequestAsync();
        Get++;
        if (result.IsSuccess)
        {
            DebugManager.Log(DebugType.ServerResponse, "Received: " + result.Result);
            return result.Result;
        }

        {
            DebugManager.Log(DebugType.ServerResponseError, "Error: " + result.Error);
        }
        return null;
    }

    public static async Task<string> GetWebRequestWithAuthorization(string customDomain, string endPoint)
    {
        var request = UnityWebRequest.Get(customDomain + endPoint);
        var reqId = RandomIDGenerator.GenerateRandomID();
        DebugManager.Log(DebugType.ServerResponse, reqId + " Sending request to: " + customDomain + endPoint);
        request.SetRequestHeader("Authorization", "Bearer " + Token);
        Get++;
        var result = await request.SendWebRequestAsync();

        if (result.IsSuccess)
        {
            DebugManager.Log(DebugType.ServerResponse, reqId + " Received: " + result.Result);
            return result.Result;
        }

        if (result.Error != null)
        {
            DebugManager.Log(DebugType.ServerResponseError, reqId + " Error: " + result.Error);
        }

        return null;
    }

    public static async Task<string> PostWebRequestWithAuthorization(string customDomain,string json)
    {
        var request = UnityWebRequest.Post(customDomain ,json, "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + Token);

        var reqId = RandomIDGenerator.GenerateRandomID();
        DebugManager.Log(DebugType.ServerResponse, reqId + " Sending request to: " + customDomain );

        Post++;
        var result = await request.SendWebRequestAsync();

        if (result.IsSuccess)
        {
            DebugManager.Log(DebugType.ServerResponse, reqId + " Received: " + result.Result);
            return result.Result;
        }

        if (result.Error != null)
        {
            DebugManager.Log(DebugType.ServerResponseError, reqId + " Error: " + result.Error);
        }

        return null;
    }

    public static async Task<Sprite> ImageDownloadRequest(string imageUrl)
    {
        using var webRequest = UnityWebRequestTexture.GetTexture(imageUrl);
        // Send the request asynchronously
        var asyncOperation = webRequest.SendWebRequest();
        Get++;
        while (!asyncOperation.isDone)
        {
            await Task.Yield(); // Yield to the main Unity thread to avoid blocking it
        }

        // Check for errors
        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error downloading image: " + webRequest.error);
            return ImageDownloadFailed; // Return null if there's an error
        }


        // Convert downloaded texture to sprite
        var texture = DownloadHandlerTexture.GetContent(webRequest);
        var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
        return sprite;
    }

    public static async Task<Sprite> DownloadSvg(string downloadURL)
    {
        var www = UnityWebRequest.Get(downloadURL);

        var asyncOperation = www.SendWebRequest();
        while (!asyncOperation.isDone)
        {
            await Task.Yield(); // Yield to the main Unity thread to avoid blocking it
        }

        if (www.error == null) return SVGConverter.ConvertSVGToSprite(www);
        Debug.Log("Error while Receiving: " + www.error);
        return ImageDownloadFailed;
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
            var texture = DownloadHandlerTexture.GetContent(webRequest);

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