using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloorsOnPlanPrefab : MonoBehaviour
{
    public int Korpus;
    public int StartFloor;
    public List<GameObject> Rooms = new List<GameObject>();
    
    private List<FlatPointOnFloorsPrefab> _flatsPoint = new List<FlatPointOnFloorsPrefab>();
    private GameManager _manager;
    
    public void Init(GameManager manager)
    {
        _manager = manager;
        int floor = StartFloor;
        foreach (var room in Rooms)
        {
            FlatPointOnFloorsPrefab[] flatsPoint = room.GetComponentsInChildren<FlatPointOnFloorsPrefab>();
            for (int i = 0; i < flatsPoint.Length; i++)
            {
                flatsPoint[i].Init(GetFlat(floor, i+1));
            }
            floor++;
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private MyFlat GetFlat(int floor, int numberOnFloor)
    {
        foreach (var myBuilding in _manager.MyData.Buildings)
        {
            if (myBuilding.Korpus == Korpus)
            {
                foreach (var flat in myBuilding.Flats)
                {
                    //Debug.Log(flat.Floor+ " " + flat.NumberOnFloor + " " + floor + " " + numberOnFloor);
                    if (flat.Floor == floor && flat.NumberOnFloor == numberOnFloor)
                    {
                        return flat;
                    }
                }
            }
        }

        return null;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
