using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnGenplanPanel : MonoBehaviour
{
    private GameManager _manager;

    public Button b_1;
    public Button b_2;
    public Button b_3;

    public GameObject Korpus1;
    public GameObject Korpus2;
    public GameObject Korpus3;

    [Header("Info Panel")]
    public GameObject InfoPanel;

    public TMP_Text NumberKorpus;
    public TMP_Text NumberFlat;
    
    public TMP_Text Count_All;
    public GameObject InfoFlatsPrefab;

    private List<GameObject> _prefabs = new List<GameObject>();

    public Button AllFlats;
    private MyBuilding _building;

    public void Init(GameManager manager)
    {
        _manager = manager;
        b_1.onClick.AddListener(On1);
        b_2.onClick.AddListener(On2);
        b_3.onClick.AddListener(On3);
        Korpus1.SetActive(false);
        Korpus2.SetActive(false);
        Korpus3.SetActive(false);
        AllFlats.onClick.AddListener(OnAllFlats);
    }

    public void Show()
    {
        InfoPanel.SetActive(false);
        gameObject.SetActive(true);
        Korpus1.SetActive(false);
        Korpus2.SetActive(false);
        Korpus3.SetActive(false);
    }

    public void Hide()
    {
        InfoPanel.SetActive(false);
        gameObject.SetActive(false);
    }

    private void On1()
    {
        bool isCreate = false;
        foreach (var myBuilding in _manager.MyData.Buildings)
        {
            if (myBuilding.Korpus == 1 && myBuilding.Flats.Count>0)
            {
                isCreate = true;
            }
        }
        if(!isCreate) return;
        Korpus1.SetActive(true);
        Korpus2.SetActive(false);
        Korpus3.SetActive(false);
        Show(1);
    }
    
    private void On2()
    {
        bool isCreate = false;
        foreach (var myBuilding in _manager.MyData.Buildings)
        {
            if (myBuilding.Korpus == 2 && myBuilding.Flats.Count>0)
            {
                isCreate = true;
            }
        }
        if(!isCreate) return;
        Korpus1.SetActive(false);
        Korpus2.SetActive(true);
        Korpus3.SetActive(false);
        Show(2);
    }
    
    private void On3()
    {
        bool isCreate = false;
        foreach (var myBuilding in _manager.MyData.Buildings)
        {
            if (myBuilding.Korpus == 3 && myBuilding.Flats.Count>0)
            {
                isCreate = true;
            }
        }
        if(!isCreate) return;

        Korpus1.SetActive(false);
        Korpus2.SetActive(false);
        Korpus3.SetActive(true);
        Show(3);
    }

    private void Show(int numberKorpus)
    {
        for (int i = 0; i < _prefabs.Count; i++)
        {
            Destroy(_prefabs[i]);
        }
        _prefabs.Clear();
        InfoPanel.SetActive(true);
        foreach (var myBuilding in _manager.MyData.Buildings)
        {
            if (myBuilding.Korpus == numberKorpus)
            {
                _building = myBuilding;
                NumberKorpus.text = numberKorpus + " корпус";
                NumberFlat.text = myBuilding.Flats.Count + "  квартиры, Сдача 2 кв. 2028 г.";
                Count_All.text = myBuilding.Flats.Count.ToString();

                CreatePrefab(myBuilding,0);
                CreatePrefab(myBuilding,1);
                CreatePrefab(myBuilding,2);
                CreatePrefab(myBuilding,3);
                CreatePrefab(myBuilding,4);
                CreatePrefab(myBuilding,5);

                Canvas.ForceUpdateCanvases();

                var horizontal_all = Count_All.transform.parent.GetComponent<HorizontalLayoutGroup>();
                horizontal_all.spacing++;
                horizontal_all.spacing--;
                
                Canvas.ForceUpdateCanvases();
            }
        }
    }

    private void CreatePrefab(MyBuilding building, int rooms)
    {
        if (building.GetCountFlats(rooms) > 0)
        {
            InfoFlatsPrefab prefab = Instantiate(InfoFlatsPrefab, InfoPanel.transform)
                .GetComponent<InfoFlatsPrefab>();
            prefab.transform.SetSiblingIndex(prefab.transform.parent.childCount-2);
            prefab.Init(building, rooms, _manager);
            _prefabs.Add(prefab.gameObject);
        }
    }

    private void OnAllFlats()
    {
        GameManager.Instance.choseFlatPanel.OnChoseFlayOnParameters();
        GameManager.Instance.choseFlatPanel._choseFlatOnParameterPanel.ShowOnParameters(_building, -1);
    }
}
