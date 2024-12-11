using UnityEngine;
using System.Collections;
using PayBuild;
using UnityEngine.UI;

public class GameRebornControllor : UIBoxBase {
	public static GameRebornControllor Instance;

	public GameObject goCloseBtn, goUpgradeBtn, goCancelBtn;
	public GameObject goUpgradeDesc, goTips;
	public Text textTipsNum;
    public Image imageCloseBt;

	private bool bIsFreeReborn;
	private bool isGuideReborn;

	#region 重写父类方法
	public override void Init ()
	{
		Instance = this;

		base.Init();
	}
    
    public override void Show ()
	{
		base.Show();
		if (PlayerData.Instance.GetUIGuideState (UIGuideType.GameRebornGuide)) {
			UIGuideControllor_GameClone.Instance.Show (UIGuideType.GameRebornGuide);
			isGuideReborn = true;
            
		} else {
			isGuideReborn = false;
		}

		//transform.localPosition = ShowPosV2;
		AudioManger.Instance.PlaySound(AudioManger.SoundName.AotemanDeath);


		if(PlayerData.Instance.GetFreeRebornTimes() >= PayJsonData.Instance.GetFreeRebornTimes() 
		   && (PlayerData.Instance.GetSelectedLevel() > 2 || PlayerData.Instance.GetGameMode().CompareTo("WuJin") == 0)
		   && isGuideReborn == false)
		{
			bIsFreeReborn = false;
		}else
		{
			bIsFreeReborn = true;
		}
        		        
		RefreshTipsNum();
        
	}

	void SetCancelBt(bool state)
	{
		if(state)
		{
			goCancelBtn.SetActive(true);
			//goCancelBtn.transform.localPosition = new Vector3(0, -165, 0f);
		}else
		{
			goCancelBtn.SetActive(false);
		}
	}

	public override void Hide ()
	{
		gameObject.SetActive (false);
		//transform.localPosition = GlobalConst.LeftHidePos;
		AudioManger.Instance.StopSound(AudioManger.SoundName.AotemanDeath);
		GameUIManager.Instance.HideModule(UISceneModuleType.GameReborn);
	}
	
	public override void Back ()
	{
		AudioManger.Instance.PlaySound(AudioManger.SoundName.CloseBtClick);
		Hide();
		GameUIManager.Instance.ShowModule(UISceneModuleType.GameEndingScore);
	}
	#endregion

	#region  数据处理
	public void RefreshTipsNum()
	{
		int iCoinCount = PlayerData.Instance.GetItemNum(PlayerData.ItemType.Coin);
		int iModelId = PlayerData.Instance.GetSelectedModel();
		int iModelMaxId = iModelId / 100 * 100 + ModelData.Instance.GetMaxLevel(iModelId);
		int iTipsNum = 0;

		goUpgradeBtn.SetActive(false);
		goUpgradeDesc.SetActive(false);
		//goManjiDesc.SetActive(iModelId == iModelMaxId);

		for(int i = iModelId; i < iModelMaxId; i ++)
		{
			int cost = ModelData.Instance.GetUpgradeCost(i);

			if(iCoinCount < cost)
				break;

			iTipsNum ++;
			iCoinCount -= cost;
		}
		goTips.SetActive(iTipsNum != 0);
		textTipsNum.text = iTipsNum.ToString();
	}
	#endregion
	
	#region 按钮控制
	public void CloseBtnOnClick(){
		
		AudioManger.Instance.PlaySound(AudioManger.SoundName.CloseBtClick);
		Hide();
		GameUIManager.Instance.ShowModule(UISceneModuleType.GameEndingScore);
		return;
	}
    public void UpgradeBtnOnClick(){
		AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
		CharacterDetailControllor.Instance.SetModelIndex(GameData.Instance.selectedModelType - 1);
		GameUIManager.Instance.ShowModule(UISceneModuleType.CharacterDetail);
		return;
	}

    public void RebornBtnOnClick()
    {
        AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
        Reborn();
        return;
    }
    #endregion

    #region  扣费处理
    void Reborn()
    {
        if (bIsFreeReborn)
        {
            RebornCall("Success");
            return;
        }

        if (PayJsonData.Instance.GetUsePropsState(PayType.Reborn))
        {
            int count = PayJsonData.Instance.GetPropsCostCount(PayType.Reborn);
            PlayerData.ItemType type = PayJsonData.Instance.GetPropsCostType(PayType.Reborn);

            if (PlayerData.Instance.GetItemNum(type) >= count)
            {
                PlayerData.Instance.ReduceItemNum(type, count);
                RebornCall("Success");
            }
            else
            {
                PayType payType = (type == PlayerData.ItemType.Coin) ? PayType.CoinGift : PayType.JewelGift;
                GiftPackageControllor.Instance.Show(payType, Reborn);
                
            }
        }
        else
        {
            PayBuildPayManage.Instance.Pay(PayData.Instance.GetPayCode(PayType.Reborn), RebornCall);
        }
    }


    void RebornCall(string result)
    {
        if (result.CompareTo("Success") == 0)
        {
            Hide();
            //第二关之前无限免费复活, 教程不计入复活次数
            if (bIsFreeReborn && !isGuideReborn && (PlayerData.Instance.GetSelectedLevel() > 2 || PlayerData.Instance.GetGameMode().CompareTo("WuJin") == 0))
                PlayerData.Instance.AddFreeRebornTimes();
            GameController.Instance.Reborn();
            
        }
    }
    #endregion
}
