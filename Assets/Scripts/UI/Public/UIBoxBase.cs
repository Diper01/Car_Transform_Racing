using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIBoxBase : UIBase
{	
	[Range(-1f, 1f)] public float alignX;
	[Range(-1f, 1f)] public float alignY;
	
	public UISceneModuleType preBoxType = UISceneModuleType.MainInterface;

	[HideInInspector]public List<tk2dUIItem> BoxAllButtonList = new List<tk2dUIItem>();
    [HideInInspector]public List<ButtonEntities> buttonEntities = new List<ButtonEntities>();

    public override void Init(){
		BoxCollider collider;
		gameObject.GetComponentsInChildren<tk2dUIItem> (true, BoxAllButtonList);
        gameObject.GetComponentsInChildren(true, buttonEntities);
        
        for (int i = BoxAllButtonList.Count - 1; i >= 0; i--)
		{
			if ((collider = BoxAllButtonList[i].GetComponent<BoxCollider>()) == null|| !BoxAllButtonList[i].HasOnClickEvent)
			{
				BoxAllButtonList.Remove(BoxAllButtonList[i]);
			}
			else
			{
				if(!collider.enabled)
					BoxAllButtonList.Remove(BoxAllButtonList[i]);
			}
		}
        if(SceneButtonsController.Instance!=null)
        SceneButtonsController.Instance.RegisterAllButton(buttonEntities);
        PublicSceneObject.Instance.RegisterBoxAllButton(BoxAllButtonList, 1 << gameObject.layer);
	}

    public override void Show(){
		gameObject.SetActive(true);

        if (buttonEntities.Count > 0)
        {
            SceneButtonsController.Instance.SetActiveButtons(buttonEntities);
        }

        //foreach (EasyFontTextMesh item in FindObjectsOfType<EasyFontTextMesh>())
        //{
        //    //item.RefreshMeshEditor();
        //    //item.isDirty = true;
        //}

        if (GetComponent<UIBoxTween>() != null)
			GetComponent<UIBoxTween>().ShowUIBoxTween();
	}
	public override void Hide(){

        if (buttonEntities.Count > 0)
        {
            SceneButtonsController.Instance.ClearActiveButtons();
        }

        foreach (EasyFontTextMesh item in FindObjectsOfType<EasyFontTextMesh>())
        {
            //item.RefreshMeshEditor();
            item.isDirty = true;
        }
        if (GetComponent<UIBoxTween>() != null)
			GetComponent<UIBoxTween>().HideUIBoxTween();
	}
	public override void Back(){}
	
	
	// ------------------- RESOLUTION MOVING COMPENSATION -------------------------------
	
	internal Vector3 ShowPosV2 
	{
		get 
		{
			return UIResAdjust.Adjust(GlobalConst.ShowPos, alignX, alignY);
		}
	}
}
