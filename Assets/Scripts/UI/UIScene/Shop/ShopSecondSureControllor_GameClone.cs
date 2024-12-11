using UnityEngine;
using PayBuild;
using UnityEngine.UI;
using System;

public class ShopSecondSureControllor_GameClone : UIBoxBase {


	public static ShopSecondSureControllor_GameClone Instance;

	public GameObject goCancelBtn;
	public Text textTitleText;
	public Text textHint, textPayBt, textGiftCount;
    public Image imageGiftIcon;

    public Sprite[] sprites = new Sprite[5];

	private PlayerData.ItemType itemType;
	private int iPayId, iCount;
	private string sTitle;

	#region 重写父类方法
	public override void Init ()
	{
		Instance = this;

		base.Init();
	}
	
	public override void Show ()
	{
		base.Show ();
		//if (PlatformSetting.Instance.PayVersionType == PayVersionItemType.ShenHe) {
		//	// goShenheBG.SetActive (true); // 关闭背景切换
		//	goBuyButton.transform.Find("UI_libao01_hg").gameObject.SetActive(false); //审核版不显示按钮特效，更换特效时要改名称
		//} else {
		//	// goShenheBG.SetActive(false); // 关闭背景切换
		//}

	}
	
	public override void Hide ()
	{
		base.Hide();
		if(GlobalConst.SceneName == SceneType.UIScene)
			UIManager.Instance.HideModule (UISceneModuleType.ShopSecondSure);
		else
			GameUIManager.Instance.HideModule (UISceneModuleType.ShopSecondSure);
	}
	
	public override void Back ()
	{
		AudioManger.Instance.PlaySound(AudioManger.SoundName.CloseBtClick);
		Hide();
	}
	#endregion

	public void InitData(int index)
	{
		itemType = ShopData.Instance.GetItemType (index);
		iPayId = ShopData.Instance.GetPayId(index);

		sTitle = ShopData.Instance.GetItemName(index);

		textTitleText.text = sTitle;


		imageGiftIcon.sprite = Array.Find(sprites, o => o.name == ShopData.Instance.GetGiftIcon(index));

		if(itemType != PlayerData.ItemType.DoubleCoin)
		{
			iCount = ShopData.Instance.GetCount(index);
		    textGiftCount.text = iCount.ToString();
			//textGiftCount.transform.localPosition = new Vector3(70, -111, 0);
		}else
		{
			//textGiftCount.transform.localPosition = new Vector3(70, -111, 0);
			textGiftCount.text = "Permanent";
		}
		//ImageGiftType.gameObject.SetActive(itemType != PlayerData.ItemType.DoubleCoin);

		//PayUIConfigurator.PayUIConfig(ShopData.Instance.GetPayJsonType(index), textHint, textAdWords, textDesc1, textDesc2, textPayBt, imageCloseBt,goBuyButton.GetComponent<BoxCollider>(),SetCancelBt);
	   

	}

	void SetCancelBt(bool state)
	{
		if(PlatformSetting.Instance.PlatformType == PlatformItemType.DianXin)
		{
			string hint = textHint.text, pay = textPayBt.text;
			textHint.text = hint.Replace("30", "20");
			textPayBt.text = pay.Replace("30", "20");
		}

		if(state)
		{
			goCancelBtn.SetActive(true);
			//goCancelBtn.transform.localPosition = new Vector3(-125, -185, 0);
			//goBuyButton.transform.localPosition = new Vector3(90, -185, 0);
		}else
		{
			goCancelBtn.SetActive(false);
			//goBuyButton.transform.localPosition = new Vector3(0, -185, 0);
		}
	}

	public void CloseButtonOnClick()
	{
		AudioManger.Instance.PlaySound(AudioManger.SoundName.CloseBtClick);
		Hide();
		return;
	}
	
	public void BuyButtonOnClick()
	{
		AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
		PayBuildPayManage.Instance.Pay(iPayId, PayCallBack);
	}

	private void PayCallBack(string result)
	{
		if (result.CompareTo ("Success") == 0) {

			Hide();

			if(itemType == PlayerData.ItemType.DoubleCoin)
				PlayerData.Instance.SetForeverDoubleCoin();
			else
				PlayerData.Instance.AddItemNum (itemType, iCount);
		}
	}
}
