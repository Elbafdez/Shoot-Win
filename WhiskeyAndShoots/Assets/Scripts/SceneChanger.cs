using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NuevaPartida();
        }
    }
    public void NuevaPartida()
    {
        SceneManager.LoadScene("Game"); // Asegúrate que el nombre coincide exactamente
    }
}
