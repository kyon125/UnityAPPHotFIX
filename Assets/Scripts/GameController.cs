using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameController : MonoBehaviour
{
    public List<AssetReference> assetReferences = new List<AssetReference>();

    public AsyncOperationHandle handle;

    public static GameController gameController;

    public GameObject checkUI;
    public Text sizeText;

    int sceneIndex;
    // Start is called before the first frame update
    private void Awake()
    {
        gameController = this;
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        for (int i = 0; i < assetReferences.Count; i++)
        {
            Addressables.ClearDependencyCacheAsync(assetReferences[i]);
        }
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
        StartCoroutine(WaitInformation(sceneIndex));
    }

    public void downLoadstage()
    {
        StartCoroutine(DownloadSence(sceneIndex));
    }
    IEnumerator WaitInformation(int i)
    {
        var downsize = Addressables.GetDownloadSizeAsync(assetReferences[i]);
        yield return downsize;
        long size = downsize.Result;
        checkUI.SetActive(true);
        sizeText.text = "Á`¦@¡G"+ "\t" + size+"kb";
        
        if (handle.IsDone)
        {
            //Debug.Log(handle.Status);
        }
        else
        {
            //Debug.Log(handle.Status+"aa");
        }
    }

    IEnumerator DownloadSence(int i)
    {
        checkUI.SetActive(false);
        handle = Addressables.LoadSceneAsync(assetReferences[i], LoadSceneMode.Additive);
        while (!handle.IsDone)
        {
            yield return new WaitForSeconds(.1f);
            Debug.Log(handle.GetDownloadStatus().Percent *100 +"%" .ToString());
        }
    }
    public void backMainmenu()
    {
        Addressables.UnloadSceneAsync(handle);
    }
}
