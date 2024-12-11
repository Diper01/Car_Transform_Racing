using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FontContainer : MonoBehaviour {

    public Font font;
	void Start ()
    {
        DontDestroyOnLoad(this);
	}

}
