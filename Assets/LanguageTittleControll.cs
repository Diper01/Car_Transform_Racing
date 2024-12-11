using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageTittleControll : MonoBehaviour {

    public EasyFontTextMesh textTittle;
    EasyFontTextMesh text;
    private void Start()
    {
        text = GetComponent<EasyFontTextMesh>();
        ChangeDescription();
    }
    public void ChangeDescription ()
    {
        switch (textTittle.text)
        {
            case "Arabic":
                text.text = "تحتاج إلى إعادة تشغيل";
                break;
            case "Chinese":
                text.text = "需要重新启动";
                text.Size = 15;
                break;
            case "ChineseTraditional":
                text.text = "需要重新啟動";
                break;
            case "English":
                text.text = "*Need to restart";
                break;
            case "French":
                text.text = "*Besoin de redémarrer";
                break;
            case "German":
                text.text = "*Muss neu gestartet werden";
                break;
            case "Italian":
                text.text = "*Bisogno di riavviare";
                break;
            case "Polish":
                text.text = "*Musisz ponownie uruchomić";
                break;
            case "Portuguese":
                text.text = "*Precisa reiniciar";
                break;
            case "Russian":
                text.text = "*Нужен перезапуск";
                break;
            case "Turkish":
                text.text = "*Yeniden başlatılması gerekiyor";
                break;
            case "Ukrainian":
                text.text = "*Потрібен перезапуск";
                break;
            case "Urdu":
                text.text = "دوبارہ شروع کرنے کی ضرورت ہے";
                break;
            default:
                text.text = "*Need to restart";
                break;
        }
	}


}
