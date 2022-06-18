using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public GameObject _talk_panel;// { get => _talk_panel; private set { _talk_panel = value; } }
    public Text _talk_text;// { get => _talk_text; private set { _talk_text = value; } }
    void Awake()
    {
        _talk_panel = GameObject.Find("TalkPanel");
        _talk_text = GameObject.Find("TalkText").GetComponent<Text>();
        if (_talk_panel)
            _talk_panel.SetActive(false);
    }
}
