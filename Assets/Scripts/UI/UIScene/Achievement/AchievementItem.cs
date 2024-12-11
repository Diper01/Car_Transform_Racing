using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public enum GetState
{
	NowGet = 1,
	BeforeGet,
	AfterGet
};
public class AchievementItem : MonoBehaviour {

   
	public GameObject goGetButton;
	public tk2dSprite IconImage;
	public ProgressBarNoMask progress;
	public EasyFontTextMesh DescText;
	public EasyFontTextMesh perfectDescText;
	public EasyFontTextMesh NowGetText;
    //public tk2dTextMesh DescText2;

    public tk2dTextMesh ProgressText;
	public tk2dTextMesh CoinCountText, JewelCountText;
	public tk2dTextMesh TitleText;

	public ParticleSystem Particle1,Particle2;
	public ParticleSystem OnClickParticle;

	public GameObject BeforeGet, NowGet, AfterGet, ContentGO;

//	[HideInInspector]
	public int achievementId;
    public int achievementIndex;
	[HideInInspector]
	public int achievementLevel;
	private int[] rewardIdArr;
	private int[] rewardCountArr;
	private int curNum;
	private int targetNum;
	[HideInInspector]
	public GetState getState;
	private bool isCumulative;

    private Transform maskUp, maskDown;
    EasyFontTextMesh beforeGetText;
    public GameObject afterGetText;
    private tk2dUIItem nowGetUIItem;
    IEnumerator PlayParticle()
	{
		//Particle1.gameObject.SetActive (true);
		//Particle1.Play ();
		yield return new WaitForSeconds (0.05f);
		Particle2.gameObject.SetActive (true);
		Particle2.Play ();
	}

    private void Awake()
    {
        nowGetUIItem = NowGet.GetComponent<tk2dUIItem>();
    }
    private void Start()
    {
        beforeGetText = BeforeGet.GetComponent<EasyFontTextMesh>();
    }
    void OnDisable()
	{
		Particle1.gameObject.SetActive (false);
		Particle2.gameObject.SetActive (false);
	}

	public void Init(int achievementId, int achievementIndex,Transform _maskUp, Transform _maskDown)
	{
		this.achievementId = achievementId;
		this.achievementIndex = achievementIndex;
        maskUp = _maskUp;
        maskDown = _maskDown;

        InitData ();
	}


	public void InitData()
	{
		achievementLevel = AchievementData.Instance.GetLevel (achievementId);
		
		SetIconImage (AchievementData.Instance.GetIconName (achievementId));
		//SetTitleText (AchievementData.Instance.GetTitleName (achievementId));
		SetDescText(AchievementData.Instance.GetDesc (achievementId));
		
		isCumulative = AchievementData.Instance.GetIsCumulative (achievementId);

		curNum = (int)PlayerData.Instance.GetAchievementCurrentNum (achievementIndex);
		targetNum = AchievementData.Instance.GetTargetNum (achievementId);
		
		SetProgressText (curNum, targetNum);
		SetProgressSlider ((float)curNum / targetNum);
		
		if (PlayerData.Instance.GetAchievementAreadyIsGet (achievementIndex) == 0) {
			if (curNum < targetNum)
				getState = GetState.BeforeGet;
			else
				getState = GetState.NowGet;
		} else {
			getState = GetState.AfterGet;
		}

		SetGetState (getState);

		rewardIdArr = AchievementData.Instance.GetRewardIdArr (achievementId);
		rewardCountArr = AchievementData.Instance.GetRewardCountArr (achievementId);
		SetCoinCountText (rewardCountArr [0]);
		SetJewelCountText (rewardCountArr [1]);
	}
    private void Update()//shisty wizard!
    {

        CheckObjPos(DescText.gameObject, 150, 155);
        CheckObjPos(NowGetText.gameObject, 160, 165);
        CheckObjPos(perfectDescText.gameObject, 145, 150);
        CheckObjPos(afterGetText, 145, 150);


        //CheckObjPos(beforeGetText.gameObject, 89, 69);


        if ((maskUp.position - transform.position).sqrMagnitude <= (100 * 100) || (maskDown.position - transform.position).sqrMagnitude <= (95 * 95))
        {
            if (beforeGetText.OrderInLayer == 0)//Only if changed. Performanse complete)
                return;

            beforeGetText.OrderInLayer = 0;
        }
        else
        {
            if (beforeGetText.OrderInLayer == 50)
                return;

            beforeGetText.OrderInLayer = 50;
        }
    }
    //bool shich = true;
    private void CheckObjPos(GameObject obj, float down, float up)
    {
        if ((maskUp.position - obj.transform.position).sqrMagnitude <= (up * up) || (maskDown.position - obj.transform.position).sqrMagnitude <= (down * down))
        {
            //if (!shich)
            //    return;

            //shich = false;
            obj.SetActive(false);
        }
        else
        {
            //if (shich)
            //    return;

            //shich = true;
            obj.SetActive(true);
        }

    }
    public void SetIconImage(string iconName)
	{
		IconImage.SetSprite(iconName);
	}

	public void SetDescText(string desc)
    {
        DescText.isDirty = true;
        DescText.text = Localisation.GetString(desc);
	}
	public void SetPerfectDescText(string desc)
	{
		perfectDescText.isDirty = true;
		perfectDescText.text = Localisation.GetString(desc);
	}
	public void SetTitleText(string title)
	{
        //Debug.Log ("setTitle :: ---> title");
		TitleText.text = title;
	}

	public void SetProgressSlider(float percent)
	{
		progress.SetProgress (percent);
	}

	public void SetProgressText(int curNum, int targetNum)
	{
		ProgressText.text = curNum + "/" + targetNum;
	}

	public void SetCoinCountText(int coinCount)
	{
		CoinCountText.text = "x" + coinCount;
	}

	public void SetJewelCountText(int jewelCount)
	{
		JewelCountText.text = "x" + jewelCount;
	}

	public void SetGetState(GetState getState)
	{
		this.getState = getState;
		switch (getState) {
		case GetState.BeforeGet:
			BeforeGet.SetActive(true);
			NowGet.SetActive(false);
                PublicSceneObject.Instance.RemoveButtonFromSelectList(nowGetUIItem);
			ContentGO.SetActive(true);
			AfterGet.SetActive(false);
			break;
		case GetState.NowGet:
			BeforeGet.SetActive(false);
			NowGet.SetActive(true);

                if (gameObject.activeInHierarchy)
                    PublicSceneObject.Instance.AddButtonToSelectList(nowGetUIItem);
                ContentGO.SetActive(true);
			AfterGet.SetActive(false);
			break;
		case GetState.AfterGet:
			BeforeGet.SetActive(false);
			NowGet.SetActive(false);
                PublicSceneObject.Instance.RemoveButtonFromSelectList(nowGetUIItem);
                ContentGO.SetActive(false);

			SetPerfectDescText(AchievementData.Instance.GetDesc(achievementId));
			AfterGet.SetActive(true);
			break;
		}
	}

	void GetOnClick()
	{
		AudioManger.Instance.PlaySound(AudioManger.SoundName.ButtonClick);
		AudioManger.Instance.PlaySound(AudioManger.SoundName.CashMachine);
		//OnClickParticle.Play ();
		if(getState == GetState.NowGet)
		{
			if(achievementLevel < 3)
			{
				if(isCumulative)
				{
					PlayerData.Instance.SetAchievementCurrentNum (achievementIndex, curNum);
				}
				else
				{
					PlayerData.Instance.SetAchievementCurrentNum (achievementIndex, 0);
				}
				++ achievementId;
				PlayerData.Instance.SetAchievementIds(achievementIndex, achievementId);

				AchievementCheckManager.Instance.CheckAgainAfterGetReward (achievementId);
			}
			else
			{
				SetGetState(GetState.AfterGet);
				PlayerData.Instance.SetAchievementAreadyIsGet(achievementIndex, achievementId);
			}

			AchievementControllor.Instance.RefreshData ();
            AchievementControllor.Instance.MoveItemPosAfterGet(transform);

			for(int i=0; i<rewardIdArr.Length; ++i)
			{
				PlayerData.Instance.AddItemNum(RewardData.Instance.GetItemType(rewardIdArr[i]), rewardCountArr[i]);
			}
			StartCoroutine(PlayParticle());

		}
	}
}
