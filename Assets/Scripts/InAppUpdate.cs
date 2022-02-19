using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//google
using Google.Play.AppUpdate;
using Google.Play.Common;
//to
using UnityEngine.UI;
public class InAppUpdate : MonoBehaviour
{
    [SerializeField] private Text inAppStatus;

    AppUpdateManager appUpdateManager;
    // Start is called before the first frame update
    void Start()
    {
        appUpdateManager = new AppUpdateManager();
        
    }
    public void check()
    {
        StartCoroutine(CheckForUpdate());
    }

    private IEnumerator CheckForUpdate()
    {
        PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appUpdateInfoOperation =  appUpdateManager.GetAppUpdateInfo();
        // wait until the async completes
        yield return appUpdateInfoOperation;

        if (appUpdateInfoOperation.IsSuccessful)
        {
            var appUpdateInforResult = appUpdateInfoOperation.GetResult();

            if (appUpdateInforResult.UpdateAvailability == UpdateAvailability.UpdateAvailable)
            {
                inAppStatus.text = UpdateAvailability.UpdateAvailable.ToString();
            }
            else
            {
                inAppStatus.text = "No Update";
            }

            var appUpdateOptions = AppUpdateOptions.ImmediateAppUpdateOptions();
            StartCoroutine(StartImmediateUpdate(appUpdateInforResult, appUpdateOptions));
        }
    }
    IEnumerator StartImmediateUpdate(AppUpdateInfo appUpdateInfop_i, AppUpdateOptions appUpdateOptions_i)
    {
        var startUpdateRequest = appUpdateManager.StartUpdate(appUpdateInfop_i, appUpdateOptions_i);
        yield return appUpdateManager;
;    }
}
