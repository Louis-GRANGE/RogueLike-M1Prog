using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SaveDatas", menuName = "ScriptableObjects/Save/SaveDatas")]
public class SOSaveDatas : ASaveSO
{
    private int _ChipsNumber = 0;
    public int ChipsNumber
    {
        get { return _ChipsNumber; }
        set
        {
            if (_ChipsNumber == value) return;
            _ChipsNumber = value;
            if (ChipsNumberValueChange != null)
                ChipsNumberValueChange(_ChipsNumber);
        }
    }
    public delegate void OnValueChangeDelegate(int newVal);
    public event OnValueChangeDelegate ChipsNumberValueChange;
}
