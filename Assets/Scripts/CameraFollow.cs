using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target, pointReset;
    public Vector3 offSet = new Vector3(-2.5f, -3f, -10f);
    public float dampingTime= 0.3f;
    public Vector3 velocity = Vector3.zero;
    Vector3 destination;


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

    public void MoveCamera(bool isTouchingTheGround){
      
        if(!isTouchingTheGround){
             destination = new Vector3(target.position.x-offSet.x,
                                            this.transform.position.y,
                                            offSet.z);
            
        }else{
            destination = new Vector3(target.position.x-offSet.x,
                                            target.position.y-offSet.y,
                                            offSet.z);
          
        }
          this.transform.position = Vector3.SmoothDamp(this.transform.position,
                                            destination,
                                            ref velocity,
                                            dampingTime);
    }

    public void ResetCamera(){
        this.transform.position = new Vector3(pointReset.position.x-offSet.x,
                                        pointReset.position.y,
                                        pointReset.position.z);
        
    }
}
