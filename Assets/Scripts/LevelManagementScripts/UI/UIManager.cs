using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMPro.TMP_Text AnimalCount;
    public TMPro.TMP_Text MaxAnimals;

    public TMPro.TMP_Text FireCount;
    public TMPro.TMP_Text MaxFires;

    private void Start()
    {
        MaxAnimals.SetText("/" + LevelManager.Instance.MaxAnimals.ToString());
        MaxFires.SetText("/" + LevelManager.Instance.MaxFires.ToString());
    }

    private void Update()
    {
        AnimalCount.SetText(LevelManager.Instance.Animals.ToString());
        FireCount.SetText(LevelManager.Instance.Fires.ToString());
    }
}
