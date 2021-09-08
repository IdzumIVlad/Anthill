using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkersAI : MonoBehaviour
{
    [SerializeField] GameObject appleStorage;
    Baggage baggage;
    private PickUpResourses pickUpResourses;

    // Start is called before the first frame update
    void Start()
    {
        baggage = GetComponent<Baggage>();

    }

    private void Update()
    {
        if (baggage.currentResAmount > 0)
        {
            ResTypes res = ResTypes.Apple;
            for (int i = 0; i < baggage.resSlots.Length; i++)
            {
                if (baggage.resSlots[i].resAmount > 0)
                    res = baggage.resSlots[i].resTypes;
            }
            switch (res)
            {
                case ResTypes.Apple:
                    NewDestination(appleStorage.transform.position);
                    break;

                case ResTypes.Seed:
                    Debug.Log("Seed");
                    break;

                case ResTypes.Mushroom:
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //pickUpResourses = other.gameObject.GetComponent<PickUpResourses>();
        //if (pickUpResourses == null) return;
        if (other.gameObject.tag == "Apple" || other.tag == "Seed")
        {
            pickUpResourses = other.gameObject.GetComponent<PickUpResourses>();
            
            if (baggage.currentResAmount >= baggage.antCapacity) return;
            baggage.IncreaseRes(pickUpResourses.resTypes, baggage.antCapacity);
            pickUpResourses.capacity -= baggage.antCapacity;
        }
        if(other.tag == "AppleStorage")
        {
            baggage.DecreaseRes(ResTypes.Apple);
            Instantiate(baggage.halfApple, gameObject.transform.position, Quaternion.identity);
        }
    }

    public void NewDestination(Vector3 position)
    {
        transform.gameObject.GetComponent<NavMeshAgent>().SetDestination(position);
    }
    

    }



