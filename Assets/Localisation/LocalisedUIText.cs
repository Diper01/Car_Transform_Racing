using System;
using UnityEngine;
using UnityEngine.UI;


public class LocalisedUIText : MonoBehaviour {

	Text currentText;
    public AdditionalSettingForUIText[] additionalSettingForUIText;

	// Use this for initialization
	void Start () {

        currentText = GetComponent<Text>();
        if (currentText != null)
        {
		    LocaliseString ();
            ApplyAdditionalStringSetting();
		}
	}
	
	void LocaliseString()
    {
        string text = Localisation.GetString(currentText.text);
        if(text == "Unknown string")
        {
            Debug.LogError("Unknown string: " + currentText.text);
        }
        currentText.text = text;
	}

    void ApplyAdditionalStringSetting()
    {
        if (additionalSettingForUIText.Length > 0)
        {
            for (int i = 0; i < additionalSettingForUIText.Length; i++)
            {
                if (Localisation.CurrentLanguage == additionalSettingForUIText[i].language)
                {
                    if (additionalSettingForUIText[i].fontFile != null)
                    {
                        currentText.font = additionalSettingForUIText[i].fontFile;
                    }
                }
            }
        }
    }
}

[Serializable]
public struct AdditionalSettingForUIText
{
    public Languages language;
    public Font fontFile;
}