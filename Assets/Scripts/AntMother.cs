using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntMother : MonoBehaviour
{
    [SerializeField] int anthillCapacity = 5;
    [SerializeField] float processingTime = 2;
    [SerializeField] GameObject egg;
    [SerializeField] GameObject ant;

    ControllerAI controllerAI;

    int currentAmount = 0;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Mushroom" && anthillCapacity > currentAmount)
        {
            BornNewEgg();
            Destroy(other.gameObject);
        }
    }


    private void BornNewEgg()
    {
        egg.SetActive(true);  
        if(currentAmount < anthillCapacity)
        {
            Invoke("BornNewAnt", processingTime);
            Invoke("OffEgg", processingTime);
            controllerAI = FindObjectOfType<ControllerAI>();
            
        }
    }

    private void BornNewAnt()
    {
        Instantiate(ant, new Vector3(-8.65f, 0, 7.82f), Quaternion.identity);
        currentAmount++;
        controllerAI.UpdateMassiveAnts();
    }

    private void OffEgg()
    {
        egg.SetActive(false);
    }

}
