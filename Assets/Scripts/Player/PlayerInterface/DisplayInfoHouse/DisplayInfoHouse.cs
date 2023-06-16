using UnityEngine;
using UnityEngine.UI;

public class DisplayInfoHouse : MonoBehaviour
{
    [SerializeField] private GameObject _imageForInfo;

    [SerializeField] private Text _infoHouse;

    public void ViewInfoHouse(string nameHouse, string levelHouse, string healthHouse, bool isActive)
    {
        _infoHouse.text = string.Format($"Name House: {nameHouse} \nLVL: {levelHouse} \nHealth House: {healthHouse}");

        _imageForInfo.SetActive(isActive);
    }
}
