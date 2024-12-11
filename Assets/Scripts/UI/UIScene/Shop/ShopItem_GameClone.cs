using UnityEngine;
using System;
using PayBuild;
using UnityEngine.UI;

public class ShopItem_GameClone : MonoBehaviour {

	public GameObject goBuyButton;
	public Image IconImage;
    public Sprite[] iconsImage;
    public Image TypeImage;
    public Sprite[] iconsType;
	public Text CountText;
	public Text BtnText;
	public Text TitleText;

	private int count = 0;
	private int payId;
	[HideInInspector]public PlayerData.ItemType itemType;
	private int iIndex;

	public void Init(int index)
	{
		iIndex = index;
		SetIconImage (ShopData.Instance.GetGiftIcon (index));
		SetTitleText (ShopData.Instance.GetItemName (index));
		itemType = ShopData.Instance.GetItemType (index);
		payId = ShopData.Instance.GetPayId (index);

		if(itemType != PlayerData.ItemType.DoubleCoin)
		{
		    SetTypeImage (ShopData.Instance.GetIconName (index));
		    SetCountText (ShopData.Instance.GetCount (index));
			//CountText.transform.localPosition = new Vector3(18, 0, 0);

			string costString = PayBuildPayManage.Instance.GetProductPriceString((int)index);
			SetBtnText (costString);//ShopData.Instance.GetCost(index) + "usd"

		}else
		{
			//CountText.transform.localPosition = new Vector3(0, 0, 0);
			CountText.text = "Permanent";
			if(PlayerData.Instance.GetForeverDoubleCoin() == 1)
			{
				goBuyButton.GetComponent<BoxCollider>().enabled = false;
				SetBtnText ("Activated");
			}
			else
			{
				string costString = PayBuildPayManage.Instance.GetProductPriceString((int)index);
				SetBtnText (costString);
			}
		}
		TypeImage.gameObject.SetActive(itemType != PlayerData.ItemType.DoubleCoin);

	}

	public void SetIconImage(string iconName)
	{
		IconImage.sprite = Array.Find(iconsImage, i => i.name == iconName);
	}

	public void SetTypeImage(string typeName)
	{
		TypeImage.sprite = Array.Find(iconsType, i=>i.name==typeName);
	}

	public void SetCountText(int count)
	{
		this.count = count;
		CountText.text = count.ToString ();
	}

	public void SetBtnText(string btnName)
	{
		BtnText.text = btnName;
	}

	public void SetTitleText(string titleName)
	{
		TitleText.text = titleName;
	}

	public void GetButtonOnClick()
	{
		AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
		if(PayJsonData.Instance.GetIsActivedState(PayType.Shop))
		{
			ShopSecondSureControllor.Instance.InitData(iIndex);

			if(GlobalConst.SceneName == SceneType.UIScene)
				UIManager.Instance.ShowModule(UISceneModuleType.ShopSecondSure);
			else
				GameUIManager.Instance.ShowModule(UISceneModuleType.ShopSecondSure);	

		}else
		{
		    PayBuildPayManage.Instance.Pay(payId, PayCallBack);

		}
	}

	private void PayCallBack(string result)
	{
		if (result.CompareTo ("Success") == 0) {
			
			if (itemType == PlayerData.ItemType.DoubleCoin)
				PlayerData.Instance.SetForeverDoubleCoin ();
			else
				PlayerData.Instance.AddItemNum (itemType, count);
		}
	}
}
