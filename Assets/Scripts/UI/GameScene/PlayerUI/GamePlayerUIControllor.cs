using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// 游戏场景中的UI
/// </summary>
public class GamePlayerUIControllor : UIBoxBase
{

    public static GamePlayerUIControllor Instance;
    public GameObject giftBagButton;
    public Text speedText, rankText, textTime;
    public Text textCoinCount, timeDownText, opponentDisText, rankDescText;
    public Image pathProgressFront;
    public RectTransform imageCarProgerss;
    public GameObject pathLenGO, opponentTipsGO, useShieldBtnGO, useSpeedupBtnGO, useFlyBombBtnGO;
    public GameObject inkSprite;
    public Image propIconSprite, opponentIcon;
    public Sprite[] propIcons;
    public Sprite[] opponentIcons;
    public GameObject lockGO;//propEffectGO, 
    public UIItemData SpeedUpItemData, FlyBombItemData, ShieldItemData;
    public GameObject leftBtnGO, rightBtnGO, giftBagGO, pauseBtnGO, pickPropBtnGO;

    public RectTransform[] carPointList;

    [HideInInspector]public string spriteId;
    bool isShowTime = false;

    void Awake()
    {
        Instance = this;
        
    }
    private void Start()
    {
        StartCoroutine(ProhibitedPause());
        giftBagGO.SetActive(false);
    }
    bool canPause = false;
    IEnumerator ProhibitedPause()
    {
        yield return new WaitForSeconds(8);
        //Debug.Log("CanPauseNow!");
        canPause = true;
    }

    void OnEnable()
    {
        iCoinCount = 0;

        textCoinCount.text = iCoinCount.ToString();
        timeDownText.gameObject.SetActive(false);

        GameData.Instance.CoinChangeEvnet += CoinCountChange;
        PlayerData.Instance.SpeedUpChangeEvent += SpeedUpCountChange;
        PlayerData.Instance.FlyBombChangeEvent += FlyBombCountChange;
        PlayerData.Instance.ShieldChangeEvent += ShieldCountChange;
    }

    #region 重写父类方法
    public override void Init()
    {
        Instance = this;
	    transform.localPosition = ShowPosV2;//Vector3.zero;
        SetPropIcon("");
        isPropLock = false;

        SpeedUpItemData.Init(PlayerData.ItemType.SpeedUp);
        FlyBombItemData.Init(PlayerData.ItemType.FlyBomb);
        ShieldItemData.Init(PlayerData.ItemType.ProtectShield);

        base.Init();
    }

    public override void Show()
    {
        base.Show();
        gameObject.SetActive(true);


        if (CarManager.Instance.gameLevelModel == GameLevelModel.Weedout)
        {
            pathLenGO.SetActive(false);
        }
        else if (CarManager.Instance.gameLevelModel == GameLevelModel.WuJing)
        {
            pathLenGO.SetActive(true);
            pathLenGO.transform.Find("Point").gameObject.SetActive(false);
        }
        else
        {
            pathLenGO.SetActive(true);
        }

        if (PlayerData.Instance.IsWuJinGameMode())
        {
            isShowTime = true;
            //textTime.transform.localPosition = new Vector3(-330f, 165f, 0);
            textTime.transform.gameObject.SetActive(true);
        }
        else if (CarManager.Instance.gameLevelModel == GameLevelModel.Weedout)
        {
            isShowTime = true;
            //textTime.transform.localPosition = new Vector3(-330f, 165f, 0);
            textTime.transform.gameObject.SetActive(true);
        }
        else
        {
            isShowTime = false;
            //textTime.transform.localPosition = new Vector3(-330f, 165f, 0);
            textTime.transform.gameObject.SetActive(false);
        }

        if (PlayerData.Instance.GetCurrentChallengeLevel() == 1)
        {

            pickPropBtnGO.SetActive(false);
            leftBtnGO.SetActive(false);
            rightBtnGO.SetActive(false);
            giftBagGO.SetActive(false);
            pauseBtnGO.SetActive(false);
        }

        if (PlayerData.Instance.GetCurrentChallengeLevel() <= 2)
        {
            useShieldBtnGO.SetActive(false);
            useSpeedupBtnGO.SetActive(false);
            useFlyBombBtnGO.SetActive(false);
        }

        if (PlatformSetting.Instance.PayVersionType == PayVersionItemType.GuangDian)
        {
            giftBagGO.SetActive(false);
        }
    }

    public override void Hide()
    {
        gameObject.SetActive(false);
        GameUIManager.Instance.HideModule(UISceneModuleType.GamePlayerUI);
    }

    public override void Back()
    {
        if (!canPause) return;
        AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
        GameUIManager.Instance.ShowModule(UISceneModuleType.GamePause);
    }

    #endregion

    void Update()
    {
        if (isLeft)
        {
            PlayerCarControl.Instance.carMove.isTurnLeft = true;
            PlayerCarControl.Instance.carMove.isTurnRight = false;
        }
        else if (isRight)
        {
            PlayerCarControl.Instance.carMove.isTurnRight = true;
            PlayerCarControl.Instance.carMove.isTurnLeft = false;
        }
        else
        {
            PlayerCarControl.Instance.carMove.isTurnLeft = false;
            PlayerCarControl.Instance.carMove.isTurnRight = false;
        }
        speedText.text = (PlayerCarControl.Instance.carMove.speed * 2).ToString("F0");

        UpdateRank();
        UpdateDistanceProcess();
        UpdateUseTime();
        //giftBagButton.SetActive(false);
    }

    public void SetPropIcon(string iconName)
    {
        if (string.IsNullOrEmpty(iconName))
        {
            propIconSprite.gameObject.SetActive(false);
            //propEffectGO.SetActive(false);
            StopCoroutine("IEShowUserPropGuide");
            isPreShowUsePropGuide = false;
        }
        else
        {
            spriteId = iconName;
            
            propIconSprite.gameObject.SetActive(true);
            propIconSprite.sprite = Array.Find(propIcons, i => i.name == iconName);
            //propEffectGO.SetActive(true);

            if (PlayerData.Instance.GetCurrentChallengeLevel() < 6 && isPreShowUsePropGuide == false)
            {
                StartCoroutine("IEShowUserPropGuide");
            }
        }
    }

    void SpeedUpCountChange(int speedUpCount)
    {
        SpeedUpItemData.SetNumberText(speedUpCount);
    }

    void FlyBombCountChange(int flyBombCount)
    {
        FlyBombItemData.SetNumberText(flyBombCount);
    }

    void ShieldCountChange(int shieldCount)
    {
        ShieldItemData.SetNumberText(shieldCount);
    }

    #region 分数、金币数
    [HideInInspector] public int iCoinCount;
    private int checkRankCount = 0;

    void CoinCountChange(int coinNum)
    {
        DOTween.Kill("CoinCountChange");
        DOTween.To(() => iCoinCount, x => iCoinCount = x, coinNum, 0.8f).OnUpdate(UpdateCoinCount).SetId("CoinCountChange");
    }
    void UpdateCoinCount()
    {
        textCoinCount.text = iCoinCount.ToString();
    }

    void UpdateRank()
    {
        ++checkRankCount;
        if (checkRankCount > 8)
        {
            checkRankCount = 0;
            int rank = CarManager.Instance.GetPlayerRank();
            rankText.text = rank.ToString();
            GameData.Instance.rank = rank;
            switch(rank)
            {
                case 1: rankDescText.text = Localisation.GetString("st"); break;
                case 2: rankDescText.text = Localisation.GetString("nd"); break;
                case 3: rankDescText.text = Localisation.GetString("rd"); break;
                default: rankDescText.text = Localisation.GetString("th"); break;
            }
        }
    }

    Vector2 pathpersent = new Vector2(0,0);
    void UpdateDistanceProcess()
    {
        if (CarManager.Instance.gameLevelModel == GameLevelModel.Weedout)
        {
            return;
        }
        //路程进度条
        float pathPercent = PlayerCarControl.Instance.GetPathPercent();
        pathProgressFront.fillAmount = pathPercent;//.clipTopRight = new Vector2(p, 1);

        //小车辆位置
        //x = -15 - 0% 185-100%
        pathpersent.x = (pathPercent * 185) - 15;
        imageCarProgerss.anchoredPosition = pathpersent;

        UpdateCarPathPercent();
    }
    Vector2 carpercent = new Vector2(0,-10);
    void UpdateCarPathPercent()
    {
        if (CarManager.Instance.gameLevelModel == GameLevelModel.WuJing)
        {
            return;
        }

        float totalLen = CarManager.Instance.totalPathLen;
        for (int i = 0; i < CarManager.Instance.carMoveList.Count; ++i)
        {
            CarMove carMove = CarManager.Instance.carMoveList[i];
            float percent = carMove.moveLen / totalLen;

            //x = -10 - 0% 190-100%
            carpercent.x = (percent * 190) - 10;
            carPointList[i].anchoredPosition = carpercent;
            
        }
        for (int k = CarManager.Instance.carMoveList.Count - 1; k < carPointList.Length; ++k)
        {
            carPointList[k].gameObject.SetActive(false);
        }
    }

    void UpdateUseTime()
    {
        if (isShowTime)
        {
            float time = CarManager.Instance.totalUseTime - CarManager.Instance.playerUseTime;
            textTime.text = SecondsToTimeStr(time);

            if (time < 10 + 2 * Time.deltaTime)
            {
                StartTimeDown();
            }
        }
    }

    string SecondsToTimeStr(float sec)
    {
        string sSecond;
        string sMinute;
        string sMinSec;
        int iSecond;
        int iMinute;

        int minSec = (int)((sec - (int)sec) * 100);
        if (minSec < 10)
            sMinSec = "0" + minSec;
        else
            sMinSec = minSec.ToString();


        int iSec = (int)sec;
        iSecond = iSec % 60;
        if (iSecond < 10)
            sSecond = "0" + iSecond;
        else
            sSecond = iSecond + "";

        iMinute = iSec / 60;
        sMinute = iMinute + "";

        return sMinute + ":" + sSecond + ":" + sMinSec;
    }
    #endregion

    #region 倒计时
    public void StartTimeDown()
    {
        isShowTime = false;
        textTime.gameObject.SetActive(false);
        StopCoroutine("IETimeDown");
        StartCoroutine("IETimeDown");
    }
    public void StopTimeDown()
    {
        isShowTime = true;
        textTime.gameObject.SetActive(true);
        timeDownText.gameObject.SetActive(false);
        StopCoroutine("IETimeDown");
    }

    IEnumerator IETimeDown()
    {
        int preT, curT;
        curT = (int)(CarManager.Instance.totalUseTime - CarManager.Instance.playerUseTime);
        preT = curT;
        timeDownText.gameObject.SetActive(true);
        timeDownText.text = curT.ToString();

        Sequence timeFirstSe = DOTween.Sequence();
        timeFirstSe.Append(timeDownText.transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.1f));
        timeFirstSe.Append(timeDownText.transform.DOScale(Vector3.one, 0.4f));
        while (true)
        {
            yield return null;
            curT = (int)(CarManager.Instance.totalUseTime - CarManager.Instance.playerUseTime);

            if (curT != preT)
            {
                if (curT < 0 || curT > 10)
                {
                    StopTimeDown();
                    yield break;
                }
                timeDownText.text = curT.ToString();
                timeDownText.transform.DOKill();
                Sequence timeSe = DOTween.Sequence();
                timeSe.Append(timeDownText.transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.1f));
                timeSe.Append(timeDownText.transform.DOScale(Vector3.one, 0.4f));
                AudioManger.Instance.PlaySound(AudioManger.SoundName.Fuelout);

                preT = curT;
            }
        }
    }

    #endregion

    private bool isPropLock = false;
    public void SetPropLock(bool lockFlag)
    {
        isPropLock = lockFlag;
        lockGO.SetActive(lockFlag);
    }

    private bool isPreShowUsePropGuide = false;
    private IEnumerator IEShowUserPropGuide()
    {
        //Debug.Log("IEShowUserPropGuide");
        isPreShowUsePropGuide = true;
        float cal = 0;
        while (cal < 10f)
        {
            cal += Time.deltaTime;
            yield return 0;
            while (GameData.Instance.IsPause)
            {
                yield return 0;
            }
        }

        if (CarManager.Instance.isFinish == false && isPropLock == false && GameData.Instance.IsWin == false)
        {
            GameController.Instance.PauseGame();
            UIGuideControllor_GameClone.Instance.Show(UIGuideType.GamePlayerUIUseCurPropGuide);
            if (!SceneButtonsController._keyboard)
            {
                UIGuideControllor_GameClone.Instance.ShowBubbleTipByID(15);
            }
            else UIGuideControllor_GameClone.Instance.ShowBubbleTipByID(19);
        }
        isPreShowUsePropGuide = false;
    }

    public void ShowOpponentTips(int carId, float xPercent, float distance)
    {
        opponentTipsGO.SetActive(true);
        string iname = ModelData.Instance.GetPlayerIcon(carId);
        opponentIcon.sprite = Array.Find(opponentIcons, op => op.name == iname);
        opponentDisText.text = ((int)distance).ToString() + " m";
        xPercent = Mathf.Clamp01(xPercent);
        float x = xPercent * 120 * 2 - 120f;
        Vector3 pos = opponentTipsGO.transform.localPosition;
        pos.x = x;
        opponentTipsGO.transform.localPosition = pos;
    }
    public void HideOpponentTips()
    {
        opponentTipsGO.SetActive(false);
    }

    #region  按钮控制

    private bool isLeft = false;
    private bool isRight = false;

    public void LeftDown()
    {
        isLeft = true;
        isRight = false;
        if (PlayerCarControl.Instance.carMove.xOffset >= PlayerCarControl.Instance.carMove.maxXOffset - 0.5f)
            return;
        PlayerCarControl.Instance.carMove.animManager.LeftMove();
    }
    public void LeftUp()
    {
        isLeft = false;
        PlayerCarControl.Instance.carMove.animManager.LeftMoveBack();
    }

    public void RightDown()
    {
        isRight = true;
        isLeft = false;
        if (PlayerCarControl.Instance.carMove.xOffset <= PlayerCarControl.Instance.carMove.minXOffset + 0.5f)
            return;
        PlayerCarControl.Instance.carMove.animManager.RightMove();
    }

    public void RightUp()
    {
        isRight = false;
        PlayerCarControl.Instance.carMove.animManager.RightMoveBack();
    }

    public void UsePropOnClick()
    {
        if (isPropLock)
            return;
        AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
        PlayerCarControl.Instance.propCon.UseCurProp();
        SetPropIcon("");
    }

    public void GiftBtnOnClick()
    {
        AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
        GameController.Instance.PauseGame();

        LevelGiftControllor.Instance.Show(PayType.InnerGameGift, UseFreeFlyBmobProp, false);
    }

    public void UseSpeedUpProp()
    {
        if (SpeedUpItemData.coolFlag || GameData.Instance.IsPause || GameData.Instance.IsWin)
            return;

        AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
        if (PlayerCarControl.Instance.propCon.isSpeedUp)
            return;
        if (PlayerData.Instance.GetItemNum(PlayerData.ItemType.SpeedUp) <= 0)
        {
            int propCost = int.Parse(BuySkillData.Instance.GetCost((int)PlayerData.ItemType.SpeedUp));
            if (PlayerData.Instance.GetItemNum(PlayerData.ItemType.Jewel) >= propCost)
            {
                PlayerData.Instance.ReduceItemNum(PlayerData.ItemType.Jewel, propCost);
            }
            else
            {
              //  GameData.Instance.IsPause = true;
              //  GiftPackageControllor.Instance.Show(PayType.JewelGift, UseSpeedUpProp);
                return;
            }
        }
        else
        {
            PlayerData.Instance.ReduceItemNum(PlayerData.ItemType.SpeedUp, 1);
        }
        PlayerCarControl.Instance.propCon.UsePropByType(PropType.SpeedUp);
        SpeedUpItemData.ShowClippedEffect();
        CreatePropManager.Instance.InsertGruop();
    }
    public void UseFlyBmobProp()
    {
        if (FlyBombItemData.coolFlag || GameData.Instance.IsPause || GameData.Instance.IsWin)
            return;

        AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
        if (PlayerData.Instance.GetItemNum(PlayerData.ItemType.FlyBomb) <= 0)
        {
            int propCost = int.Parse(BuySkillData.Instance.GetCost((int)PlayerData.ItemType.FlyBomb));
            if (PlayerData.Instance.GetItemNum(PlayerData.ItemType.Jewel) >= propCost)
            {
                PlayerData.Instance.ReduceItemNum(PlayerData.ItemType.Jewel, propCost);
            }
            else
            {
              //  GameData.Instance.IsPause = true;
              //  GiftPackageControllor.Instance.Show(PayType.JewelGift, UseFlyBmobProp);
                return;
            }
        }
        else
        {
            PlayerData.Instance.ReduceItemNum(PlayerData.ItemType.FlyBomb, 1);
        }
        PlayerCarControl.Instance.propCon.UsePropByType(PropType.FlyBmob);
        FlyBombItemData.ShowClippedEffect();
    }

    //关卡内礼包购买后触发一次的必杀方法
    void UseFreeFlyBmobProp()
    {
        PlayerCarControl.Instance.propCon.UsePropByType(PropType.FlyBmob);
        FlyBombItemData.ShowClippedEffect();
    }

    public void UseShieldProp()
    {
        if (ShieldItemData.coolFlag || GameData.Instance.IsPause || GameData.Instance.IsWin)
            return;

        AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
        if (PlayerData.Instance.GetItemNum(PlayerData.ItemType.ProtectShield) <= 0)
        {
            //if (PlatformSetting.Instance.PayVersionType != PayVersionItemType.GuangDian)
            //{
            //   // GameData.Instance.IsPause = true;
            // //   GameUIManager.Instance.ShowModule(UISceneModuleType.ProtectShield);
            //    return;
            //}
            if (PlayerCarControl.Instance.propCon.isShield)
                return;
            int propCost = int.Parse(BuySkillData.Instance.GetCost((int)PlayerData.ItemType.ProtectShield));
            if (PlayerData.Instance.GetItemNum(PlayerData.ItemType.Jewel) >= propCost)
            {
                PlayerData.Instance.ReduceItemNum(PlayerData.ItemType.Jewel, propCost);
            }
            else
            {
             //   GameData.Instance.IsPause = true;
              //  GiftPackageControllor.Instance.Show(PayType.JewelGift, UseShieldProp);
                return;
            }
        }
        else
        {
            if (PlayerCarControl.Instance.propCon.isShield)
                return;
            PlayerData.Instance.ReduceItemNum(PlayerData.ItemType.ProtectShield, 1);
        }
        PlayerCarControl.Instance.propCon.UsePropByType(PropType.Shield);
        ShieldItemData.ShowClippedEffect();
    }

    public void PuaseButtonOnClick()
    {
        if (GameData.Instance.IsPause)
            return;

        AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
        GameUIManager.Instance.ShowModule(UISceneModuleType.GamePause);
    }

    private void OnApplicationPause(bool pause)
    {
        if(!GameData.Instance.IsPause)
            GameUIManager.Instance.ShowModule(UISceneModuleType.GamePause);
    }

    private void OnApplicationFocus(bool focused)
    {
        if(!GameData.Instance.IsPause)
            GameUIManager.Instance.ShowModule(UISceneModuleType.GamePause);
    }
    #endregion
}
