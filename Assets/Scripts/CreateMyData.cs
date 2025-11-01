using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class CreateMyData : MonoBehaviour
{

    private GameManager _manager;
    private MyData _myData;
    // private string PlaneFloor = "//Plans//PlanFloors//";
    // private string PlaneApartment = "//Plans//PlanApartments//";
    // private string PlaneApartmentFurn = "//Plans//PlanApartmentFurns//";

    //private string _nameJK = "ЖК «Мангазея в Богородском»";
    private string _complexName = "Симоновский Вал";

    public void Init(GameManager gameManager)
    {
        Debug.Log("Create My Data");
        _manager = gameManager;
        CreateData();
    }

    private void CreateData()
    {
        _myData = new MyData();
        List<string> nameHouse = new List<string>();
        
        foreach (var region in _manager.serializeXML.FeedClass.Regions.AllRegion)
        {
            foreach (var complex in region.Complexes)  
            {
                if (complex.Name == "Симоновский Вал")
                {
                    foreach (var corpus in complex.Buildings.Corpuses)
                    {
                        foreach (var section in corpus.Sections)
                        {
                            MyBuilding myBuilding = new MyBuilding();
                            _myData.Buildings.Add(myBuilding);
                            myBuilding.Korpus = int.Parse(corpus.Number);
                            myBuilding.Section = int.Parse(section.Number);
                            myBuilding.CountFloors = int.Parse(section.FloorCount);
                            foreach (var floor in section.Floors)
                            {
                                foreach (var flat in floor.Flats)   
                                {
                                    MyFlat myFlat = new MyFlat(flat, myBuilding.Korpus, myBuilding.Section);
                                    myBuilding.Flats.Add(myFlat);
                                }
                            }
                            Debug.Log(section.Number + " " + section.FloorCount);
                        }
                    }
                }
            }
        }
    }

    // private void CreateData()
    // {
    //     _myData = new MyData();
    //     List<string> nameHouse = new List<string>();
    //     foreach (var objectClass in _manager.Feed.Object) 
    //     {
    //         if (objectClass.JKSchema.Id == _idJK && !nameHouse.Contains(objectClass.JKSchema.House.Name))
    //         {
    //             nameHouse.Add(objectClass.JKSchema.House.Name);
    //         }
    //     }
    //
    //     foreach (var house in nameHouse)
    //     {
    //         MyBuilding building = new MyBuilding();
    //         foreach (var objectClass in _manager.Feed.Object)
    //         {
    //             if (objectClass.JKSchema.Id == _idJK && objectClass.JKSchema.House.Name==house)
    //             {
    //                 MyObject myObject = new MyObject(objectClass);
    //                 building.Objects.Add(myObject);
    //             }
    //         }
    //         //Debug.Log(house + " " + building.Objects.Count + " "+  building.Objects[0].Korpus);
    //         building.Korpus = building.Objects[0].Korpus;
    //         building.Section = building.Objects[0].Section;
    //         _myData.Buildings.Add(building);
    //     }
    //
    //     foreach (var building in _myData.Buildings)
    //     {
    //         int maxPrice = 0;
    //         int minPrice = int.MaxValue;
    //         float maxArea = 0;
    //         float minArea = float.MaxValue;
    //         int maxFloor = 0;
    //         int minFloor = int.MaxValue;
    //         foreach (var myObject in building.Objects)
    //         {
    //             if (myObject.Price > maxPrice) maxPrice = myObject.Price;
    //             if (myObject.Price < minPrice) minPrice = myObject.Price;
    //             
    //             if (myObject.Area > maxArea) maxArea = myObject.Area;
    //             if (myObject.Area < minArea) minArea = myObject.Area;
    //             
    //             if (myObject.Floor > maxFloor) maxFloor = myObject.Floor;
    //             if (myObject.Floor < minFloor) minFloor = myObject.Floor;
    //         }
    //
    //         building.MaxPrice = maxPrice;
    //         building.MinPrice = minPrice;
    //         building.MaxArea = maxArea;
    //         building.MinArea = minArea;
    //         building.MaxFloor = maxFloor;
    //         building.MinFloor = minFloor;
    //     }
    //
    //     _manager.MyData = _myData;
    //
    // }

    // public IEnumerator CreateSprites()
    // {
    //     int count = 0;
    //     foreach (var building in _myData.Buildings) 
    //     {
    //         foreach (var myObject in building.Objects)
    //         {
    //             count++;
    //         }
    //     }
    //     string str = GameManager.Instance.InfoStartPanel.text;
    //     int count2 = 0;
    //     foreach (var building in _myData.Buildings)
    //     {
    //         foreach (var myObject in building.Objects)
    //         {
    //             yield return StartCoroutine(_manager.CreateImagePng.LoadSpritePNG(myObject));
    //             count2++;
    //             GameManager.Instance.InfoStartPanel.text = str + "\r\n" + "Load Image: " +count2 + "/" + count;
    //         }
    //     }
    // }

}

[Serializable]
public class MyData
{
    public List<MyBuilding> Buildings = new List<MyBuilding>();
}

[Serializable]
public class MyBuilding
{
    public List<MyFlat> Flats = new List<MyFlat>();
    public int Korpus;
    public int Section;
    public int CountFloors;
    public float MinArea;
    public float MaxArea;
    public int MinPrice;
    public int MaxPrice;
    public int MinFloor;
    public int MaxFloor;

}

[Serializable]
public class MyFlat
{
    public string Id;
    public MyBuilding Building;
    public int CountRooms;
    public float Area;
    public int Korpus;
    public int Section;
    public int Floor;
    public int CountOnFloor;
    public float CeilingHeight; //Находиться в Билдинге
    public int Number;
    public int Price;
    public string UrlFurniture;
    public string PathFurniture;
    public string PathFloor;
    public bool IsFree;
    public Sprite PlanSprite;
    public string Decoration;
    public int NumberOnFloor;
    
    //Отделка
    //Высота потолков
    //Уровни: Двухуровневая, Антресольная, Второй свет
    //Особенности планировки: Двухсторонняя ориентация, Евроформат, Мастер-спальня, гардеробная, Большая ванная, Постирочная...
    //Остеклени: Панорамное остекление
    //Летние помещения: Терраса
    //Виды из окон: на реку

    public MyFlat(Flat objectClass, int korpus, int section)
    {
        CountRooms = int.Parse(objectClass.Rooms);
        Area = objectClass.Area;
        Korpus = korpus;
        Floor = int.Parse(objectClass.Floor);
        Number = int.Parse(objectClass.Number);
        //Price = objectClass.BargainTerms.Price;
        //UrlFurniture = objectClass.LayoutPhoto.FullUrl;
        //IsFree = objectClass.Booking.Status=="free";
        Section = section;

        Id = objectClass.Id;
        PathFurniture = Directory.GetCurrentDirectory() + "//Plans//PlanFlat//" + Id + ".png";
        PathFloor = Directory.GetCurrentDirectory() + "//Plans//PlanFloor//" + Id + ".png";
    }

}


