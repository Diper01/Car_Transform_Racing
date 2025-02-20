﻿using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using System;

/// <summary>
/// 道具攻击提示
/// </summary>
public class PropTips : MonoBehaviour {

	public static PropTips Instance=null;

	public GameObject typeUseGO,typeBeHit,typeHit;
	public Image useSprite,beHitSprite,hitSprite;
	public Text useText,beText,hitText;

    public Sprite[] icons;
	private bool isShowing=false;
	private Transform curTransform;

	void Awake()
	{
		Instance=this;
	}

	public void ShowUse(int carId,int propId)
	{
		if(isShowing)
			return;

		string nameStr= ModelData.Instance.GetName(carId);
		string icon = PropConfigData.Instance.GetIconName(propId);
        
		useSprite.sprite = Array.Find(icons, i => i.name == icon);
		useText.text=nameStr+" used";
		ShowAnimate(typeUseGO.transform);
	}

	public void ShowBeHit(PropType propType)
	{
		if(isShowing)
			return;
		int propId = (int)propType;
		string icon = PropConfigData.Instance.GetIconName(propId);
		beHitSprite.sprite = Array.Find(icons, i => i.name == icon);
        ShowAnimate(typeBeHit.transform);
	}

	public void ShowHit(PropType propType)
	{
		if(isShowing)
			return;
		int propId = (int)propType;
		string icon = PropConfigData.Instance.GetIconName(propId);
		hitSprite.sprite = Array.Find(icons, i => i.name == icon);
        ShowAnimate(typeHit.transform);
	}

	void ShowAnimate(Transform trans)
	{
		isShowing=true;
		this.curTransform = trans;
		
		curTransform.gameObject.SetActive(true);
		curTransform.transform.localScale = Vector3.zero;
		Sequence seq= DOTween.Sequence();
		seq.Append(curTransform.transform.DOScale(2f,0.1f));
		seq.Append(curTransform.transform.DOScale(1f,0.2f));
		seq.Append(curTransform.transform.DOScale(0,0.2f).OnComplete(Recycle).SetDelay(1f));
	}
	void Recycle()
	{
		curTransform.gameObject.SetActive(false);
		isShowing=false;
	}
}
