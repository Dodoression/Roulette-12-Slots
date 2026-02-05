using UnityEngine;
using System;

public class WinPanel : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(Helpers.WaitAndExecute(3.5f, () => gameObject.SetActive(false)));
    }
}
