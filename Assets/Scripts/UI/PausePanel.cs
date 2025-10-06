using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class PausePanel : MonoBehaviour
    {
        [SerializeField] Button resumeBtn;
        [SerializeField] Button restartBtn;
        [SerializeField] Button exitBtn;

        void Start()
        {
            resumeBtn.onClick.AddListener(Close);
            restartBtn.onClick.AddListener(() =>
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("City");
            });
            exitBtn.onClick.AddListener(() =>
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("MainMenu");
            });
        }

        void Close()
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }

        public void Open()
        {
            Time.timeScale = 0;
            gameObject.SetActive(true);
        }
    }
}
