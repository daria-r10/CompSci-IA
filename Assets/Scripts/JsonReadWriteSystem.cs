using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
 
public class JsonReadWriteSystem : MonoBehaviour
{
    public TMP_InputField idInputField;
    public TMP_InputField nameInputField;
    public TMP_InputField infoInputField;
 
    public void SaveToJson()
    {
        WeaponData data = new WeaponData();
        data.Id = idInputField.text;
        data.Name = nameInputField.text;
        data.Information = infoInputField.text;
 
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/WeaponDataFile.json", json);
    }
 
    public void LoadFromJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/WeaponDataFile.json");
        WeaponData data = JsonUtility.FromJson<WeaponData>(json);
 
        idInputField.text = data.Id;
        nameInputField.text = data.Name;
        infoInputField.text = data.Information;
    }
}
