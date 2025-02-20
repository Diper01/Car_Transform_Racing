using UnityEngine;
using System.Collections;
using PayBuild;
using UnityEngine.UI;
using System;

public class ProtectShieldControllor : UIBoxBase {

	public GameObject goBuyButton, goCancelBtn;
	public Text titleText, textHint1, textHint2, textDesc1, textDesc2, textPayBt;
	public Image imageCloseBt;
	
	public Image[] iconSpriteArr;
	public Text[] countTextArr;
    public Sprite[] sprites;
	
	public override void Init ()
	{
		titleText.text = PayJsonData.Instance.GetGiftTitle(PayType.SheildGift);
		string[] IconArr  = PayJsonData.Instance.GetGiftItemsIconArr(PayType.SheildGift);
		int[]    CountArr = PayJsonData.Instance.GetGiftItemsCountsArr(PayType.SheildGift);
		
		for(int i=0; i<IconArr.Length; ++i)
		{
			if(i < IconArr.Length)
			{                                                                             
				iconSpriteArr[i]. sprite = Array.Find(sprites, o => o.name == IconArr[i]);
				countTextArr[i].text = "X" + CountArr[i].ToString();
			}
		}

		base.Init();
	}
	
	public override void Show ()
	{
		base.Show();


		//if (PlatformSetting.Instance.PayVersionType == PayVersionItemType.ShenHe) {
		//	goBuyButton.transform.Find("UI_libao01_hg").gameObject.SetActive(false); //审核版不显示按钮特效，更换特效时要改名称
		//}
	}
	
	void SetCancelBt(bool state)
	{
		if(state)
		{
			goCancelBtn.SetActive(true);
			//goCancelBtn.transform.localPosition = new Vector3(-50, -124, 0);
			//goBuyButton.transform.localPosition = new Vector3(128, -124, 0);
		}else
		{
			goCancelBtn.SetActive(false);
			//goBuyButton.transform.localPosition = new Vector3(100, -124, 0);
		}
	}
	
	public override void Hide ()
	{
		GameController.Instance.ResumeGame();
		base.Hide();
		GameUIManager.Instance.HideModule (UISceneModuleType.ProtectShield);
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
	
	public void BuyOnClick()
	{
		AudioManger.Instance.PlaySound (AudioManger.SoundName.ButtonClick);
		PayBuildPayManage.Instance.Pay ((int)PayType.SheildGift, GetGiftPayCallBack);
	}
	
	void GetGiftPayCallBack(string result)
	{
		if (result.CompareTo ("Success") != 0)
			return;
        
		
		PlayerData.ItemType[] itemType = PayJsonData.Instance.GetGiftItemsTypeArr (PayType.SheildGift);
		int[] count = PayJsonData.Instance.GetGiftItemsCountsArr (PayType.SheildGift);
		
		for (int i = 0; i < itemType.Length; i ++) {
			PlayerData.Instance.AddItemNum (itemType [i], count [i]);
		}

		Hide ();
	}
}