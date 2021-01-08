using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    public GameObject myDestinationContainer;
    public Animator animatorZ4;

    GameManager myGameManager;

    GameObject[] mydestinationArray;
    GameObject[] myDestinationArray2;
    NavMeshAgent myNavMeshAgent;
    private int destinationIndex=-1;

    public int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        mydestinationArray = myDestinationContainer.GetComponent<DestinationContainer>().AllDestinations;
        myGameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        SetNextDestination();
    }

    void SetNextDestination() {
        destinationIndex++;
        if (destinationIndex == mydestinationArray.Length) destinationIndex--;
        animatorZ4.SetBool("InMotion", true);
        myNavMeshAgent.SetDestination(mydestinationArray[destinationIndex].transform.position);
      
    }


    private void OnTriggerEnter(Collider other)
    {   
        
        if (other.gameObject.tag == "LosingTrigger") {
            print("Game Is Over");
            myGameManager.GameIsOver = true;
        }

        if (other.gameObject.tag == "CutterTrap") {

            TakeDamage(15);
          
        }

       // Hits twice
        if (other.gameObject.tag == "FireTrap") {
            TakeDamage(25);
        }

        if (other.gameObject.tag == "BladeTrap") {

            GameObject bladeChild = null;
            foreach (Transform child in other.gameObject.transform) {
                if (child.CompareTag("BladeChild")) {
                    bladeChild = child.gameObject;
                } }
         
            if (bladeChild.transform.position.y > -1) {
               
                TakeDamage(30);
           }
        }
        print("Enemy health = " + health);
    }

    void TakeDamage(int damageAmt)
    {
        animatorZ4.SetBool("IsHit", true);
        health -= damageAmt;
   
        if (health <= 0)
        {
            animatorZ4.SetBool("NoHealth", true);
            Destroy(gameObject);
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (myNavMeshAgent.remainingDistance < 0.1) {
            SetNextDestination();
        }
       
    }


}
