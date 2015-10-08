using UnityEngine;
using System.Collections;

public class Entrance : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Rendering.RenderingMgr.Instance.Init();
        Rendering.PostProcessUnits.PPUBlackAndWhite testppu = new Rendering.PostProcessUnits.PPUBlackAndWhite();
        WindowMgr.Instance.Init();
        Rendering.RenderingMgr.Instance.AddUnitAtLast(testppu);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
