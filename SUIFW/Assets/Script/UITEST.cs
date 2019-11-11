using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;
public class UITEST : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.GetInstance().ShowUIForms("MainPanel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
