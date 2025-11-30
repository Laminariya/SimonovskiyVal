using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalereyaPanel : MonoBehaviour
{

    public Button b_Back;
    public Button b_ChoseFlatOnParemeters;
    public Button b_Location;
    public Button b_Infrastructura;

    public Image ImagePanel;
    public Button b_Left;
    public Button b_Right;
    
    public List<GalereyaImage> GalereyaImages = new List<GalereyaImage>();
    
    private List<Sprite> _sprites = new List<Sprite>();
    private int _currentSprite;
    private GameManager _manager;
    
    public void Init(GameManager manager)
    {
        _manager = manager;
        b_Back.onClick.AddListener(OnBack);
        b_Left.onClick.AddListener(OnLeft);
        b_Right.onClick.AddListener(OnRight);
        b_ChoseFlatOnParemeters.onClick.AddListener(OnChoseFlatParameters);
        b_Location.onClick.AddListener(OnLocation);
        b_Infrastructura.onClick.AddListener(OnInfrastructura);
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        ImagePanel.gameObject.SetActive(false);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        ImagePanel.gameObject.SetActive(false);
    }

    public void ShowImagePanel(int number)
    {
        _currentSprite = 0;
       ImagePanel.gameObject.SetActive(true);
       _sprites = new List<Sprite>(GalereyaImages[number].Sprites);
       ImagePanel.sprite = _sprites[_currentSprite];
    }

    private void OnLeft()
    {
        _currentSprite--;
        if (_currentSprite < 0)
            _currentSprite = 0;
        ImagePanel.sprite = _sprites[_currentSprite];
    }

    private void OnRight()
    {
        _currentSprite++;
        if (_currentSprite > _sprites.Count - 1)
            _currentSprite = _sprites.Count - 1;
        ImagePanel.sprite = _sprites[_currentSprite];
    }

    private void OnBack()
    {
        if (ImagePanel.gameObject.activeSelf)
        {
            ImagePanel.gameObject.SetActive(false);
            return;
        }
        Hide();
    }

    private void OnChoseFlatParameters()
    {
        Hide();
    }

    private void OnLocation()
    {
        _manager.locationPanel.Show();
        Hide();
    }

    private void OnInfrastructura()
    {
        _manager.infrastructuraPanel.Show();
        Hide();
    }

}

[Serializable]
public class GalereyaImage
{
    public List<Sprite> Sprites = new List<Sprite>();
}