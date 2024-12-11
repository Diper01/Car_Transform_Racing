using UnityEngine;
using System.Collections;
using PayBuild;
using DG.Tweening;

/// <summary>
/// 月卡礼包弹出框.
/// </summary>
public class MonthCardGiftControllor : UIBoxBase {
	
	public GameObject goShenheBG;
	public GameObject goBuyButton, goCancelBtn;
	public EasyFontTextMesh titleText, textHint1, textHint2, textDesc1, textDesc2, textPayBt;
	public tk2dSprite imageCloseBt;
	
	public tk2dSprite[] iconSpriteArr;
	public tk2dTextMesh[] countTextArr;
	
	public override void Init ()
	{
		titleText.text = PayJsonData.Instance.GetGiftTitle(PayType.MonthCardGift);
		string[] IconArr  = PayJsonData.Instance.GetGiftItemsIconArr(PayType.MonthCardGift);
		int[]    CountArr = PayJsonData.Instance.GetGiftItemsCountsArr(PayType.MonthCardGift);
		
		for(int i=0; i<IconArr.Length; ++i)
		{
			if(i < IconArr.Length)
			{
				if(IconArr[i].CompareTo("coin_icon") == 0 || IconArr[i].CompareTo("jewel_icon") == 0)
					IconArr[i] = IconArr[i].Replace("icon", "2");
				iconSpriteArr[i].SetSprite (IconArr[i]);
				countTextArr[i].text = "X" + CountArr[i].ToString();
			}
		}

		base.Init();
	}
	
	public override void Show ()
	{
		base.Show();
		
		PayUIConfigurator.PayUIConfig(PayType.MonthCardGift, textHint1, textHint2, textDesc1, textDesc2, textPayBt, imageCloseBt, goBuyButton.GetComponent<BoxCollider>(), SetCancelBt);
		
	}
	
	void SetCancelBt(bool state)
	{
		if(state)
		{
			goCancelBtn.SetActive(true);
		}
		else
		{
			goCancelBtn.SetActive(false);
		}
	}
	
	public override void Hide ()
	{
		base.Hide();
		UIManager.Instance.HideModule (UISceneModuleType.MonthCardGift);
		if(preBoxType == UISceneModuleType.LevelInfo)
		{
			if(PlatformSetting.Instance.PayVersionType == PayVersionItemType.ChangWan && PlayerData.Instance.GetHuiKuiMiniGiftState()==false)
			{
				UIManager.Instance.ShowModule (UISceneModuleType.NewPlayerGift);
			}
			else if (AutoGiftChecker.CTypeGiftBagEnabled) {
				UIManager.Instance.ShowModule (UISceneModuleType.DiscountGift);
			}else if (PlatformSetting.Instance.IsOpenCommonGiftType && !PlayerData.Instance.GetCommonGiftIsBuy())
			{
				UIManager.Instance.ShowModule(UISceneModuleType.CommonGift);
			} 
			else if (GlobalConst.StartGameGuideEnabled)
				UIGuideControllor.Instance.Show (UIGuideType.LevelInfoGuide);
		}
		
	}
	public override void Back ()
	{
		AudioManger.Instance.PlaySound(AudioManger.SoundName.CloseBtClick);
		Hide();
	}
	
	public void CancelOnClick()
	{
		AudioManger.Instance.PlaySound(AudioManger.SoundName.CloseBtClick);
		Hide();
	}
	
	public void CloseOnClick()
	{
		AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
		Hide();
	}
	
	public void GetOnClick()
	{
		AudioManger.Instance.PlaySound (AudioManger.SoundName.ButtonClick);
		PayBuildPayManage.Instance.Pay ((int)PayType.MonthCardGift, GetGiftPayCallBack);
	}
	
	void GetGiftPayCallBack(string result)
	{
		if (result.CompareTo ("Success") != 0) {
			Hide();
			return;
		}
		
		
		//设置月卡礼包已经购买
		PlayerData.Instance.SetMonthCardGiftState (true);
		
		//默认设置月卡礼包为已续费状态
		PlayerData.Instance.SetMonthCardGiftAutoRenewState (true);
		
		//设置购买月卡礼包的月份
		PlayerData.Instance.SetBuyMonthCardDate (System.DateTime.Now.Month);
		MainInterfaceControllor.Instance.goMonthCardGiftButton.transform.Find("BeforeGetBtn").gameObject.SetActive(false);
		MainInterfaceControllor.Instance.goMonthCardGiftButton.transform.Find("AfterGetBtn").gameObject.SetActive(true);
		
		
		PlayerData.ItemType[] itemType = PayJsonData.Instance.GetGiftItemsTypeArr(PayType.MonthCardGift);
		int[] count = PayJsonData.Instance.GetGiftItemsCountsArr(PayType.MonthCardGift);
		
		for(int i = 0; i < itemType.Length; i ++)
		{
			PlayerData.Instance.AddItemNum(itemType[i], count[i]);
		}
		//设置月卡礼包奖励已经获取
		PlayerData.Instance.SetMonthCardGiftRewardsState (true);
		Hide();
	}
}
