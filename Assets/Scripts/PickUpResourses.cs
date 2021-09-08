using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickUpResourses : MonoBehaviour
{
    [SerializeField] public int capacity = 5;
    [SerializeField] public ResTypes resTypes;
    [SerializeField] Button grab;
    [SerializeField] TMP_Text buttonText;

    private void Awake()
    {
        grab = GameObject.FindGameObjectWithTag("ButtonGrab").GetComponent<Button>();
        buttonText = grab.transform.GetChild(0).GetComponent<TMP_Text>();

        grab.interactable = false;
    }

    private void OnTriggerStay(Collider other) // trigger должен быть на мураьве, яблоко не должно спрашивать муравья
    {
        if (other.gameObject.tag == "Player") 
        {
            VisibleButtonAndText();
        }


        if (capacity == 0)
        {
            Destroy(gameObject); //телепортировать в новое случайное место, а не дестроить
            grab.interactable = false;
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            grab.interactable = false;
        }
    }

    private void VisibleButtonAndText()
    {
        buttonText.text = "Grab";
        grab.interactable = true;
    }

    public void PickUpRes()
    {
        Baggage baggage = FindObjectOfType<Baggage>();
        if (baggage.currentResAmount >= baggage.antCapacity) return;
        baggage.IncreaseRes(resTypes, baggage.antCapacity);
        capacity -= baggage.antCapacity;
    }


}
