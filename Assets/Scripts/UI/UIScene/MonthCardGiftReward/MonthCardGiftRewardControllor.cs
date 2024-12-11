using UnityEngine;
using System.Collections;

public class MonthCardGiftRewardControllor : UIBoxBase {
	
	
	
	public override void Init ()
	{
		base.Init();
	}
	
	public override void Show ()
	{
		base.Show();
		
	}
	
	
	public override void Hide ()
	{
		base.Hide();
		UIManager.Instance.HideModule (UISceneModuleType.MonthCardGiftReward);
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
		
		PlayerData.Instance.SetMonthCardGiftRewardsState(true);
		PlayerData.Instance.SetBuyMonthCardDate (System.DateTime.Now.Month);
		
		PlayerData.Instance.AddItemNum(PlayerData.ItemType.Jewel, 400);
		
		Hide ();
	}
}
