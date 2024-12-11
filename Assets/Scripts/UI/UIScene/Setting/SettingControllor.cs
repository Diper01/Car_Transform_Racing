using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class SettingControllor : UIBoxBase {

	public GameObject goCloseButton, goAboutButton, goMoreButton;
    public EasyFontTextMesh title, text;
    public Transform ContentTran, BottomTran;

	public tk2dUIScrollbar MusicValueBar, SoundValueBar;
	private bool IsChangingMusicVolume = false;
	private bool IsChangingSoundVolume = false;

	public EasyFontTextMesh uicAboutUsText;
    public LanguageTittleControll languageTittleControll;

    private void Start()
    {
        text.text = Localisation.CurrentLanguage.ToString();
        languageTittleControll.ChangeDescription();
    }
    void Update()
	{
		if (IsChangingMusicVolume)
		{
			AudioManger.Instance.MusicVolume = Mathf.Max(MusicValueBar.Value, 0.001f);
		}
		
		if(IsChangingSoundVolume)
		{
			AudioManger.Instance.SoundVolume = SoundValueBar.Value;
		}
	}




    #region Override the parent class method

    public override void Init ()
	{
		AudioManger.Instance.MusicVolume = PlayerData.Instance.GetMusicVolume();
		AudioManger.Instance.SoundVolume = PlayerData.Instance.GetSoundVolume();
		
		MusicValueBar.Value = PlayerData.Instance.GetMusicVolume ();
		SoundValueBar.Value = PlayerData.Instance.GetSoundVolume ();
		
		SetAboutUsText();

		base.Init();
	}

	public override void Show ()
	{
		base.Show();
		ContentTran.transform.localPosition = Vector3.zero;
		BottomTran.transform.localPosition = new Vector3 (0, -200, 0);
		goMoreButton.SetActive (PlatformSetting.Instance.ShowMoreGame);
		goAboutButton.SetActive (PlatformSetting.Instance.ShowAboutInfo);
		if (PlatformSetting.Instance.ShowMoreGame) {
			ContentTran.transform.localPosition = new Vector3 (0, 20, 0);
			if(PlatformSetting.Instance.ShowAboutInfo)
			{
				goMoreButton.transform.localPosition = new Vector3(80, 0, 0);
				goAboutButton.transform.localPosition = new Vector3(-80, 0, 0);
			}
			else
			{
				goMoreButton.transform.localPosition = Vector3.zero;
			}
		}
		if (PlatformSetting.Instance.ShowAboutInfo) {
			ContentTran.transform.localPosition = new Vector3 (0, 20, 0);
			if(PlatformSetting.Instance.ShowMoreGame)
			{
				goMoreButton.transform.localPosition = new Vector3(80, 0, 0);
				goAboutButton.transform.localPosition = new Vector3(-80, 0, 0);
			}
			else
			{
				goAboutButton.transform.localPosition = Vector3.zero;
			}
		}
	}

	public override void Hide ()
	{
		base.Hide ();
		UIManager.Instance.HideModule (UISceneModuleType.Setting);
	}

	public override void Back ()
	{
		AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
		Hide();
	}
    #endregion


    #region Button control
    public void ChangeLanguage()
    {
        string switcher = PlayerPrefs.GetString("SavedLanguage");
        string nextLanguage = "";
        if (switcher == "") switcher = Localisation.CurrentLanguage.ToString();
        switch (switcher)
        {
            case "Arabic":
                nextLanguage = "English";
                break;
            case "English":
                nextLanguage = "French";
                break;
            case "French":
                nextLanguage = "German";
                break;
            case "German":
                nextLanguage = "Italian";
                break;
            case "Italian":
                nextLanguage = "Polish";
                break;
            case "Polish":
                nextLanguage = "Portuguese";
                break;
            case "Portuguese":
                nextLanguage = "Russian";
                break;
            case "Russian":
                nextLanguage = "Turkish";
                break;
            case "Turkish":
                nextLanguage = "Ukrainian";
                break;
            case "Ukrainian":
                nextLanguage = "English";
                break;
            default:
                nextLanguage = "English";
                break;
        }
        text.text = nextLanguage;
        languageTittleControll.ChangeDescription();
        PlayerPrefs.SetString("SavedLanguage", nextLanguage);
        PlayerPrefs.SetString("TestLanguage", nextLanguage);
        PlayerData.Instance.SaveData();
    }

    void CloseOnClick()
	{
		AudioManger.Instance.PlaySound(AudioManger.SoundName.CloseBtClick);
		Hide();
	}

	void MusicVolumeBtDown()
	{
		IsChangingMusicVolume = true;
	}
	
	void MusicVolumeBtUp()
	{
		IsChangingMusicVolume = false;
	}
	
	void SoundVolumeBtDown()
	{
		IsChangingSoundVolume = true;
	}
	
	void SoundVolumeBtUp()
	{
		IsChangingSoundVolume = false;
	}

	void AboutOnClick()
	{
		PlatformSetting.Instance.AboutInfo ();
	}

	void MoreGameOnClick()
	{
		PlatformSetting.Instance.MoreGame ();
	}
    #endregion


    #region Internal method
    void SetAboutUsText()
	{
  //      return;

		//string data = "";
		//int dataCount = AboutUsData.Instance.GetDataRow();
		//List<string> dataList = new List<string> ();
		//for(int i=1; i<=dataCount; ++i)
		//{
		//	string dataTemp = AboutUsData.Instance.GetAboutUsItem(i);
		//	if(string.IsNullOrEmpty(dataTemp))
		//		continue;
		
		//	if (i == 1)
		//		dataTemp = dataTemp.Replace ("AppName", PlatformSetting.Instance.AppName);
		//	else if (i == 2)
		//		dataTemp = dataTemp.Replace ("TelephoneNumber", PlatformSetting.Instance.TelephoneNumber);

		//	dataList.Add(dataTemp);
		//}
		//for (int i=0; i<dataList.Count; ++i) {
		//	data += dataList[i];
		//	if(i < dataList.Count-1)
		//	{
		//		data += '\n';
		//	}
		//}
		//uicAboutUsText.text = data;
	}
	#endregion
}
