using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Chasing : MonoBehaviour
{
   
    public Transform sheep;
    public Transform fence;
    public float speed = 2.0f;
    public float rotSpeed = 1.0f;
    //==================================
    public float xSpread =20;
    public float zSpread = 20;
    public float y0offset;
    public bool rotateClocwise;
    float timer = 0;
    bool goalReached = false;
    //======================
    private NavMeshAgent Dog;
    public float OrdibitngRange = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        Dog = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * rotSpeed;

        //Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);
        //Vector3 direction = lookAtGoal - this.transform.position;

        //transform.rotation = (Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed));
        //transform.Translate(0, 0, speed * Time.deltaTime);
        //==============================
        float distance = Vector3.Distance(transform.position, sheep.transform.position);
        float distSheepFence = Vector3.Distance(fence.transform.position, sheep.transform.position);
        float disDogFence = Vector3.Distance(transform.position, fence.transform.position);
        if (distance > OrdibitngRange)
        {
            //Vector3 dirToSheep = transform.position - goal.transform.position;
            //Vector3 newPos = transform.position - dirToSheep;
            //Dog.SetDestination(newPos);
            Vector3 lookAtGoal = new Vector3(sheep.position.x, this.transform.position.y, sheep.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;

            transform.rotation = (Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed));
            transform.Translate(0, 0, speed * Time.deltaTime);
            Debug.Log("Reaching the sheeps");
        }
       if(distance < OrdibitngRange && !isBetween())
        {
            Rotate();
            Debug.Log("In rotate");
        }
       else if (disDogFence< distSheepFence)
        {
            Rotate();
        }
        if (isBetween()&& disDogFence > distSheepFence)
        {
            Vector3 lookAtGoal = new Vector3(fence.position.x, this.transform.position.y, fence.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;

            transform.rotation = (Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed));
            transform.Translate(0, 0, speed * Time.deltaTime);
            Debug.Log("Pushing the sheeps");
        }


    }
    void Rotate()
    {
        if (rotateClocwise)
        {
            float x = -Mathf.Cos(timer) * xSpread;
            float z = Mathf.Sin(timer) * zSpread;
            Vector3 pos = new Vector3(x, y0offset, z);
            transform.position = pos + sheep.position;
        }
        else
        {
            float x = Mathf.Cos(timer) * xSpread;
            float z = Mathf.Sin(timer) * zSpread;
            Vector3 pos = new Vector3(x, y0offset, z);
            transform.position = pos + sheep.position;
        }
    }

    bool isBetween()
    {
        //float crossproduct = (fence.position.z- this.transform.position.z)*(sheep.position.x - this.transform.position.x) -(fence.position.x - this.transform.position.x) *(sheep.position.z - this.transform.position.z);
        //Debug.Log(""+crossproduct);
        //if (crossproduct == 0)
        //{
        //    Debug.Log("Tacci tua");
        //    return true;
        //}
        //====================================
        //float AB =(fence.position.z - this.transform.position.z) /(fence.position.x - this.transform.position.x);
        //float BC = (this.transform.position.z - sheep.position.z) / (this.transform.position.x - sheep.position.x);
        //float AC = (fence.position.z - sheep.position.z) / (fence.position.x - sheep.position.x);
        //if (AB == BC && AB == AC && AC== BC)
        //{
        //    Debug.Log("SLOPE FUNGE");
        //    return true;
        //}
        float triangleArea = this.transform.position.x * (sheep.position.z - fence.position.z) + sheep.position.x * (fence.position.z - this.transform.position.z) + fence.position.x * (this.transform.position.z - sheep.position.z);

        if (50 >triangleArea && triangleArea > -50)
        {
            Debug.Log("PORCO DIO");
            return true;
        }
      
        return false;
    }
}
