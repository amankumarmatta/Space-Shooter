using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Image Bar;

    public void SetAmount(float _amount)
    {
        Bar.fillAmount = _amount;
    }
}
