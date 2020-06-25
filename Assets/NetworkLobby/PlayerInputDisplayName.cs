using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerInputDisplayName : MonoBehaviour
{
    private const string PlayerPrefsNameKey = "PlayerName";

    [SerializeField] private TMP_InputField nameInputField = default;
    [SerializeField] private Button continueButton = default;

    public static string DisplayName { get; private set; }


    public void SavePlayerName(string name)
    {
        DisplayName = nameInputField.text;

        PlayerPrefs.SetString(PlayerPrefsNameKey, DisplayName);
    }


    void Start()
    {
        SetupInputField();
    }


    private void SetupInputField()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsNameKey)) { return; }

        string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);

        nameInputField.text = defaultName;

        EnableButtonInteractionOnInput(defaultName);
    }

    private void EnableButtonInteractionOnInput(string name)
    {
        continueButton.interactable = !string.IsNullOrEmpty(name);
    }



}
