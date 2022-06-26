using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipNumberUI : MonoBehaviour
{
    public TMPro.TMP_Text Number;

    private void Start()
    {
        SaveManager.instance.GetSave<SOSaveDatas>().ChipsNumberValueChange += UpdateChipNumber;
        Number.text = SaveManager.instance.GetSave<SOSaveDatas>().ChipsNumber.ToString();
    }

    public void UpdateChipNumber(int newnb)
    {
        Number.text = newnb.ToString();
    }
}
