using UnityEngine;

public class WaysunBridgePlugin : MonoBehaviour
{

    private AndroidJavaObject activityContext = null;
    private AndroidJavaObject pluginInstance = null;
    private AndroidJavaClass pluginClass = null;
    private AndroidJavaClass activityClass = null;
    private string mApkId = "GP001-GM000015"; //Transform Rally Racing (变换拉力赛)

	// Use this for initialization
	void Start()
    {
        pluginClass = new AndroidJavaClass("com.gms.plugins.UnityPlugin");
        activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        if (pluginClass != null && activityClass != null)
        {
            activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            pluginInstance = pluginClass.CallStatic<AndroidJavaObject>("instance");
            pluginInstance.Call("initBridgeClientSDK", activityContext, mApkId, new WaysunBridgeCallback());
        }
    }

    public string GetApkId(string game)
    {
        switch (game)
        {
            case "Tobuscus Adventures: Wizards":
                mApkId = "GP001-GM000001";
                break;
            case "Rage of Titans":
                mApkId = "GP001-GM000002";
                break;
            case "Gridlock":
                mApkId = "GP001-GM000004";
                break;
            case "Skulls of the Shogun":
                mApkId = "GP001-GM000005";
                break;
            case "Qbert: Rebooted":
                mApkId = "GP001-GM000007";
                break;
            default:
                mApkId = "";
                break;
        }
        return mApkId;
    }

    public void StartPurchase()
    {
        if (pluginInstance != null)
        {
            pluginInstance.Call("startPurchase", "GM000010-PROP0002", 1, "");

        }
    }

    public void StartRecharge()
    {
        if (pluginInstance != null)
        {
            pluginInstance.Call("startRecharge");
        }
    }

    public void BackToGP()
    {
        if (pluginInstance != null && activityContext != null)
        {
            pluginInstance.Call("backToGP", activityContext);
        }
    }
}
