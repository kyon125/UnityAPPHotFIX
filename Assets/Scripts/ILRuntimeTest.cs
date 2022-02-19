using System.Collections;
using System.Collections.Generic;
using System.IO;
using ILRuntime.Runtime.Enviorment;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ILRuntimeTest : MonoBehaviour
{
    AppDomain appDomain;
    public Text UI;
    System.IO.MemoryStream fs;
    System.IO.MemoryStream p;
    // Start is called before the first frame update
    void Start()
    {
        print("斷點");
        StartCoroutine(LoadHotFixAssembly());
    }


    IEnumerator LoadHotFixAssembly()
    {
        if(app)
        appDomain = new AppDomain();

        UnityWebRequest www = UnityWebRequest.Get(Application.streamingAssetsPath + "/HotUpdate.dll");
        //UnityWebRequest www = UnityWebRequest.Get(Application.streamingAssetsPath + "/HotFix_Project.dll");
        yield return www.SendWebRequest();

        if (!string.IsNullOrEmpty(www.error))
        {
            UnityEngine.Debug.LogError(www.error);
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
        appDomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
    }

    void OnHotFixLoaded()
    {
        appDomain.Invoke("HotUpdate.HotClass", "ShowWorld", null, null);
        //appDomain.Invoke("HotFix_Project.InstanceClass", "StaticFunTest", null, null);
    }

}
