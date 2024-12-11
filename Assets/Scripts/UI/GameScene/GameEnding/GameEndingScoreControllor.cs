using UnityEngine;
using System.Collections;
using DG.Tweening;
using PathologicalGames;
using UnityEngine.UI;
using System;

public class GameEndingScoreControllor : UIBoxBase {

	public static GameEndingScoreControllor Instance;

	public GameObject  goLevelModeRestartBtn, goNextLevelBtn, goClassicModeCloseBtn, goClassicModeRestartBtn;
	public GameObject goLevelMode, goClassicMode;

	#region  闯关模式变量
	public Text textNextLevelBtn;
	public Transform[] transArrStarsFront;
	public Transform[] tranArrProps;
	public Image[] imageArrPropsIcon;
    public Sprite[] propsIcons;

    public Text[] textPropsCount;
    //public Transform LeftContent, RightContent, tranCharacterParent;
    public Transform tranCharacterParent;
    public Text textLevelScoreCount, textLevelCoinCount;
	//public ParticleSystem WinPS;
	public Image titleSprite;
    public Sprite text_crosslevel;
    public Sprite again_word;


    public GameObject[] stars;

	private int iOldChallengeLevel, iCurLevel, iMaxLevel = 40;
	private int iStarCount, iOldStarCount;

	//public GameObject goLevelColorEgg;
	//public Text textLevelColorEggCount;

	private bool showLockTip;
	#endregion

	#region  经典模式变量
	public Text textClassicJewelCount, textClassicCoinCount;
	public Text textClassDistance;

	//public GameObject goClassColorEgg;
	//public Text textClassicColorEggCount;

	#endregion

	#region 重写父类方法
	public override void Init ()
	{
		Instance = this;
		base.Init();
	}
	
	public override void Show ()
	{
		base.Show ();
		//transform.localPosition = ShowPosV2;


		if (PlayerData.Instance.IsWuJinGameMode ()) {
			goLevelMode.SetActive (false);
			goClassicMode.SetActive (true);
			InitClassicMode ();
		} else {
			goLevelMode.SetActive (true);
			goClassicMode.SetActive (false);
			InitLevelMode ();
		}
	}
	
	public override void Hide ()
	{

		if(PlayerData.Instance.GetGameMode().CompareTo("Level") == 0)
		{
			modelPool.Despawn(model);
		}

		transform.localPosition = GlobalConst.LeftHidePos;
		GameUIManager.Instance.HideModule(UISceneModuleType.GameEndingScore);
	}
	
	public override void Back ()
	{
		AudioManger.Instance.PlaySound(AudioManger.SoundName.CloseBtClick);
		GlobalConst.ShowModuleType = UISceneModuleType.MainInterface;
		GlobalConst.SceneName = SceneType.UIScene;
		LoadingPage.Instance.LoadScene ();
	}
	#endregion

	#region 闯关模式处理
	SpawnPool modelPool;
	Transform model;
    //void ShowLevelModeAni()//анимация вылезания менюшки наградами.
    //{
    //    LeftContent.localPosition = GlobalConst.LeftHidePos;
    //    RightContent.localPosition = GlobalConst.RightHidePos;
    //    DOTween.Kill(LeftContent);
    //    DOTween.Kill(RightContent);
    //    LeftContent.DOLocalMove(new Vector3(0, 0, 0), GlobalConst.PlayTime).SetEase(Ease.OutBack);
    //    RightContent.DOLocalMove(new Vector3(0, 0, 0), GlobalConst.PlayTime).SetEase(Ease.OutBack).OnComplete(ShowStartEffect);
    //}

    //void ShowLevelMode()
    //{
    //	LeftContent.localPosition = new Vector3 (0, 0, 0);
    //	RightContent.localPosition = new Vector3 (0, 0, 0);
    //}

    void InitLevelMode()
	{
		iCurLevel = PlayerData.Instance.GetSelectedLevel();
		iOldChallengeLevel = PlayerData.Instance.GetCurrentChallengeLevel();

		float fCoinAddition = GameData.Instance.AddCoinPecent - 1;
		int fCoinCount = Mathf.RoundToInt(GameData.Instance.curCoin * GameData.Instance.AddCoinPecent);

		iStarCount = GameData.Instance.StarCount;
		iOldStarCount = PlayerData.Instance.GetLevelStarState(iCurLevel);

		if(GameData.Instance.IsWin)
		{
			//Add expertience
			PlayerData.Instance.AddItemNum(PlayerData.ItemType.ColorEgg,GameData.Instance.curEggCount);

            //if(GameData.Instance.curEggCount > 0)
            //{
            //	goLevelColorEgg.SetActive(false);
            //	textLevelColorEggCount.text ="x"+GameData.Instance.curEggCount.ToString();
            //}else{
            //	goLevelColorEgg.SetActive(false);
            //}

            //ShowLevelModeAni();
            ShowStartEffect();
            SetButtonEnable(false);

            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].SetActive(true);
            }

			textNextLevelBtn.text = Localisation.GetString("Next level");
			titleSprite.sprite = text_crosslevel;
			//WinPS.gameObject.SetActive(true);
			AudioManger.Instance.PlaySound(AudioManger.SoundName.Win);
			PlayerData.Instance.SetCurrentChallengeLevel((iCurLevel == iOldChallengeLevel && iCurLevel < iMaxLevel)? iCurLevel + 1 : iOldChallengeLevel);

			if (iCurLevel == iOldChallengeLevel
			   && iCurLevel == GlobalConst.UnlockWujinLevel - 1
			   && PlatformSetting.Instance.PayVersionType != PayVersionItemType.ShenHe
			   && PlatformSetting.Instance.PayVersionType != PayVersionItemType.ChangWan
			   && PlatformSetting.Instance.PayVersionType != PayVersionItemType.GuangDian)
				showLockTip = true;
			else
				showLockTip = false;

			if(GameData.Instance.completeMissionIds.Count>0)
			{
			    PlayerData.Instance.SetLevelMissionState(iCurLevel, ConvertTool.StringToAnyTypeArray<int>(ConvertTool.AnyTypeListToString(GameData.Instance.completeMissionIds, "*"), '*'));
			}
			PlayerData.Instance.AddItemNum(PlayerData.ItemType.Coin, fCoinCount);

			if(PlayerData.Instance.GetNewPlayerState())
				PlayerData.Instance.SetNewPlayerToFalse();


			if(CarManager.Instance.gameLevelModel == GameLevelModel.LimitTime)
			{
				textLevelScoreCount.text = ((int)(CarManager.Instance.playerUseTime)).ToString();
			}else
			{
				textLevelScoreCount.text = Localisation.GetString("Rank") +" "+ CarManager.Instance.finalRank;
			}

			textLevelCoinCount.text  = fCoinCount.ToString();


			SetProps();
            //GamePassedGfitControllor.Instance.SaveData();
            SaveStarsData();
            //自定义事件.
            PlayerData.Instance.SetEachLevelFailCount(0);

			DailyMissionCheckManager.Instance.Check(DailyMissionType.LevelModel,1);

			CheckAchievement();

		}else
		{
            //goLevelColorEgg.SetActive(false);

            //ShowLevelMode();
            
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].SetActive(false);
            }

            textNextLevelBtn.text = Localisation.GetString("Main Menu");
			titleSprite.sprite = again_word;
			//WinPS.gameObject.SetActive(false);
			AudioManger.Instance.PlaySound(AudioManger.SoundName.Lose);
			if(CarManager.Instance.gameLevelModel == GameLevelModel.Weedout)
			{
				textLevelScoreCount.text = ((int)(CarManager.Instance.playerUseTime))+"(s)";
			}else
			{
				textLevelScoreCount.text = Localisation.GetString("Rank") + " " + CarManager.Instance.finalRank;
			}
			textLevelCoinCount.text  = "0";


			for(int i = 0; i < tranArrProps.Length; i ++)
			{
				tranArrProps[i].gameObject.SetActive(false);
			}

			for(int i = 0; i < transArrStarsFront.Length; i ++)
			{
				transArrStarsFront[i].gameObject.SetActive(false);
			}

			//设置关卡失败次数
			PlayerData.Instance.SetEachLevelFailCount(1);
            
		}

		//显示角色
		//SkinnedMeshRenderer skin;
		//modelPool = PoolManager.Pools["CharactersPool"];
		//model = modelPool.Spawn(ModelData.Instance.GetPrefabName(PlayerData.Instance.GetSelectedModel()));
		//model.parent = tranCharacterParent;
		//model.localPosition = Vector3.zero;
		//model.localRotation = Quaternion.Euler(0, 0, 0);
		//model.localScale = new Vector3(45, 45, 45);
		//model.gameObject.SetActive(true);
		//model.gameObject.SetActive(true);
			
		//model.gameObject.layer = 15;
		//for(int j=0; j<model.childCount; ++j)
		//{
		//	model.GetChild(j).gameObject.layer = 15;
		//}
			
	}
    void SaveStarsData()
    {
        iCurLevel = PlayerData.Instance.GetSelectedLevel();
        iOldStarCount = PlayerData.Instance.GetLevelStarState(iCurLevel);
        iStarCount = GameData.Instance.StarCount;

        if (iStarCount > iOldStarCount)
        {
            PlayerData.Instance.SetLevelStarState(iCurLevel, iStarCount);
            //三星奖励写入数据
            for (int i = iOldStarCount + 1; i <= iStarCount; i++)
            {
                string sAwards = LevelData.Instance.GetLevelAwardStarStr(i, iCurLevel);
                string[] sArrAwards = sAwards.Split('|');
                for (int j = 0; j < sArrAwards.Length; j++)
                {
                    string[] sArrInfo = sArrAwards[j].Split('=');
                    PlayerData.ItemType itemType = (PlayerData.ItemType)System.Enum.Parse(typeof(PlayerData.ItemType), sArrInfo[1]);
                    int count = int.Parse(sArrInfo[3]);

                    PlayerData.Instance.AddItemNum(itemType, count);
                }
            }
        }
        //iCurStarCount = iOldStarCount + 1;
    }
    void CheckAchievement()
	{
		AchievementCheckManager.Instance.Check(AchievementType.UsePropCount_Total,GameData.Instance.itemUseCount);
		AchievementCheckManager.Instance.Check(AchievementType.WinCountTotal,1);
		switch(CarManager.Instance.gameLevelModel)
		{
		case GameLevelModel.DualMeet:
			AchievementCheckManager.Instance.Check(AchievementType.DealMeatWinCount,1);
			break;
		case GameLevelModel.LimitTime:
			AchievementCheckManager.Instance.Check(AchievementType.LimitTimeWinCount,1);
			break;
		case GameLevelModel.Rank:
			AchievementCheckManager.Instance.Check(AchievementType.RankWinCount,1);
			break;
		case GameLevelModel.Weedout:
			AchievementCheckManager.Instance.Check(AchievementType.WeedoutWinCount,1);
			break;
		}
		
		int[] starArray= PlayerData.Instance.GetLevelStarState();
		int starCount=0;
		for(int i=0;i<starArray.Length;++i)
		{
			starCount+=starArray[i];
		}
		AchievementCheckManager.Instance.Check(AchievementType.Level_Star_Count_Total,starCount);
		
		int maxGameLevel = PlayerData.Instance.GetCurrentChallengeLevel();
		AchievementCheckManager.Instance.Check(AchievementType.Level_Count,maxGameLevel);
	}
	
	void SetProps()
	{
		int iPropsNum = 0;
		PlayerData.ItemType[] PropsType = new PlayerData.ItemType[GameData.Instance.RewardDic.Count];
		int[] iArrCounts = new int[GameData.Instance.RewardDic.Count];
		PlayerData.ItemType tempType;
		
		foreach(PlayerData.ItemType type in GameData.Instance.RewardDic.Keys)
		{
			if(GameData.Instance.RewardDic[type] > 0)
			{
				PropsType[iPropsNum++] = type;
				iArrCounts[iPropsNum - 1] = GameData.Instance.RewardDic[type];
				PlayerData.Instance.AddItemNum(type, iArrCounts[iPropsNum - 1]);
			}
		}

        for (int i = 0; i < tranArrProps.Length; i ++)
		{
			tranArrProps[i].gameObject.SetActive(i < iPropsNum);
			
			tempType = PropsType[i];
			if(i < iPropsNum)
			{         
				switch(tempType)
				{
				case PlayerData.ItemType.Apple:
                        
                        imageArrPropsIcon[i].sprite = propsIcons[0];
                        break;
				case PlayerData.ItemType.Banana:
                        

                        imageArrPropsIcon[i].sprite = propsIcons[1];
                        break;
				case PlayerData.ItemType.Ganoderma:
                        
                        imageArrPropsIcon[i].sprite = propsIcons[2];
                        break;
				case PlayerData.ItemType.Ginseng:
                        
                        imageArrPropsIcon[i].sprite = propsIcons[3];
                        break;
				case PlayerData.ItemType.Grape:
                        
                        imageArrPropsIcon[i].sprite = propsIcons[4];
                        break;
				case PlayerData.ItemType.Pear:
                        
					imageArrPropsIcon[i].sprite = propsIcons[5];
                        break;
				}

				textPropsCount[i].text = "x" + iArrCounts[i];
				//tranArrProps[i].localPosition = new Vector3(fGiftPosMinX + (fGiftPosMaxX - fGiftPosMinX) / (iPropsNum * 2) * (2 * i + 1), fGiftPosY, 0);
			}
		}
	}

	void ShowStartEffect()
	{
		for(int i = 0; i < transArrStarsFront.Length; i ++)
		{
			transArrStarsFront[i].gameObject.SetActive(false);
		}

		StartCoroutine("ShowStartEffectIE");
	}

	[HideInInspector]public Vector3 FronStarTempScale;
	private Transform tranFronStarTemp;
	//private ParticleSystem FronStarParticle;
	IEnumerator ShowStartEffectIE()
	{
		for(int i = 0; i < transArrStarsFront.Length; i ++)
		{
			tranFronStarTemp = transArrStarsFront[i];
			//FronStarParticle = tranFronStarTemp.Find("UI_XingXing").GetComponent<ParticleSystem>();
			
			if(i < iStarCount && GameData.Instance.IsWin)
			{
				FronStarTempScale = new Vector3(1.8f, 1.8f, 1.8f);
				tranFronStarTemp.localScale = FronStarTempScale;
				tranFronStarTemp.gameObject.SetActive(true);

				yield return new WaitForSeconds(0.2f);
				DOTween.Kill("StarEffectTween");
				DOTween.To(()=> FronStarTempScale, x => FronStarTempScale = x, new Vector3(1f, 1f, 1f), 0.3f).SetEase(Ease.InOutBack).OnUpdate(UpdateStarScale).SetId("StarEffectTween");
				AudioManger.Instance.PlaySound(AudioManger.SoundName.Star);
				//FronStarParticle.Play();
				yield return new  WaitForSeconds(0.5f);
			}else
			{
				tranFronStarTemp.gameObject.SetActive(false);
			}
		}

		if(iStarCount > 0 && QualitySetting.IsHighQuality && GameData.Instance.IsWin)
		{
			yield return new WaitForSeconds(0.5f);
		}

        //if(iStarCount > iOldStarCount || LevelData.Instance.GetPassGameGiftState(iCurLevel))
        //{
        //	GameUIManager.Instance.ShowModule(UISceneModuleType.GamePassedGfit);
        //}else
        //      {
        SetButtonEnable(true);
        //}
    }

	void UpdateStarScale()
	{
		tranFronStarTemp.localScale = FronStarTempScale;
	}
	#endregion

	#region 经典模式处理
	private float fGiftPosMinX = -120, fGiftPosMaxX = 120, fGiftPosY = 0;
	void InitClassicMode()
	{
		PlayerData.Instance.AddItemNum(PlayerData.ItemType.ColorEgg,GameData.Instance.curEggCount);

		float fCoinAddition = GameData.Instance.AddCoinPecent - 1;
		int iCoinCount = Mathf.RoundToInt(GameData.Instance.curCoin * GameData.Instance.AddCoinPecent);
		PlayerData.Instance.AddItemNum(PlayerData.ItemType.Coin, iCoinCount);
		textClassicCoinCount.text  = iCoinCount.ToString();


        int distance = ((int)PlayerCarControl.Instance.carMove.moveLen);
        int iJewelCount = WuJingConfigData.Instance.GetJewelCountByDistance(distance);
        PlayerData.Instance.AddItemNum(PlayerData.ItemType.Jewel, iJewelCount);
        textClassicJewelCount.text = iJewelCount.ToString();
		textClassDistance.text =  distance.ToString ()+ " m";
        

		//if(GameData.Instance.curEggCount > 0)
		//{
		//	goClassColorEgg.SetActive(true);
		//	textClassicColorEggCount.text = "x"+GameData.Instance.curEggCount.ToString();
		//}else{
		//	goLevelColorEgg.SetActive(false);
		//}
		//游戏进行时活动
		if(PlatformSetting.Instance.isOpenGamePlayingActivity)
		{
			PlayerData.Instance.SetGamePlayingActivityScore((int)PlayerCarControl.Instance.carMove.moveLen + PlayerData.Instance.GetGamePlayingActivityScore());
		}
		//幸运数字活动
		if(PlatformSetting.Instance.isOpenLuckyNumbersActivity)
		{
			if( (int)PlayerCarControl.Instance.carMove.moveLen % 10 == 1)
			{
				PlayerData.Instance.SetLuckyNumbersOneState(true);
			}
			else if( (int)PlayerCarControl.Instance.carMove.moveLen * 100 % 10 == 6)
			{
				PlayerData.Instance.SetLuckyNumbersSixState(true);
			}
			else if( (int)PlayerCarControl.Instance.carMove.moveLen % 10 == 8)
			{
				PlayerData.Instance.SetLuckyNumbersEightState(true);
			}
			else if( (int)PlayerCarControl.Instance.carMove.moveLen % 1000 == 168)
			{
				PlayerData.Instance.SetLuckyNumbersOneSixEightState(true);
			}
		}
		DailyMissionCheckManager.Instance.Check(DailyMissionType.ClassModel,1);
	}
	#endregion

	#region 按钮控制
	void CheckAutoGift()
	{
		int modelLevel = IDTool.GetModelLevel(PlayerData.Instance.GetSelectedModel());
		OneKeyToFullLevelControllor.Instance.bOneKeyToFullLevelBoxIsHidden = false;

		if (PlayerData.Instance.GetGameMode ().CompareTo ("Level") == 0
		    && GameData.Instance.IsWin == false
		    && PayJsonData.Instance.GetIsActivedState (PayType.OneKey2FullLV)
		    && modelLevel <= AutoGiftChecker.MaxModelLevelOpenOneKeyToFull
		    && PlatformSetting.Instance.PayVersionType != PayVersionItemType.ShenHe
		    && PlatformSetting.Instance.PayVersionType != PayVersionItemType.ChangWan
		    && PlatformSetting.Instance.PayVersionType != PayVersionItemType.GuangDian) {
			OneKeyToFullLevelControllor.Instance.InitData (PlayerData.Instance.GetSelectedModel ());
			GameUIManager.Instance.ShowModule (UISceneModuleType.OneKeyToFullLevel);
		} else {
			OneKeyToFullLevelControllor.Instance.bOneKeyToFullLevelBoxIsHidden = true;
		}
	}

	void CloseBoxIE()
	{
		//CheckAutoGift();

		//while (OneKeyToFullLevelControllor.Instance.bOneKeyToFullLevelBoxIsHidden == false) 
		//{
		//	yield return 0;
		//}

		if(showLockTip)
		{
			GlobalConst.ShowModuleType = UISceneModuleType.LockTip;
			GlobalConst.SceneName = SceneType.UIScene;
			LoadingPage.Instance.LoadScene ();
		}
		else
		{
			GlobalConst.ShowModuleType = UISceneModuleType.MainInterface;
			GlobalConst.SceneName = SceneType.UIScene;
			LoadingPage.Instance.LoadScene ();
		}
	}

	void LevelModeRestartIE()
	{
		//CheckAutoGift();
		
		//while (OneKeyToFullLevelControllor.Instance.bOneKeyToFullLevelBoxIsHidden == false) 
		//{
		//	yield return 0;
		//}

		if(showLockTip)
		{
			GlobalConst.ShowModuleType = UISceneModuleType.LockTip;
			GlobalConst.SceneName = SceneType.UIScene;
			LoadingPage.Instance.LoadScene ();
		}else if(AutoGiftChecker.ForeseeAutoGiftCheck(AutomaticGiftName.HuiKuiBigGift))
		{
			GlobalConst.ShowModuleType = UISceneModuleType.MainInterface;
			GlobalConst.SceneName = SceneType.UIScene;
			LoadingPage.Instance.LoadScene ();
		}
		else
		{
            if (PlayerData.Instance.GetItemNum(PlayerData.ItemType.Strength) > 0)
            {
                PlayerData.Instance.ReduceItemNum(PlayerData.ItemType.Strength, 1);
                GameController.Instance.RestartGame();
            }
            else
            {
                GlobalConst.ShowModuleType = UISceneModuleType.MainInterface;
                GlobalConst.SceneName = SceneType.UIScene;
                LoadingPage.Instance.LoadScene();
                //GiftPackageControllor.Instance.Show(PayType.PowerGift, RestartCall);
            }
        }
	}

	void NextLevelIE()
	{
		//CheckAutoGift();
		
		//while (OneKeyToFullLevelControllor.Instance.bOneKeyToFullLevelBoxIsHidden == false) 
		//{
		//	yield return 0;
		//}

		if(showLockTip)
		{
			GlobalConst.ShowModuleType = UISceneModuleType.LockTip;
			GlobalConst.SceneName = SceneType.UIScene;
			LoadingPage.Instance.LoadScene ();
		}
		else
		{
			if(AutoGiftChecker.ForeseeAutoGiftCheck(AutomaticGiftName.HuiKuiBigGift))
			{
				GlobalConst.ShowModuleType = UISceneModuleType.MainInterface;
				GlobalConst.SceneName = SceneType.UIScene;
				LoadingPage.Instance.LoadScene ();
				
			}else if(AutoGiftChecker.ForeseeAutoGiftCheck(AutomaticGiftName.DoubleCoin)  || AutoGiftChecker.ForeseeAutoGiftCheck(AutomaticGiftName.NewPlayerGift) )
			{
				//自动弹出双倍金币 新手礼包 与关卡信息显示冲突
				GlobalConst.ShowModuleType = UISceneModuleType.LevelSelect;
				GlobalConst.SceneName = SceneType.UIScene;
				LoadingPage.Instance.LoadScene ();
			}else{
				GlobalConst.ShowModuleType = UISceneModuleType.LevelInfo;
				GlobalConst.SceneName = SceneType.UIScene;
				LoadingPage.Instance.LoadScene ();
			}
		}
	}

	public void CloseOnClick()
	{
		if(!isButtonEnable)
		{
			return;
		}

		AudioManger.Instance.PlaySound(AudioManger.SoundName.CloseBtClick);
		goLevelMode.SetActive(false);
        goClassicMode.SetActive(false);
        CloseBoxIE();
	}

	public void LevelModeRestartOnClick()
	{
		if(!isButtonEnable)
		{
			return;
		}
		AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
		goLevelMode.SetActive(false);
		LevelModeRestartIE();
	}
		
	public void NextLevelOnClick()	
	{
		if(!isButtonEnable)
		{
			return;
		}
		goLevelMode.SetActive(false);
		NextLevelIE();
	}

	public void ClassicModeRestartOnClick()
	{
		if(!isButtonEnable)
		{
			return;
        }
        goClassicMode.SetActive(false);
        GameController.Instance.RestartGame();
	}


	//private void RestartCall()
	//{
	//	if(PlayerData.Instance.GetItemNum(PlayerData.ItemType.Strength) > 0)
	//	{
	//		PlayerData.Instance.ReduceItemNum(PlayerData.ItemType.Strength, 1);
	//		GameController.Instance.RestartGame();
	//	}
	//	else
	//	{
	//		GiftPackageControllor.Instance.Show(PayType.PowerGift, RestartCall);
	//	}
	//}

	private bool isButtonEnable=true;
	public void SetButtonEnable(bool enable)
	{
		isButtonEnable=enable;
        SceneButtonsController.Instance.IsReceiveAndroidBackKeyEvent = enable;
        //goLevelModeRestartBtn.GetComponent<BoxCollider>().enabled = enable;
        //goNextLevelBtn.GetComponent<BoxCollider>().enabled = enable;
        //goClassicModeCloseBtn.GetComponent<BoxCollider>().enabled = enable;
        //goClassicModeRestartBtn.GetComponent<BoxCollider>().enabled = enable;

        //goLevelModeRestartBtn.GetComponent<Button>().interactable = enable;
        //goNextLevelBtn.GetComponent<Button>().interactable = enable;
        //goClassicModeCloseBtn.GetComponent<Button>().interactable = enable;
        //goClassicModeRestartBtn.GetComponent<Button>().interactable = enable;
        goLevelModeRestartBtn.SetActive(enable);
        goNextLevelBtn.SetActive(enable);
        goClassicModeCloseBtn.SetActive(enable);
        goClassicModeRestartBtn.SetActive(enable);
    }
    #endregion
}
