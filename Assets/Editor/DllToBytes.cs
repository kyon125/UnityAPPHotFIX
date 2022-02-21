using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.AddressableAssets;

public class DllToBytes
{
    public static string normalPath = Application.dataPath + "/AddressableRes/Hotfix";
    public static string normalPathToSave = Application.dataPath + "/AddressableRes/Hotfix";

    [MenuItem("Costom Tool/AddressableGroup/DllToByte")]
    public static void DllToByte()
    {
        _DllToByte(true);
    }
    [MenuItem("Costom Tool/AddressableGroup/DllToByteCustom")]
    public static void DllToByteCustom()
    {
        _DllToByte(false);
    }
    private static void _DllToByte(bool autochoose)
    {
        string foldpath, savepath;

        if (autochoose)
        {
            foldpath = normalPath;
        }
        else
        {
            foldpath = EditorUtility.OpenFolderPanel(".DLL所在的資料夾", Application.dataPath + "addressable/IlRuntime", string.Empty);
        }
        if (string.IsNullOrEmpty(foldpath))
            return;

        DirectoryInfo directoryInfo = new DirectoryInfo(foldpath);
        FileInfo[] fileInfos = directoryInfo.GetFiles();
        List<FileInfo> listDll = new List<FileInfo>();
        //List<FileInfo> listPdb = new List<FileInfo>();

        for (int i = 0; i < fileInfos.Length; i++)
        {
            if (fileInfos[i].Extension == ".dll")
            {
                listDll.Add(fileInfos[i]);
            }

            //else if(fileInfos[i].Extension == ".pdb")
            //{
            //    listPdb.Add(fileInfos[i]);
            //}
            
        }
        if (listDll.Count/*+listPdb.count*/== 0)
        {
            Debug.Log("目錄下無文件");
        }
        else
        {
            Debug.Log("路徑：" + foldpath);
        }

        if (autochoose)
        {
            savepath = normalPathToSave;
        }
        else
        {
            savepath = EditorUtility.OpenFolderPanel("要儲存的資料夾", Application.dataPath + "addressable/IlRuntime", string.Empty);
        }

        Debug.Log("開始轉換文件");
        string path = string.Empty;

        for (int i = 0; i < listDll.Count; i++)
        {
            path = $"{savepath}/{Path.GetFileNameWithoutExtension(listDll[i].Name)}_dll_res.bytes";
            Debug.Log(path);
            BytesToFile(path, FileToBytes(listDll[i]));
        }

        Debug.Log("dll文件轉換結束");
        
        
    }
    private static byte[] FileToBytes(FileInfo fileInfo)
    {
        return File.ReadAllBytes(fileInfo.FullName);
    }
    private static void BytesToFile(string path ,byte[] bytes)
    {
        File.WriteAllBytes(path, bytes);
    }
}
