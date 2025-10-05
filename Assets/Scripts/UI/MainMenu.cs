using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] Button newGameBtn;
        [SerializeField] Button exitBtn;

        void Start()
        {
            newGameBtn.onClick.AddListener(() => SceneManager.LoadScene("City"));
            exitBtn.onClick.AddListener(() => Application.Quit());
        }
    }
}
