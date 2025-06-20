using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBlockAdv
{
    public class CongratulationWriting : MonoBehaviour
    {
        [SerializeField] private List<GameObject> writings;
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
            var index = Random.Range(0, writings.Count);
            writings[index].SetActive(true);
        }
    }
}

