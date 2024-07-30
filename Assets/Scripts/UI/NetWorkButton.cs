using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NetWorkButton : MonoBehaviour
{
    [SerializeField] private Mode mode;

    public UnityEvent <Mode> request = new UnityEvent<Mode>();
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => request.Invoke(mode));
    }

    public enum Mode
    {
        Host,
        Client
    }

}
