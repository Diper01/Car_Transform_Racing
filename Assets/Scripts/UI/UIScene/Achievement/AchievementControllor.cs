using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using DG.Tweening;

public class AchievementControllor : UIBoxBase {

	public static AchievementControllor Instance;

	public GameObject goCloseButton;
	public Transform ItemContainer;
	public tk2dUIScrollableArea scroll;

    public Transform maskUp, maskDown;

    private float ItemLength = 115;
	private float posY;
	private List<AchievementItem> AchievementItemList = new List<AchievementItem> ();
	private int itemCount;
	private Transform itemTran;
	private AchievementItem itemScript;

	private int CompareAchievementItem(AchievementItem x, AchievementItem y)
	{
		int ret = x.getState.CompareTo (y.getState);
		if (ret == 0) {
			ret = x.achievementLevel.CompareTo (y.achievementLevel);
			if (ret == 0)
				return x.achievementId.CompareTo (y.achievementId);
			else
				return ret;
		} else {
			return ret;
		}
	}

    //private Vector3 pos = new Vector3(0, 42, 0);
    //private void FixedUpdate()
    //{
    //    if(transform.position != pos)
    //        transform.position = pos;//Fix this pls!
    //}

    public override void Init ()
	{
		Instance = this;
		InitData ();

		base.Init();
	}

	public void RefreshData()
	{
		int achievementCount = 0;
		for(int i=0; i<itemCount; ++i)
		{
			itemTran = ItemContainer.Find("AchievementItem" + i);
			itemScript = itemTran.GetComponent<AchievementItem>();
            itemScript.InitData();
			if(itemScript.getState == GetState.NowGet)
				++ achievementCount;
		}
		MainInterfaceControllor.Instance.SetAchievementCount (achievementCount);
	}

	private void InitData()
	{
		AchievementCheckManager.Instance.Init ();
		int[] achievementIds = PlayerData.Instance.GetAchievementIds ();
		itemCount = achievementIds.Length;

		SpawnPool spUIItems = PoolManager.Pools["UIItemsPool"];

		int achievementCount = 0;
		for(int i=0; i<itemCount; ++i)
		{
			itemTran = spUIItems.Spawn("AchievementItem");
			itemTran.parent = ItemContainer;
			itemTran.name = "AchievementItem" + i;
			itemTran.localPosition = new Vector3(0, 110 - ItemLength * i, 0);
			itemTran.localScale = Vector3.one;
			itemScript = itemTran.GetComponent<AchievementItem> ();
			itemScript.Init (achievementIds[i], i,maskUp,maskDown);
			if(itemScript.getState == GetState.NowGet)
				++ achievementCount;

			AchievementItemList.Add(itemScript);
		}
		scroll.ContentLength = (itemCount + 0.9f) * ItemLength;
		scroll.VisibleAreaLength = 3.7f * ItemLength;

		MainInterfaceControllor.Instance.SetAchievementCount (achievementCount);
	}

	private void SortAchievementItem()
	{
		AchievementItemList.Sort (CompareAchievementItem);
		for (int i=0; i<AchievementItemList.Count; ++i) {
			//print(AchievementItemList[i].achievementId + " : " + AchievementItemList[i].getState);
			AchievementItemList[i].transform.localPosition = new Vector3(0, 110 - ItemLength * i, 0);
		}
	}

    public void MoveItemPosAfterGet(Transform curTrans)
    {
        curTrans.DOLocalMoveX(1800, 0.2f);//1500
        curTrans.DOLocalMoveY(-300, .2f);//

        AchievementItemList.Sort(CompareAchievementItem);

        for (int i=0; i<AchievementItemList.Count; ++i) {
            AchievementItemList[i].transform.DOLocalMove(new Vector3(0, 110 - ItemLength * i), 0.2f).SetDelay(0.2f + i * 0.05f);
        }
    }
	
	public override void Show ()
	{
		base.Show();
		SortAchievementItem ();
        scroll.Value = 0;
	}
	
	public override void Hide ()
	{
		base.Hide ();
		UIManager.Instance.HideModule (UISceneModuleType.Achievement);
	}
	
	public override void Back ()
	{
		AudioManger.Instance.PlaySound(AudioManger.SoundName.CloseBtClick);
		Hide();
	}

	void CloseOnClick()
	{
		AudioManger.Instance.PlaySound(AudioManger.SoundName.CloseBtClick);
		Hide();
        //foreach(EasyFontTextMesh item in FindObjectsOfType<EasyFontTextMesh>())
        //{
        //    item.RefreshMesh(true);
        //}
	}
	public void SetCenterPosition(tk2dUIItem item=null)
	{
		int index = 0;
		if (item != null) {
			if (item.sendMessageTarget == null)
				return;
			AchievementItem achieveItem = item.sendMessageTarget.GetComponent<AchievementItem> ();
			if (achieveItem == null)
				return;
			index = achieveItem.achievementIndex;
		}
		
		Transform itemTran = ItemContainer.Find ("AchievementItem" + index);
		float height = this.scroll.ContentLength - scroll.VisibleAreaLength;
		float moveY = -itemTran.localPosition.y;
		if (moveY < 0) {
			ItemContainer.localPosition = new Vector3 (0, 0, 0.3f);
		} else if (moveY > height) {
			ItemContainer.localPosition = new Vector3 (0, height, 0.3f);
		} else {
			ItemContainer.localPosition = new Vector3 (0, moveY, 0.3f);
		}
	}

}
