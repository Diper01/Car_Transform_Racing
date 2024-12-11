using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using System.Xml;

public enum Languages {
	Undefined,
	Unknown,

	Russian,
	Ukrainian,
	Belarusian,

	English,
	Italian,
	Spanish,
	French,
	German,
	Polish,
	Czech,

	Chinese,
	Japanese,
	Korean,

	Afrikaans,
	Arabic,
	Basque,
	Bulgarian,
	Catalan,
	Danish,
	Dutch,
	Estonian,
	Faroese,
	Finnish,
	Greek,
	Hebrew,
	Icelandic,
	Indonesian,
	Latvian,
	Lithuanian,
	Norwegian,
	Portuguese,
	Romanian,
	Slovak,
	Slovenian,
	Swedish,
	Thai,
	Turkish,
	Vietnamese,
	Hungarian,
    Urdu,
    ChineseTraditional
}

public static class Localisation{

	public static Languages CurrentLanguage = Languages.Undefined;
	static public Dictionary<string,string> Strings;
	static private XmlDocument LoadedLanguage;
	static bool LanguageLoaded = false;
	static TextAsset newbyLanguage;

	public static void DetectLanguage()
    {
        if (!Application.isPlaying) return;

        if (!PlayerPrefs.HasKey("SavedLanguage"))
        {
            CurrentLanguage = (Languages)Enum.Parse(typeof(Languages), Application.systemLanguage.ToString());
        }
        else
        {
            CurrentLanguage = (Languages)Enum.Parse(typeof(Languages), PlayerPrefs.GetString("SavedLanguage"));
        }

#if UNITY_EDITOR
        if (PlayerPrefs.HasKey("TestLanguage")){
				CurrentLanguage = (Languages)Enum.Parse (typeof(Languages),PlayerPrefs.GetString("TestLanguage"));
			}
#endif

        PlayerPrefs.SetString("SavedLanguage", CurrentLanguage.ToString());



            Debug.Log ("DetectedLanguage "+ CurrentLanguage);

	}

	static public void LoadLanguage(){
        if (!Application.isPlaying) return;
        DetectLanguage();
		LoadedLanguage = new XmlDocument ();
		Strings = new Dictionary<string, string>();
       // if (PlayerPrefs.GetString("SavedLanguage") != "") CurrentLanguage = (Languages)Enum.Parse(typeof(Languages), PlayerPrefs.GetString("CurrentLanguage"));
        CurrentLanguage = CheckSavedLanguage();
        Debug.Log("LoadLanguage " + CurrentLanguage);
        newbyLanguage = (TextAsset) Resources.Load ("Localisation/" + CurrentLanguage.ToString() + ".xml", typeof(TextAsset));
		if(newbyLanguage == null){
			newbyLanguage = (TextAsset) Resources.Load ("Localisation/English.xml", typeof(TextAsset));
		}
		LoadedLanguage.LoadXml(newbyLanguage.text);
		foreach(XmlNode document in LoadedLanguage.ChildNodes){
			foreach(XmlNode newbyString in document.ChildNodes){
                if(newbyString.NodeType != XmlNodeType.Comment && newbyString.NodeType != XmlNodeType.Text)
				    Strings.Add(newbyString.Attributes["name"].Value,newbyString.InnerText);
			}
		}
		LanguageLoaded = true;

        //PlayerData.Instance.SaveData();
    }

    static Languages CheckSavedLanguage()
    {
        string switcher = PlayerPrefs.GetString("SavedLanguage");
        switch (switcher)
        {
            case "Arabic":
                return Languages.Arabic;
            //case "Chinese":
            //    return Languages.Chinese;
            //case "ChineseTraditional":
            //    return Languages.ChineseTraditional;
            case "Spanish":
                return Languages.Spanish;
            case "English":
                return Languages.English;
            case "French":
                return Languages.French;
            case "German":
                return Languages.German;
            case "Italian":
                return Languages.Italian;
            case "Polish":
                return Languages.Polish;
            case "Portuguese":
                return Languages.Portuguese;
            case "Russian":
                return Languages.Russian;
            case "Turkish":
                return Languages.Turkish;
            case "Ukrainian":
                return Languages.Ukrainian;
            case "Urdu":
                return Languages.Urdu;
            default:
                return Languages.English;
        }
    }

	static public Languages GetCurrentLanguage(){


		if(LanguageLoaded == false){
			LoadLanguage();
		}
		return CurrentLanguage;
	}
	
	static public string GetString(string SearchString)
    {
        //if (CurrentLanguage == Languages.English) return SearchString;
        //if (!Application.isPlaying) return SearchString;

        //Debug.Log(SearchString);
        string before = Special(SearchString);
        if (before != SearchString) return before;

        if (LanguageLoaded == false){
			LoadLanguage();
		}
       
        if (Strings.ContainsKey (SearchString)) {
            //Debug.Log(Strings[SearchString]);
			return Strings [SearchString];
		} else {
			return WordWithNumber(SearchString);
		}		
	}


    // Ниже две функции для подгонки перевода для специальных случаев(задания перед игрой, большие тексты), разделил на 2 ф-ции потому что стаковерфлоу ловит почему то
    static public string Special(string search)
    {
        string needed = "";
        if (search.Contains("Outside little"))
        {
            needed = "Outside little proud";
        }
        else if (search.Contains("Only need 2000"))
        {
            needed = "Only need 2000 gold coins";
        }
        else if (search.Contains("Diamond"))
        {
            needed = "Diamond";
        }
        else if (search.Contains("Shield duration increased"))
        {
            needed = "Shield duration increased 1 second";
        }
        else if (search.Contains("Change time is increased"))
        {
            needed = "Change time is increased by 1 second";
        }
        else if (search.Contains("Missile dizziness increased"))
        {
            needed = "Missile dizziness increased by 1 second";
        }
        else if (search.Contains("Lightning acceleration time"))
        {
            needed = "Lightning acceleration time increased by 10%"; 
        }
        else if (search.Contains("full force"))
        {
            needed = "with full force";
        }
        else if (search.Contains("Invincible"))
        {
            needed = "Full Force Gift";
        }
        else if (search.Contains("Full force"))
        {
            needed = "Role packs";
        }
        //else if (Search.Contains("After the countdown"))
        //{
        //    needed = "After the countdown";
        //}
        else return search;

        needed = Strings[needed];
        return SetNumber(search, needed);
    }

    static public string WordWithNumber(string SearchString)
    {
        string needed ="";
        //if (CurrentLanguage == Languages.English) return SearchString;

        if (SearchString.Contains("Use speed up"))
        {
            needed = "Use speed up times";
        }
        else if (SearchString.Contains("Collect") && SearchString.Contains("gold"))
        {
            needed = "Collect 1 coins";
        }
        else if (SearchString.Contains("Win in"))
        {
            needed = "Win in 1 seconds";
        }
        else if (SearchString.Contains("Rank before"))
        {
            needed = "Rank before 1";
        }
        else if (SearchString.Contains("Hit props limit"))
        {
            needed = "Hit props limit 1 times";
        }
        else if (SearchString.Contains("Less crash"))
        {
            needed = "Less crash 1";
        }
        else if (SearchString.Contains("Use") && SearchString.Contains("props"))
        {
            needed = "Use props";
        }
        else if (SearchString.Contains("Crash flyers"))
        {
            needed = "Crash flyers 1 times";
        }
        else if (SearchString.Contains("Sweet and cute"))
        {
            needed = "Sweet and cute.\nI love pink";
        }
        else if (SearchString.Contains("Very forgetful"))
        {
            needed = "Very forgetful.\nPut the gren";
        }
        else if (SearchString.Contains("Head can be broken"))
        {
            needed = "Head can be broken.\nHair can't be chaos";
        }
        else if (SearchString.Contains(" gold"))
        {
            needed = "Gold";
        }
        else return "Unknown string";

        needed = GetString(needed);
        return SetNumber(SearchString,needed);
    }

    // search входная строка, needed выходная уже взятая из хмл файла
    
    static string SetNumber(string Search, string needed)
    {
        Debug.Log(Search);
        string[] words = Search.Split(' ');
        double number;
        for(int i=0;i<words.Length;i++)
        {
            Double.TryParse(words[i], out number);
            if(number!=0)
            {
                
                needed = needed.Replace("  ", " " + words[i]);
                Debug.Log(needed);
                return needed;
            }
        }
        if (needed.Contains("  ")&&SceneManager.GetActiveScene().name != "GameScene")
        {
            needed = needed.Replace("  ", " 1" + 1);
            return needed;
        }
        return needed;
        //return "Unknown string";
    }
}