using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;


public interface AddressableInterface
{
    //Addressable��l��
    //�ˬd�ç�sCatalog
    public void UpdateCatalog();
    //��o�귽�]�j�p
    public string GetDownloadInformation(string BundleTag);
    //���J�����귽
    public void LoadScene(string BundleTag);
    //���񤺦s
    public void ReleaseHandle();
}
