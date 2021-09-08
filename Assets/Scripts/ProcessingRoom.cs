using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessingRoom : MonoBehaviour
{
    [SerializeField] int roomCapacity = 5;
    [SerializeField] float processingTime = 3;

    int currentAmount = 0;

    [SerializeField] GameObject indicator;
    [SerializeField] GameObject mushroomPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Seed" && roomCapacity > currentAmount)
        {
            StartCoroutine(PaintInGreen());
            Destroy(other.gameObject);
        }
    }


    IEnumerator PaintInGreen()
    {
        for (int i = 0; i < indicator.transform.childCount; i++)
        {
            indicator.transform.GetChild(i).GetComponent<MeshRenderer>().material.color = Color.green;
            yield return new WaitForSeconds(processingTime / 3);
        }

        // если подвинешь комнату - грибы заспавнятся под подушкой (transform.position + рандом в пределах комнаты)
        // OnDrawGizmos в помощь
        Instantiate(mushroomPrefab, new Vector3(Random.Range(-10.8f, -14.03f), 0.16f, Random.Range(1.67f, 8.09f)), Quaternion.identity);
        currentAmount++;

        for (int i = 0; i < indicator.transform.childCount; i++)
        {
            indicator.transform.GetChild(i).GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
    } 
}
