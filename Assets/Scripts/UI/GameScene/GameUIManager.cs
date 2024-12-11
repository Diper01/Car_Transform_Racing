using UnityEngine;
using System.Collections;
using PathologicalGames;

public class GameUIManager : MonoBehaviour {

	public static GameUIManager Instance;

    //public Transform playerUI, translucentUIMask;
    //public TranslucentUIMaskManager translucentUIMaskManager;
    //public DialogBox dialogBox;
    //public GamePlayerUIControllor gamePlayerUIControllor;
    //public Transform FirstCanvas, SecondCanvas, ThirdCanvas;

	Transform tranUIMask, tranDialog;

	public UIBoxBase hGamePlayerUIControllor;           /*Transform tranGamePlayerUI;*/
	public UIBoxBase hGamePauseControllor;              /*Transform tranGamePause;*/
	public UIBoxBase hGameRebornControllor;             /*Transform tranGameReborn;*/
	//public UIBoxBase hGameGiftPackageControllor;        /*Transform tranGameGiftPackage;*/
	public UIBoxBase hGameEndingScoreControllor;        /*Transform tranGameEndingScore;*/
	//public UIBoxBase hGamePassedGfitControllor;         /*Transform tranGamePassedGfit;*/
	//public UIBoxBase hCharacterDetailControllor;        /*Transform tranCharacterDetail;*/
	//public UIBoxBase hPropertyDisplayControllor;        /*Transform tranPropertyDisplay;*/
	//public UIBoxBase hShopControllor;                    /*Transform tranShop;*/
    //public UIBoxBase hOneKeyToFullLevelControllor;      /*Transform tranOneKeyToFullLevel;*/
	public UIBoxBase hHintInGameControllor;             /*Transform tranHintInGame;*/
	//public UIBoxBase hShopSecondSureControllor;          /*Transform tranShopSecondSure;*/
    //public UIBoxBase hStrengthControllor;      	 	 /*Transform tranStrength;*/
	public UIBoxBase hGameResumeControllor;             /*Transform tranGameResume;*/
    public UIBoxBase hUIGuideControllor;     		 	 /*Transform tranUIGuide;*/
	public UIBoxBase hGameSkillControllor;              /*Transform tranGameSkill;*/
	//public UIBoxBase hLevelGiftControllor;           	 /*Transform tranLevelGift;*/
	//public UIBoxBase hProtectShieldControllor;          /*Transform tranProtectShield;*/
    public UIBoxBase hGameRankControllor;               //Transform tranGameRank;

	public UISceneModuleType curBoxType;
	SpawnPool spUIModules;

	void Awake ()
	{
		Instance = this;
        //ApplicationChrome.navigationBarState = ApplicationChrome.States.Hidden;
        //ApplicationChrome.statusBarState = ApplicationChrome.States.Hidden;
    }

    public void Init () {
		if (GlobalConst.FirstIn) {
			GlobalConst.FirstIn = false;

			GameObject PlatformSetting = (GameObject)Instantiate(Resources.Load("UIScene/PlatformSetting"));
			PlatformSetting.SetActive(true);

			GameObject LDCanvas = (GameObject)Instantiate(Resources.Load ("UIScene/LoadingPage"));
			GlobalConst.SceneName = SceneType.GameScene;
			LDCanvas.GetComponentInChildren<LoadingPage>().InitScene ();
		}
		//spUIModules = PoolManager.Pools["UIModulesPool"];
		UIBoxInit();
        
        //EventLayerController.Instance.Init ();
	}

	void UIBoxInit()
	{
        // "+" - можно сделать
        // "-" - Используются в других меню. Можно сделать собственые аналоги.
		curBoxType = UISceneModuleType.GamePlayerUI;

        //transform
        //translucentUIMaskManager.Init();//-

        ////UICanvas
        hGamePlayerUIControllor.Init();//+
        
        hGameSkillControllor.Init();//+

        //游戏UI准备标志
        GlobalConst.IsUIReady = true;

        ////DialogBox
        //dialogBox.Init ();//+
        
		hUIGuideControllor.Init ();//-
		hHintInGameControllor.Init();//+
		hGameResumeControllor.Init();//+

		//FirstCanvas
		//hPropertyDisplayControllor.Init();//-
		//hShopControllor.Init ();//-
		hGamePauseControllor.Init();//+
		hGameRankControllor.Init();//+
		hGameRebornControllor.Init();//+
		hGameEndingScoreControllor.Init();//+
		//hCharacterDetailControllor.Init();//-
		//hLevelGiftControllor.Init();//+
		//hProtectShieldControllor.Init();//+

		//SecondCanvas
		//hGamePassedGfitControllor.Init();//-
		//hOneKeyToFullLevelControllor.Init();//-
		//hShopSecondSureControllor.Init ();//-
		//hStrengthControllor.Init ();//-

		//ThirdCanvas
		//hGameGiftPackageControllor.Init();//+
	}

	public void ShowModule(UISceneModuleType boxType)
	{
        switch (boxType)
        {
            case UISceneModuleType.GamePlayerUI:
                hGamePlayerUIControllor.preBoxType = curBoxType;
                break;
            case UISceneModuleType.PropertyDisplay:
                break;
            case UISceneModuleType.GamePause:
                hGamePauseControllor.preBoxType = curBoxType;
                break;
            case UISceneModuleType.GameRank:
                hGameRankControllor.preBoxType = curBoxType;
                break;
            case UISceneModuleType.GameReborn:
                hGameRebornControllor.preBoxType = curBoxType;
                break;
            //case UISceneModuleType.GiftPackage:
            //    hGameGiftPackageControllor.preBoxType = curBoxType;
            //    break;
            case UISceneModuleType.GameEndingScore:
                hGameEndingScoreControllor.preBoxType = curBoxType;
                break;
            //case UISceneModuleType.CharacterDetail:
            //    hCharacterDetailControllor.preBoxType = curBoxType;
            //    break;
            //case UISceneModuleType.OneKeyToFullLevel:
            //    hOneKeyToFullLevelControllor.preBoxType = curBoxType;
            //    break;
            //case UISceneModuleType.GamePassedGfit:
            //    hGamePassedGfitControllor.preBoxType = curBoxType;
            //    break;
            case UISceneModuleType.HintInGame:
                hHintInGameControllor.preBoxType = curBoxType;
                break;
            case UISceneModuleType.GameResume:
                hGameResumeControllor.preBoxType = curBoxType;
                break;
            //case UISceneModuleType.Shop:
            //    hShopControllor.preBoxType = curBoxType;
            //    break;
            //case UISceneModuleType.Strength:
            //    hStrengthControllor.preBoxType = curBoxType;
            //    break;
            //case UISceneModuleType.ShopSecondSure:
            //    hShopSecondSureControllor.preBoxType = curBoxType;
            //    break;
            case UISceneModuleType.UIGuide:
                hUIGuideControllor.preBoxType = curBoxType;
                break;
            case UISceneModuleType.GameSkill:
                hGameSkillControllor.preBoxType = curBoxType;
                break;
            //case UISceneModuleType.LevelGift:
            //    hLevelGiftControllor.preBoxType = curBoxType;
            //    break;
            //case UISceneModuleType.ProtectShield:
            //    hProtectShieldControllor.preBoxType = curBoxType;
            //    break;
        }

        if (boxType != UISceneModuleType.PropertyDisplay)
        {
            curBoxType = boxType;
            //EventLayerController.Instance.SetEventLayer(curBoxType);
            //TranslucentUIMaskManager.Instance.Show(curBoxType);
        }

        switch (boxType)
        {
            case UISceneModuleType.GamePlayerUI:
                hGamePlayerUIControllor.Show();
                break;
            //case UISceneModuleType.PropertyDisplay:
            //    hPropertyDisplayControllor.Show();
            //    break;
            case UISceneModuleType.GamePause:
                hGamePauseControllor.Show();
                break;
            case UISceneModuleType.GameRank:
                hGameRankControllor.Show();
                break;
            case UISceneModuleType.GameReborn:
                hGameRebornControllor.Show();
                break;
            //case UISceneModuleType.GiftPackage:
            //    hGameGiftPackageControllor.Show();
            //    break;
            case UISceneModuleType.GameEndingScore:
                hGameEndingScoreControllor.Show();
                break;
            //case UISceneModuleType.CharacterDetail:
            //    hCharacterDetailControllor.Show();
            //    break;
            //case UISceneModuleType.OneKeyToFullLevel:
            //    hOneKeyToFullLevelControllor.Show();
            //    break;
            //case UISceneModuleType.GamePassedGfit:
            //    hGamePassedGfitControllor.Show();
            //    break;
            case UISceneModuleType.HintInGame:
                hHintInGameControllor.Show();
                break;
            case UISceneModuleType.GameResume:
                hGameResumeControllor.Show();
                break;
            //case UISceneModuleType.Shop:
            //    hShopControllor.Show();
            //    break;
            //case UISceneModuleType.Strength:
            //    hStrengthControllor.Show();
            //    break;
            //case UISceneModuleType.ShopSecondSure:
            //    hShopSecondSureControllor.Show();
            //    break;
            case UISceneModuleType.UIGuide:
                hUIGuideControllor.Show();
                break;
            case UISceneModuleType.GameSkill:
                hGameSkillControllor.Show();
                break;
            //case UISceneModuleType.LevelGift:
            //    hLevelGiftControllor.Show();
            //    break;
            //case UISceneModuleType.ProtectShield:
            //    hProtectShieldControllor.Show();
            //    break;
        }
    }

	public void HideModule(UISceneModuleType boxType)
	{
		switch(boxType)
		{
		case UISceneModuleType.GamePlayerUI:
			curBoxType = hGamePlayerUIControllor.preBoxType;
			break;
		//case UISceneModuleType.PropertyDisplay:
		//	curBoxType = hCharacterDetailControllor.preBoxType;
		//	hPropertyDisplayControllor.Hide();
		//	break;
		case UISceneModuleType.GamePause:
			curBoxType = hGamePauseControllor.preBoxType;
			break;
		case UISceneModuleType.GameRank:
			curBoxType = hGameRankControllor.preBoxType;
			break;
		case UISceneModuleType.GameReborn:
			curBoxType = hGameRebornControllor.preBoxType;
			break;
		//case UISceneModuleType.GiftPackage:
		//	curBoxType = hGameGiftPackageControllor.preBoxType;
		//	break;
		case UISceneModuleType.GameEndingScore:
			curBoxType = hGameEndingScoreControllor.preBoxType;
			break;
		//case UISceneModuleType.CharacterDetail:
		//	curBoxType = hCharacterDetailControllor.preBoxType;
		//	break;
		//case UISceneModuleType.OneKeyToFullLevel:
		//	curBoxType = hOneKeyToFullLevelControllor.preBoxType;
		//	break;
		//case UISceneModuleType.GamePassedGfit:
		//	curBoxType = hGamePassedGfitControllor.preBoxType;
		//	break;
		case UISceneModuleType.HintInGame:
			curBoxType = hHintInGameControllor.preBoxType;
			break;
		case UISceneModuleType.GameResume:
			curBoxType = hGameResumeControllor.preBoxType;
			break;
		//case UISceneModuleType.Shop:
		//	curBoxType = hShopControllor.preBoxType;
		//	break;
		//case UISceneModuleType.Strength:
		//	curBoxType = hStrengthControllor.preBoxType;
		//	break;
		//case UISceneModuleType.ShopSecondSure:
		//	curBoxType = hShopSecondSureControllor.preBoxType;
		//	break;
		case UISceneModuleType.UIGuide:
			curBoxType = hUIGuideControllor.preBoxType;
			break;
		case UISceneModuleType.GameSkill:
			curBoxType = hGameSkillControllor.preBoxType;
			break;
		//case UISceneModuleType.LevelGift:
		//	curBoxType = hLevelGiftControllor.preBoxType;
		//	break;
		//case UISceneModuleType.ProtectShield:
		//	curBoxType = hProtectShieldControllor.preBoxType;
		//	break;
		}
		//EventLayerController.Instance.SetEventLayer (curBoxType);
		//TranslucentUIMaskManager.Instance.Show (curBoxType);
	}
	
	/// <summary>
	/// Android手机返回键的点击事件.
	/// </summary>
	private void AndroidBackOnClick()
	{
		switch(curBoxType)
		{
		case UISceneModuleType.GamePlayerUI:
			hGamePlayerUIControllor.Back();
			break;
		case UISceneModuleType.GamePause:
			hGamePauseControllor.Back();
			break;
		case UISceneModuleType.GameRank:
			hGameRankControllor.Back();
			break;
		case UISceneModuleType.GameReborn:
			hGameRebornControllor.Back();
			break;
		//case UISceneModuleType.GiftPackage:
		//	hGameGiftPackageControllor.Back();
		//	break;
		case UISceneModuleType.GameEndingScore:
			hGameEndingScoreControllor.Back();
			break;
		//case UISceneModuleType.OneKeyToFullLevel:
		//	hOneKeyToFullLevelControllor.Back();
		//	break;
		//case UISceneModuleType.GamePassedGfit:
		//	hGamePassedGfitControllor.Back();
		//	break;
		case UISceneModuleType.HintInGame:
			hHintInGameControllor.Back();
			break;
		case UISceneModuleType.GameResume:
			hGameResumeControllor.Back();
			break;
		//case UISceneModuleType.Shop:
		//	hShopControllor.Back();
		//	break;
		//case UISceneModuleType.Strength:
		//	hStrengthControllor.Back();
		//	break;
		//case UISceneModuleType.ShopSecondSure:
		//	hShopSecondSureControllor.Back();
		//	break;
		//case UISceneModuleType.CharacterDetail:
		//	hCharacterDetailControllor.Back();
		//	break;
		case UISceneModuleType.UIGuide:
			hUIGuideControllor.Back();
			break;
		case UISceneModuleType.GameSkill:
			hGameSkillControllor.Back();
			break;
		//case UISceneModuleType.LevelGift:
		//	hLevelGiftControllor.Back();
		//	break;
		//case UISceneModuleType.ProtectShield:
		//	hProtectShieldControllor.Back();
		//	break;
		}
	}

	void OnEnable()
	{
        SceneButtonsController.Instance.ClearAndroidBackKeyEvent();
        SceneButtonsController.Instance.AndroidBackKeyEvent += AndroidBackOnClick;
    }
}
