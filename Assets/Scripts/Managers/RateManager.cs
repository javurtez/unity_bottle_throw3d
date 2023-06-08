using Google.Play.Review;
using System.Collections;
using UnityEngine;

public class RateManager : MonoManager<RateManager>
{
    private bool isShowRate = false;

    public void ShowRate()
    {
        if (isShowRate) return;
#if !UNITY_EDITOR
        StartCoroutine(ShowRateAsync());
#else
        Debug.Log("Show Review!!");
#endif
        isShowRate = true;
    }

    private IEnumerator ShowRateAsync()
    {
        //yield return null;
        // Create instance of ReviewManager
        ReviewManager _reviewManager;
        // ...
        _reviewManager = new ReviewManager();
        var requestFlowOperation = _reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        if (requestFlowOperation.Error != ReviewErrorCode.NoError)
        {
            Debug.Log(requestFlowOperation.Error.ToString());
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }
        PlayReviewInfo _playReviewInfo = requestFlowOperation.GetResult();
        var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
        yield return launchFlowOperation;
        _playReviewInfo = null; // Reset the object
        if (launchFlowOperation.Error != ReviewErrorCode.NoError)
        {
            Debug.Log(launchFlowOperation.Error.ToString());
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }

        Debug.Log(launchFlowOperation.IsSuccessful);
        Debug.Log(launchFlowOperation.IsDone);
    }
}