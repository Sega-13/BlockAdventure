using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CongratulationWriting : MonoBehaviour
{
    public List<GameObject> writings;
    private void Start()
    {
        GameEvents.ShowCongratulations += ShowCongratulations;
    }
    private void OnDisable()
    {
        GameEvents.ShowCongratulations -= ShowCongratulations;
    }
    private void ShowCongratulations()
    {
        var index = UnityEngine.Random.Range(0, writings.Count);
        writings[index].SetActive(true);
    }
}
