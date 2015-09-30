using UnityEngine;
using System.Collections;

public class Entrance : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Rendering.RenderingMgr.Instance.Init();
        WindowMgr.Instance.Init();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
