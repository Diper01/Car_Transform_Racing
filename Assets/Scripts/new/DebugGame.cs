using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DebugGame : MonoBehaviour {
    private EventSystem eventSystem;
	void Awake () {
        DontDestroyOnLoad(gameObject);
        eventSystem = EventSystem.current;
    }

    //void Update()
    //{
    //    if (Input.GetButtonDown("Submit") || Input.GetKeyDown((KeyCode)10))
    //    {
    //        if (eventSystem.currentSelectedGameObject)
    //            Debug.Log("Selected: " + eventSystem.currentSelectedGameObject.name);
    //    }
    //}
}
