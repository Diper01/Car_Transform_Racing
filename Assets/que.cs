using UnityEngine;
using System.Collections;

public class que : MonoBehaviour {

    public int queue = 4999;
    float count = 0;
    string curentWord;
    void Awake()
    {

    }
    void Start()
    {
        GetComponent<Renderer>().material.renderQueue = queue;
    }

	
	
}
