using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BombUI : MonoBehaviour
{

    public TextMeshProUGUI count;
    public float maxWidth = 500;
    public Image cooldownBar;
    private RectTransform barSize;

    
    // Start is called before the first frame update
    void Awake()
    {
        count = GetComponentInChildren<TextMeshProUGUI>();
        barSize = cooldownBar.rectTransform;
        CooldownBar(0);
    }

    public void UpdateBomb(int newCount)
    {
        count.text = "" + newCount;
    }

    public void CooldownBar(float percentOfSize)
    {
        barSize.sizeDelta = new Vector2(maxWidth * percentOfSize, barSize.sizeDelta.y);
    }
}
