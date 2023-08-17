using UnityEngine;
using UnityEngine.UI;

public class SyncScrollView : MonoBehaviour
{
    public ScrollRect scrollView1;
    public ScrollRect scrollView2;

    private bool syncingScrollViews = false;

    private void Start()
    {
        // Assuming you have references to the ScrollRect components in the Inspector.
    }

    private void Update()
    {
        if (!syncingScrollViews)
        {
            syncingScrollViews = true;

            // Synchronize the horizontalNormalizedPosition of scrollView1 with scrollView2.
            scrollView2.horizontalNormalizedPosition = scrollView1.horizontalNormalizedPosition;

            // Synchronize the horizontalNormalizedPosition of scrollView2 with scrollView1.
            scrollView1.horizontalNormalizedPosition = scrollView2.horizontalNormalizedPosition;

            syncingScrollViews = false;
        }
    }
}