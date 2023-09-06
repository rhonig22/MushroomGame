using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] private GameObject _selectedUnitObject, _tileObject, _tileUnitObject;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowSelectedUnit(BaseUnit unit)
    {
        if (unit == null)
        {
            _selectedUnitObject.SetActive(false);
            return;
        }

        _selectedUnitObject.GetComponentInChildren<TextMeshProUGUI>().text = unit.UnitName;
        _selectedUnitObject.SetActive(true);
    }

    public void ShowTileInfo(Tile tile)
    {
        if (tile == null)
        {
            _tileObject.SetActive(false);
            _tileUnitObject.SetActive(false);
            return;
        }

        _tileObject.GetComponentInChildren<TextMeshProUGUI>().text = tile.TileName;
        _tileObject.SetActive(true);

        if (tile.OccupiedUnit != null)
        {
            _tileUnitObject.GetComponentInChildren<TextMeshProUGUI>().text = tile.OccupiedUnit.UnitName;
            _tileUnitObject.SetActive(true);
        }
        else
        {
            _tileUnitObject.SetActive(false);
        }
    }
}
