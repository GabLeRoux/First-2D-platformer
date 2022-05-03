using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetGraphicAndSize : MonoBehaviour
{
    [SerializeField] private Dropdown[] dropdownArray; // [0] dropdown(quality), [1]dropdown(resoution)
    
    private string[] qualityNames; // use for dropdown quality
    private Resolution[] arrayRes; // use for dropdown resolution

    void Start()
    {
        InitialiseQuality();
        InitialiseResolution();
    }

    #region Resolution
    private void InitialiseResolution()
    {
        arrayRes = Screen.resolutions;
        List<string> dropOption = new List<string>(); // string value of resolution
       Resolution currentRes = Screen.currentResolution;
        //print(currentRes);
        
        int myScreenResolution = 0;
       for(int i = 0; i< arrayRes.Length; i++)
       {
            dropOption.Add(arrayRes[i].ToString());
            if(currentRes.ToString() == arrayRes[i].ToString())
            {
                myScreenResolution = i;
            }
       }
        dropdownArray[1].AddOptions(dropOption);
        dropdownArray[1].value = myScreenResolution;
    }
    public void SetResolution()
    {
        var res = arrayRes[dropdownArray[1].value];
        Screen.SetResolution(res.width, res.height, Screen.fullScreenMode, res.refreshRate);
    }
    #endregion

    #region Quality
    private void InitialiseQuality()
    {
        qualityNames = QualitySettings.names;
        List<string> dropOption = new List<string>();
        foreach (var quality in qualityNames)
        {
            dropOption.Add(quality);
        }
        dropdownArray[0].AddOptions(dropOption);
        dropdownArray[0].value = QualitySettings.GetQualityLevel();
    }

    
    public void SetQuality()
    {
        QualitySettings.SetQualityLevel(dropdownArray[0].value, true);
    }
    #endregion
}
