﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class HintInGameControllor : UIBoxBase {

	public static HintInGameControllor Instance;

	public delegate void HideEventHander();
	public event HideEventHander HideHintTextEvent;
    public Animator anim;
	public GameObject failGO;
	
	private float modelDelayHideTime = 2;

	public enum HintType
	{
		RoleRelay,
		DeadFly,
		Win,
		Fail
	}

	#region 重写父类方法
	public override void Init ()
	{
		Instance = this;
		base.Init();
	}
	
	public override void Show ()
	{
		base.Show();
		//transform.localPosition = ShowPosV2;

//		transform.localPosition = GlobalConst.FirstLeftHidePos;
//		DOTween.Kill("HintInGame");
//		transform.DOLocalMove(GlobalConst.FirstShowPos, GlobalConst.PlayTime).SetEase(Ease.OutBack).SetId("HintInGame");

		StartCoroutine(HideHintIE());
	}

	IEnumerator HideHintIE()
	{
		yield return new WaitForSeconds(modelDelayHideTime);
		Hide();
//		DOTween.Kill("HintInGame");
//		transform.DOLocalMove(GlobalConst.FirstRightHidePos, GlobalConst.PlayTime).SetEase(Ease.InBack).SetId("HintInGame").OnComplete(Hide);
	}

	public override void Hide()
	{
		GameUIManager.Instance.HideModule(UISceneModuleType.HintInGame);

		if(HideHintTextEvent != null)
			HideHintTextEvent();
	}
	
	public override void Back ()
	{
		Hide();
	}
	#endregion

	public void InitData(HintType type, HideEventHander callBack)
	{

		switch(type)
		{
		case HintType.RoleRelay:
			modelDelayHideTime = 2;
			break;
		case HintType.DeadFly:
			modelDelayHideTime = 2;
			break;
		case HintType.Win:
			AudioManger.Instance.PlaySound(AudioManger.SoundName.WinCheer);
			modelDelayHideTime = 3;
            AudioManger.Instance.PlaySound(AudioManger.SoundName.CompleteLevel);
                
            Invoke("DelayPlayLihua",0.8f);
			break;
		case HintType.Fail:
			failGO.SetActive(true);
			break;
		}
		HideHintTextEvent = callBack;
	}

	void DelayPlayLihua()
	{
        anim.SetTrigger("EndGame");
    }
}
