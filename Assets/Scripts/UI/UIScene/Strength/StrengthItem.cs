using UnityEngine;
using System.Collections;
using DG.Tweening;

public class StrengthItem : MonoBehaviour
{

    public GameObject goBuyButton;
    public Transform tranIconForAnimation;
    public tk2dTextMesh CountText;
    public tk2dSprite CostTypeImage;
    public tk2dTextMesh CostCountText, CountTipsText;

    [HideInInspector] public int iItemID;
    private int count;
    private int costCount;
    private PlayerData.ItemType itemType;

    public GameObject goCostType;
    public GameObject goCount;

    public GameObject goViewAds;
    public GameObject goViewAdsButton;
    public GameObject goNoVideoAds;


    public void Init()
    {
    }


    public void SetCountText(int count)
    {
        this.count = count;
        CountText.text = "x" + count;
        CountTipsText.text = count.ToString();
    }

    public void SetItemType(PlayerData.ItemType itemType)
    {
        this.itemType = itemType;
        string typeName;
        if (itemType == PlayerData.ItemType.Jewel)
        {
            CostTypeImage.transform.localPosition = new Vector3(7, 0, 0);
        }
        else
        {
            if (this.costCount > 10000)
                CostTypeImage.transform.localPosition = new Vector3(7, 0, 0);
            else
                CostTypeImage.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    public void SetCostTypeImage(string typeName)
    {
        CostTypeImage.SetSprite(typeName);
    }

    public void SetCostCountText(int costCount)
    {
        this.costCount = costCount;
        CostCountText.text = costCount.ToString();
    }

    public void ShowIconAnimation()
    {
        tranIconForAnimation.gameObject.SetActive(true);
        tranIconForAnimation.localPosition = new Vector3(-130.4f, 1, 0);
        tranIconForAnimation.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        DOTween.Kill("IconAnimation");
        tranIconForAnimation.DOLocalMove(new Vector3(-367, 181 + 85 * (iItemID - 1), 0), 0.6f).SetId("IconAnimation").OnComplete(IconAnimationCall);
    }

    void IconAnimationCall()
    {
        tranIconForAnimation.gameObject.SetActive(false);
        tranIconForAnimation.localPosition = new Vector3(-100, 1, 0);
        tranIconForAnimation.localScale = new Vector3(0.7f, 0.7f, 0.7f);
    }

    public void BuyButtonOnClick()
    {
        AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
        if (PlayerData.Instance.GetItemNum(this.itemType) < this.costCount)
        {
            //if (this.itemType == PlayerData.ItemType.Coin)
            //{
            //    Debug.Log("Gold");
            //    GiftPackageControllor.Instance.Show(PayType.CoinGift, GiftBoxCallBack);
            //    CollectInfoEvent.SendEvent(CollectInfoEvent.EventType.Gift_Coin, "State", "Automatically pop up", "Level", PlayerData.Instance.GetSelectedLevel().ToString());
            //}
            //else
            //{
            //    Debug.Log("Diamond");
            //    GiftPackageControllor.Instance.Show(PayType.JewelGift, GiftBoxCallBack);
            //    CollectInfoEvent.SendEvent(CollectInfoEvent.EventType.Gift_Jewel, "State", "Automatically pop up", "Level", PlayerData.Instance.GetSelectedLevel().ToString());
            //}
        }
        else
        {
            Debug.Log("AllGood");
            SetPlayerData();
        }
        return;
    }

    public void ViewAdsButtonOnClick()
    {
        AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
        
    }

    private void SetPlayerData()
    {
        ShowIconAnimation();
        PlayerData.Instance.ReduceItemNum(this.itemType, this.costCount);
        PlayerData.Instance.AddItemNum(PlayerData.ItemType.Strength, this.count);
    }

    private void GiftBoxCallBack()
    {
        SetPlayerData();
    }

    public void ShowViewAds()
    {
        goCostType.SetActive(false);
        goCount.SetActive(false);
        goBuyButton.SetActive(false);

        goNoVideoAds.SetActive(false);
        goViewAds.SetActive(false);
        goViewAdsButton.SetActive(false);
       
    }

    public void ShowBuy()
    {
        goCostType.SetActive(true);
        goCount.SetActive(true);
        goBuyButton.SetActive(true);

        goViewAds.SetActive(false);
        goNoVideoAds.SetActive(false);
        goViewAdsButton.SetActive(false);
    }

    public void Update()
    {
        if (gameObject.activeSelf == false) { return; }

        // 4: video ads
        if (iItemID != 4) { return; }

        //if (AndroidPackage.instance.hasVideoAds)
        //{
        //    goViewAds.SetActive(true);
        //    goViewAdsButton.SetActive(true);
        //    goNoVideoAds.SetActive(false);
        //}
        //else
        //{
        //    goViewAds.SetActive(false);
        //    goViewAdsButton.SetActive(false);
        //    goNoVideoAds.SetActive(true);
        //}

    }
}
