using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformMovil : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag =="Player" && other is BoxCollider2D){
            GameObject.Find("Player").GetComponent<Transform>().SetParent(this.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag =="Player" && other is BoxCollider2D){
            GameObject.Find("Player").GetComponent<Transform>().parent =null;
        }
    }
}
