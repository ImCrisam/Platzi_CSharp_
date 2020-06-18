using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform target;
    public Vector3 offSet = new Vector3(-2.5f, -0.5f, -10f);
    public float dampingTime= 0.3f;
    public Vector3 velocity = Vector3.zero;


    private void Awake() {
        Application.targetFrameRate=60;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    public void ResetCameraPosition(){
        MoveCamera(false);
    }

    public void SetTraget(Transform transform){
        this.target = transform;
    }

    public void MoveCamera(bool smooth){
        Vector3 destination = new Vector3(target.position.x-offSet.x,
                                            target.position.y-offSet.y,
                                            offSet.z);
        if(smooth){
            this.transform.position = Vector3.SmoothDamp(this.transform.position,
                                            destination,
                                            ref velocity,
                                            dampingTime);
        }else{
            this.transform.position = destination;
        }
    }
}
