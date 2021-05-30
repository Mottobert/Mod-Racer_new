using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameInput : MonoBehaviour
{
    [SerializeField]
    private InputField nameInputField = null;
    //[SerializeField]
    //private Button continueButton = null;

    private const string playerPrefsNameKey = "PlayerName";

    private TouchScreenKeyboard keyboard;

    void Awake()
    {
        #if !UNITY_EDITOR && UNITY_WEBGL
            WebGLInput.captureAllKeyboardInput = true;
        #endif
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUpInputField();
    }

    private void SetUpInputField()
    {
        if (!PlayerPrefs.HasKey(playerPrefsNameKey))
        {
            return;
        }

        string defaultName = PlayerPrefs.GetString(playerPrefsNameKey);

        nameInputField.text = defaultName;

        SetPlayerName(defaultName);
    }

    public void SetPlayerName(string name)
    {
        //continueButton.interactable = !string.IsNullOrEmpty(name);
    }

    public void SavePlayerName()
    {
        string playerName = nameInputField.text;

        if(playerName == "")
        {
            playerName = "Player " + UnityEngine.Random.Range(0, 1000);
        }

        PhotonNetwork.NickName = playerName;

        PlayerPrefs.SetString(playerPrefsNameKey, playerName);
    }
}
