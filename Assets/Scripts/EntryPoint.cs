using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private NetWorkButtons netWorkButtons;
    [SerializeField] private NetworkManager networkManager;

    void Start()
    {
        netWorkButtons.request.AddListener(mode => 
        {
            switch (mode)
            {
                case NetWorkButton.Mode.Host:
                    networkManager.StartHost();
                    break;
                case NetWorkButton.Mode.Client:
                    networkManager.StartClient();
                    break;   
            }
            netWorkButtons.Hide();
        });
    }
}
