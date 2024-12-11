using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
//using UnityEngine.EventSystems;

public class UIGuideControllor_GameClone : UIBoxBase {
    private UIGuideType curr_guideType;



    public static UIGuideControllor_GameClone Instance;
	public GameObject goGameRebornGuideBtn, goGameEndingScoreNextGuideBtn, goGameEndingScoreCloseGuideBtn, goGamePlayerUILeftGuideBtn, goGamePlayerUIRightGuideBtn, goLeftRoleGuideBtn,goUseCurPropBtn,goUseFlyBmobBtn,goUseSpeedupBtn, HandGO;
	//public GameObject LevelInfoGuideGO, MainInterfaceUpgradeGuideGO, MainInterfaceStartGameGuideGO, CharaterDetailGuideGO, GameRebornGuideGO, GameEndingScoreNextGuideGO, GameEndingScoreCloseGuideGO, GamePlayerUILeftGuideGO, GamePlayerUIRightGuideGO, LeftRoleGuideGO, HandGO,UseCurPropGO,UseFlyBmobGO,UseSpeedupGO;

	public Image UseCurPropSprite;
    public Sprite[] propSprites;

	public Transform BubbleTran;
	public Text TipText;
	public Text RebornText;
	//public Text MainInterfaceUpgradeCountText, CharaterDetailUpgradeCountText ;
	public Text  useFlyBombCountText,useSpeedUpCountText;

    //Переделать относительно экрана
	private Vector3 LeftUpHandPos = new Vector3(-60, 35, 0);
	private Vector3 RightUpHandPos = new Vector3(80, 35, 0);
	private Vector3 LeftDownHandPos = new Vector3(-60, -15, 0);
	private Vector3 RightDownHandPos = new Vector3(140, -10, 0);

	private Vector3 LeftUpHandRotation = new Vector3 (0, 0, 270);
	private Vector3 RightUpHandRotation = new Vector3 (0, 0, 90);
	private Vector3 LeftDownHandRotation = new Vector3 (0, 0, 330);
	private Vector3 RightDownHandRotation = new Vector3 (0, 0, 30);

	private Vector3 LeftUpMovePos = new Vector3 (-90, 55, 0);
	private Vector3 RightUpMovePos = new Vector3 (100, 55, 0);
	private Vector3 LeftDownMovePos = new Vector3 (-80, -35, 0);
	private Vector3 RightDownMovePos = new Vector3 (160, -30, 0);

//	[HideInInspector][System.NonSerialized]public string newRoleTip1 = "试试满级超人强\n的实力吧";
//	[HideInInspector][System.NonSerialized]public string newRoleTip2 = "超人强实力\n很强大哦";
//	[HideInInspector][System.NonSerialized]public string upgradeRoleTip1 = "升级加强角色能力";
//	[HideInInspector][System.NonSerialized]public string upgradeRoleTip2 = "升到2级有特技哦";
//	[HideInInspector][System.NonSerialized]public string upgradeRoleTip3 = "点击Use props吧！";

	//private Transform CoinTypeTran1, JewelTypeTran1, CoinTypeTran2, JewelTypeTran2;
	private UIGuideType guideType;

	public override void Init()
	{
		Instance = this;
		//transform.localPosition = ShowPosV2;

		//CoinTypeTran1 = goMainInterfaceUpgradeGuideBtn.transform.Find ("CoinType");
		//JewelTypeTran1 = goMainInterfaceUpgradeGuideBtn.transform.Find ("JewelType");
		//CoinTypeTran2 = goCharaterDetailGuideBtn.transform.Find ("CoinType");
		//JewelTypeTran2 = goCharaterDetailGuideBtn.transform.Find ("JewelType");
		//if (ModelData.Instance.GetUpgradeCostType (102).CompareTo ("Coin") == 0) {
		//	CoinTypeTran1.gameObject.SetActive (true);
		//	JewelTypeTran1.gameObject.SetActive (false);
		//	CoinTypeTran2.gameObject.SetActive (true);
		//	JewelTypeTran2.gameObject.SetActive (false);
		//	CoinTypeTran1.Find("Cost").GetComponent<Text>().text = ModelData.Instance.GetUpgradeCost(102).ToString();
		//	CoinTypeTran1.Find("Text").GetComponent<Text>().text = "Refit";
		//	CoinTypeTran2.Find("Cost").GetComponent<Text>().text = ModelData.Instance.GetUpgradeCost(102).ToString();
		//	CoinTypeTran2.Find("Text").GetComponent<Text>().text = "Refit";
		//} else {
		//	CoinTypeTran1.gameObject.SetActive (false);
		//	JewelTypeTran1.gameObject.SetActive (true);
		//	CoinTypeTran2.gameObject.SetActive (false);
		//	JewelTypeTran2.gameObject.SetActive (true);
		//	JewelTypeTran1.Find("Cost").GetComponent<Text>().text = ModelData.Instance.GetUpgradeCost(102).ToString();
		//	JewelTypeTran1.Find("Text").GetComponent<Text>().text = "Refit";
		//	JewelTypeTran2.Find("Cost").GetComponent<Text>().text = ModelData.Instance.GetUpgradeCost(102).ToString();
		//	JewelTypeTran2.Find("Text").GetComponent<Text>().text = "Refit";
		//}

		//if (PayJsonData.Instance.GetNeedShowCancelBt (PayType.FreeReborn)) {
		//	goGameRebornGuideBtn.transform.localPosition = new Vector3 (72, -165, 0);
		//} else {
		//	goGameRebornGuideBtn.transform.localPosition = new Vector3 (0, -165, 0);
		//}

		RebornText.text = PayJsonData.Instance.GetButtonText (PayType.FreeReborn);

		base.Init();
	}

	//public void SetUpgradeCount()
	//{
		//int count = ToolController.GetTipCounnt (101);
		//if (count > 0) {
		//	MainInterfaceUpgradeCountText.transform.parent.gameObject.SetActive (true);
		//	MainInterfaceUpgradeCountText.text = count.ToString ();
		//	CharaterDetailUpgradeCountText.transform.parent.gameObject.SetActive (true);
		//	CharaterDetailUpgradeCountText.text = count.ToString ();
		//} else {
		//	MainInterfaceUpgradeCountText.transform.parent.gameObject.SetActive (false);
		//	CharaterDetailUpgradeCountText.transform.parent.gameObject.SetActive (false);
		//}
        
	//}


	public void ShowBubbleTipByID(int id)
	{
		string tipStr = GuideTextData.Instance.GetText(id);
		Vector3 showPos = GuideTextData.Instance.GetPosition(id);
		float xScale = GuideTextData.Instance.GetXScale(id);
		ShowBubbleTip(tipStr,showPos,xScale);
	}

	public void ShowBubbleTip(string tipStr, Vector3 showPos, float XScale = 1)
	{
		BubbleTran.gameObject.SetActive (true);
		TipText.text = Localisation.GetString(tipStr);
		BubbleTran.localPosition = showPos;
		//TipText.transform.localScale = new Vector3(XScale,1,1);
		//BubbleTran.localScale = new Vector3(XScale,1,1);
	}

	private Transform iconTran;
	private Vector3 iconPos;
	public void ShowAnim(UIGuideType guideType)
	{
		GameUIManager.Instance.ShowModule (UISceneModuleType.UIGuide);
		HandGO.SetActive (false);

		switch (guideType) {
		case UIGuideType.GamePlayerUILeftGuide:
                goGamePlayerUILeftGuideBtn.SetActive (true);
			iconTran = goGamePlayerUILeftGuideBtn.transform;
			break;
		case UIGuideType.GamePlayerUIRightGuide:
                goGamePlayerUIRightGuideBtn.SetActive (true);
			iconTran = goGamePlayerUIRightGuideBtn.transform;
			break;
		}

		StartCoroutine (MoveAnim(guideType));
	}

	private void ShowHandAnim(DirectionType directionType, Transform targetTran)
	{
		HandGO.SetActive (true);

		//Debug.Log ("ShowHandAnim : " + targetTran.gameObject.name + " ---> " + targetTran.localPosition);
		
		Sequence myQuence = DOTween.Sequence().SetLoops(int.MaxValue, LoopType.Yoyo).SetId("HandQuence");

		switch (directionType) {
		case DirectionType.LeftUp:
			HandGO.transform.localPosition = targetTran.localPosition + LeftUpHandPos;
			HandGO.transform.eulerAngles = LeftUpHandRotation;
			HandGO.transform.localScale = new Vector3(-1, 1, 1);
			myQuence.Append(HandGO.transform.DOLocalMove(targetTran.localPosition + LeftUpMovePos, 0.5f));
			break;
		case DirectionType.RightUp:
			HandGO.transform.localPosition = targetTran.localPosition + RightUpHandPos;
			HandGO.transform.eulerAngles = RightUpHandRotation;
			HandGO.transform.localScale = Vector3.one;
			myQuence.Append(HandGO.transform.DOLocalMove(targetTran.localPosition + RightUpMovePos, 0.5f));
			break;
		case DirectionType.LeftDown:
			HandGO.transform.localPosition = targetTran.localPosition + LeftDownHandPos;
			HandGO.transform.eulerAngles = LeftDownHandRotation;
			HandGO.transform.localScale = new Vector3(-1, 1, 1);
			myQuence.Append(HandGO.transform.DOLocalMove(targetTran.localPosition + LeftDownMovePos, 0.5f));
			break;
		case DirectionType.RightDown:
			HandGO.transform.localPosition = targetTran.localPosition + RightDownHandPos;
			HandGO.transform.eulerAngles = RightDownHandRotation;
			HandGO.transform.localScale = Vector3.one;
			myQuence.Append(HandGO.transform.DOLocalMove(targetTran.localPosition + RightDownMovePos, 0.5f));
			break;
		}

		myQuence.Play();
	}

	IEnumerator MoveAnim(UIGuideType guideType)
	{
		iconPos = iconTran.localPosition;
		iconTran.localPosition = Vector3.zero;
		//iconTran.GetComponent<BoxCollider> ().enabled = false;
		BubbleTran.gameObject.SetActive (false);
		yield return new WaitForSeconds (0.5f);
		iconTran.DOLocalMove (iconPos, 0.5f).SetEase (Ease.Linear);
		yield return new WaitForSeconds (0.5f);
		//iconTran.GetComponent<BoxCollider> ().enabled = true;
		BubbleTran.gameObject.SetActive (true);
		
		switch (guideType) {
		case UIGuideType.GamePlayerUILeftGuide:
			GamePlayerUIControllor.Instance.leftBtnGO.SetActive(true);
			ShowHandAnim(DirectionType.RightDown, iconTran);
			break;
		case UIGuideType.GamePlayerUIRightGuide:
			GamePlayerUIControllor.Instance.rightBtnGO.SetActive(true);
			ShowHandAnim(DirectionType.LeftDown, iconTran);
			break;
		}
	}

	public override void Back ()
	{

	}

	public override void Hide ()
	{
		GameUIManager.Instance.HideModule (UISceneModuleType.UIGuide);
	}

	public override void Show()
	{
        SceneButtonsController.Instance.IsReceiveAndroidBackKeyEvent = false;
		gameObject.SetActive (true);
	}

	public void Show(UIGuideType guideType)
	{
		GameUIManager.Instance.ShowModule (UISceneModuleType.UIGuide);
        
        curr_guideType = guideType;
        //EventSystem eventSystem = EventSystem.current;

        switch (guideType) {
		//case UIGuideType.LevelInfoGuide:
  //              //goLevelInfoGuideBtn.SetActive (true);
  //              //SceneButtonsController.Instance.SetActiveGuideButton(goLevelInfoGuideBtn.GetComponent<ButtonEntities>());
  //              ////eventSystem.SetSelectedGameObject(goLevelInfoGuideBtn);
  //              //ShowHandAnim(DirectionType.RightUp, goLevelInfoGuideBtn.transform);
		//	break;
		//case UIGuideType.MainInterfaceUpgradeGuide:
		//	SetUpgradeCount ();
  //              //goMainInterfaceUpgradeGuideBtn.SetActive (true);
  //              //SceneButtonsController.Instance.SetActiveGuideButton(goMainInterfaceUpgradeGuideBtn.GetComponent<ButtonEntities>());
  //              ////eventSystem.SetSelectedGameObject(goMainInterfaceUpgradeGuideBtn);
  //              //ShowHandAnim(DirectionType.LeftUp, goMainInterfaceUpgradeGuideBtn.transform);
		//	break;
		//case UIGuideType.MainInterfaceStartGameGuide:
  //              //goMainInterfaceStartGameGuideBtn.SetActive (true);
  //              //SceneButtonsController.Instance.SetActiveGuideButton(goMainInterfaceStartGameGuideBtn.GetComponent<ButtonEntities>());
  //              ////eventSystem.SetSelectedGameObject(goMainInterfaceStartGameGuideBtn);
  //              //ShowHandAnim(DirectionType.RightUp, goMainInterfaceStartGameGuideBtn.transform);
		//	break;
		//case UIGuideType.CharaterDetailGuide:
		//	SetUpgradeCount ();
  //              //goCharaterDetailGuideBtn.SetActive (true);
  //              //SceneButtonsController.Instance.SetActiveGuideButton(goCharaterDetailGuideBtn.GetComponent<ButtonEntities>());
  //              ////eventSystem.SetSelectedGameObject(goCharaterDetailGuideBtn);
  //              //ShowHandAnim(DirectionType.RightDown, goCharaterDetailGuideBtn.transform);
		//	break;
		case UIGuideType.GameRebornGuide:
                goGameRebornGuideBtn.SetActive (true);
                SceneButtonsController.Instance.SetActiveGuideButton(goGameRebornGuideBtn.GetComponent<ButtonEntities>());
                //eventSystem.SetSelectedGameObject(goGameRebornGuideBtn);
                ShowHandAnim(DirectionType.RightDown, goGameRebornGuideBtn.transform);
			break;
		case UIGuideType.GameEndingScoreNextGuide:
                goGameEndingScoreNextGuideBtn.SetActive (true);
                SceneButtonsController.Instance.SetActiveGuideButton(goGameEndingScoreNextGuideBtn.GetComponent<ButtonEntities>());
                //eventSystem.SetSelectedGameObject(goGameEndingScoreNextGuideBtn);
                ShowHandAnim(DirectionType.RightUp, goGameEndingScoreNextGuideBtn.transform);
			break;
		case UIGuideType.GameEndingScoreCloseGuide:
                goGameEndingScoreCloseGuideBtn.SetActive (true);
                SceneButtonsController.Instance.SetActiveGuideButton(goGameEndingScoreCloseGuideBtn.GetComponent<ButtonEntities>());
                //eventSystem.SetSelectedGameObject(goGameEndingScoreCloseGuideBtn);
                ShowHandAnim(DirectionType.LeftDown, goGameEndingScoreCloseGuideBtn.transform);
			break;
		case UIGuideType.GamePlayerUILeftGuide:
                goGamePlayerUILeftGuideBtn.SetActive (true);
                SceneButtonsController.Instance.SetActiveGuideButton(goGamePlayerUILeftGuideBtn.GetComponent<ButtonEntities>());
                //eventSystem.SetSelectedGameObject(goGamePlayerUILeftGuideBtn);
                ShowHandAnim(DirectionType.RightDown, goGamePlayerUILeftGuideBtn.transform);
			break;
		case UIGuideType.GamePlayerUIRightGuide:
                goGamePlayerUIRightGuideBtn.SetActive (true);
                SceneButtonsController.Instance.SetActiveGuideButton(goGamePlayerUIRightGuideBtn.GetComponent<ButtonEntities>());
                //eventSystem.SetSelectedGameObject(goGamePlayerUIRightGuideBtn);
                ShowHandAnim(DirectionType.LeftDown, goGamePlayerUIRightGuideBtn.transform);
			break;
		case UIGuideType.GamePlayerUIUseCurPropGuide:
                goUseCurPropBtn.SetActive(true);
                SceneButtonsController.Instance.SetActiveGuideButton(goUseCurPropBtn.GetComponent<ButtonEntities>());
                //eventSystem.SetSelectedGameObject(goUseCurPropBtn);
                ShowHandAnim(DirectionType.LeftDown,goUseCurPropBtn.transform);
                string spId = GamePlayerUIControllor.Instance.spriteId; //
                UseCurPropSprite.sprite = System.Array.Find(propSprites, s => s.name == spId);
                break;
		case UIGuideType.GamePlayerUIUseFlyBmobGuide:
                goUseFlyBmobBtn.SetActive(true);
                SceneButtonsController.Instance.SetActiveGuideButton(goUseFlyBmobBtn.GetComponent<ButtonEntities>());
                //eventSystem.SetSelectedGameObject(goUseFlyBmobBtn);
                useFlyBombCountText.text="X"+PlayerData.Instance.GetItemNum(PlayerData.ItemType.FlyBomb);
			ShowHandAnim(DirectionType.RightUp,goUseFlyBmobBtn.transform);
			break;
		case UIGuideType.GamePlayerUIUseSpeedupGuide:
                goUseSpeedupBtn.SetActive(true);
                SceneButtonsController.Instance.SetActiveGuideButton(goUseSpeedupBtn.GetComponent<ButtonEntities>());
                //eventSystem.SetSelectedGameObject(goUseSpeedupBtn);
                useSpeedUpCountText.text ="X"+ PlayerData.Instance.GetItemNum(PlayerData.ItemType.SpeedUp);
			ShowHandAnim(DirectionType.RightUp,goUseSpeedupBtn.transform);
			break;
		case UIGuideType.GamePlayerUIShowTextGuide:
			HandGO.SetActive (false);
			break;
		case UIGuideType.LeftRoleGuide:
                goLeftRoleGuideBtn.SetActive (true);
                SceneButtonsController.Instance.SetActiveGuideButton(goLeftRoleGuideBtn.GetComponent<ButtonEntities>());
                //eventSystem.SetSelectedGameObject(goLeftRoleGuideBtn);
                ShowHandAnim(DirectionType.RightDown, goLeftRoleGuideBtn.transform);
			break;
		}
	}

	public IEnumerator HideInvoke(float delayTime, UIGuideType type)
	{
		yield return new WaitForSeconds (delayTime);
		Hide (type);
	}

	public void Hide(UIGuideType guideType)
	{
		Hide ();
        SceneButtonsController.Instance.IsReceiveAndroidBackKeyEvent = true;
		gameObject.SetActive (false);
		BubbleTran.gameObject.SetActive (false);

		DOTween.Kill ("HandQuence");

		switch (guideType)
		{
			//case UIGuideType.LevelInfoGuide:
			//	goLevelInfoGuideBtn.SetActive(false);
			//	break;
			//case UIGuideType.MainInterfaceUpgradeGuide:
			//	goMainInterfaceUpgradeGuideBtn.SetActive(false);
			//	break;
			//case UIGuideType.MainInterfaceStartGameGuide:
			//	goMainInterfaceStartGameGuideBtn.SetActive(false);
			//	break;
			//case UIGuideType.CharaterDetailGuide:
			//	goCharaterDetailGuideBtn.SetActive(false);
			//	break;
			case UIGuideType.GameRebornGuide:
				goGameRebornGuideBtn.SetActive(false);
				break;
			case UIGuideType.GameEndingScoreNextGuide:
				goGameEndingScoreNextGuideBtn.SetActive(false);
				break;
			case UIGuideType.GameEndingScoreCloseGuide:
				goGameEndingScoreCloseGuideBtn.SetActive(false);
				break;
			case UIGuideType.GamePlayerUILeftGuide:
				goGamePlayerUILeftGuideBtn.SetActive(false);
				GameController.Instance.ResumeGame();
				break;
			case UIGuideType.GamePlayerUIRightGuide:
				goGamePlayerUIRightGuideBtn.SetActive(false);
				GameController.Instance.ResumeGame();
				break;
			case UIGuideType.GamePlayerUIUseCurPropGuide:
				goUseCurPropBtn.SetActive(false);
				GameController.Instance.ResumeGame();
				break;
			case UIGuideType.GamePlayerUIUseFlyBmobGuide:
				goUseFlyBmobBtn.SetActive(false);
				GameController.Instance.ResumeGame();
				GamePlayerUIControllor.Instance.useFlyBombBtnGO.SetActive(true);
				break;
			case UIGuideType.GamePlayerUIUseSpeedupGuide:
				goUseSpeedupBtn.SetActive(false);
				GameController.Instance.ResumeGame();
				GamePlayerUIControllor.Instance.useSpeedupBtnGO.SetActive(true);
				GamePlayerUIControllor.Instance.useShieldBtnGO.SetActive(true);
				break;
			case UIGuideType.GamePlayerUIShowTextGuide:
				GameController.Instance.ResumeGame();
				break;
			case UIGuideType.LeftRoleGuide:
				goLeftRoleGuideBtn.SetActive(false);
				break;

		}
        curr_guideType = UIGuideType.None;
        //EventSystem.current.SetSelectedGameObject(null);

    }

    public void LevelInfoGuideBtnOnClick()
	{
		AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
		Hide (UIGuideType.LevelInfoGuide);
		if(PlayerData.Instance.GetCurrentChallengeLevel () == 4)
		{
			PlayerData.Instance.SetUIGuideToFalse (UIGuideType.LevelInfoGuide);
			PlayerData.Instance.SetUIGuideToFalse (UIGuideType.LeftRoleGuide);
			PlayerData.Instance.SetIsUIGuideEndToTrue ();
		}
		LevelInfoControllor.Instance.StartOnClick();
	}

    public void MainInterfaceUpgradeGuideBtnOnClick()
	{
		AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
		Hide (UIGuideType.MainInterfaceUpgradeGuide);
		MainInterfaceControllor.Instance.ZaohuanOnClick();
	}

    public void MainInterfaceStartGameGuideBtnOnClick()
	{
		AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
		Hide (UIGuideType.MainInterfaceStartGameGuide);
		MainInterfaceControllor.Instance.LevelGameOnClick();
		if (GlobalConst.LeftRoleGuideEnabled) {
			//下一步教程
			LevelInfoControllor.Instance.InitData (PlayerData.Instance.GetCurrentChallengeLevel());
			UIManager.Instance.ShowModule (UISceneModuleType.LevelInfo);
		}
	}

    public void CharaterDetailGuideBtnOnClick()
	{
		AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
		Hide (UIGuideType.CharaterDetailGuide);
		PlayerData.Instance.SetUIGuideToFalse (UIGuideType.CharaterDetailGuide);
		CharacterDetailControllor.Instance.ZaohuanOnClick ();
	}

    public void GameRebornGuideBtnOnClick()
	{
		AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
		Hide (UIGuideType.GameRebornGuide);
		PlayerData.Instance.SetUIGuideToFalse (UIGuideType.GameRebornGuide);
		GameRebornControllor.Instance.RebornBtnOnClick ();
	}

    public void GameEndingScoreNextGuideBtnOnClick()
	{
		AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
		Hide (UIGuideType.GameEndingScoreNextGuide);
		PlayerData.Instance.SetUIGuideToFalse (UIGuideType.GameEndingScoreNextGuide);
		GameEndingScoreControllor.Instance.NextLevelOnClick ();
	}

    public void GameEndingScoreCloseGuideBtnOnClick()
	{
		AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
		Hide (UIGuideType.GameEndingScoreCloseGuide);
		if(PlayerData.Instance.GetCurrentChallengeLevel () == 4)
			PlayerData.Instance.SetUIGuideToFalse (UIGuideType.GameEndingScoreCloseGuide);
		GameEndingScoreControllor.Instance.CloseOnClick ();
	}

    public void LeftRoleGuideBtnOnClick()
	{
		AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
		Hide (UIGuideType.LeftRoleGuide);
		MainInterfaceControllor.Instance.LeftOnClick();
		//下一步教程
		Show (UIGuideType.MainInterfaceStartGameGuide);
        ShowBubbleTipByID(3);
		//TranslucentUIMaskManager.Instance.SetLayer (11);
		ActorCameraController1.Instance.SetCameraDepth (4);
	}

    public void GamePlayerUILeftGuideBtnOnClick()
	{
		AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
		Hide (UIGuideType.GamePlayerUILeftGuide);
		GamePlayerUIControllor.Instance.LeftDown();
		GamePlayerUIControllor.Instance.leftBtnGO.SetActive(true);
		Invoke("LeftRecov",0.5f);
	}

    public void LeftRecov()
	{
		GamePlayerUIControllor.Instance.LeftUp();
	}

    public void GamePlayerUIRightGuideBtnOnClick()
	{
		AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
		Hide (UIGuideType.GamePlayerUIRightGuide);
		GamePlayerUIControllor.Instance.RightDown();
		GamePlayerUIControllor.Instance.rightBtnGO.SetActive(true);

		Invoke("RightRecov",0.6f);
	}


    public void RightRecov()
	{
		GamePlayerUIControllor.Instance.RightUp();
	}

    public void GamePlayerUIUseCurPropGuideBtnOnClick()
	{
		GamePlayerUIControllor.Instance.UsePropOnClick();
		Hide(UIGuideType.GamePlayerUIUseCurPropGuide);
		GamePlayerUIControllor.Instance.pauseBtnGO.SetActive(true);
		//if(PlatformSetting.Instance.PayVersionType != PayVersionItemType.GuangDian)
		//	GamePlayerUIControllor.Instance.giftBagGO.SetActive(true);
		GamePlayerUIControllor.Instance.pickPropBtnGO.SetActive (true);
	}

    public void GamePlayerUIUseFlyBmobGuideBtnOnClick()
    {
        Hide(UIGuideType.GamePlayerUIUseFlyBmobGuide);
        GamePlayerUIControllor.Instance.UseFlyBmobProp();
	}

    public void GamePlayerUIUseSpeedupGuideBtnOnClick()
    {
        Hide(UIGuideType.GamePlayerUIUseSpeedupGuide);
        GamePlayerUIControllor.Instance.UseSpeedUpProp();
	}


}