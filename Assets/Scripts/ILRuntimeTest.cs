using System.Collections;
using System.Collections.Generic;
using System.IO;
using ILRuntime.Runtime.Enviorment;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.AddressableAssets;

public class ILRuntimeTest : MonoBehaviour
{
    public static ILRuntimeTest iL;
    public static AppDomain appDomain;
    public Text UI;
    public string Header;
    public List<AssetReference> DllRes = new List<AssetReference>();
    public AsyncOperation handle;

    
    System.IO.MemoryStream fs;
    System.IO.MemoryStream p;
    // Start is called before the first frame update
    void Start()
    {
        iL = this;
        appDomain = new AppDomain();
        UI.text = Header;
    }


    public IEnumerator LoadHotFixAssembly( )
    {
        //重新將bytes檔反編譯成dll，並寫入資料夾
        var b = Addressables.LoadAssetAsync<TextAsset>(DllRes[0]);
        yield return b;
        string path = Application.streamingAssetsPath + "/Hotfix.dll"; 
        File.WriteAllBytes(path.Trim(), b.Result.bytes);

        //讀取在資料夾裡的dll
        UnityWebRequest www = UnityWebRequest.Get(Application.streamingAssetsPath + "/Hotfix.dll");
        yield return www.SendWebRequest();
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.LogError(www.error);
        }
        byte[] dll = www.downloadHandler.data;

        www.Dispose();
        fs = new MemoryStream(dll);

        try
        {
            appDomain.LoadAssembly(fs, null, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
        }
        catch
        {
            Debug.LogError("加载热更DLL失败，请确保已经通过VS打开Assets/Samples/ILRuntime/1.6/Demo/HotFix_Project/HotFix_Project.sln编译过热更DLL");
        }

        InitializeILRuntime();
        OnHotFixLoaded();
    }

    void InitializeILRuntime()
    {
        //由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
        //appDomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
    }

    void OnHotFixLoaded()
    {
        appDomain.Invoke("Hotfix.BasicClass", "HotfixGetVersion", null, null);
        var v =  appDomain.Invoke("Hotfix.BasicClass", "HeaderChange", null, null);
        Header = v.ToString();
        UI.text = Header;
        //appDomain.Invoke("HotFix_Project.InstanceClass", "StaticFunTest", null, null);
    }

}
