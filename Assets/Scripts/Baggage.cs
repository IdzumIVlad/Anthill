using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baggage : MonoBehaviour
{
    [SerializeField] public ResSlot[] resSlots;
    [SerializeField] public int antCapacity = 1;
    [SerializeField] public int currentResAmount;

    [SerializeField] public GameObject halfApple; //  преф должен быть на самом объекте, который дает ресурс, а сюда его только прокидывал
    //потом объект появляется в жвалах муравья, если скидываем, то пишем parent == null
    [SerializeField] GameObject seed;
    [SerializeField] GameObject mushroom;

    [System.Serializable]
    public class ResSlot
    {
        public ResTypes resTypes;
        public int resAmount;
    }

    public int GetCurrentRes(ResTypes resTypes)
    {
        return GetResSlot(resTypes).resAmount;
    }

    public void DecreaseRes(ResTypes resTypes) // СДЕЛАТЬ: не только обнулять но и отдавать в казну
    {
        int currentAmount = GetResSlot(resTypes).resAmount;
        //for ( ; currentAmount > 0; currentAmount--) // необходимо только если увеличивать вместимость муравья
        //{
            switch (resTypes)
            {
                case ResTypes.Apple:
                    Instantiate(halfApple, gameObject.transform.position, Quaternion.identity);
                break;

                case ResTypes.Seed:
                    Instantiate(seed, gameObject.transform.position, Quaternion.Euler(90, 0, -90));
                break;

                case ResTypes.Mushroom:
                    Instantiate(mushroom, gameObject.transform.position, Quaternion.identity);
                break;
            }
            
        //}
        GetResSlot(resTypes).resAmount = 0;
        currentResAmount = 0;
        
    }

    public void IncreaseRes(ResTypes resTypes, int amount)
    {
        GetResSlot(resTypes).resAmount += amount;
        currentResAmount += amount;
    }

    private ResSlot GetResSlot(ResTypes resTypes)
    {
        foreach(ResSlot slot in resSlots)
        {
            if(slot.resTypes == resTypes)
            {
                return slot;
            }
        }
        return null;
    }

    public void GrabButton()
    {
        PickUpResourses []pickUps = FindObjectsOfType<PickUpResourses>();
        foreach(PickUpResourses pickUp in pickUps)
        {
            float minDistance = 10;
            
            float distance = Vector3.Distance(transform.position, pickUp.transform.position);
            if (distance <= minDistance)
                pickUp.PickUpRes();
        }
    }
}
