using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RightClickNav : MonoBehaviour
{
    public Transform goal;
    public NavMeshAgent agent;
    public Camera myCamera;
    public RaycastHit hit;
    public GameObject hint;
    public GameObject hintParent;

    void Start(){
        agent = GetComponent<NavMeshAgent>();
        //agent.destination = goal.position;
        myCamera = Camera.main;

    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Mouse1)){
            Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)){
                Transform objectHit = hit.transform;
                Debug.Log("You hit" + objectHit);

                AgentMove(hit.point);
                ReplaceHint(hit.point);
            }
        }

        //VerticleControl();
    }

    void AgentMove(Vector3 pos){
        agent.destination = pos;
    }
    void VerticleControl(){
        Ray ray = new Ray(transform.position + 0.5f * transform.localScale, -transform.up); 
        float maxDis = 0.48f * transform.localScale.y;
        float minDis = 0.52f * transform.localScale.y;

        if (Physics.Raycast(ray, out hit, maxDis)){
            Debug.Log("Lift_Up");
            float add_Y= 0.5f * transform.localScale.y-hit.distance  ;
            transform.position += new Vector3(0, add_Y, 0);
        }
        else if (!Physics.Raycast(ray, out hit, minDis))
        {
            Debug.Log("Lift_Down");
            float minus_Y = hit.distance - transform.position.y;
            transform.position -= new Vector3(0, minus_Y, 0);
        }

    }

    void ReplaceHint(Vector3 pos){

        if(FindObjectOfType<Hint>() != null){
            Hint oldHint = FindObjectOfType<Hint>();
            Destroy(oldHint.gameObject);
        }


        GameObject hintObject = Instantiate(hint,pos, Quaternion.identity, hintParent.transform);

    }
}
