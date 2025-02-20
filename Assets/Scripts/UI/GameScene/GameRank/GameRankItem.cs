﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 游戏排名数据
/// </summary>
public class GameRankItem : MonoBehaviour {

	public Text rankIndexText;
	public Text nameText,timeText,unFinishText;
	public Image iconSprite;
	public int carId;
	//public GameObject particleGO;
    public Sprite[] icons;

	public void SetData(int rank,int carId,float useTime,bool isPlayer)
	{
		rankIndexText.text= rank.ToString();
		string nameStr= ModelData.Instance.GetName(carId);
		string iconStr= ModelData.Instance.GetPlayerIcon(carId);
		nameText.text= nameStr;
        
        iconSprite.sprite = System.Array.Find(icons, ic=>ic.name==iconStr);

		if(useTime<0)
		{
			unFinishText.gameObject.SetActive(true);
			timeText.gameObject.SetActive(false);
			if(CarManager.Instance.gameLevelModel == GameLevelModel.Weedout)
			{
				unFinishText.text="Finished";
			}else
			{
				unFinishText.text= "Un Finish";
			}
		}else
		{
			string timeStr= SecondsToTimeStr(useTime);
			timeText.text= timeStr;
			timeText.gameObject.SetActive(true);
			unFinishText.gameObject.SetActive(false);
		}

		//particleGO.SetActive(isPlayer);
	}
	
	string SecondsToTimeStr(float sec)
	{
		string sSecond;
		string sMinute;
		string sMinSec;
		int iSecond;
		int iMinute;
		
		int minSec =(int)(( sec - (int)sec)*100);
		if(minSec<10)
			sMinSec="0"+minSec;
		else
			sMinSec=minSec.ToString();
		
		
		int iSec =(int)sec;
		iSecond = iSec % 60;
		if (iSecond < 10)
			sSecond = "0" + iSecond;
		else
			sSecond = iSecond + "";
		
		iMinute = iSec / 60;
		sMinute = iMinute + "";
		
		return sMinute + ":" + sSecond+":"+sMinSec;
	}

}
