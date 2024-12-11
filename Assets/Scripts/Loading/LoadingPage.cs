using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingPage : MonoBehaviour {

	public static LoadingPage Instance;

    public RectTransform progressBar;//760 - 0%. 8-100%
    public Text progressText;
	//public Transform roleSprite;

	public Text tipText;

	private float curProgress;
	//private AsyncOperation Async;

	public void InitScene ()
	{
		Instance = this;
		DontDestroyOnLoad (gameObject);

		PublicSceneObject.Instance.Init ();
		PublicSceneObject.Instance.IsReceiveAndroidBackKeyEvent = false;

        if (SceneButtonsController.Instance != null)
        {
            SceneButtonsController.Instance.Init();
            SceneButtonsController.Instance.IsReceiveAndroidBackKeyEvent = false;
        }
        

		StartCoroutine ("IEInitScene");
	}

    private void SetProgress()
	{
		if (curProgress < 50)
			curProgress += 5;
		else
			++ curProgress;
        progressText.text = curProgress + "%";
        //float progressValue = curProgress / 100;
        //760 - 0%. 8-100%
        progressBar.offsetMax = new Vector2(-760 + (752 * (curProgress / 100)), -5);//magic!

        //roleSprite.localPosition = new Vector3 (-280 + progressValue * 560, -121);
    }

    #region Scene jump
    IEnumerator IEInitScene()
	{
//		TipText.text = TipsData.Instance.GetContent (1);

		AudioManger.Instance.StopAll();

		gameObject.SetActive (true);
		curProgress = 0;
		while(curProgress < 100)
		{
			SetProgress ();
			yield return null;
		}

		yield return new WaitForSeconds (0.2f);
		LoadSceneComplete ();
	}

	public void LoadScene()
	{
        //		if (PlayerData.Instance.GetNewPlayerState ())
        //			TipText.text = TipsData.Instance.GetContent (1);
        //		else
        //			TipText.text = TipsData.Instance.GetRandomContent ();

        //Clear the commission
        PlayerData.Instance.ClearPlayerDataChangeEvent ();
		PublicSceneObject.Instance.ClearAndroidBackKeyEvent ();

        if (SceneButtonsController.Instance)
            SceneButtonsController.Instance.ClearAndroidBackKeyEvent();

        //Save game data
        PlayerData.Instance.SaveData ();
		ExchangeActivityData.Instance.SaveData();

        //Game ready logo
        GlobalConst.IsReady = false;
		GlobalConst.IsUIReady = false;

		gameObject.SetActive (true);
		StartCoroutine ("IELoadScene");
		PublicSceneObject.Instance.IsReceiveAndroidBackKeyEvent = false;
        if(SceneButtonsController.Instance)
        SceneButtonsController.Instance.IsReceiveAndroidBackKeyEvent = false;
        
	}

	IEnumerator IELoadScene()
	{
		AudioManger.Instance.StopAll();
		
		curProgress = 0;
        SceneManager.LoadScene(GlobalConst.SceneName.ToString());// Application.LoadLevelAsync (GlobalConst.SceneName.ToString ());

        while (curProgress < 100)//loading simulator
        {
			SetProgress ();
			yield return new WaitForEndOfFrame ();
		}
		
		yield return new WaitForSeconds (0.2f);
		LoadSceneComplete ();
	}

	/// <summary>
	/// 场景加载完成后调用此函数
	/// </summary>
	void LoadSceneComplete()
	{
		//ExchangeActivityData.Instance.InitFromServer ();

		StopCoroutine ("IEWaiting");
		StartCoroutine ("IEWaiting");
	}

	/// <summary>
	/// 加载完成UI场景后调用此函数.
	/// </summary>
	void LoadUISceneComplete()
	{
		UIManager.Instance.ShowModule (UISceneModuleType.PropertyDisplay);
		UIManager.Instance.ShowModule (UISceneModuleType.MainInterface);

		switch (GlobalConst.ShowModuleType) {
		case UISceneModuleType.MainInterface:
			//不过第一关, 不弹签到
			if (!GlobalConst.IsSignIn && PlayerData.Instance.GetCurrentChallengeLevel() > 1) {
				UIManager.Instance.ShowModule (UISceneModuleType.SignIn);
			}
			break;
		case UISceneModuleType.LockTip:
			LockTipControllor.Instance.InitData(LockTipType.UnlockTip);
			UIManager.Instance.ShowModule (UISceneModuleType.LockTip);
			break;
		case UISceneModuleType.LevelSelect:
			LevelInfoControllor.Instance.SetModelIndex (IDTool.GetModelType (PlayerData.Instance.GetSelectedModel ()) - 1);
			UIManager.Instance.ShowModule (UISceneModuleType.LevelSelect);
			break;
		case UISceneModuleType.LevelInfo:
			LevelInfoControllor.Instance.SetModelIndex (IDTool.GetModelType (PlayerData.Instance.GetSelectedModel ()) - 1);
			UIManager.Instance.ShowModule (UISceneModuleType.LevelSelect);
			LevelInfoControllor.Instance.InitData (PlayerData.Instance.GetCurrentChallengeLevel());
			UIManager.Instance.ShowModule (UISceneModuleType.LevelInfo);
			break;
		}
		AudioManger.Instance.PlayMusic(AudioManger.MusicName.UIBackground);
	}

	IEnumerator IEWaiting()
	{
		while (GlobalConst.IsReady == false || GlobalConst.IsUIReady == false) {
			yield return new WaitForSeconds (0.2f);
		}
        //if (PublicSceneObject.Instance)
            PublicSceneObject.Instance.IsReceiveAndroidBackKeyEvent = true;
        

        if (SceneButtonsController.Instance)
            SceneButtonsController.Instance.IsReceiveAndroidBackKeyEvent = true;

        gameObject.SetActive (false);

		switch (GlobalConst.SceneName) {
		case SceneType.UIScene:
			LoadUISceneComplete ();
			break;
		case SceneType.GameScene:
			LoadGameSceneComplete ();
			break;
		}
	}
	
	/// <summary>
	/// 加载完成游戏场景后调用此函数.
	/// </summary>
	void LoadGameSceneComplete()
	{
		GameUIManager.Instance.ShowModule (UISceneModuleType.GamePlayerUI);
		GameController.Instance.StartFromLoading ();
		AudioManger.Instance.PlayMusic (AudioManger.MusicName.GameBackground);
	}
	#endregion
}