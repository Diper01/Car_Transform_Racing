using UnityEngine;
using System.Collections;
using UnityEditor;

public class PlayerDataEditor : EditorWindow {
	
	private int CoinCount ;
	private int JewelCount;
    private int ScoreCount;
    private int ProtectShieldCount;
	private int FlyBombCount;
	private int SpeedUpCount;
	private int AppleCount ;
    private int BananaCount;
    private int PearCount;
    private int CurCallengeLevel;
	private int CurStrength;
    private int SignInTimes;
    private int ColorEgg;
    private string ModelStateStr;
	private bool IsATypeBag;
	private bool IsBTypeBag;
	private bool IsCTypeBag;
	private bool PlayBreathingEffect;
	private bool PlayFingerGuide;
    private bool IsMonthCardGift;
	private bool IsMonthCardGiftAutoRenew;

	public bool isWujinModel;

	private string[] Yunyinshang = {"电信", "联通", "移动"};
	private string[] PayVersion  = {"礼包白", "礼包黑", "审核版", "畅玩版", "广电版"};

	private string VersionFileName;

	private int SelectedYunyinShang = 0;
	private int SelectedPayVersion = 0;
    private void OnEnable()
    {
        CoinCount = PlayerData.Instance.GetItemNum(PlayerData.ItemType.Coin);
        JewelCount = PlayerData.Instance.GetItemNum(PlayerData.ItemType.Jewel);
        ScoreCount = PlayerData.Instance.GetItemNum(PlayerData.ItemType.Score);
        ProtectShieldCount = PlayerData.Instance.GetItemNum(PlayerData.ItemType.ProtectShield);
        FlyBombCount = PlayerData.Instance.GetItemNum(PlayerData.ItemType.FlyBomb);
        SpeedUpCount = PlayerData.Instance.GetItemNum(PlayerData.ItemType.SpeedUp);
        AppleCount = PlayerData.Instance.GetItemNum(PlayerData.ItemType.Apple);
        BananaCount = PlayerData.Instance.GetItemNum(PlayerData.ItemType.Banana);
        PearCount = PlayerData.Instance.GetItemNum(PlayerData.ItemType.Pear);
        CurCallengeLevel = PlayerData.Instance.GetCurrentChallengeLevel();
        CurStrength = PlayerData.Instance.GetItemNum(PlayerData.ItemType.Strength);
        SignInTimes = PlayerData.Instance.GetSignInTimes();
        ColorEgg = PlayerData.Instance.GetItemNum(PlayerData.ItemType.ColorEgg);
        ModelStateStr = ConvertTool.AnyTypeArrayToString<int>(PlayerData.Instance.GetModelState(), "|");
        IsATypeBag = PlayerData.Instance.GetIsATypeBagState();
        IsBTypeBag = PlayerData.Instance.GetIsBTypeBagState();
        IsCTypeBag = PlayerData.Instance.GetIsCTypeBagState();
        PlayBreathingEffect = PlayerData.Instance.GetPlayBreathingEffectState();
        PlayFingerGuide = PlayerData.Instance.GetPlayFingerGuideState();
        IsMonthCardGift = PlayerData.Instance.GetIsShowedMonthCardGift();
        IsMonthCardGiftAutoRenew = PlayerData.Instance.GetMonthCardGiftAutoRenewState();

        isWujinModel = PlayerData.Instance.IsWuJinGameMode();
        VersionFileName = PlayerData.Instance.GetPayVersionType();
    }
    [MenuItem ("DataSetting/User data settings", false)]//用户数据设置
    static void OpenPlayerDataSetting () {
        //GetWindowWithRect<PlayerDataEditor>(new Rect(Screen.width / 2, 100, 800, 500), true, "Redemption code function window", true).Show();
        PlayerDataEditor window = (PlayerDataEditor)GetWindow (typeof (PlayerDataEditor),true, "User data settings",true);
        window.Show();
	}

    [MenuItem("DataSetting/User data settings", true)]//用户数据设置
    static bool ValidateOpenPlayerDataSetting()
    {
        return FileTool.IsFileExists("PlayerData");
    }

    void OnFocus()
	{
		if (FileTool.IsFileExists ("PlayerData") == false)
			PlayerData.Instance.SaveData ();
		
		GetOriData();
	}

	void OnGUI()
    {

		GUILayout.Label ("DataSetting", EditorStyles.boldLabel);//用户数据设置

        GUILayout.BeginHorizontal();
		GUILayout.BeginVertical();
		CoinCount = EditorGUILayout.IntField ("Number of gold coins", CoinCount, GUILayout.Height(20));//金币数量
        JewelCount = EditorGUILayout.IntField ("Number of diamonds", JewelCount, GUILayout.Height(20));//钻石数量
        ScoreCount = EditorGUILayout.IntField ("积分数量", ScoreCount, GUILayout.Height(20));
		ProtectShieldCount = EditorGUILayout.IntField ("ProtectShieldCount", ProtectShieldCount, GUILayout.Height(20));//无敌护盾
        SpeedUpCount = EditorGUILayout.IntField ("SpeedUpCount", SpeedUpCount, GUILayout.Height(20));//闪电冲刺
        FlyBombCount = EditorGUILayout.IntField ("FlyBombCount", FlyBombCount, GUILayout.Height(20));//连发导弹
        AppleCount = EditorGUILayout.IntField ("苹果", AppleCount, GUILayout.Height(20));
		BananaCount = EditorGUILayout.IntField ("香蕉", BananaCount, GUILayout.Height(20));
		PearCount = EditorGUILayout.IntField ("雪梨", PearCount, GUILayout.Height(20));
		ColorEgg = EditorGUILayout.IntField ("ColorEgg",ColorEgg,GUILayout.Height(20));
		CurStrength = EditorGUILayout.IntField ("Physical strength", CurStrength, GUILayout.Height(20));//体力数量
        SignInTimes = EditorGUILayout.IntField ("Check-in days", SignInTimes, GUILayout.Height(20));//签到天数
        ModelStateStr = EditorGUILayout.TextField ("Role level", ModelStateStr, GUILayout.Height(20));//角色等级
        CurCallengeLevel = EditorGUILayout.IntField ("Open level", CurCallengeLevel);//开启关卡

        IsATypeBag = EditorGUILayout.Toggle("A包", IsATypeBag, GUILayout.Height(20));
		IsBTypeBag = EditorGUILayout.Toggle("B包", IsBTypeBag, GUILayout.Height(20));
		IsCTypeBag = EditorGUILayout.Toggle("C包", IsCTypeBag, GUILayout.Height(20));
		IsMonthCardGift = EditorGUILayout.Toggle("月卡礼包", IsMonthCardGift, GUILayout.Height(20));
		IsMonthCardGiftAutoRenew = EditorGUILayout.Toggle("月卡礼包自动续费", IsMonthCardGiftAutoRenew, GUILayout.Height(20));

		PlayBreathingEffect = EditorGUILayout.Toggle("Button breathing effect", PlayBreathingEffect, GUILayout.Height(20));//按钮呼吸效果
        PlayFingerGuide = EditorGUILayout.Toggle("Finger guide click", PlayFingerGuide, GUILayout.Height(20));//手指指导点击


        isWujinModel = EditorGUILayout.Toggle ("Endless mode", isWujinModel, GUILayout.Height(20));//无尽模式

        if (GUILayout.Button("Player Data"))//应用用户数据
        {
			PlayerData.Instance.SetItemNum(PlayerData.ItemType.Coin, CoinCount);
			PlayerData.Instance.SetItemNum(PlayerData.ItemType.Jewel, JewelCount);
			PlayerData.Instance.SetItemNum(PlayerData.ItemType.Score, ScoreCount);
			PlayerData.Instance.SetItemNum(PlayerData.ItemType.ProtectShield, ProtectShieldCount);
			PlayerData.Instance.SetItemNum(PlayerData.ItemType.FlyBomb, FlyBombCount);
			PlayerData.Instance.SetItemNum(PlayerData.ItemType.SpeedUp, SpeedUpCount);
			PlayerData.Instance.SetItemNum(PlayerData.ItemType.Apple, AppleCount);
			PlayerData.Instance.SetItemNum(PlayerData.ItemType.Banana, BananaCount);
			PlayerData.Instance.SetItemNum(PlayerData.ItemType.Pear, PearCount);
			PlayerData.Instance.SetItemNum(PlayerData.ItemType.Strength, CurStrength);
			PlayerData.Instance.SetSignInTimes(SignInTimes);
			PlayerData.Instance.SetCurrentChallengeLevel(CurCallengeLevel);
			PlayerData.Instance.SetIsATypeBagState(IsATypeBag);
			PlayerData.Instance.SetIsBTypeBagState(IsBTypeBag);
			PlayerData.Instance.SetIsCTypeBagState(IsCTypeBag);
			PlayerData.Instance.SetPlayBreathingEffectState(PlayBreathingEffect);
			PlayerData.Instance.SetPlayFingerGuideState(PlayFingerGuide);
			PlayerData.Instance.SetItemNum(PlayerData.ItemType.ColorEgg,ColorEgg);
			PlayerData.Instance.SetIsShowedMonthCardGift(IsMonthCardGift);
			PlayerData.Instance.SetMonthCardGiftAutoRenewState(IsMonthCardGiftAutoRenew);

			if(isWujinModel)
			{
				PlayerData.Instance.SetGameMode(PlayerData.GameMode.WuJin.ToString());
			}else{
				PlayerData.Instance.SetGameMode(PlayerData.GameMode.Level.ToString());
			}

			//直接跳过的的关上加上0星级记录
			int[] mission = {0};
			for(int i = 1; i < CurCallengeLevel; i ++)
			{
				if(PlayerData.Instance.GetLevelStarState(i) == 0)
					PlayerData.Instance.SetLevelStarState(i, 0);
				if(!PlayerData.Instance.GetLevelMissionState(i, 1))
					PlayerData.Instance.SetLevelMissionState(i, mission);
			}
			//重置设置之前玩过的关卡为0星级记录
			for(int i = CurCallengeLevel; i <= 30; i ++)
				PlayerData.Instance.SetLevelStarState(i, 0);

			PayJsonData.Instance.SaveData();
			PlayerData.Instance.SaveData();
		}

		GUILayout.Space(25);

		GetOriData();
		GUILayout.Label ("Carrier settings", EditorStyles.boldLabel, GUILayout.Width(100));//运营商设置
        SelectedYunyinShang = GUILayout.Toolbar(SelectedYunyinShang, Yunyinshang);
		
		switch(SelectedYunyinShang)
		{
		case 0:
			PlayerData.Instance.SetPlatformType("DianXin");
			break;
		case 1:
			PlayerData.Instance.SetPlatformType("LianTong");
			break;
		case 2:
			PlayerData.Instance.SetPlatformType("YiDong");
			break;
		default:
			break;
		}
		
		GUILayout.Space(25);
		
		GUILayout.Label ("Pay Version", EditorStyles.boldLabel, GUILayout.Width(100));//и版本设置
        SelectedPayVersion = GUILayout.Toolbar(SelectedPayVersion, PayVersion);
		
		switch(SelectedPayVersion)
		{
		case 0:
			PlayerData.Instance.SetPayVersionType("LiBaoBai");
			break;
		case 1:
			PlayerData.Instance.SetPayVersionType("LiBaoHei");
			break;
		case 2:
			PlayerData.Instance.SetPayVersionType("ShenHe");
			break;
		case 3:
			PlayerData.Instance.SetPayVersionType("ChangWan");
			break;
		case 4:
			PlayerData.Instance.SetPayVersionType ("GuangDian");
			break;
		default:
			break;
		}

		PlayerData.Instance.SaveData();
	}

	void GetOriData()
	{
		string yunyinType = PlayerData.Instance.GetPlatformType();
		
		switch(yunyinType)
		{
		case "DianXin":
			SelectedYunyinShang = 0;
			break;
		case "LianTong":
			SelectedYunyinShang = 1;
			break;
		case "YiDong":
			SelectedYunyinShang = 2;
			break;
		default:
			SelectedYunyinShang = 0;
			break;
		}
		
		string verType = PlayerData.Instance.GetPayVersionType();
		
		switch(verType)
		{
		case "LiBaoBai":
			SelectedPayVersion = 0;
			break;
		case "LiBaoHei":
			SelectedPayVersion = 1;
			break;
		case "ShenHe":
			SelectedPayVersion = 2;
			break;
		case "ChangWan":
			SelectedPayVersion = 3;
			break;
		case "GuangDian":
			SelectedPayVersion = 4;
			break;
		}
	}
}
