using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFeedBAck : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI score,cooldownBurst,coolDownPierce;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score.text = ScoreManager.Instance.sc.amount.ToString();
        cooldownBurst.text = Math.Round(FindObjectOfType<ShootController>().BurstTime,1).ToString() + "%";
        coolDownPierce.text = Math.Round(FindObjectOfType<ShootController>().PierceTime,1).ToString() + "%";
    }
}
