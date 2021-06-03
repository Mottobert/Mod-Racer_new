using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelectorController : MonoBehaviour
{
    public GameObject selectedCar;
    public GameObject[] allCars;

    public int carPointer = 0;
    public List<GameObject> Children;

    private void Start()
    {
        
         foreach (Transform child in GameObject.FindGameObjectWithTag("AllCars").transform)
         {
             if (child.tag == "Vehicle")
             {
                 Children.Add(child.gameObject);
             }
         }
        //allCars = GameObject.FindGameObjectWithTag("AllCars");
        //Debug.Log(Children.Count);
        carPointer = PlayerPrefs.GetInt("SelectedCarIndex");
        SetActiveCar(PlayerPrefs.GetInt("SelectedCarIndex"));
    }

    public void NextCar()
    {
        DisableActiveCar();
        if (carPointer < Children.Count - 1)
        {
            carPointer++;
        }
        else
        {
            carPointer = 0;
        }
        SetActiveCar(carPointer);
        SaveCarConfiguration();
    }

    public void PreviousCar()
    {
        DisableActiveCar();
        if (carPointer > 0)
        {
            carPointer--;
        }
        else
        {
            carPointer = Children.Count - 1;
        }
        SetActiveCar(carPointer);
        SaveCarConfiguration();
    }

    public void SetActiveCar(int index)
    {
        //Debug.Log(carPointer);
        Children[index].SetActive(true);
    }

    public void DisableActiveCar()
    {
        Children[carPointer].SetActive(false);
    }

    public void SaveCarConfiguration()
    {
        PlayerPrefs.SetInt("SelectedCarIndex", carPointer);
        Debug.Log(PlayerPrefs.GetInt("SelectedCarIndex"));
    }
}
