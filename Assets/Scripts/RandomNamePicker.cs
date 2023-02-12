using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomNamePicker : MonoBehaviour
{
    public List<string> SquadNames = new List<string>();
    public List<string> ChosenNames = new List<string>();
    public TextMeshProUGUI NamesAvailableText;
    public TextMeshProUGUI ChosenNamesText;

     void Start()
    {
        Prime();
    }
     
public void Prime()
    {    
        SquadNames.Add("Ant");
        SquadNames.Add("Ali");
        SquadNames.Add("Toby");
        SquadNames.Add("Oli");
        SquadNames.Add("Marcus");
        SquadNames.Add("Rags");
        SquadNames.Add("Dan");
        SquadNames.Add("Mars");
        SquadNames.Add("Emaad");
        SquadNames.Add("Ed");
        SquadNames.Sort();

        //Check that GO is assigned
        if (NamesAvailableText != null){
            NamesAvailableText.SetText(string.Join(", ", SquadNames));
        }

        else{
            Debug.LogError("Names Available is unassigned");
        }

        //Check that GO is assigned
        if (ChosenNamesText != null){
            ChosenNamesText.SetText(string.Join(", ", ChosenNames));
        }

        else{
            Debug.LogError("Chosen Names is unassigned");
        }
    }

    public void NamePick()
    {
        if (SquadNames.Count!=0){
            int randIndex = Random.Range(0, SquadNames.Count);
            ChosenNames.Add(SquadNames[randIndex]);
            ChosenNamesText.SetText(string.Join(", ", ChosenNames));
            
            SquadNames.RemoveAt(randIndex);
            NamesAvailableText.SetText(string.Join(", ", SquadNames));
            
        }
        else{
            Debug.LogError("List is empty. Reset names.");
            return;
        }
    

    }
    public void ResetNames()
    {
        ChosenNames.Clear();
        SquadNames.Clear();
        Prime();
    }
}