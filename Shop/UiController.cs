using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shop
{
    public class UiController : MonoBehaviour
    {
        [SerializeField] GameObject skins;
        [SerializeField] GameObject superPowers;

        private void Start()
        {
            skins.SetActive(false);
            superPowers.SetActive(false);
        }

        public void ActivatePowersShop()
        {
            superPowers.SetActive(true);
            skins.SetActive(false);
        }

        public void ActivateSkinsShop()
        {
            skins.SetActive(true);
            superPowers.SetActive(false);
        }

        public void Quit() => SceneManager.UnloadSceneAsync(GameManager.Instance.ShopSceneIndex);

    }
}
