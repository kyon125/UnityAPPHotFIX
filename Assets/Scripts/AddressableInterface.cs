using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public interface AddressableInterface
{
    //Addressable初始化
    public void InitializeAddressable();
    //檢查Catalog
    public Task<object> CheckCatalog();
    //更新Catalog
    public void UpdateCatalog();
    //獲得資源包大小
    public Task<string> GetDownloadInformation();
    //載入資源
    public Task<object> LoadSource();
    //釋放內存
    public void ReleaseHandle();

}
