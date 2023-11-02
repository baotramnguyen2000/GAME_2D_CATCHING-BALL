using UnityEngine;
using UnityEngine.SceneManagement;
public class FadeScene : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;

    public void SetTriggerFadeOutMenu2Game()
    {
        animator.SetTrigger("FadeOutMenu2Game");
    }
    public void SceneMenu2Game()
    {
        SceneManager.LoadScene("Game");
    }


    public void SceneGame2Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void SetTriggerFadeOutGame2Menu()
    {
        animator.SetTrigger("FadeOutGame2Menu");
    }
}
