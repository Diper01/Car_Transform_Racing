using UnityEngine;
using System.Collections;

public enum GameGuideStep{Left,Right,UseCurProp,UseFlyBmob,UseSpeedup}
public class GuideItem : ItemBase {
	public GameGuideStep curStep = GameGuideStep.Left;
	public override void GetItem (PropControl  propCon= null)
	{
		if(propCon==null)
		{
			return;
		}
		if(propCon.isPlayer == false)
		{
			return;
		}

		if(PlayerData.Instance.IsWuJinGameMode())
		{
			return;
		}

		switch (curStep)
		{
		case GameGuideStep.Left:
			if(PlayerData.Instance.GetCurrentChallengeLevel()==1)
			{
				GameController.Instance.PauseGame();
				UIGuideControllor_GameClone.Instance.Show(UIGuideType.GamePlayerUILeftGuide);
                    if (!SceneButtonsController._keyboard)
                    {
                        UIGuideControllor_GameClone.Instance.ShowBubbleTipByID(8);
                    }
                    else
                    {
                        UIGuideControllor_GameClone.Instance.ShowBubbleTipByID(17);
                    }
			}
			break;
		case GameGuideStep.Right:
			if(PlayerData.Instance.GetCurrentChallengeLevel()==1)
			{
				GameController.Instance.PauseGame();
                    UIGuideControllor_GameClone.Instance.Show(UIGuideType.GamePlayerUIRightGuide);
                    if (!SceneButtonsController._keyboard)
                    {
                        UIGuideControllor_GameClone.Instance.ShowBubbleTipByID(9);
                    }
                    else
                    {
                        UIGuideControllor_GameClone.Instance.ShowBubbleTipByID(18);
                    }
			}
			break;
		case GameGuideStep.UseCurProp:
			if(PlayerData.Instance.GetCurrentChallengeLevel()==1)
			{
				GameController.Instance.PauseGame();
                    UIGuideControllor_GameClone.Instance.Show(UIGuideType.GamePlayerUIUseCurPropGuide);

                    if (!SceneButtonsController._keyboard)
                    {
                        UIGuideControllor_GameClone.Instance.ShowBubbleTipByID(10);
                    }
                    else
                    {
                        UIGuideControllor_GameClone.Instance.ShowBubbleTipByID(19);
                    }
			}
			break;
		case GameGuideStep.UseSpeedup:
			if(PlayerData.Instance.GetCurrentChallengeLevel()==2  )
			{

					PlayerData.Instance.SetItemNum(PlayerData.ItemType.SpeedUp, 2);
				GameController.Instance.PauseGame();
                    UIGuideControllor_GameClone.Instance.Show(UIGuideType.GamePlayerUIUseSpeedupGuide);
                    UIGuideControllor_GameClone.Instance.ShowBubbleTipByID(12);
			}
			break;
		case GameGuideStep.UseFlyBmob:
			if(PlayerData.Instance.GetCurrentChallengeLevel()==2 )
			{

					PlayerData.Instance.SetItemNum(PlayerData.ItemType.FlyBomb, 2);
					GameController.Instance.PauseGame();
                    UIGuideControllor_GameClone.Instance.Show(UIGuideType.GamePlayerUIUseFlyBmobGuide);

                    if (!SceneButtonsController._keyboard)
                    {
                        UIGuideControllor_GameClone.Instance.ShowBubbleTipByID(11);
                    }
                    else
                    {
                        UIGuideControllor_GameClone.Instance.ShowBubbleTipByID(20);
                    }
			}
			break;
		}
	
		gameObject.SetActive (false);
	}
}
