using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnPlanPanel : MonoBehaviour
{
    private GameManager _manager;

    public Button b_Up;
    public Button b_Down;

    public Sprite SpriteUpActive;
    public Sprite SpriteDownActive;
    public Sprite SpriteUpNoActive;
    public Sprite SpriteDownNotActive;
    
    public List<FloorsOnPlanPrefab> Floors_Korpus3 = new List<FloorsOnPlanPrefab>();

    private int _currentFloorsPanel;

    public void Init(GameManager manager)
    {
        _currentFloorsPanel = 0;
        _manager = manager;
        foreach (var planPrefab in Floors_Korpus3)
        {
            planPrefab.Init(_manager);
        }
        if (Floors_Korpus3.Count > 1)
        {
            b_Up.image.sprite = SpriteUpActive;
            b_Down.image.sprite = SpriteDownNotActive;
        }

        OffAllFloorsPanels();
        Floors_Korpus3[_currentFloorsPanel].Show();
        b_Up.onClick.AddListener(OnUp);
        b_Down.onClick.AddListener(OnDown);
    }

    private void OffAllFloorsPanels()
    {
        foreach (var floorsOnPlanPrefab in Floors_Korpus3)
        {
            floorsOnPlanPrefab.Hide();
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnUp()
    {
        _currentFloorsPanel++;
        if (_currentFloorsPanel >= Floors_Korpus3.Count - 1)
        {
            b_Up.image.sprite = SpriteUpNoActive;
            _currentFloorsPanel = Floors_Korpus3.Count - 1;
        }
        b_Down.image.sprite = SpriteDownActive;
        OffAllFloorsPanels();
        Floors_Korpus3[_currentFloorsPanel].Show();
    }

    private void OnDown()
    {
        _currentFloorsPanel--;
        if (_currentFloorsPanel <= 0)
        {
            b_Down.image.sprite = SpriteDownNotActive;
            _currentFloorsPanel = 0;
        }
        b_Up.image.sprite = SpriteUpActive;
        OffAllFloorsPanels();
        Floors_Korpus3[_currentFloorsPanel].Show();
    }
}
