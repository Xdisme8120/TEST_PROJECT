using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DL 
{
    static DL instance;
    public Dictionary<int ,int > dic;
    public static DL GetDistance()
    {
        if(instance==null)
        {
            instance = new DL();
        }
        return instance;
    } 
    
    private DL()
    {
        dic = new Dictionary<int, int>();
    }
}
