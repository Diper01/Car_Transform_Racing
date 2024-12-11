using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class LocalisedString : MonoBehaviour {

	public AdditionalSettingForLanguage[] AdditionalSettingForLanguages;
	TextMesh CurrentTextMesh;
    EasyFontTextMesh easyFontTextMesh;
    bool updated = false;

	void Start ()
    {

        easyFontTextMesh = GetComponent<EasyFontTextMesh>();
        //if (!Application.isPlaying) return;
        UpdateString();
    }

    public void UpdateString()
    {

        //if (GetComponent<TextMesh>() != null)
        //{
        //    CurrentTextMesh = transform.GetComponent<TextMesh>();
        //    LocaliseString();
        //    ApplyAdditionalStringSetting();
        //}
        //else
        //{
        if (easyFontTextMesh)
        {
            string finalTxt = Localisation.GetString(easyFontTextMesh.text);
            if (finalTxt == "Unknown string")
            {
                Debug.LogError("Unknown string: " + easyFontTextMesh.text, easyFontTextMesh.gameObject);
                easyFontTextMesh.text = UpdateWordByWord(easyFontTextMesh.text);
                ReworkString(easyFontTextMesh);
            }
            else
            {
                easyFontTextMesh.text = finalTxt;
            }
        }

            //tk2dTextMesh не поддерживает китайский язык.
            //tk2dTextMesh tk2DTextMesh = GetComponent<tk2dTextMesh>();
            //if (tk2DTextMesh != null && Localisation.GetString(tk2DTextMesh.text) != "Unknown string")
            //{
            //    tk2DTextMesh.text = Localisation.GetString(tk2DTextMesh.text);
            //    ReworkString(easyFontTextMesh);
            //}
            //else if (tk2DTextMesh != null && Localisation.GetString(tk2DTextMesh.text) == "Unknown string")
            //{
            //    tk2DTextMesh.text = UpdateWordByWord(tk2DTextMesh.text);
            //    ReworkString(easyFontTextMesh);
            //}

        //}


    }

    void ReworkString(EasyFontTextMesh easyFontTextMesh)
    {

        if (easyFontTextMesh!=null&&easyFontTextMesh.text.Contains("\n"))
        {
            return;
        }
        if (easyFontTextMesh != null&&SceneManager.GetActiveScene().name != "GameScene")
        {
            int size = easyFontTextMesh.LineOfNum;
            easyFontTextMesh.text = TextLayer.RebuildString(easyFontTextMesh.text, size);
        }

    }


    string UpdateWordByWord(string Phrase)
    {
        string localised;
        if (!Phrase.Contains(" ")) return Phrase;
       string[] words = Phrase.Split(' ');
        for(int i=0;i<words.Length;i++)
        {
            localised = Localisation.GetString(words[i]);
            if (localised != "Unknown string")
            {
               // Debug.Log("Localised word - " + words[i]);
                words[i] = localised;
            }
        }
        Phrase = "";
        for (int i = 0; i < words.Length; i++)
        {
            Phrase += words[i] + " ";
        }
        return Phrase;
    }


	void LocaliseString(){
		string CurrentLocalisedString = Localisation.GetString (CurrentTextMesh.text);
		CurrentTextMesh.text = CurrentLocalisedString;
	}

	void ApplyAdditionalStringSetting()
    {
		if(AdditionalSettingForLanguages.Length > 0){
			for(int i = 0;i < AdditionalSettingForLanguages.Length; i++){
				if(Localisation.CurrentLanguage == AdditionalSettingForLanguages[i].Language){
					CurrentTextMesh.fontSize =  AdditionalSettingForLanguages[i].FontSize;
					if(AdditionalSettingForLanguages[i].FontFile != null){
					CurrentTextMesh.font = AdditionalSettingForLanguages[i].FontFile;
					CurrentTextMesh.GetComponent<Renderer>().sharedMaterial = AdditionalSettingForLanguages[i].FontFile.material;

					}
				}
			}
		}
	}
}

[Serializable]
public class AdditionalSettingForLanguage{
	public Languages Language;
	public int FontSize;
	public Font FontFile;
}
