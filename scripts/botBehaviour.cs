using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botBehaviour : MonoBehaviour
{
    public bool isNPC = true;
    public bool canmove = false;
    public bool invertRaycast = false;
    public float linspeed = 6;
    public float angspeed = 4;
    public float attack = 5;
    public float health = 100;
    public float time = 0f;

    private Rigidbody rb;
    public GameObject enemy; // only for NPC control
    public botBehaviour enemyScript;

    float horizontal = 0f;
    float vertical = 0f;

    private Vector3 raycastdirection;

    Vector3 angleVelocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(invertRaycast){
            raycastdirection = Vector3.back;
        }else{
            raycastdirection = Vector3.forward;
        }
        Time.timeScale = 1.0f;
    }
    void Update(){
        time += Time.deltaTime;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(time > 10f){
            if(isNPC){
                NPCMove();
            }else{
                playerMove();
            }
        }
        print("I be like sheeesh");
    }

    void playerMove(){
        rb.velocity = transform.forward * linspeed * Input.GetAxis("Vertical");

        angleVelocity = new Vector3(0, angspeed * 70 * Input.GetAxis("Horizontal"), 0);
        Quaternion deltaRotation = Quaternion.Euler(angleVelocity * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);

        if(Input.GetKey(KeyCode.Space)){
            Attack();
        }
    }

    void Attack(){
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(raycastdirection), Color.green, 1f);
        if(Physics.Raycast(transform.position, transform.TransformDirection(raycastdirection), out hit, 1f)){
            /*
            
            botBehaviour enemyScript = enemy.GetComponent<botBehaviour>();
            print(enemyScript);
            
            */
            if(enemyScript.health > 0){
                enemyScript.health -= attack * 0.02f;
                print(enemyScript.health);
            }else{
                Destroy(enemy);
            }

        }
    }

    void NPCMove(){
        //calculate movement

        horizontal = Random.Range(-1,1);
        vertical = Random.Range(-1,1);


        //actually move object
        rb.velocity = transform.forward * linspeed * vertical;
        angleVelocity = new Vector3(0, angspeed * 70 * horizontal, 0);
        Quaternion deltaRotation = Quaternion.Euler(angleVelocity * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
        //print("(" + horizontal + "," + vertical + ")");
        Attack();
    }
}
