using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneBehaviour : MonoBehaviour
{
    public bool myTimeToShine = false;
    public Transform marker1;
    public Transform marker2;
    public GameObject[] blades;
    public GameObject[] lights;
    public Transform marker3;

    private bool hashappened = true;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(myTimeToShine && hashappened){
            transform.position = marker1.position;
            time = Time.fixedTime;
            hashappened = false;
        }else{
            if(myTimeToShine){
                //spin the blades
                foreach(GameObject blade in blades){
                blade.transform.Rotate(0f, 2000f * Time.deltaTime, 0f, Space.Self);
                }
                //lights
                if(((int)(Time.fixedTime * 2)) % 2 == 0){
                    lights[0].SetActive(true);
                    lights[1].SetActive(false);
                    lights[2].SetActive(true);
                    lights[3].SetActive(false);
                }else{
                    lights[0].SetActive(false);
                    lights[1].SetActive(true);
                    lights[2].SetActive(false);
                    lights[3].SetActive(true);
                }
                //sideToside Bobbing
                float zswivel = 4 * Mathf.Sin(0.8f * Mathf.PI * Time.fixedTime);
                transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y, zswivel);

            }
            if(myTimeToShine && transform.position.y >= marker2.position.y){
                transform.Translate(0f,-(3f - (Time.fixedTime - time)) * Time.deltaTime,0f);
                transform.LookAt(marker3);
            }else{
                if(myTimeToShine && transform.position.y < marker2.position.y){
                    float bobby = 0.07f * Mathf.Sin(Mathf.PI * Time.fixedTime) + (marker2.position.y - 0.1f);
                    transform.position = new Vector3(Mathf.Lerp(transform.position.x, marker2.position.x, 0.1f), bobby ,Mathf.Lerp(transform.position.z, marker2.position.z, 0.1f));
                    transform.LookAt(marker3);
                    float zswivel = 4 * Mathf.Sin(0.8f * Mathf.PI * Time.fixedTime);
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y, zswivel);
                }
            }
        }
        
    }
}
