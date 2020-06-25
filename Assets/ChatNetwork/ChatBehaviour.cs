using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatBehaviour : NetworkBehaviour
{
    [SerializeField] private GameObject chatUI = default;
    [SerializeField] private TMP_Text chatText = default;
    [SerializeField] private TMP_InputField inputField = default;

    private static event Action<string> OnMessage;

    public override void OnStartAuthority()
    {
        chatUI.SetActive(true);

        OnMessage += HandleNewMessage;
    }

    [ClientCallback]
    private void OnDestroy()
    {
        if (!hasAuthority) { return; }

        OnMessage -= HandleNewMessage;
    }


    private void HandleNewMessage(string message)
    {
        chatText.text += message;
    }

    [Client]
    public void Send()
    {
        if (!Input.GetKeyDown(KeyCode.Return)) { return; }

        string message = inputField.text;

        if (string.IsNullOrWhiteSpace(message)) { return; }

        CmdSendMessage(message);

        inputField.text = string.Empty;
    }

    [Command]
    private void CmdSendMessage(string message)
    {
        // validate here


        RpcHandleMessage($"{connectionToClient.connectionId}: {message}\n");
    }

    [ClientRpc]
    private void RpcHandleMessage(string message)
    {
        OnMessage?.Invoke(message);
    }
}


