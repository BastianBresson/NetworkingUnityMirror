using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinLobbyMenu : MonoBehaviour
{
    [SerializeField] private NetworkManagerLobby networkManager = default;

    [SerializeField] private GameObject landingPagePanel = default;
    [SerializeField] private TMP_InputField ipAddressInputField = default;
    [SerializeField] private Button joinButton = default;

    public void JoingLobby()
    {
        string ipAddress = ipAddressInputField.text;

        networkManager.networkAddress = ipAddress;
        networkManager.StartClient();

        joinButton.interactable = false;
    }

    private void HandleClientDisconnected()
    {
        gameObject.SetActive(false);
        landingPagePanel.SetActive(false);
    }

    private void HandleClientConnected()
    {
        joinButton.interactable = true;
    }

    private void OnEnable()
    {
        NetworkManagerLobby.OnClientConnected += HandleClientConnected;
        NetworkManagerLobby.OnClientDisconnected += HandleClientDisconnected;

    }

    private void OnDisable()
    {
        
    }
}
