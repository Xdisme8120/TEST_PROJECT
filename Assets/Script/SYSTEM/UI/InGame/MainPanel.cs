using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;
public class MainPanel : BaseUIForm
{
    private void Awake() {
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.Normal;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            UIManager.GetInstance().ShowUIForms("Inventory");       
        }   
          if(Input.GetKeyDown(KeyCode.Q))
        {
            UIManager.GetInstance().ShowUIForms("Quest");       
        }   
    }
}
