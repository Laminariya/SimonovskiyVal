using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine.Serialization;

public class SerializeXML : MonoBehaviour
{

    private HttpClient Client = new HttpClient();
    private string _json;
    [HideInInspector] public FeedClass FeedClass;
    //public string _feedURL;

    //ссылка на json
    private string _url = "https://fsk-export.hb.bizmrg.com/production/v3/fsk_all.xml";

    public async Task Init()
    {
        await LoadJSON(_url);
    }

    private async Task LoadJSON(string url)
    {
        var uri = new Uri(url);

        var result = await Client.GetAsync(uri);
        string str = await result.Content.ReadAsStringAsync();

        //await WriteJson(str);
        
        XmlSerializer serializer = new XmlSerializer(typeof(FeedClass));

        using (StringReader reader = new StringReader(str))
        {
            Debug.Log("CC");
            try
            {
                FeedClass = (FeedClass)serializer.Deserialize(reader);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                throw;
            }

            //GameManager.Instance.Feed = _feedClass;
            //Debug.Log("XX "+_feedClass);
            foreach (var region in FeedClass.Regions.AllRegion)
            {
                foreach (var complex in region.Complexes)  
                {
                    if (complex.Name == "Симоновский Вал")
                    {
                        foreach (var corpus in complex.Buildings.Corpuses)
                        {
                            foreach (var section in corpus.Sections)
                            {
                                Debug.Log(section.Number + " " + section.FloorCount);
                            }
                        }
                    }
                }
            }
        }
    }

    private async Task WriteJson(string text)
    {
        string path = Directory.GetCurrentDirectory() + "\\note1.txt"; //"C://Projects//PlanerRooms//Assets

        // полная перезапись файла 
        using (StreamWriter writer = new StreamWriter(path, false))
        {
            await writer.WriteAsync(text);
        }

        Debug.Log("Save note1.txt complete");
    }

}


[XmlRoot("Data"), Serializable]
public class FeedClass
{
    [XmlElement("Generation-date")] 
    public string GenerationDate;

    [XmlElement("Regions")] 
    public Regions Regions;
    
    [XmlElement("Decorations")] 
    public Decorations Decorations; //
    
    [XmlElement("FeatureTypes")] 
    public FeatureTypes FeatureTypes;
}

[Serializable]
public class FeatureTypes
{
    [XmlElement("FeatureType")] 
    public List<FeatureType> AllFeatureType = new List<FeatureType>();
}

[Serializable]
public class FeatureType
{
    [XmlAttribute("Type")] 
    public string Type;
    
    [XmlElement("FeatureTypes")] 
    public FeatureTypes FeatureTypes;
}

[Serializable]
public class Feature
{
    [XmlAttribute("FeatureName")] 
    public string Name;
    
    [XmlAttribute("FeatureID1C")] 
    public string Id;
}

[Serializable]
public class Decorations
{
    [XmlElement("Decoration")] 
    public List<Decoration> AllDecoration = new List<Decoration>();
}

[Serializable]
public class Decoration
{
    [XmlAttribute("ID")] 
    public string Id;
    [XmlAttribute("Name")] 
    public string Name;
    [XmlAttribute("Translate")] 
    public string Translate;
    [XmlAttribute("ShortName")] 
    public string ShortName;
}

[Serializable]
public class Regions
{
    [XmlElement("Region")] 
    public List<Region> AllRegion = new List<Region>();
}

[Serializable]
public class Region
{
    [XmlAttribute("Region_name")] 
    public string Name;
    [XmlElement("Object")] 
    public List<Complex> Complexes = new List<Complex>();
}

[Serializable]
public class Complex
{
    [XmlAttribute("Complex_name")] 
    public string Name;
    [XmlElement("Buildings")] 
    public Buildings Buildings;
}

[Serializable]
public class Buildings
{
    [XmlElement("Corpus")] 
    public List<Corpus> Corpuses = new List<Corpus>();
}

[Serializable]
public class Corpus
{
    [XmlAttribute("Num")] 
    public string Number;
    [XmlElement("Section")] 
    public List<Section> Sections = new List<Section>();
}

[Serializable]
public class Section
{
    [XmlAttribute("Num")] 
    public string Number;
    [XmlAttribute("Floor_Count")] 
    public string FloorCount;
    [XmlElement("Floor")] 
    public List<Floor> Floors = new List<Floor>();
}

[Serializable]
public class Floor
{
    [XmlAttribute("Num")] 
    public string Number;
    [XmlElement("Flat")] 
    public List<Flat> Flats = new List<Flat>();
}

[Serializable]
public class Flat
{
    [XmlAttribute("Id")] 
    public string Id;
    [XmlAttribute("Status")] 
    public int Status;
    [XmlAttribute("Number")] 
    public string Number;
    [XmlAttribute("Floor")] 
    public string Floor;
    [XmlAttribute("Num_on_floor")] 
    public int NumberOnFloor;
    [XmlAttribute("Rooms")] 
    public string Rooms;
    [XmlAttribute("Square_tot")] 
    public float Area;
}

   
    
    

    

