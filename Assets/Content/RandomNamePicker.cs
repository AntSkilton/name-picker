using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class RandomNamePicker : MonoBehaviour
{
//List of name
// function to populate list on start
// function to put name from alphabetic list into new list in below box at random
// function to clear all from new list and revert back to the old list at the top.
    

    public void ResetNames()
    {
        //TextMeshPro textMeshPro = GetComponent<textMeshPro>();
        //textMeshPro.SetText(squadName);
    } 

    void Start()
    {    
        List<Name> squadName = new List<Name>();
    
        squadName.Add(new Name ("Ant"));
        squadName.Add(new Name ("Ali"));
        squadName.Add(new Name ("Toby"));
        squadName.Add(new Name ("Oli"));
        squadName.Add(new Name ("Marcus"));
        squadName.Add(new Name ("Rags"));
        squadName.Add(new Name ("Dan"));
        squadName.Add(new Name ("Mars"));
        squadName.Add(new Name ("Emaad"));
        squadName.Add(new Name ("Ed"));
        
        squadName.Sort();

        Debug.Log(squadName);

        //for (<Name> in squadName)
      // {
      //      Debug.Log(ToString.(guy.squadName));
     //   }

   
   
    }



    void Update()
    {
        
    }
}
