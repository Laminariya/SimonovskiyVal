using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InfrastructuraPanel : MonoBehaviour
{
    public Button b_Back;
    public Button b_ChoseFlatOnParemeters;
    public Button b_Location;
    public Button b_Galereya;

    public Button b_1;
    public Button b_2;

    public Sprite Sprite1;
    public Sprite Sprite2;

    private GameManager _manager;
    private Image _image;
    
    public void Init(GameManager manager)
    {
        _manager = manager;
        _image = GetComponent<Image>();
        _image.sprite = Sprite1;
        b_Back.onClick.AddListener(OnBack);
        b_ChoseFlatOnParemeters.onClick.AddListener(OnChoseFlatParameters);
        b_Galereya.onClick.AddListener(OnGalereya);
        b_Location.onClick.AddListener(OnLocation);
        b_1.onClick.AddListener(On1);
        b_2.onClick.AddListener(On2);
        Hide();
    }
    
    public void Show()
    {
        On1();
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void On1()
    {
        _image.sprite = Sprite1;
    }

    private void On2()
    {
        _image.sprite = Sprite2;
    }

    private void OnBack()
    {
        Hide();
    }

    private void OnChoseFlatParameters()
    {
        Hide();
    }

    private void OnGalereya()
    {
        _manager.galereyaPanel.Show();
        Hide();
    }

    private void OnLocation()
    {
        _manager.locationPanel.Show();
        Hide();
    }
}
