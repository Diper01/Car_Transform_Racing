using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaysunBridgeCallback : AndroidJavaProxy
{

    public WaysunBridgeCallback() : base("com.waysun.lib.BridgeCallback") { }

    public void OnLoginResponse(string userId) {
        Debug.Log("===============WAYSUN CALLBACK OnLoginResponse===============");
        Debug.Log(userId);
        Debug.Log("===============WAYSUN CALLBACK OnLoginResponse===============");
    }

    public void OnCheckBalanceResponse(float vfee) {
        Debug.Log("===============WAYSUN CALLBACK OnCheckBalanceResponse===============");
        Debug.Log(vfee);
        Debug.Log("===============WAYSUN CALLBACK OnCheckBalanceResponse===============");
    }

    public void OnAuthenticateResponse(AndroidJavaObject result) {
        int mFlag = result.Get<int>("mFlag");
        string mProductCode = result.Get<string>("mProductCode");
        string mProductType = result.Get<string>("mProductType");

        Debug.Log("===============WAYSUN CALLBACK OnAuthenticateResponse===============");
        Debug.Log(result.Call<string>("toString"));
        Debug.Log("===============WAYSUN CALLBACK OnAuthenticateResponse===============");
    }

    public void OnChargeResponse(AndroidJavaObject result) {
        int mFlag = result.Get<int>("mFlag");
        string mProductCode = result.Get<string>("mProductCode");
        int mRechargeCoins = result.Get<int>("mRechargeCoins");
        string mRechargeType = result.Get<string>("mRechargeType");
        string mOrderSn = result.Get<string>("mOrderSn");

        Debug.Log("===============WAYSUN CALLBACK OnChargeResponse===============");
        Debug.Log(result.Call<string>("toString"));
        Debug.Log("===============WAYSUN CALLBACK OnChargeResponse===============");
    }

    public void OnConsumeResponse(AndroidJavaObject result) {
        int mFlag = result.Get<int>("mFlag");
        string mProductCode = result.Get<string>("mProductCode");
        float mPrice = result.Get<float>("mPrice");
        int mBuyNum = result.Get<int>("mBuyNum");
        string mOrderSn = result.Get<string>("mOrderSn");

        Debug.Log("===============WAYSUN CALLBACK OnConsumeResponse===============");
        Debug.Log(result.Call<string>("toString"));
        Debug.Log("===============WAYSUN CALLBACK OnConsumeResponse===============");
    }

}
