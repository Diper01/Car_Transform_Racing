using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;

/// <summary>
/// 赛车游戏的道具生成类
/// </summary>
public class CreatePropManager : MonoBehaviour {

	public static CreatePropManager Instance=null;
	
	/*测试控制*/
	public enum State{ Run,TestList,TestLevel,TestWujing}
	public State runState=State.Run;

	public int curLevel = 1;
	public bool isWuJingModel=false;
	public List<string> itemGroupIDList=new List<string>();
	public int idListIndex = 0;
	public  int insertId=5000;
	//Item信息列表
	public List<ItemContentStru> itemContentList = new List<ItemContentStru> ();
	public int itemContentListIndex = 0;

	//场景中的Item Transform 列表
	public List<ItemContentStru> usingItemContentList = new List<ItemContentStru> ();
	private List<Transform> groupList = new List<Transform> ();



	private SpawnPool itemPool;
	private Transform playerTrans;
	private int updateCheckCount=0;
	private float propGroupTotalLen=0;
	private CarMove playerCarMove;
	private float preInsertGroupLen=0f;


	void Awake()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		itemPool = PoolManager.Pools["ItemPool"];

		playerTrans = PlayerCarControl.Instance.transform;
		playerCarMove = PlayerCarControl.Instance.carMove;

		switch(runState)
		{
		case State.Run:
			bool isWujing = PlayerData.Instance.IsWuJinGameMode();
			if(isWujing)
			{
				itemGroupIDList= WuJingConfigData.Instance.GetIDList();
				isWuJingModel = true;
			}else
			{
				curLevel = PlayerData.Instance.GetSelectedLevel();
				itemGroupIDList = GameLevelData.Instance.GetGroupIDList(curLevel);
				isWuJingModel = false;
			}
			break;
		case State.TestLevel:
			itemGroupIDList= GameLevelData.Instance.GetGroupIDList(curLevel);
			break;
		case State.TestList:
			break;
		case State.TestWujing:
			itemGroupIDList= WuJingConfigData.Instance.GetIDList();
			break;
		}
	}

	void Update()
	{
		UpdateCheckItem ();
	}

	/// <summary>
	/// 根据角色的位置来决定 Item 的生成和回收
	/// </summary>
	void UpdateCheckItem()
	{
		if (updateCheckCount == 0)//Зачем пересоздаём группу?
        {			
			CreateGroup();
			CreateItem();
			RecycleItem();

			RecycleGroup();
		}
		
		++updateCheckCount;
		
		if (updateCheckCount > 5)//Reset all every 5 frame
        {
			updateCheckCount =0;
		}
	}

	/// <summary>
	/// 创建一个道具组
	/// </summary>
	void CreateGroup()
	{
		float createZ = playerCarMove.moveLen;
		createZ +=1000f; //group 生成的向前偏移量
		
		bool isCreate = (groupList.Count == 0) || (propGroupTotalLen < createZ);
		if (isCreate) {
			
			if(isWuJingModel)
			{
				
				int id =0;// Random.Range(idRangeMin,idRangeMax+1);
				
				if(groupList.Count ==0)
				{
					id = int.Parse( itemGroupIDList[0] );
					idListIndex =0;
				}else
				{
					idListIndex= (++idListIndex)%itemGroupIDList.Count;
					idListIndex = (idListIndex>0)?idListIndex:1;
					id =int.Parse( itemGroupIDList[idListIndex] );
				}
				
				//Debug.Log("wujin index "+ idListIndex +" id "+id);
				
				AddToItemContentListByID(id);
				//Debug.Log("Wujing "+id.ToString());
			}else{
				
				string id="";
				/*最后一段路*/
				if(idListIndex >= itemGroupIDList.Count)
				{
					idListIndex =1;
				}
				
				id = itemGroupIDList[idListIndex];
				++idListIndex;
			
				AddToItemContentListByID( int.Parse(id));	
			}
		}
	}

    /// <summary>
    /// 把信息加入的 主要ItemContentList 中
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// 
    List<ItemContentStru> addToList;
    GameObject groupAdd;
    private void AddToItemContentListByID(int id)
	{
        //Debug.Log ("add group "+id);
        if (!groupList.Exists(g => g.name == id.ToString()))
        {

            groupAdd = new GameObject(id.ToString());//

            groupAdd.transform.parent = transform;
            groupAdd.transform.localPosition = Vector3.zero;
            //groupAdd.SetActive(true);


            groupList.Add(groupAdd.transform);
        }
        else
        {
            groupAdd = groupList.Find(g => g.name == id.ToString()).gameObject;
        }

        addToList = ItemGroupData.Instance.GetContentList(id);
        for (int i=0; i< addToList.Count; ++i) {
            addToList[i].parent = groupAdd.transform;
            addToList[i].pathZ = propGroupTotalLen+ addToList[i].pos.z;
			itemContentList.Add(addToList[i]);
		}

        propGroupTotalLen += ItemGroupData.Instance.GetGroupLen(addToList);
        
	}

    /// <summary>
    ///变身的时候加入大量的障碍车
    /// </summary>
    /// 
    List<ItemContentStru> insertGruopList;
    GameObject groupInsertGruop;
    public void InsertGruop()
	{
        float curMoveLen = PlayerCarControl.Instance.carMove.moveLen;
		if(curMoveLen-preInsertGroupLen <200f  && preInsertGroupLen>0f)
		{
			return;
		}
		preInsertGroupLen = curMoveLen;

        if (groupInsertGruop == null)
            groupInsertGruop = new GameObject(insertId.ToString());

        float grupStartLen = curMoveLen+100;
        groupInsertGruop.transform.parent = transform;
        groupInsertGruop.transform.localPosition = Vector3.zero;
        groupInsertGruop.SetActive(true);
		groupList.Add (groupInsertGruop.transform);

        insertGruopList = ItemGroupData.Instance.GetContentList (insertId);
		for (int i=0; i< insertGruopList.Count/2; ++i) {
            insertGruopList[i].parent = groupInsertGruop.transform;
            insertGruopList[i].pathZ = grupStartLen+ insertGruopList[i].pos.z;
			itemContentList.Insert(itemContentListIndex, insertGruopList[i]);
		}
		
		//propGroupTotalLen += ItemGroupData.Instance.GetGroupLen (list);
	}


	/// <summary>
	/// Creates the item.
	/// </summary>
	public void CreateItem()
	{
		float createZ = playerCarMove.moveLen;
		createZ += 900f; //Item 生成的向前偏移量

		int createCount=0;

		for (int i= itemContentListIndex; i<itemContentList.Count; ++i) {
			
			if(itemContentList[i].parent ==null)
			{
				continue;
			}
			float itemPosZ = itemContentList[i].pathZ ;
			
		
			if(  itemPosZ <= createZ)
			{
				//create item object
				ItemContentStru itemInfo = itemContentList[i];
				Transform itemTran = itemPool.Spawn( itemInfo.prefabName);
				itemTran.parent = itemInfo.parent;
				
				Vector3 curvePos = RoadPathManager.Instance.GetPointByLen(itemPosZ,itemInfo.pos.x);
				Vector3 curveHead=  RoadPathManager.Instance.GetPointByLen(itemPosZ+1f ,itemInfo.pos.x);
				Vector3 direcetion = curveHead - curvePos;
				
				itemTran.localPosition = curvePos;
				itemTran.forward = direcetion;
				
				ItemOtherInfo.Instance.SetOtherInfoOfTrans(itemTran,itemInfo.otherInfo);
				
				ItemBase item=itemTran.GetComponent<ItemBase>();
				item.Init();

				/*需要特殊处理 障碍车*/
				NormalCarItem carItem= itemTran.GetComponent<NormalCarItem>();
				if(carItem != null)
				{
					carItem.InitNormalCarPos(itemPosZ,itemInfo.pos.x);
					itemInfo.isCar = true;
				}else
				{
					itemInfo.isCar = false;
				}

				itemContentList[i].selfTrans = itemTran;
				usingItemContentList.Add(itemContentList[i]);
				itemContentListIndex++;

				/*控制每一帧最多新增10个物体*/
				++createCount;
				if(createCount>10)
				{
					break;
				}
			
			}else
			{
				break;
			}
		}
		
	}

	/// <summary>
	/// Recycles the item.
	/// </summary>
	public void RecycleItem()
	{
		//回收Item的位置 = player - 偏移值
		float recycleZ = playerCarMove.moveLen - 200f;
		
		int i = 0;
		while (i<usingItemContentList.Count) {
			
			if(usingItemContentList[i]==null || usingItemContentList[i].selfTrans ==null)
			{
				Debug.LogError(" some where destroy object");
				usingItemContentList.RemoveAt(i);

			}else if(usingItemContentList[i].isCar)
			{  
				/*回收障碍车*/
				CarMove carMove = usingItemContentList[i].selfTrans.GetComponent<CarMove>();
				if(carMove != null && carMove.moveLen <recycleZ)
				{
					carMove.isRunning = false;
					Transform trans =usingItemContentList[i].selfTrans;
					trans.gameObject.SetActive(false);
					trans.parent= itemPool.transform;
					itemPool.Despawn(trans);
					usingItemContentList.RemoveAt(i);
					//Debug.Log(trans.name + " Recycle");
				}else
				{
					++i;
				}

			}
			else if( usingItemContentList[i].pathZ <recycleZ)
			{
				/*回收普通道具*/
				Transform trans =usingItemContentList[i].selfTrans;
				trans.parent= itemPool.transform;
				itemPool.Despawn(trans);
				usingItemContentList.RemoveAt(i);
			}else
			{
				++i;
			}
		}
	}

	private void RecycleGroup()
	{
		//回收Item的位置 = player - 偏移值
		float recycleZ = playerCarMove.moveLen - 200f;
		//recycle gruop
		if (groupList.Count >2) {
			
			Transform group=groupList [0];
			if(group.childCount >0 )
			{
				return;
			}

            //Destroy(groupList[0].gameObject);
            groupList[0].gameObject.SetActive(false);

            groupList.RemoveAt(0);
			
			//recycle itemContentList
			itemContentList.RemoveRange (0, itemContentListIndex);
			itemContentListIndex = 0;
		}
	}
}
