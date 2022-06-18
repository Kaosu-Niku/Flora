using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputOpenPanel : MonoBehaviour
{
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject SkillPanel;
    MyInput GetInput;
    private void Awake()
    {
        GetInput = new MyInput();
    }
    private void OnEnable()
    {
        GetInput.Enable();
    }
    private void OnDisable()
    {
        GetInput.Disable();
    }
    private void Start()
    {
        GetInput.UI.OpenPause.started += OnOpenPause;
        GetInput.UI.OpenSkill.started += OnOpenSkill;
    }
    private void OnOpenPause(InputAction.CallbackContext context)//? 開啟暫停選單
    {
        if (PausePanel)
            if (PausePanel.activeInHierarchy == false)
                PausePanel.SetActive(true);
        if (SkillPanel)
            if (SkillPanel.activeInHierarchy == true)
                SkillPanel.SetActive(false);
    }
    private void OnOpenSkill(InputAction.CallbackContext context)//? 開啟技能選單
    {
        if (SkillPanel)
            if (SkillPanel.activeInHierarchy == false)
                SkillPanel.SetActive(true);
        if (PausePanel)
            if (PausePanel.activeInHierarchy == true)
                PausePanel.SetActive(false);
    }
}
