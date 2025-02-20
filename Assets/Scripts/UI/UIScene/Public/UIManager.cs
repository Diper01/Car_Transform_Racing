﻿using UnityEngine;
using System.Collections;
using PathologicalGames;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    public Transform UICanvas, FirstCanvas, SecondCanvas, ThirdCanvas;

    public TranslucentUIMaskManager tranUIMask;
    //Some staff load frome resources!
    public UIBoxBase hMainInterfaceControllor;     Transform tranMainInterface;
    //public UIBoxBase hGiftPackageControllor;       Transform tranGiftPackage;
    public UIBoxBase hPropertyDisplayControllor;    Transform tranPropertyDisplay;
    public UIBoxBase hSettingControllor;           Transform tranSetting;
    public UIBoxBase hAotemanZaohuanControllor;    Transform tranAotemanZaohuan;
    //public UIBoxBase hShopControllor;               Transform tranShop;
    public UIBoxBase hExitGameControllor;          Transform tranExitGame;
    public UIBoxBase hCharacterDetailControllor;    Transform tranCharacterDetail;
    public UIBoxBase hLevelSelectControllor;           Transform tranLevelSelect;
    //public UIBoxBase hOneKeyToFullLevelControllor; Transform tranOneKeyToFullLevel;
    //public UIBoxBase hComplainControllor;          Transform tranComplain;
    public UIBoxBase hLevelInfoControllor;             Transform tranLevelInfo;
    public UIBoxBase hTurnplateControllor;         Transform tranTurnplate;
    public UIBoxBase hSmashEggControllor;          Transform tranSmashEgg;
    //public UIBoxBase hDoubleCoinControllor;            Transform tranDoubleCoin;
    public UIBoxBase hConvertCenterControllor;          Transform tranConvertCenter;
    public UIBoxBase hSignInControllor;                Transform tranSignIn;    
    public UIBoxBase hSignInRewardControllor;          Transform tranSignInReward;
    public UIBoxBase hAchievementControllor;            Transform tranAchievement;
    //public UIBoxBase hAotemanFamilyControllor;             Transform tranAotemanFamily;
    public UIBoxBase hStrengthControllor;              Transform tranStrength;//*
    //public UIBoxBase hDayTaskControllor;               Transform tranDayTask;
    //public UIBoxBase hActivityControllor;              Transform tranActivity;
    public UIBoxBase hDailyMissionControllor;          Transform tranDailyMission;
    public UIBoxBase hDailyMissionRewardControllor;     Transform tranDailyMissionReward;
    //public UIBoxBase hShopSecondSureControllor;         Transform tranShopSecondSure;
    //public UIBoxBase hExchangeCodeControllor;          Transform tranExchangeCode;
    //public UIBoxBase hExchangeCodeRewardControllor;     Transform tranExchangeCodeReward;
    public UIBoxBase hTurnplateAwardControllor;         Transform tranTurnplateAward;
    public UIBoxBase hLockTipControllor;                   Transform tranLockTip;
    public UIBoxBase hUIGuideControllor;               Transform tranUIGuide;
    //public UIBoxBase hHuaFeiManager;                   Transform tranHuaFei;
    //public UIBoxBase hHistoricRecordManager;           Transform tranHistoricRecord;
    //public UIBoxBase hPhoneNumberInputControllor;           Transform tranPhoneNumberInput;
    //public UIBoxBase hMechaDetailControllor;            Transform tranMechaDetailControllor;
    //public UIBoxBase hGamePassedGfitControllor;        Transform tranGamePassedGfit;
    //public UIBoxBase hNewPlayerGfitControllor;      Transform tranNewPlayerGfit;
    //public UIBoxBase hThreeStarControllor;             Transform tranThreeStar;
    //public UIBoxBase hFourStarControllor;           Transform tranFourStar;
    //public UIBoxBase hPassGiftControllor;               Transform tranPassGift;
   
    //public UIBoxBase hDiscountGiftControllor;        Transform tranDiscountGift;
    //public UIBoxBase hRemoveAdsControllor;       Transform tranRemoveAds;
    
    //public UIBoxBase hExchangeActivityControllor;        Transform tranExchangeActivity;
    public UIBoxBase hAwardItemBoxControllor;        Transform tranAwardItemBox;
    //public UIBoxBase hMonthCardGiftControllor;      Transform tranMonthCardGift;
    //public UIBoxBase hMonthCardGiftRewardControllor;    Transform tranMonthCardGiftReward;
    //public UIBoxBase hLuckyNumbersControllor;        Transform tranLuckyNumbersControllor;//*
    //public UIBoxBase hGamePlayingActivity;      Transform tranGamPlayingActivity;//*
    //public UIBoxBase hClearanceRedPaper;         Transform tranClearanceRedPaper;//*
     
    //public UIBoxBase hCommonGift; Transform tranCommonGift;
    //public UIBoxBase hCommonGiftAward; Transform tranCommonGiftAward;    

    SpawnPool spUIModules;

    public UISceneModuleType curBoxType;

    void Awake()
    {
        Instance = this;
        //ApplicationChrome.navigationBarState = ApplicationChrome.States.Hidden;
        //ApplicationChrome.statusBarState = ApplicationChrome.States.Hidden;
    }


    void Start()
    {
        if (GlobalConst.FirstIn)
        {
            GlobalConst.FirstIn = false;

            GameObject PlatformSetting = (GameObject)Instantiate(Resources.Load("UIScene/PlatformSetting"));
            PlatformSetting.SetActive(true);

            GameObject LDCanvas = (GameObject)Instantiate(Resources.Load("UIScene/LoadingPage_Canvas"));
            GlobalConst.SceneName = SceneType.UIScene;
            LDCanvas.GetComponentInChildren<LoadingPage>().InitScene();
        }
        spUIModules = PoolManager.Pools["UIModulesPool"];
        UIBoxInit();

        EventLayerController.Instance.Init();
    }

    bool isPaused;
    void OnApplicationPause(bool pauseStatus)
    {
        isPaused = pauseStatus;
        if (isPaused)
        {
            //ApplicationChrome.statusBarState = ApplicationChrome.navigationBarState = ApplicationChrome.States.Hidden;
        }
        else
        {
            //ApplicationChrome.statusBarState = ApplicationChrome.navigationBarState = ApplicationChrome.States.Hidden;
        }
    }

    void UIBoxInit()
    {

        curBoxType = UISceneModuleType.MainInterface;

        //transform
        //tranUIMask = CreateUIBox("TranslucentUIMask", transform);
        //tranUIMask.GetComponent<TranslucentUIMaskManager>().Init();
        tranUIMask.Init();

        //UICanvas
        //tranMainInterface = CreateUIBox(UISceneModuleType.MainInterface.ToString(), UICanvas);
        //hMainInterfaceControllor = tranMainInterface.GetComponent<MainInterfaceControllor>();
        hMainInterfaceControllor.Init();
        //tranLevelSelect = CreateUIBox(UISceneModuleType.LevelSelect.ToString(), UICanvas);
        //hLevelSelectControllor = tranLevelSelect.GetComponent<LevelSelectControllor>();
        hLevelSelectControllor.Init();
        
        //tranUIGuide = CreateUIBox(UISceneModuleType.UIGuide.ToString(), ThirdCanvas);
        //hUIGuideControllor = tranUIGuide.GetComponent<UIGuideControllor>();
        hUIGuideControllor.Init();

        //FirstCanvasg
        //tranPropertyDisplay = CreateUIBox(UISceneModuleType.PropertyDisplay.ToString(), FirstCanvas);
        //hPropertyDisplayControllor = tranPropertyDisplay.GetComponent<PropertyDisplayControllor>();
        hPropertyDisplayControllor.Init();
        //tranSignIn = CreateUIBox(UISceneModuleType.SignIn.ToString(), SecondCanvas);
        //hSignInControllor = tranSignIn.GetComponent<SignInControllor>();
        hSignInControllor.Init();       
        //tranLevelInfo = CreateUIBox(UISceneModuleType.LevelInfo.ToString(), FirstCanvas);
        //hLevelInfoControllor = tranLevelInfo.GetComponent<LevelInfoControllor>();
        hLevelInfoControllor.Init();

        GlobalConst.IsReady = true;
        GlobalConst.IsUIReady = true;
        
        //tranCharacterDetail = CreateUIBox(UISceneModuleType.CharacterDetail.ToString(), FirstCanvas);
        //hCharacterDetailControllor = tranCharacterDetail.GetComponent<CharacterDetailControllor>();
        hCharacterDetailControllor.Init();
        //tranActivity = CreateUIBox(UISceneModuleType.Activity.ToString(), FirstCanvas);
        //hActivityControllor = tranActivity.GetComponent<ActivityControllor>();
        //hActivityControllor.Init();
        //yield return new WaitForEndOfFrame();
        ////tranShop = CreateUIBox(UISceneModuleType.Shop.ToString(), FirstCanvas);
        ////hShopControllor = tranShop.GetComponent<ShopControllor>();
        ////hShopControllor.Init();
        ////yield return new WaitForEndOfFrame();
        //tranDoubleCoin = CreateUIBox(UISceneModuleType.DoubleCoin.ToString(), FirstCanvas);
        //hDoubleCoinControllor = tranDoubleCoin.GetComponent<DoubleCoinControllor>();
        //hDoubleCoinControllor.Init();
        //yield return new WaitForEndOfFrame();
        //tranStrength = CreateUIBox(UISceneModuleType.Strength.ToString(), FirstCanvas);
        //hStrengthControllor = tranStrength.GetComponent<StrengthControllor>();
        hStrengthControllor.Init();
        //yield return new WaitForEndOfFrame();
        //tranNewPlayerGfit = CreateUIBox(UISceneModuleType.NewPlayerGift.ToString(), FirstCanvas);
        //hNewPlayerGfitControllor = tranNewPlayerGfit.GetComponent<NewPlayerGiftControllor>();
        //hNewPlayerGfitControllor.Init();
        //yield return new WaitForEndOfFrame();
        //tranThreeStar = CreateUIBox(UISceneModuleType.ThreeStar.ToString(), FirstCanvas);
        //hThreeStarControllor = tranThreeStar.GetComponent<ThreeStarControllor>();
        //hThreeStarControllor.Init();
        //yield return new WaitForEndOfFrame();
        //tranFourStar = CreateUIBox(UISceneModuleType.FourStar.ToString(), FirstCanvas);
        //hFourStarControllor = tranFourStar.GetComponent<FourStarControllor>();
        //hFourStarControllor.Init();
        //yield return new WaitForEndOfFrame();
        //tranPassGift = CreateUIBox(UISceneModuleType.PassGift.ToString(), FirstCanvas);
        //hPassGiftControllor = tranPassGift.GetComponent<PassGiftControllor>();
        //hPassGiftControllor.Init();

        //yield return new WaitForEndOfFrame();
        //tranDiscountGift = CreateUIBox(UISceneModuleType.DiscountGift.ToString(), FirstCanvas);
        //hDiscountGiftControllor = tranDiscountGift.GetComponent<DiscountGiftControllor>();
        //hDiscountGiftControllor.Init();

        //yield return new WaitForEndOfFrame();
        //tranRemoveAds = CreateUIBox(UISceneModuleType.RemoveAds.ToString(), FirstCanvas);
        //hRemoveAdsControllor = tranRemoveAds.GetComponent<RemoveAdsController>();
        //hRemoveAdsControllor.Init();

        //yield return new WaitForEndOfFrame();
        //tranMonthCardGift = CreateUIBox(UISceneModuleType.MonthCardGift.ToString(), FirstCanvas);
        //hMonthCardGiftControllor = tranMonthCardGift.GetComponent<MonthCardGiftControllor>();
        //hMonthCardGiftControllor.Init();

        //yield return new WaitForEndOfFrame();
        //tranMonthCardGiftReward = CreateUIBox(UISceneModuleType.MonthCardGiftReward.ToString(), FirstCanvas);
        //hMonthCardGiftRewardControllor = tranMonthCardGiftReward.GetComponent<MonthCardGiftRewardControllor>();
        //hMonthCardGiftRewardControllor.Init();

        //SecondCanvas
        //tranSetting = CreateUIBox(UISceneModuleType.Setting.ToString(), SecondCanvas);
        //hSettingControllor = tranSetting.GetComponent<SettingControllor>();
        hSettingControllor.Init();
        //tranAotemanZaohuan = CreateUIBox(UISceneModuleType.AotemanZhaohuan.ToString(), SecondCanvas);
        //hAotemanZaohuanControllor = tranAotemanZaohuan.GetComponent<AotemanZhaohuanControllor>();
        hAotemanZaohuanControllor.Init();
        //tranExitGame = CreateUIBox(UISceneModuleType.ExitGame.ToString(), SecondCanvas);
        //hExitGameControllor = tranExitGame.GetComponent<ExitGameControllor>();
        hExitGameControllor.Init();
        //tranOneKeyToFullLevel = CreateUIBox(UISceneModuleType.OneKeyToFullLevel.ToString(), SecondCanvas);
        //hOneKeyToFullLevelControllor = tranOneKeyToFullLevel.GetComponent<OneKeyToFullLevelControllor>();
        //hOneKeyToFullLevelControllor.Init();
        //tranComplain = CreateUIBox(UISceneModuleType.Complain.ToString(), SecondCanvas);
        //hComplainControllor = tranComplain.GetComponent<ComplainControllor>();
        //hComplainControllor.Init();
        //tranTurnplate = CreateUIBox(UISceneModuleType.Turnplate.ToString(), SecondCanvas);
        //hTurnplateControllor = tranTurnplate.GetComponent<TurnplateControllor>();
        hTurnplateControllor.Init();
        //tranSmashEgg = CreateUIBox(UISceneModuleType.SmashEgg.ToString(), SecondCanvas);
        //hSmashEggControllor = tranSmashEgg.GetComponent<SmashEggControllor>();
        hSmashEggControllor.Init();
        //tranConvertCenter = CreateUIBox(UISceneModuleType.ConvertCenter.ToString(), SecondCanvas);
        //hConvertCenterControllor = tranConvertCenter.GetComponent<ConvertCenterControllor>();
        hConvertCenterControllor.Init();
        //tranExchangeActivity = CreateUIBox(UISceneModuleType.ExchangeActivity.ToString(), SecondCanvas);
        //hExchangeActivityControllor = tranExchangeActivity.GetComponent<ExchangeActivityControllor>();
        //hExchangeActivityControllor.Init();
        //tranLuckyNumbersControllor = CreateUIBox(UISceneModuleType.LuckyNumbers.ToString(), SecondCanvas);
        //hLuckyNumbersControllor = tranLuckyNumbersControllor.GetComponent<LuckyNumbersController>();
        //hLuckyNumbersControllor.Init();
        //tranGamPlayingActivity = CreateUIBox(UISceneModuleType.GamePlayingActivity.ToString(), SecondCanvas);
        //hGamePlayingActivity = tranGamPlayingActivity.GetComponent<GamePlayingActivity>();
        //hGamePlayingActivity.Init();
        //tranClearanceRedPaper = CreateUIBox(UISceneModuleType.ClearanceRedPaper.ToString(), SecondCanvas);
        //hClearanceRedPaper = tranClearanceRedPaper.GetComponent<ClearanceRedPaper>();
        //hClearanceRedPaper.Init();
        //tranAchievement = CreateUIBox(UISceneModuleType.Achievement.ToString(), SecondCanvas);
        //hAchievementControllor = tranAchievement.GetComponent<AchievementControllor>();
        hAchievementControllor.Init();
        //tranAotemanFamily = CreateUIBox(UISceneModuleType.AotemanFamily.ToString(), SecondCanvas);
        //hAotemanFamilyControllor = tranAotemanFamily.GetComponent<AotemanFamilyControllor>();
        //hAotemanFamilyControllor.Init();
        //tranDailyMission = CreateUIBox(UISceneModuleType.DailyMission.ToString(), SecondCanvas);
        //hDailyMissionControllor = tranDailyMission.GetComponent<DailyMissionControllor>();
        hDailyMissionControllor.Init();
        //tranShopSecondSure = CreateUIBox(UISceneModuleType.ShopSecondSure.ToString(), SecondCanvas);
        //hShopSecondSureControllor = tranShopSecondSure.GetComponent<ShopSecondSureControllor>();
        //hShopSecondSureControllor.Init();
        //tranExchangeCode = CreateUIBox(UISceneModuleType.ExchangeCode.ToString(), SecondCanvas);
        //hExchangeCodeControllor = tranExchangeCode.GetComponent<ExchangeCodeControllor>();
        //hExchangeCodeControllor.Init();
        //tranLockTip = CreateUIBox(UISceneModuleType.LockTip.ToString(), SecondCanvas);
        //hLockTipControllor = tranLockTip.GetComponent<LockTipControllor>();
        hLockTipControllor.Init();

        //ThirdCanvas
        //yield return new WaitForEndOfFrame();
        //tranHuaFei = CreateUIBox(UISceneModuleType.HuaFeiDisplay.ToString(), ThirdCanvas);
        //hHuaFeiManager = tranHuaFei.GetComponent<HuaFeiManager>();
        //hHuaFeiManager.Init();
        //yield return new WaitForEndOfFrame();
        //tranHistoricRecord = CreateUIBox(UISceneModuleType.HistoricHuaFeiDisplay.ToString(), ThirdCanvas);
        //hHistoricRecordManager = tranHistoricRecord.GetComponent<HistoricRecordManager>();
        //hHistoricRecordManager.Init();
        //tranPhoneNumberInput = CreateUIBox(UISceneModuleType.PhoneNumberInput.ToString(), ThirdCanvas);
        //hPhoneNumberInputControllor = tranPhoneNumberInput.GetComponent<PhoneNumberInputControllor>();
        //hPhoneNumberInputControllor.Init();
        //yield return new WaitForEndOfFrame();
        //tranGiftPackage = CreateUIBox(UISceneModuleType.GiftPackage.ToString(), ThirdCanvas);
        //hGiftPackageControllor = tranGiftPackage.GetComponent<GiftPackageControllor>();
        //hGiftPackageControllor.Init();
        //yield return new WaitForEndOfFrame();
        //tranSignInReward = CreateUIBox(UISceneModuleType.SignInReward.ToString(), ThirdCanvas);
        //hSignInRewardControllor = tranSignInReward.GetComponent<SignInRewardControllor>();
        hSignInRewardControllor.Init();
        //yield return new WaitForEndOfFrame();
        //tranExchangeCodeReward = CreateUIBox(UISceneModuleType.ExchangeCodeReward.ToString(), ThirdCanvas);
        //hExchangeCodeRewardControllor = tranExchangeCodeReward.GetComponent<ExchangeCodeRewardControllor>();
        ////hExchangeCodeRewardControllor.Init();
        //yield return new WaitForEndOfFrame();
        //tranTurnplateAward = CreateUIBox(UISceneModuleType.TurnplateAward.ToString(), ThirdCanvas);
        //hTurnplateAwardControllor = tranTurnplateAward.GetComponent<TurnplateAwardControllor>();
        hTurnplateAwardControllor.Init();
        //tranAwardItemBox = CreateUIBox(UISceneModuleType.AwardItemBox.ToString(), ThirdCanvas);
        //hAwardItemBoxControllor = tranAwardItemBox.GetComponent<AwardItemBoxControllor>();
        hAwardItemBoxControllor.Init();
        //tranDailyMissionReward = CreateUIBox(UISceneModuleType.DailyMissionReward.ToString(), ThirdCanvas);
        //hDailyMissionRewardControllor = tranDailyMissionReward.GetComponent<DailyMissionRewardControllor>();
        hDailyMissionRewardControllor.Init();
        //tranGamePassedGfit = CreateUIBox(UISceneModuleType.GamePassedGfit.ToString(), ThirdCanvas);
        //hGamePassedGfitControllor = tranGamePassedGfit.GetComponent<GamePassedGfitControllor>();
        //hGamePassedGfitControllor.Init();
        
        //tranCommonGift = CreateUIBox(UISceneModuleType.CommonGift.ToString(), ThirdCanvas);
        //hCommonGift = tranCommonGift.GetComponent<CommonGift>();
        //hCommonGift.Init();
        //tranCommonGiftAward = CreateUIBox(UISceneModuleType.CommonGiftAward.ToString(), ThirdCanvas);
        //hCommonGiftAward = tranCommonGiftAward.GetComponent<CommonGiftAward>();
        //hCommonGiftAward.Init();
    }

    Transform CreateUIBox(string prefabName, Transform parentTran)
    {
	    Transform moduleTran = spUIModules.Spawn(prefabName);
        if (parentTran.localScale == Vector3.zero) parentTran.localScale = Vector3.one * 0.1f;
	    
        moduleTran.SetParent(parentTran, false);
        //moduleTran.localPosition = GlobalConst.TopHidePos;
        moduleTran.gameObject.SetActive(false);
        return moduleTran;
    }

    public void ShowModule(UISceneModuleType boxType)
    {
        switch (boxType)
        {
            case UISceneModuleType.MainInterface:
                hMainInterfaceControllor.preBoxType = curBoxType;
                break;
            //case UISceneModuleType.GiftPackage:
            //    hGiftPackageControllor.preBoxType = curBoxType;
            //    break;
            case UISceneModuleType.PropertyDisplay:
                break;
            case UISceneModuleType.Setting:
                hSettingControllor.preBoxType = curBoxType;
                break;
            case UISceneModuleType.AotemanZhaohuan:
                hAotemanZaohuanControllor.preBoxType = curBoxType;
                break;
            case UISceneModuleType.ExitGame:
                hExitGameControllor.preBoxType = curBoxType;
                break;
            //case UISceneModuleType.Shop:
            //    hShopControllor.preBoxType = curBoxType;
            //    break;
            //case UISceneModuleType.ShopSecondSure:
            //    hShopSecondSureControllor.preBoxType = curBoxType;
            //    break;
            case UISceneModuleType.CharacterDetail:
                hCharacterDetailControllor.preBoxType = curBoxType;
                break;
            case UISceneModuleType.LevelSelect:
                hLevelSelectControllor.preBoxType = curBoxType;
                break;
            //case UISceneModuleType.OneKeyToFullLevel:
            //    hOneKeyToFullLevelControllor.preBoxType = curBoxType;
            //    break;
            //case UISceneModuleType.Complain:
            //    hComplainControllor.preBoxType = curBoxType;
            //    break;
            case UISceneModuleType.LevelInfo:
                hLevelInfoControllor.preBoxType = curBoxType;
                break;
            //case UISceneModuleType.DoubleCoin:
            //    hDoubleCoinControllor.preBoxType = curBoxType;
            //    break;
            case UISceneModuleType.Turnplate:
                hTurnplateControllor.preBoxType = curBoxType;
                break;
            case UISceneModuleType.SmashEgg:
                hSmashEggControllor.preBoxType = curBoxType;
                break;
            case UISceneModuleType.ConvertCenter:
                hConvertCenterControllor.preBoxType = curBoxType;
                break;
            //case UISceneModuleType.ExchangeActivity:
            //    hExchangeActivityControllor.preBoxType = curBoxType;
            //    break;
            //case UISceneModuleType.LuckyNumbers:
            //    hLuckyNumbersControllor.preBoxType = curBoxType;
            //    break;
            //case UISceneModuleType.GamePlayingActivity:
            //    hGamePlayingActivity.preBoxType = curBoxType;
            //    break;
            //case UISceneModuleType.ClearanceRedPaper:
            //    hClearanceRedPaper.preBoxType = curBoxType;
            //    break;
            case UISceneModuleType.SignIn:
                hSignInControllor.preBoxType = curBoxType;
                break;           
            case UISceneModuleType.SignInReward:
                hSignInRewardControllor.preBoxType = curBoxType;
                break;
            case UISceneModuleType.Achievement:
                hAchievementControllor.preBoxType = curBoxType;
                break;
            //case UISceneModuleType.AotemanFamily:
            //    hAotemanFamilyControllor.preBoxType = curBoxType;
            //    break;
            case UISceneModuleType.Strength:
                hStrengthControllor.preBoxType = curBoxType;
                break;
            //case UISceneModuleType.Activity:
            //    hActivityControllor.preBoxType = curBoxType;
            //    break;
            case UISceneModuleType.DailyMission:
                hDailyMissionControllor.preBoxType = curBoxType;
                break;
            //case UISceneModuleType.ExchangeCode:
            //    hExchangeCodeControllor.preBoxType = curBoxType;
            //    break;
            //case UISceneModuleType.ExchangeCodeReward:
            //    hExchangeCodeRewardControllor.preBoxType = curBoxType;
            //    break;
            case UISceneModuleType.TurnplateAward:
                hTurnplateAwardControllor.preBoxType = curBoxType;
                break;
            case UISceneModuleType.AwardItemBox:
                hAwardItemBoxControllor.preBoxType = curBoxType;
                break;
            //case UISceneModuleType.HuaFeiDisplay:
            //    hHuaFeiManager.preBoxType = curBoxType;
            //    break;
            //case UISceneModuleType.HistoricHuaFeiDisplay:
            //    hHistoricRecordManager.preBoxType = curBoxType;
            //    break;
            //case UISceneModuleType.PhoneNumberInput:
            //    hPhoneNumberInputControllor.preBoxType = curBoxType;
            //    break;
            case UISceneModuleType.LockTip:
                hLockTipControllor.preBoxType = curBoxType;
                break;
            case UISceneModuleType.UIGuide:
                hUIGuideControllor.preBoxType = curBoxType;
                break;
            case UISceneModuleType.DailyMissionReward:
                hDailyMissionRewardControllor.preBoxType = curBoxType;
                break;
            //case UISceneModuleType.GamePassedGfit:
            //    hGamePassedGfitControllor.preBoxType = curBoxType;
            //    break;
            //case UISceneModuleType.NewPlayerGift:
            //    hNewPlayerGfitControllor.preBoxType = curBoxType;
            //    break;
            //case UISceneModuleType.ThreeStar:
            //    hThreeStarControllor.preBoxType = curBoxType;
            //    break;
            //case UISceneModuleType.FourStar:
            //    hFourStarControllor.preBoxType = curBoxType;
            //    break;
            //case UISceneModuleType.PassGift:
            //    hPassGiftControllor.preBoxType = curBoxType;
            //    break;
            //case UISceneModuleType.DiscountGift:
            //    hDiscountGiftControllor.preBoxType = curBoxType;
            //    break;
            //case UISceneModuleType.RemoveAds:
            //    hRemoveAdsControllor.preBoxType = curBoxType;
            //    break;
            //case UISceneModuleType.MonthCardGift:
            //    hMonthCardGiftControllor.preBoxType = curBoxType;
            //    break;
            //case UISceneModuleType.MonthCardGiftReward:
            //    hMonthCardGiftRewardControllor.preBoxType = curBoxType;
            //    break;
            //case UISceneModuleType.CommonGift:
            //    hCommonGift.preBoxType = curBoxType;
            //    break;
            //case UISceneModuleType.CommonGiftAward:
            //    hCommonGiftAward.preBoxType = curBoxType;
            //    break;
            
        }

        if (boxType != UISceneModuleType.PropertyDisplay)
        {
            curBoxType = boxType;
            EventLayerController.Instance.SetEventLayer(curBoxType);
            TranslucentUIMaskManager.Instance.Show(curBoxType);
        }

        switch (boxType)
        {
            case UISceneModuleType.MainInterface:
                hMainInterfaceControllor.Show();
                break;
            //case UISceneModuleType.GiftPackage:
            //    hGiftPackageControllor.Show();
            //    break;
            case UISceneModuleType.PropertyDisplay:
                hPropertyDisplayControllor.Show();
                break;
            case UISceneModuleType.Setting:
                hSettingControllor.Show();
                break;
            case UISceneModuleType.AotemanZhaohuan:
                hAotemanZaohuanControllor.Show();
                break;
            case UISceneModuleType.ExitGame:
                hExitGameControllor.Show();
                break;
            //case UISceneModuleType.Shop:
            //    hShopControllor.Show();
            //    break;
            //case UISceneModuleType.ShopSecondSure:
            //    hShopSecondSureControllor.Show();
            //    break;
            case UISceneModuleType.CharacterDetail:
                hCharacterDetailControllor.Show();
                break;
            case UISceneModuleType.LevelSelect:
                hLevelSelectControllor.Show();
                break;
            //case UISceneModuleType.OneKeyToFullLevel:
            //    hOneKeyToFullLevelControllor.Show();
            //    break;
            //case UISceneModuleType.Complain:
            //    hComplainControllor.Show();
            //    break;
            case UISceneModuleType.LevelInfo:
                hLevelInfoControllor.Show();
                break;
            //case UISceneModuleType.DoubleCoin:
            //    hDoubleCoinControllor.Show();
            //    break;
            case UISceneModuleType.Turnplate:
                hTurnplateControllor.Show();
                break;
            case UISceneModuleType.SmashEgg:
                hSmashEggControllor.Show();
                break;
            case UISceneModuleType.ConvertCenter:
                hConvertCenterControllor.Show();
                break;
            //case UISceneModuleType.ExchangeActivity:
            //    hExchangeActivityControllor.Show();
            //    break;
            //case UISceneModuleType.LuckyNumbers:
            //    hLuckyNumbersControllor.Show();
            //    break;
            //case UISceneModuleType.GamePlayingActivity:
            //    hGamePlayingActivity.Show();
            //    break;
            //case UISceneModuleType.ClearanceRedPaper:
            //    hClearanceRedPaper.Show();
            //    break;
            case UISceneModuleType.SignIn:
                hSignInControllor.Show();
                break;           
            case UISceneModuleType.SignInReward:
                hSignInRewardControllor.Show();
                break;
            case UISceneModuleType.Achievement:
                hAchievementControllor.Show();
                break;
            //case UISceneModuleType.AotemanFamily:
            //    hAotemanFamilyControllor.Show();
            //    break;
            case UISceneModuleType.Strength:
                hStrengthControllor.Show();
                break;
            //case UISceneModuleType.Activity:
            //    hActivityControllor.Show();
            //    break;
            case UISceneModuleType.DailyMission:
                hDailyMissionControllor.Show();
                break;
            //case UISceneModuleType.ExchangeCode:
            //    hExchangeCodeControllor.Show();
            //    break;
            //case UISceneModuleType.ExchangeCodeReward:
            //    hExchangeCodeRewardControllor.Show();
            //    break;
            case UISceneModuleType.TurnplateAward:
                hTurnplateAwardControllor.Show();
                break;
            case UISceneModuleType.AwardItemBox:
                hAwardItemBoxControllor.Show();
                break;
            //case UISceneModuleType.HuaFeiDisplay:
            //    hHuaFeiManager.Show();
            //    break;
            //case UISceneModuleType.HistoricHuaFeiDisplay:
            //    hHistoricRecordManager.Show();
            //    break;
            //case UISceneModuleType.PhoneNumberInput:
            //    hPhoneNumberInputControllor.Show();
            //    break;
            case UISceneModuleType.LockTip:
                hLockTipControllor.Show();
                break;
            case UISceneModuleType.UIGuide:
                hUIGuideControllor.Show();
                break;
            case UISceneModuleType.DailyMissionReward:
                hDailyMissionRewardControllor.Show();
                break;
            //case UISceneModuleType.GamePassedGfit:
            //    hGamePassedGfitControllor.Show();
            //    break;
            //case UISceneModuleType.NewPlayerGift:
            //    hNewPlayerGfitControllor.Show();
            //    break;
            //case UISceneModuleType.ThreeStar:
            //    hThreeStarControllor.Show();
            //    break;
            //case UISceneModuleType.FourStar:
            //    hFourStarControllor.Show();
            //    break;
            //case UISceneModuleType.PassGift:
            //    hPassGiftControllor.Show();
            //    break;
            //case UISceneModuleType.DiscountGift:
            //    hDiscountGiftControllor.Show();
            //    break;
            //case UISceneModuleType.RemoveAds:
            //    hRemoveAdsControllor.Show();
            //    break;
            //case UISceneModuleType.MonthCardGift:
            //    hMonthCardGiftControllor.Show();
            //    break;
            //case UISceneModuleType.MonthCardGiftReward:
            //    hMonthCardGiftRewardControllor.Show();
            //    break;
            //case UISceneModuleType.CommonGift:
            //    hCommonGift.Show();
            //    break;
            //case UISceneModuleType.CommonGiftAward:
            //    hCommonGiftAward.Show();
            //    break;
        }
    }

    public void HideModule(UISceneModuleType boxType)
    {
        //print (boxType);
        switch (boxType)
        {
            case UISceneModuleType.MainInterface:
                break;
            //case UISceneModuleType.GiftPackage:
            //    curBoxType = hGiftPackageControllor.preBoxType;
            //    break;
            case UISceneModuleType.PropertyDisplay:
                //curBoxType = hCharacterDetailControllor.preBoxType;
                hPropertyDisplayControllor.Hide();
                break;
            case UISceneModuleType.Setting:
                curBoxType = hSettingControllor.preBoxType;
                break;
            case UISceneModuleType.AotemanZhaohuan:
                curBoxType = hAotemanZaohuanControllor.preBoxType;
                break;
            case UISceneModuleType.ExitGame:
                curBoxType = hExitGameControllor.preBoxType;
                break;
            //case UISceneModuleType.Shop:
            //    curBoxType = hShopControllor.preBoxType;
            //    break;
            //case UISceneModuleType.ShopSecondSure:
            //    curBoxType = hShopSecondSureControllor.preBoxType;
            //    break;
            case UISceneModuleType.CharacterDetail:
                curBoxType = hCharacterDetailControllor.preBoxType;
                break;
            case UISceneModuleType.LevelSelect:
                curBoxType = hLevelSelectControllor.preBoxType;
                break;
            //case UISceneModuleType.OneKeyToFullLevel:
            //    curBoxType = hOneKeyToFullLevelControllor.preBoxType;
            //    break;
            //case UISceneModuleType.Complain:
            //    curBoxType = hComplainControllor.preBoxType;
            //    break;
            case UISceneModuleType.Turnplate:
                curBoxType = hTurnplateControllor.preBoxType;
                break;
            case UISceneModuleType.SmashEgg:
                curBoxType = hSmashEggControllor.preBoxType;
                break;
            //case UISceneModuleType.DoubleCoin:
            //    curBoxType = hDoubleCoinControllor.preBoxType;
            //    break;
            case UISceneModuleType.LevelInfo:
                curBoxType = hLevelInfoControllor.preBoxType;
                break;
            case UISceneModuleType.ConvertCenter:
                curBoxType = hConvertCenterControllor.preBoxType;
                break;
            //case UISceneModuleType.ExchangeActivity:
            //    curBoxType = hExchangeActivityControllor.preBoxType;
            //    break;
            //case UISceneModuleType.LuckyNumbers:
            //    curBoxType = hLuckyNumbersControllor.preBoxType;
            //    break;
            //case UISceneModuleType.GamePlayingActivity:
            //    curBoxType = hGamePlayingActivity.preBoxType;
            //    break;
            //case UISceneModuleType.ClearanceRedPaper:
            //    curBoxType = hClearanceRedPaper.preBoxType;
            //    break;
            case UISceneModuleType.SignIn:
                curBoxType = hSignInControllor.preBoxType;
                break;           
            case UISceneModuleType.SignInReward:
                curBoxType = hSignInRewardControllor.preBoxType;
                break;
            case UISceneModuleType.Achievement:
                curBoxType = hAchievementControllor.preBoxType;
                break;
            //case UISceneModuleType.AotemanFamily:
            //    curBoxType = hAotemanFamilyControllor.preBoxType;
            //    break;
            case UISceneModuleType.Strength:
                curBoxType = hStrengthControllor.preBoxType;
                break;
            //case UISceneModuleType.Activity:
            //    curBoxType = hActivityControllor.preBoxType;
            //    break;
            case UISceneModuleType.DailyMission:
                curBoxType = hDailyMissionControllor.preBoxType;
                break;
            //case UISceneModuleType.ExchangeCode:
            //    curBoxType = hExchangeCodeControllor.preBoxType;
            //    break;
            //case UISceneModuleType.ExchangeCodeReward:
            //    curBoxType = hExchangeCodeRewardControllor.preBoxType;
            //    break;
            case UISceneModuleType.TurnplateAward:
                curBoxType = hTurnplateAwardControllor.preBoxType;
                break;
            case UISceneModuleType.AwardItemBox:
                curBoxType = hAwardItemBoxControllor.preBoxType;
                break;
            //case UISceneModuleType.HuaFeiDisplay:
            //    curBoxType = hHuaFeiManager.preBoxType;
            //    break;
            //case UISceneModuleType.HistoricHuaFeiDisplay:
            //    curBoxType = hHistoricRecordManager.preBoxType;
            //    break;
            //case UISceneModuleType.PhoneNumberInput:
            //    curBoxType = hPhoneNumberInputControllor.preBoxType;
            //    break;
            case UISceneModuleType.LockTip:
                curBoxType = hLockTipControllor.preBoxType;
                break;
            case UISceneModuleType.UIGuide:
                curBoxType = hUIGuideControllor.preBoxType;
                break;
            case UISceneModuleType.DailyMissionReward:
                curBoxType = hDailyMissionRewardControllor.preBoxType;
                break;
            //case UISceneModuleType.GamePassedGfit:
            //    curBoxType = hGamePassedGfitControllor.preBoxType;
            //    break;
            //case UISceneModuleType.NewPlayerGift:
            //    curBoxType = hNewPlayerGfitControllor.preBoxType;
            //    break;
            //case UISceneModuleType.ThreeStar:
            //    curBoxType = hThreeStarControllor.preBoxType;
            //    break;
            //case UISceneModuleType.FourStar:
            //    curBoxType = hFourStarControllor.preBoxType;
            //    break;
            //case UISceneModuleType.PassGift:
            //    curBoxType = hPassGiftControllor.preBoxType;
            //    break;
            //case UISceneModuleType.DiscountGift:
            //    curBoxType = hDiscountGiftControllor.preBoxType;
            //    break;
            //case UISceneModuleType.RemoveAds:
            //    curBoxType = hRemoveAdsControllor.preBoxType;
            //    break;
            //case UISceneModuleType.MonthCardGift:
            //    curBoxType = hMonthCardGiftControllor.preBoxType;
            //    break;
            //case UISceneModuleType.MonthCardGiftReward:
            //    curBoxType = hMonthCardGiftRewardControllor.preBoxType;
            //    break;
            //case UISceneModuleType.CommonGift:
            //    curBoxType = hCommonGift.preBoxType;
            //    break;
            //case UISceneModuleType.CommonGiftAward:
            //    curBoxType = hCommonGiftAward.preBoxType;
            //    break;
        }

        EventLayerController.Instance.SetEventLayer(curBoxType);
        TranslucentUIMaskManager.Instance.Show(curBoxType);
    }

    /// <summary>
    /// Android手机返回键的点击事件.
    /// </summary>
    private void AndroidBackOnClick()
    {
        switch (curBoxType)
        {
            case UISceneModuleType.MainInterface:
                hMainInterfaceControllor.Back();
                break;
            //case UISceneModuleType.GiftPackage:
            //    hGiftPackageControllor.Back();
            //    break;
            case UISceneModuleType.Setting:
                hSettingControllor.Back();
                break;
            //case UISceneModuleType.Shop:
            //    hShopControllor.Back();
            //    break;
            //case UISceneModuleType.ShopSecondSure:
            //    hShopSecondSureControllor.Back();
            //    break;
            case UISceneModuleType.ExitGame:
                hExitGameControllor.Back();
                break;
            case UISceneModuleType.CharacterDetail:
                hCharacterDetailControllor.Back();
                break;
            case UISceneModuleType.AotemanZhaohuan:
                hAotemanZaohuanControllor.Back();
                break;
            case UISceneModuleType.LevelSelect:
                hLevelSelectControllor.Back();
                break;
            //case UISceneModuleType.OneKeyToFullLevel:
            //    hOneKeyToFullLevelControllor.Back();
            //    break;
            //case UISceneModuleType.Complain:
            //    hComplainControllor.Back();
            //    break;
            case UISceneModuleType.LevelInfo:
                hLevelInfoControllor.Back();
                break;
            case UISceneModuleType.Turnplate:
                hTurnplateControllor.Back();
                break;
            case UISceneModuleType.SmashEgg:
                hSmashEggControllor.Back();
                break;
            case UISceneModuleType.ConvertCenter:
                hConvertCenterControllor.Back();
                break;
            //case UISceneModuleType.ExchangeActivity:
            //    hExchangeActivityControllor.Back();
            //    break;
            //case UISceneModuleType.LuckyNumbers:
            //    hLuckyNumbersControllor.Back();
            //    break;
            //case UISceneModuleType.GamePlayingActivity:
            //    hGamePlayingActivity.Back();
            //    break;
            //case UISceneModuleType.ClearanceRedPaper:
            //    hClearanceRedPaper.Back();
            //    break;
            case UISceneModuleType.SignIn:
                hSignInControllor.Back();
                break;            
            case UISceneModuleType.SignInReward:
                hSignInRewardControllor.Back();
                break;
            case UISceneModuleType.Achievement:
                hAchievementControllor.Back();
                break;
            //case UISceneModuleType.AotemanFamily:
            //    hAotemanFamilyControllor.Back();
            //    break;
            case UISceneModuleType.Strength:
                hStrengthControllor.Back();
                break;
            //case UISceneModuleType.Activity:
            //    hActivityControllor.Back();
            //    break;
            case UISceneModuleType.DailyMission:
                hDailyMissionControllor.Back();
                break;
            //case UISceneModuleType.ExchangeCode:
            //    hExchangeCodeControllor.Back();
            //    break;
            //case UISceneModuleType.ExchangeCodeReward:
            //    hExchangeCodeRewardControllor.Back();
            //    break;
            case UISceneModuleType.TurnplateAward:
                hTurnplateAwardControllor.Back();
                break;
            case UISceneModuleType.AwardItemBox:
                hAwardItemBoxControllor.Back();
                break;
            //case UISceneModuleType.HuaFeiDisplay:
            //    hHuaFeiManager.Back();
            //    break;
            //case UISceneModuleType.HistoricHuaFeiDisplay:
            //    hHistoricRecordManager.Back();
            //    break;
            //case UISceneModuleType.PhoneNumberInput:
            //    hPhoneNumberInputControllor.Back();
            //    break;
            //case UISceneModuleType.DoubleCoin:
            //    hDoubleCoinControllor.Back();
            //    break;
            case UISceneModuleType.LockTip:
                hLockTipControllor.Back();
                break;
            case UISceneModuleType.UIGuide:
                hUIGuideControllor.Back();
                break;
            case UISceneModuleType.DailyMissionReward:
                hDailyMissionRewardControllor.Back();
                break;
            //case UISceneModuleType.GamePassedGfit:
            //    hGamePassedGfitControllor.Back();
            //    break;
            //case UISceneModuleType.NewPlayerGift:
            //    hNewPlayerGfitControllor.Back();
            //    break;
            //case UISceneModuleType.ThreeStar:
            //    hThreeStarControllor.Back();
            //    break;
            //case UISceneModuleType.FourStar:
            //    hFourStarControllor.Back();
            //    break;
            //case UISceneModuleType.PassGift:
            //    hPassGiftControllor.Back();
            //    break;
            //case UISceneModuleType.DiscountGift:
            //    hDiscountGiftControllor.Back();
            //    break;
            //case UISceneModuleType.RemoveAds:
            //    hRemoveAdsControllor.Back();
            //    break;
            //case UISceneModuleType.MonthCardGift:
            //    hMonthCardGiftControllor.Back();
            //    break;
            //case UISceneModuleType.MonthCardGiftReward:
            //    hMonthCardGiftRewardControllor.Back();
            //    break;
            //case UISceneModuleType.CommonGift:
            //    hCommonGift.Back();
            //    break;
            //case UISceneModuleType.CommonGiftAward:
            //    hCommonGiftAward.Back();
            //    break;
        }
    }

    void OnEnable()
    {
        PublicSceneObject.Instance.ClearAndroidBackKeyEvent();
        PublicSceneObject.Instance.AndroidBackKeyEvent += AndroidBackOnClick;
    }
    void OnDisable()
    {
        //if (PublicSceneObject.Instance)
        //{
        PublicSceneObject.Instance.ClearAndroidBackKeyEvent();
        PublicSceneObject.Instance.AndroidBackKeyEvent -= AndroidBackOnClick;
        //}
    }
}

public enum UISceneModuleType
{
    MainInterface,          //UICanvas
    LevelSelect,            //UICanvas

    PropertyDisplay,        //FirstCanvas
    Activity,               //FirstCanvas
    CharacterDetail,        //FirstCanvas
    Shop,                   //FirstCanvas
    Strength,               //FirstCanvas
    LevelInfo,              //FirstCanvas
    DoubleCoin,             //FirstCanvas

    Setting,                //SecondCanvas
    AotemanZhaohuan,        //SecondCanvas
    ExitGame,               //SecondCanvas
    LockTip,                //SecondCanvas
    OneKeyToFullLevel,      //SecondCanvas
    Complain,               //SecondCanvas
    Turnplate,              //SecondCanvas
    SmashEgg,               //SecondCanvas
    ConvertCenter,          //SecondCanvas
    ExchangeActivity,       //SecondCanvas
    SignIn,                 //SecondCanvas
    AotemanFamily,          //SecondCanvas
    ExchangeCode,           //SecondCanvas
    Achievement,            //SecondCanvas
    DayTask,                //SecondCanvas
    DailyMission,           //SecondCanvas
    ShopSecondSure,         //SecondCanvas
    PlayerLevel,            //SecondCanvas
    LuckyNumbers,           //SecondCanvas
    GamePlayingActivity,    //SecondCanvas
    ClearanceRedPaper,      //SecondCanvas

    HuaFeiDisplay,          //ThirdCanvas
    HistoricHuaFeiDisplay,  //ThirdCanvas
    PhoneNumberInput,       //ThirdCanvas
    GiftPackage,            //ThirdCanvas
    SignInReward,           //ThirdCanvas
    ExchangeCodeReward,     //ThirdCanvas
    TurnplateAward,         //ThirdCanvas
    DailyMissionReward,     //ThirdCanvas
    AwardItemBox,           //ThirdCanvas

    CommonGift,
    CommonGiftAward,

    NewPlayerGift,
    ThreeStar,
    FourStar,
    PassGift,
    PassReward,
    DiscountGift,
    LevelGift,
    ProtectShield,
    MonthCardGift,
    MonthCardGiftReward,

    #region 游戏场景内UI模块
    GamePlayerUI,
    GamePause,
    GameRank,
    GameReborn,
    GameEndingScore,
    GamePassedGfit,
    HintInGame,
    GameResume,
    GameSkill,
    #endregion

    UIGuide,

    RemoveAds,					// xQueen
}