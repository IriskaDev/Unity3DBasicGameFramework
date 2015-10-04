using UnityEngine;
using System.Collections;
using Rendering;

public class RenderingNodeTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Camera cam = GetComponent<Camera>();
        Rendering.RenderingUnit unit = new RenderingUnit("SceneCam", cam);
        RenderingMgr.Instance.AddCrucialUnitAtLast("SceneCam", unit);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
