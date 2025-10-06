using System;
using TMPro;
using Unity.IntegerTime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class EndPanel : MonoBehaviour
    {
        [SerializeField] Button exitBtn;
        [SerializeField] TMP_Text results;

        public void Show(int souls, int rounds)
        {
            gameObject.SetActive(true);
            var totalTime = TimeSpan.FromSeconds(Time.timeSinceLevelLoad);
            results.text = $"souls: {souls}\npahses: {rounds}\ntotal time: {totalTime:mm\\:ss}";

            exitBtn.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
        }
    }
}
