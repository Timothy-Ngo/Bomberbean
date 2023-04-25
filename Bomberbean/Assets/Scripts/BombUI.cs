using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BombUI : MonoBehaviour
{

    public TextMeshProUGUI count;
    // TODO Add cooldown bar UI
    // Start is called before the first frame update
    void Awake()
    {
        count = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateBomb(int newCount)
    {
        count.text = "" + newCount;
    }

    public void CooldownBar(float width)
    {

    }
}
