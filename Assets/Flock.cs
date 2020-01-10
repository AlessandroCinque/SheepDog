using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flock : MonoBehaviour
{
    private NavMeshAgent _agent;
    public GameObject Player;
    public float EnemyDistanceRun = 4.0f;
    float speed = 5.0f;
    float rotationSpeed = 4.0f;
    Vector3 averageHeading;
    Vector3 averagePosition;
    float neightbourDistance = 2.0f;
    bool turning = false;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        speed = Random.Range(0.5f,1);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);
        if (distance < EnemyDistanceRun)
        {
            speed = 11.0f;
            Vector3 dirToPlayer = transform.position - Player.transform.position;
            Vector3 newPos = transform.position + dirToPlayer;
            _agent.SetDestination(newPos);
        }
        //if (Vector3.Distance(transform.position, Vector3.zero) >= Flocking.praireSize)
        //{
        //    turning = true;
        //}
        //else
        //{
        //    turning = false;
        //}
        //if (turning)
        //{
        //    Vector3 direction = Vector3.zero - transform.position;
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        //    speed = Random.Range(0.5f,1);
        //}
        //else
        //{
            //if (Random.Range(0, 5) < 1)
            //{
                //This make them flock
                ApplyRules();
          //  }
      //  }
        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    void ApplyRules()
    {
        GameObject[] gos;
        gos = Flocking.allSheep;
        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;

        float gSpeed = 0.1f;
        Vector3 goalPos = Flocking.goalPos;
        float dist;
        int groupSize = 0;
        foreach (GameObject go in gos)
        {
            if (go!= this.gameObject)
            {
                dist = Vector3.Distance(go.transform.position, this.transform.position);
                if (dist <= neightbourDistance)
                {
                    vcentre += go.transform.position;
                    groupSize++;
                    if (dist <1.0f)
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }
                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }
        if (groupSize > 0)
        {
            vcentre = vcentre / groupSize + (goalPos - this.transform.position);
            speed = gSpeed / groupSize;

            Vector3 direction = (vcentre + vavoid) - transform.position;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            }
     
        }
    }
    void FleeFools()
    {
            
    }
}
