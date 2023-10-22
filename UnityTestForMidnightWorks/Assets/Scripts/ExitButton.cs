using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
