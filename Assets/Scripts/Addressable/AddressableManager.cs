using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;


public class AddressableManager : MonoBehaviour
{
    public static AddressableManager Instance;
    public AssetReference environmentObject;
    public AssetReference environment;
    public AssetReference props;

    public bool isAddressableInitialized;
    public bool isEnvironmentLoaded;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }   

    public Task StartAddressable()
    {
        Addressables.InitializeAsync().Completed += LoadEnvironment;
        return Task.CompletedTask;
    }

    private void LoadEnvironment(AsyncOperationHandle<IResourceLocator> objHandle)
    {
        Debug.Log("Addressable Initialized");
        isAddressableInitialized = true;
        StartCoroutine(LoadEnvironmentGameObject());
    }

    AsyncOperationHandle<GameObject> opHandle;

    public IEnumerator LoadEnvironmentGameObject()
    {
        opHandle = Addressables.LoadAssetAsync<GameObject>(environmentObject);
        yield return opHandle;

        if (opHandle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject obj = opHandle.Result;
            Instantiate(obj);
            isEnvironmentLoaded = true;
        }
    }

    private void OnAddressableInitialized(AsyncOperationHandle<IResourceLocator> obj)
    {
        Debug.Log("Addressable Initialized");
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        var handle = Addressables.LoadSceneAsync(environment, LoadSceneMode.Additive);

        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Loaded Environment Scene");
            StartCoroutine(LoadPropScene());
        }
        else
        {
            Debug.Log("Failed to load Environment Scene");
        }
    }

    IEnumerator LoadPropScene()
    {
        var handle = Addressables.LoadSceneAsync(props, LoadSceneMode.Additive);

        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
            Debug.Log("Loaded Props Scene");
        Addressables.WebRequestOverride = EditWebRequestURL;
    }


    public string addonJsonPath;

    [Button]
    public void LoadFromSecondaryProject()
    {
        addonJsonPath = "https://4040-addressable.blr1.digitaloceanspaces.com/Addon/StandaloneWindows64/";
        Addressables.WebRequestOverride = EditWebRequestURL;
        var loadContentCatalogAsync = Addressables.LoadContentCatalogAsync(
            "https://4040-addressable.blr1.digitaloceanspaces.com/Addon/StandaloneWindows64/catalog_0.1.0.json", false);
        loadContentCatalogAsync.Completed += OnCompleted;
    }

    [Button]
    public void LoadFromPrimaryProject()
    {
        addonJsonPath = "https://4040-addressable.blr1.digitaloceanspaces.com/StandaloneWindows64/";
        Addressables.WebRequestOverride = EditWebRequestURL;
        var loadContentCatalogAsync = Addressables.LoadContentCatalogAsync(
            "https://4040-addressable.blr1.digitaloceanspaces.com/StandaloneWindows64/catalog_0.1.0.json", false);
        loadContentCatalogAsync.Completed += OnAddressableInitialized;
    }

    [Button]
    public void LoadFromTertiaryProject()
    {
        addonJsonPath = "https://4040-addressable.blr1.digitaloceanspaces.com/addon-tertiary/StandaloneWindows64/";
        Addressables.WebRequestOverride = EditWebRequestURL;

        var loadContentCatalogAsync = Addressables.LoadContentCatalogAsync(
            "https://4040-addressable.blr1.digitaloceanspaces.com/addon-tertiary/StandaloneWindows64/catalog_0.1.0.json",
            false);
        loadContentCatalogAsync.Completed += OnCompletedTertiary;
    }


    private void OnCompleted(AsyncOperationHandle<IResourceLocator> obj)
    {
        Debug.Log("Completed");
        var handle = Addressables.LoadSceneAsync("Sphere", LoadSceneMode.Additive);
        handle.Completed += (_) => { Debug.Log("Loaded Scene"); };
    }

    private void OnCompletedTertiary(AsyncOperationHandle<IResourceLocator> obj)
    {
        Debug.Log("Completed");
        var handle = Addressables.LoadSceneAsync("Cylinder", LoadSceneMode.Additive);
        handle.Completed += (_) => { Debug.Log("Loaded Scene"); };
    }

    private void EditWebRequestURL(UnityWebRequest request)
    {
        // Modify the URL based on file types
        if (request.url.EndsWith(".bundle") || request.url.EndsWith(".hash") || request.url.EndsWith(".json"))
        {
            var result = request.url.Split(new[] { "/" }, StringSplitOptions.None);
            Debug.Log(addonJsonPath + result[^1]);
            request.url = addonJsonPath + result[^1];
        }
    }
}