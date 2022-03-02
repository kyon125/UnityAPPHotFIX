using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class AddressableController : MonoBehaviour, AddressableInterface
{
    public static AsyncOperationHandle handle;

    public void UpdateCatalog()
    {
        StartCoroutine(CheckCatalog());
    }
    public string GetDownloadInformation(string BundleTag)
    {
        //return StartCoroutine(GetdDownloadInformationIE(BundleTag)).ToString();

        return GetDownloadInformationAsync(BundleTag).GetAwaiter().GetResult();
    }

    public void LoadScene(string BundleTag)
    {
        StartCoroutine(DownloadSourceIE(BundleTag));
    }

    public void ReleaseHandle()
    {
        Addressables.UnloadSceneAsync(handle);
        Addressables.Release(handle);
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------//
    IEnumerator CheckCatalog()
    {
        var inihandle = Addressables.InitializeAsync();
        yield return inihandle;

        var handler = Addressables.CheckForCatalogUpdates(false);
        yield return handler;

        var catalogs = handler.Result;
        Debug.Log($"need update catalog:{catalogs.Count}");
        foreach (var catalog in catalogs)
        {
            Debug.Log(catalog);
        }

        if (catalogs.Count > 0)
        {
            var updateHandle = Addressables.UpdateCatalogs(catalogs, false);
            yield return updateHandle;
            var locators = updateHandle.Result;
            foreach (var locator in locators)
            {
                foreach (var key in locator.Keys)
                {
                    Debug.Log($"update {key}");
                }
            }
        }
        Addressables.Release(handler);
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------//
    IEnumerator<string> GetdDownloadInformationIE(string BundleTag)
    {
        //獲得下載資訊
        var downsize = Addressables.GetDownloadSizeAsync(BundleTag);
        while (downsize.IsDone)
        {
            string size = downsize.Result.ToString();
            yield return size;
        }
        Addressables.Release(downsize);
    }
    async Task<string> GetDownloadInformationAsync(string BundleTag)
    {
        //獲得下載資訊
        var downsize = Addressables.GetDownloadSizeAsync(BundleTag);
        await downsize.Task;
        long size = downsize.Result;
        Addressables.Release(downsize);
        return size.ToString();
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------//
    IEnumerator DownloadSourceIE(string BundleTag)
    {
        handle = Addressables.LoadSceneAsync(BundleTag, LoadSceneMode.Additive);
        while (!handle.IsDone)
        {
            yield return new WaitForSeconds(.1f);
            Debug.Log(handle.GetDownloadStatus().Percent * 100 + "%".ToString());
        }
    }

   
}
