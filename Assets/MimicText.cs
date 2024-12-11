using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicText : MonoBehaviour {


    EasyFontTextMesh me;
    tk2dTextMesh mesh;


    void Start()
    {
        me = GetComponent<EasyFontTextMesh>();
        mesh = GetComponentInParent<tk2dTextMesh>();
        //me.originalString = mesh.text;
        me.text = Localisation.GetString(mesh.text);
        //me.RefreshLocalisedString();
        mesh.text = "";
        GetComponent<Renderer>().sortingLayerName = "Default";
    }
	
	//// Update is called once per frame
	//void Update ()
 //   {
 //     //  GetComponent<Renderer>().sortingOrder = 50;
 //       GetComponent<Renderer>().sortingLayerName = "Default";
 //       GetComponentInParent<tk2dTextMesh>().text = "";
 //   }
}
