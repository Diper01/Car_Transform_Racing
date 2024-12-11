using UnityEngine;
using System.Collections;
using PathologicalGames;
using DG.Tweening;

/// <summary>
/// 开场道具
/// </summary>
public class GameSkillControl : UIBoxBase {
	public static GameSkillControl Instance;

    //public Animation anim;
    public Animator anim;
    
    private float Duration = 4;

	public override void Init ()
	{
		Instance = this;
		base.Init();
	}

	public override void Show ()
	{
		base.Show ();
        anim.SetTrigger("Start");
		StartCoroutine ("IEDelayHide");
	}

	public override void Hide ()
	{
		base.Hide ();
		GameUIManager.Instance.HideModule (UISceneModuleType.GameSkill);
        gameObject.SetActive(false);
	}

	public override void Back ()
	{
	}

	[HideInInspector]
	public float ColorA = 1.0f;
	IEnumerator IEDelayHide()
	{
		//CountDownSprite.gameObject.SetActive(true);
		AudioManger.Instance.PlaySound (AudioManger.SoundName.StartGo);


        float count = Duration;
        while (count > 0)
        {
            count--;
            //CountDownSprite.gameObject.SetActive (true);
            //CountDownSprite.SetSprite ("Countdown_" + (count-- <= 1 ? "go" : (count).ToString ()));
            AudioManger.Instance.PlaySound(AudioManger.SoundName.CountDown);
            //CountDownSprite.transform.localScale = Vector3.zero;
            //this.ColorA = 1.0f;
            //CountDownSprite.color = new Color (1, 1, 1, this.ColorA);
            //DOTween.To (() => this.ColorA, x => this.ColorA = x, 0.5f, 0.85f).SetEase (Ease.Linear).OnUpdate (ChangeColorA);
            //CountDownSprite.transform.DOScale (Vector3.one * 1.5f, 0.85f).SetEase (Ease.Linear).OnComplete (CompleteEvent);
            yield return new WaitForSeconds(0.85f);
        }
        
        GameController.Instance.StartGo ();
		Hide ();
	}
}