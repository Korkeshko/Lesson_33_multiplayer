using UnityEngine;
using UnityEngine.Events;

public class NetWorkButtons : MonoBehaviour
{
    [SerializeField] private NetWorkButton[] buttons;
    public UnityEvent<NetWorkButton.Mode> request = new();

    void Start()
    {
        foreach (NetWorkButton button in buttons)
        {
            button.request.AddListener((mode) => request.Invoke(mode));
        }   
    }

    public void Hide()
    {
        foreach (NetWorkButton button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }

    
}
