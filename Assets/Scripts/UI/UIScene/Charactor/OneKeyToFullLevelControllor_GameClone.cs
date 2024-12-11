using UnityEngine;
using UnityEngine.UI;
using System;
using PayBuild;


public class OneKeyToFullLevelControllor_GameClone : UIBoxBase {

	public static OneKeyToFullLevelControllor_GameClone Instance;
    public GameObject goCancelBtn;
    public Text textTitle;//, textHint1, textHint2, textDesc1, textDesc2, textPayBt;
	public Image imageCloseBt;

	public Image IconImage;
	public Text NameText;
    public Sprite[] sprites = new Sprite[5];


    public bool bOneKeyToFullLevelBoxIsHidden;

	private int modelId;
	private int modelType;
	private int maxLevel;

	#region 重写父类方法
	public override void Init ()
	{
		Instance = this;

		base.Init();
	}

	public void InitData(int modelId)
	{
		this.modelId = modelId;
		modelType = IDTool.GetModelType (modelId);
		maxLevel = ModelData.Instance.GetMaxLevel (modelId);
	}

	public override void Show ()
	{
		base.Show();
		bOneKeyToFullLevelBoxIsHidden = false;

		NameText.text = ModelData.Instance.GetName (modelId);
		IconImage.sprite = Array.Find(sprites, o => o.name == ModelData.Instance.GetPlayerIcon(modelId));

		//PayUIConfigurator.PayUIConfig(PayType.OneKey2FullLV, textHint1, textHint2, textDesc1, textDesc2, textPayBt, imageCloseBt, goGetButton.GetComponent<BoxCollider>(), SetCancelBt);
		textTitle.text = PayJsonData.Instance.GetGiftTitle(PayType.OneKey2FullLV);

		//if (PlatformSetting.Instance.PayVersionType == PayVersionItemType.ShenHe) {
		//	goGetButton.transform.Find("UI_libao01_hg").gameObject.SetActive(false); //审核版不显示按钮特效，更换特效时要改名称
		//}
	}
	
	void SetCancelBt(bool state)
	{
		if(state)
		{
			goCancelBtn.SetActive(true);
			//goCancelBtn.transform.localPosition = new Vector3(-50, -122, 0);
			//goGetButton.transform.localPosition = new Vector3(128, -122, 0);
		}else
		{
			goCancelBtn.SetActive(false);
			//goGetButton.transform.localPosition = new Vector3(100, -122, 0);
		}
	}

	public override void Hide ()
	{
		base.Hide ();
		if(GlobalConst.SceneName == SceneType.UIScene)
			UIManager.Instance.HideModule (UISceneModuleType.OneKeyToFullLevel);
		else
			GameUIManager.Instance.HideModule (UISceneModuleType.OneKeyToFullLevel);

		bOneKeyToFullLevelBoxIsHidden = true;
	}

	public override void Back ()
	{
		AudioManger.Instance.PlaySound (AudioManger.SoundName.ButtonClick);
		Hide ();
	}
	#endregion

	#region 按钮控制
	private void PayCallBack(string result)
	{
		if (result.CompareTo ("Success") != 0)
			return;
		Hide ();
		PlayerData.Instance.SetOneKeyToFullLevelGetedState(true);
		PlayerData.Instance.UpdateModelState (modelType * 100 + maxLevel);
        
	}

	public void CloseOnClick()
	{
		AudioManger.Instance.PlaySound(AudioManger.SoundName.CloseBtClick);
		Hide();
	}

	public void CancelOnClick()
	{
		AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
		Hide();
	}

	public void GetOnClick()
	{
		AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
		PayBuildPayManage.Instance.Pay ((int)PayType.OneKey2FullLV, PayCallBack);
		
	}
	#endregion
}
