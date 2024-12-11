using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoController : MonoBehaviour {

    public Image logo;
    public Sprite eng, ru;

	void Start ()
    {
        //ApplicationChrome.navigationBarState = ApplicationChrome.States.Hidden;
        //ApplicationChrome.statusBarState = ApplicationChrome.States.Hidden;
        if (PlayerPrefs.GetString("SavedLanguage") != "")
        {
            if (PlayerPrefs.GetString("SavedLanguage") == "Russian")
            {
                logo.sprite = ru;
                logo.SetNativeSize();
            }
            else logo.sprite = eng;
        }
        else
        {
            if (Application.systemLanguage == SystemLanguage.Russian)
            {
                logo.sprite = ru;
                logo.SetNativeSize();
            }
            else
            {
                logo.sprite = eng;
            }
        }
	}
	

}
