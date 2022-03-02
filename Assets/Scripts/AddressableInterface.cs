using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;


public interface AddressableInterface
{
    //Addressable初始化
    //檢查並更新Catalog
    public void UpdateCatalog();
    //獲得資源包大小
    public string GetDownloadInformation(string BundleTag);
    //載入場景資源
    public void LoadScene(string BundleTag);
    //釋放內存
    public void ReleaseHandle();
}
