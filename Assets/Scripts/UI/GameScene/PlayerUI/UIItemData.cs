using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIItemData : MonoBehaviour {

	public Image clippedMask;
	public Text UINumberText;
	public Text UIJewelCostText;
	public Text UICoinCostText;
	public GameObject UIJewelCostObj;
	public GameObject UICoinCostObj;
	//public ParticleSystem ProtectShieldParticle;
	
	private PlayerData.ItemType SkillType;
	private float skillTime, coolTime;
	[HideInInspector]
	public bool coolFlag;

	public void Init (PlayerData.ItemType type)
	{
		SkillType = type;
		SetNumberText (PlayerData.Instance.GetItemNum (SkillType));

		switch (SkillType) {
		case PlayerData.ItemType.ProtectShield:
			skillTime = PropConfigData.Instance.GetSkillTime ((int)PropType.Shield);
			break;
		case PlayerData.ItemType.SpeedUp:
			skillTime = PropConfigData.Instance.GetSkillTime ((int)PropType.SpeedUp);
			break;
		case PlayerData.ItemType.FlyBomb:
			skillTime = PropConfigData.Instance.GetSkillTime ((int)PropType.FlyBmob);
			break;
		}
		coolFlag = false;
	}

	public void SetNumberText(int number)
	{
		if (Application.isPlaying == false)
			return;

		if (number > 0)
		{
			UINumberText.text = "X" + number;
			UINumberText.gameObject.SetActive (true);

			UIJewelCostObj.SetActive (false);
			UICoinCostObj.SetActive(false);
            //if (SkillType == PlayerData.ItemType.ProtectShield)
            //{
            //    ProtectShieldParticle.Stop();
            //    ProtectShieldParticle.gameObject.SetActive(false);
            //}
        } 
		else
		{
			//if(SkillType == PlayerData.ItemType.ProtectShield && PlatformSetting.Instance.PayVersionType != PayVersionItemType.GuangDian)
			//{
			//	UINumberText.text = "X" + number;
			//	UINumberText.gameObject.SetActive (false);

			//	UIJewelCostObj.SetActive (true);
			//	UICoinCostObj.SetActive(false);
			//	//if (!ProtectShieldParticle.gameObject.activeSelf)
			//	//	ProtectShieldParticle.gameObject.SetActive (true);
			//	//ProtectShieldParticle.Play ();
			//}
			//else
			//{
				if(BuySkillData.Instance.GetBuyType((int)SkillType).CompareTo("Jewel") == 0)
				{
					UIJewelCostObj.SetActive (true);
					UICoinCostObj.SetActive(false);
					UIJewelCostText.text = BuySkillData.Instance.GetCost((int)SkillType);
				}
				else
				{
					UIJewelCostObj.SetActive (false);
					UICoinCostObj.SetActive(true);
					UICoinCostText.text = BuySkillData.Instance.GetCost((int)SkillType);
                }
				UINumberText.gameObject.SetActive (false);
			//}


		}
	}

	public void HideClippedEffect()
	{
		StopCoroutine ("HideClippedSprite");
        clippedMask.fillAmount = 1f;
		gameObject.GetComponent<Button>().interactable = true;
        coolFlag = false;
        clippedMask.gameObject.SetActive (false);
	}
	
	public void ShowClippedEffect()
	{
        if (!this.gameObject.activeInHierarchy)
            return;
		coolTime = skillTime;
		gameObject.GetComponent<Button> ().interactable = false;
		coolFlag = true;
        clippedMask.gameObject.SetActive (true);
        clippedMask.fillAmount = 0f;
        StartCoroutine ("HideClippedSprite");
	}

	IEnumerator HideClippedSprite()
	{
		while(coolTime > 0)
		{
			while(GameData.Instance.IsPause)
				yield return 0;
			
			coolTime -= Time.deltaTime;
            clippedMask.fillAmount = coolTime / skillTime;
			yield return 0;
		}
		gameObject.GetComponent<Button>().interactable = true;
        coolFlag = false;
        clippedMask.gameObject.SetActive (false);
	}
}
