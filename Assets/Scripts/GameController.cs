using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.Networking;

public class GameController : MonoBehaviour
{
    public Text text;

    public AsyncOperationHandle handle;

    public static GameController gameController;

    public GameObject checkUI;
    public Text sizeText;
    public float gameVersion;

    int sceneIndex;
    // Start is called before the first frame update
    private void Awake()
    {
        gameController = this;
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        Addressables.ClearDependencyCacheAsync("Scene");
        //for (int i = 0; i < assetReferences.Count; i++)
        //{
        //    Addressables.ClearDependencyCacheAsync(assetReferences[i]);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void goStage1()
    {
        loadstage(0);
    }
    public void goStage2()
    {
        loadstage(1);
    }
    public void goStage3()
    {
        loadstage(2);
    }
    void loadstage(int i)
    {
        sceneIndex = i;
        text.text = "It's Stark";
        StartCoroutine(WaitInformation());
    }

    //連線至後台，獲得當前版本號，若app版本號較舊則透過addressable執行更新
    public void CheckHotFix()
    {
        StartCoroutine(ILRuntimeTest.iL.LoadHotFixAssembly());
        //StartCoroutine(CheckAppVersion(gameVersion));
    }

    public void downLoadstage()
    {
        StartCoroutine(DownloadSence(sceneIndex));
    }
    IEnumerator WaitInformation()
    {
        //初始化addressable
        var inihandle = Addressables.InitializeAsync();
        yield return inihandle;
        text.text += " 初始化完";
        var handler = Addressables.CheckForCatalogUpdates(false);
        yield return handler;
        text.text += " 檢查catalog完成";

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


        //獲得下載資訊
        var downsize = Addressables.GetDownloadSizeAsync("Scene");
        yield return downsize;
        long size = downsize.Result;
        checkUI.SetActive(true);
        sizeText.text = "總共："+ "\t" + size+"kb";
        //text.text = "總共：" + "\t" + size + "kb";

        if (handle.IsDone)
        {
            //Debug.Log(handle.Status);
        }
        else
        {
            //Debug.Log(handle.Status+"aa");
        }

        Addressables.Release(downsize);
    }

    IEnumerator DownloadSence(int i)
    {
       
        checkUI.SetActive(false);
        
        handle = Addressables.LoadSceneAsync("Scene", LoadSceneMode.Single);
        while (!handle.IsDone)
        {
            yield return new WaitForSeconds(.1f);
            text.text = handle.GetDownloadStatus().Percent * 100 + "%".ToString();
            Debug.Log(handle.GetDownloadStatus().Percent *100 +"%" .ToString());
        }
    }
    public void backMainmenu()
    {
        Addressables.UnloadSceneAsync(handle);
        Addressables.Release(handle);
    }
}
