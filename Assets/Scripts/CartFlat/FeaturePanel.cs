using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeaturePanel : MonoBehaviour
{
    public GameObject HorizontalFeaturePrefab;
    public GameObject FeaturePrefab;

    private MyFlat _myFlat;
    private Dictionary<string, List<string>> _dictionary = new Dictionary<string, List<string>>();
    private List<VerticalLayoutGroup> _featureBlock = new List<VerticalLayoutGroup>();
    private VerticalLayoutGroup _verticalLayout;
    private List<GameObject> _prefabs = new List<GameObject>();

    public void Init()
    {
        _verticalLayout = GetComponent<VerticalLayoutGroup>();
    }

    public void Show(MyFlat myFlat)
    {
        for (int i = 0; i < _prefabs.Count; i++)
        {
            Destroy(_prefabs[i]);
        }
        _prefabs.Clear();
        
        _dictionary.Clear();
        
        _myFlat = myFlat;
        GetFeature();
        int k = 0;
        foreach (var dic in _dictionary)
        {
            if (k == 0)
            {
                GameObject horizontal = Instantiate(HorizontalFeaturePrefab, transform);
                _featureBlock = horizontal.GetComponentsInChildren<VerticalLayoutGroup>().ToList();
                _prefabs.Add(horizontal);
            }

            _featureBlock[k].GetComponentInChildren<TMP_Text>().text = dic.Key + ":";
            foreach (var feature in dic.Value)
            {
                GameObject obj = Instantiate(FeaturePrefab, _featureBlock[k].transform);
                obj.GetComponentInChildren<TMP_Text>().text = feature;
            }

            _featureBlock[k].spacing++;
            Canvas.ForceUpdateCanvases();
            _featureBlock[k].spacing--;
            
            _verticalLayout.spacing++;
            Canvas.ForceUpdateCanvases();
            _verticalLayout.spacing--;
            
            k++;
            if (k == 2) k = 0;
        }
        _verticalLayout.spacing++;
        Canvas.ForceUpdateCanvases();
        _verticalLayout.spacing--;
        Canvas.ForceUpdateCanvases();
    }
    
    private void GetFeature()
    {
        foreach (var featureType in GameManager.Instance.serializeXML.FeedClass.FeatureTypes.AllFeatureType)
        {
            if(featureType.Type=="Комнатность" || featureType.Type=="Настройки для сайта") return;
            
            foreach (var feature in featureType.Features)
            {
                foreach (var id in _myFlat.Feature)
                {
                    if (id == feature.Id)
                    {
                        if(!_dictionary.ContainsKey(featureType.Type))
                            _dictionary.Add(featureType.Type, new List<string>());
                        _dictionary[featureType.Type].Add(feature.Name);
                        //Debug.Log(featureType.Type + " // " + feature.Name);
                    }
                }
            }
        }
    }
}
