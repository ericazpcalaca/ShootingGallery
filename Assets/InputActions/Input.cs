//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/InputActions/Input.inputactions
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

namespace ShootingGallery
{
    public partial class @Input: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @Input()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""c3ad6574-da36-4a4b-b1d7-bacc1560b01c"",
            ""actions"": [
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""f02cb0d5-480f-46eb-8951-1cb01cf3baef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveCamera"",
                    ""type"": ""Value"",
                    ""id"": ""19eda5f7-700a-4b0c-b801-c46156ba2ba1"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""AllowShooting"",
                    ""type"": ""Button"",
                    ""id"": ""25ff1cbb-209a-42f6-ade7-e6232773bd67"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PauseShooting"",
                    ""type"": ""Button"",
                    ""id"": ""480d1cea-2d73-48f7-801a-6b775c9cf26f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a53c046b-bb7d-4bd1-8947-1a7837d3f5b3"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8959d2ae-9b57-4c15-a404-89a7f3c46428"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e0cf4e03-73bc-4138-864d-3a4f17c34606"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AllowShooting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8daa4b58-7b03-43d4-a0a7-2f145bc89617"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseShooting"",
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
            m_Player_Shoot = m_Player.FindAction("Shoot", throwIfNotFound: true);
            m_Player_MoveCamera = m_Player.FindAction("MoveCamera", throwIfNotFound: true);
            m_Player_AllowShooting = m_Player.FindAction("AllowShooting", throwIfNotFound: true);
            m_Player_PauseShooting = m_Player.FindAction("PauseShooting", throwIfNotFound: true);
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
        private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
        private readonly InputAction m_Player_Shoot;
        private readonly InputAction m_Player_MoveCamera;
        private readonly InputAction m_Player_AllowShooting;
        private readonly InputAction m_Player_PauseShooting;
        public struct PlayerActions
        {
            private @Input m_Wrapper;
            public PlayerActions(@Input wrapper) { m_Wrapper = wrapper; }
            public InputAction @Shoot => m_Wrapper.m_Player_Shoot;
            public InputAction @MoveCamera => m_Wrapper.m_Player_MoveCamera;
            public InputAction @AllowShooting => m_Wrapper.m_Player_AllowShooting;
            public InputAction @PauseShooting => m_Wrapper.m_Player_PauseShooting;
            public InputActionMap Get() { return m_Wrapper.m_Player; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
            public void AddCallbacks(IPlayerActions instance)
            {
                if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @MoveCamera.started += instance.OnMoveCamera;
                @MoveCamera.performed += instance.OnMoveCamera;
                @MoveCamera.canceled += instance.OnMoveCamera;
                @AllowShooting.started += instance.OnAllowShooting;
                @AllowShooting.performed += instance.OnAllowShooting;
                @AllowShooting.canceled += instance.OnAllowShooting;
                @PauseShooting.started += instance.OnPauseShooting;
                @PauseShooting.performed += instance.OnPauseShooting;
                @PauseShooting.canceled += instance.OnPauseShooting;
            }

            private void UnregisterCallbacks(IPlayerActions instance)
            {
                @Shoot.started -= instance.OnShoot;
                @Shoot.performed -= instance.OnShoot;
                @Shoot.canceled -= instance.OnShoot;
                @MoveCamera.started -= instance.OnMoveCamera;
                @MoveCamera.performed -= instance.OnMoveCamera;
                @MoveCamera.canceled -= instance.OnMoveCamera;
                @AllowShooting.started -= instance.OnAllowShooting;
                @AllowShooting.performed -= instance.OnAllowShooting;
                @AllowShooting.canceled -= instance.OnAllowShooting;
                @PauseShooting.started -= instance.OnPauseShooting;
                @PauseShooting.performed -= instance.OnPauseShooting;
                @PauseShooting.canceled -= instance.OnPauseShooting;
            }

            public void RemoveCallbacks(IPlayerActions instance)
            {
                if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IPlayerActions instance)
            {
                foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public PlayerActions @Player => new PlayerActions(this);
        public interface IPlayerActions
        {
            void OnShoot(InputAction.CallbackContext context);
            void OnMoveCamera(InputAction.CallbackContext context);
            void OnAllowShooting(InputAction.CallbackContext context);
            void OnPauseShooting(InputAction.CallbackContext context);
        }
    }
}
