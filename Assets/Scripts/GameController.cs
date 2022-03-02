using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.Networking;

public class GameController : AddressableController
{
    public Text text;

    public static GameController gameController;

    public GameObject checkUI;
    public Text sizeText;
    public float gameVersion;

    int sceneIndex;
    // Start is called before the first frame update
    private void Awake()
    {
        if (gameController == null)
            gameController = this;
        else
            Destroy(this.gameObject);
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
        UpdateCatalog();
        string size = GetDownloadInformation("Scene");
        checkUI.SetActive(true);
        sizeText.text = "�`�@�G" + "\t" + size + "kb";
    }
    public void DownloadStage()
    {
        LoadScene("Scene");
        checkUI.SetActive(false);
    }

    //�s�u�ܫ�x�A��o��e�������A�Yapp���������«h�z�Laddressable�����s
    public void CheckHotFix()
    {
        StartCoroutine(ILRuntimeTest.iL.LoadHotFixAssembly());
        //StartCoroutine(CheckAppVersion(gameVersion));
    }
    public void backMainmenu()
    {
        Addressables.UnloadSceneAsync(handle);
        Addressables.Release(handle);
    }
}
