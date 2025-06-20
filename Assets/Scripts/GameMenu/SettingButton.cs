using UnityEngine;
using UnityEngine.UI;

namespace GameBlockAdv.GameMenu
{
    public class SettingButton : MonoBehaviour
    {
        [SerializeField] private Button openSettingButton;
        [SerializeField] private Button closeSettingButton;

        public void SettingsOpened()
        {
            openSettingButton.gameObject.SetActive(false);
            closeSettingButton.gameObject.SetActive(true);
            closeSettingButton.interactable = true;
        }
        public void SettingClosed()
        {
            openSettingButton.gameObject.SetActive(true);
            openSettingButton.interactable = true;
            closeSettingButton.gameObject.SetActive(false);

        }
    }

}
