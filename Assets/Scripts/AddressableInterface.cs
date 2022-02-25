using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public interface AddressableInterface
{
    //Addressable��l��
    public void InitializeAddressable();
    //�ˬdCatalog
    public Task<object> CheckCatalog();
    //��sCatalog
    public void UpdateCatalog();
    //��o�귽�]�j�p
    public Task<string> GetDownloadInformation();
    //���J�귽
    public Task<object> LoadSource();
    //���񤺦s
    public void ReleaseHandle();

}
