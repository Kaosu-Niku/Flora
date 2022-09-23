//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/MyInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @MyInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @MyInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MyInput"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""7279d538-e286-4f49-8583-868fcb95de86"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""92aac432-11b8-4622-835f-bb1248306f1e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""468b0978-45f6-4002-8878-106d77ee80e6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""d8fc303e-fa8d-41a5-8a5e-c83b9a3af7fa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Flash"",
                    ""type"": ""Button"",
                    ""id"": ""959e108c-ca31-48fa-9e04-20db21f142b8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Action"",
                    ""type"": ""Button"",
                    ""id"": ""55b4da71-d76f-4288-87f8-d637c837d6ce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Restore"",
                    ""type"": ""Button"",
                    ""id"": ""bc642e64-741b-439d-afea-89534594fc96"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ef86f180-910a-434a-a0b1-2f23b8cf4de6"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ca990afd-ebda-4cae-9811-9646bcc2a7f0"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""55c037c6-966b-41e1-a143-a80851364ab0"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Flash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7902c6b6-0fe3-4f01-abd8-d60aa52a5723"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0ce5ad9a-e3f8-45a2-b5f3-e05fa3acb4a4"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Restore"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""262c908a-85eb-46f7-849f-debe1e3daf50"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""791919d9-82f0-4008-b878-ba1b822d9b1e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""1ca04c3a-a536-4e81-99c0-94a5bed10999"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""f46abc9a-8ec5-47e0-8f7f-48a3dd5e4f91"",
            ""actions"": [
                {
                    ""name"": ""Esc"",
                    ""type"": ""Button"",
                    ""id"": ""ab28b6ab-c4db-4e8b-8f5d-55542421f6d6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Tab"",
                    ""type"": ""Button"",
                    ""id"": ""9f670fb8-e3f4-4a36-88bc-16053bef6310"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Enter"",
                    ""type"": ""Button"",
                    ""id"": ""94b2e4a8-0a35-48a2-b2b6-df4bc9972b08"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Q"",
                    ""type"": ""Button"",
                    ""id"": ""69c7d08b-753b-4958-8227-f843814db43b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""E"",
                    ""type"": ""Button"",
                    ""id"": ""45ed8846-953c-47e6-a237-ead1a13484b0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""A"",
                    ""type"": ""Button"",
                    ""id"": ""112cbd1c-f6c5-4a71-a74a-e49078d3e954"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""D"",
                    ""type"": ""Button"",
                    ""id"": ""c58cb4c7-d521-41c0-b1a9-8e498de8403b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8d7186db-7b03-4885-8b9a-fa59e8356ea5"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Esc"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c2284429-b204-455c-8153-77fb23344d71"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b9eb5f76-d710-4f39-8ab7-f89df8377287"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Enter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7cb9f5dc-cfcd-4fcc-8428-de2f76fedeab"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Q"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6f5143e8-2729-4798-b10b-2aadb82e4167"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""E"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bfc0ca73-475f-4e95-8b53-f01b93563763"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""A"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e82b353d-5109-441d-aba3-759e0fc1a586"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""D"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Attack = m_Player.FindAction("Attack", throwIfNotFound: true);
        m_Player_Flash = m_Player.FindAction("Flash", throwIfNotFound: true);
        m_Player_Action = m_Player.FindAction("Action", throwIfNotFound: true);
        m_Player_Restore = m_Player.FindAction("Restore", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Esc = m_UI.FindAction("Esc", throwIfNotFound: true);
        m_UI_Tab = m_UI.FindAction("Tab", throwIfNotFound: true);
        m_UI_Enter = m_UI.FindAction("Enter", throwIfNotFound: true);
        m_UI_Q = m_UI.FindAction("Q", throwIfNotFound: true);
        m_UI_E = m_UI.FindAction("E", throwIfNotFound: true);
        m_UI_A = m_UI.FindAction("A", throwIfNotFound: true);
        m_UI_D = m_UI.FindAction("D", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Attack;
    private readonly InputAction m_Player_Flash;
    private readonly InputAction m_Player_Action;
    private readonly InputAction m_Player_Restore;
    public struct PlayerActions
    {
        private @MyInput m_Wrapper;
        public PlayerActions(@MyInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Attack => m_Wrapper.m_Player_Attack;
        public InputAction @Flash => m_Wrapper.m_Player_Flash;
        public InputAction @Action => m_Wrapper.m_Player_Action;
        public InputAction @Restore => m_Wrapper.m_Player_Restore;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Attack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Flash.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFlash;
                @Flash.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFlash;
                @Flash.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFlash;
                @Action.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAction;
                @Action.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAction;
                @Action.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAction;
                @Restore.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRestore;
                @Restore.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRestore;
                @Restore.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRestore;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Flash.started += instance.OnFlash;
                @Flash.performed += instance.OnFlash;
                @Flash.canceled += instance.OnFlash;
                @Action.started += instance.OnAction;
                @Action.performed += instance.OnAction;
                @Action.canceled += instance.OnAction;
                @Restore.started += instance.OnRestore;
                @Restore.performed += instance.OnRestore;
                @Restore.canceled += instance.OnRestore;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Esc;
    private readonly InputAction m_UI_Tab;
    private readonly InputAction m_UI_Enter;
    private readonly InputAction m_UI_Q;
    private readonly InputAction m_UI_E;
    private readonly InputAction m_UI_A;
    private readonly InputAction m_UI_D;
    public struct UIActions
    {
        private @MyInput m_Wrapper;
        public UIActions(@MyInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Esc => m_Wrapper.m_UI_Esc;
        public InputAction @Tab => m_Wrapper.m_UI_Tab;
        public InputAction @Enter => m_Wrapper.m_UI_Enter;
        public InputAction @Q => m_Wrapper.m_UI_Q;
        public InputAction @E => m_Wrapper.m_UI_E;
        public InputAction @A => m_Wrapper.m_UI_A;
        public InputAction @D => m_Wrapper.m_UI_D;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Esc.started -= m_Wrapper.m_UIActionsCallbackInterface.OnEsc;
                @Esc.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnEsc;
                @Esc.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnEsc;
                @Tab.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTab;
                @Tab.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTab;
                @Tab.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTab;
                @Enter.started -= m_Wrapper.m_UIActionsCallbackInterface.OnEnter;
                @Enter.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnEnter;
                @Enter.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnEnter;
                @Q.started -= m_Wrapper.m_UIActionsCallbackInterface.OnQ;
                @Q.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnQ;
                @Q.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnQ;
                @E.started -= m_Wrapper.m_UIActionsCallbackInterface.OnE;
                @E.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnE;
                @E.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnE;
                @A.started -= m_Wrapper.m_UIActionsCallbackInterface.OnA;
                @A.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnA;
                @A.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnA;
                @D.started -= m_Wrapper.m_UIActionsCallbackInterface.OnD;
                @D.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnD;
                @D.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnD;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Esc.started += instance.OnEsc;
                @Esc.performed += instance.OnEsc;
                @Esc.canceled += instance.OnEsc;
                @Tab.started += instance.OnTab;
                @Tab.performed += instance.OnTab;
                @Tab.canceled += instance.OnTab;
                @Enter.started += instance.OnEnter;
                @Enter.performed += instance.OnEnter;
                @Enter.canceled += instance.OnEnter;
                @Q.started += instance.OnQ;
                @Q.performed += instance.OnQ;
                @Q.canceled += instance.OnQ;
                @E.started += instance.OnE;
                @E.performed += instance.OnE;
                @E.canceled += instance.OnE;
                @A.started += instance.OnA;
                @A.performed += instance.OnA;
                @A.canceled += instance.OnA;
                @D.started += instance.OnD;
                @D.performed += instance.OnD;
                @D.canceled += instance.OnD;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnFlash(InputAction.CallbackContext context);
        void OnAction(InputAction.CallbackContext context);
        void OnRestore(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnEsc(InputAction.CallbackContext context);
        void OnTab(InputAction.CallbackContext context);
        void OnEnter(InputAction.CallbackContext context);
        void OnQ(InputAction.CallbackContext context);
        void OnE(InputAction.CallbackContext context);
        void OnA(InputAction.CallbackContext context);
        void OnD(InputAction.CallbackContext context);
    }
}
