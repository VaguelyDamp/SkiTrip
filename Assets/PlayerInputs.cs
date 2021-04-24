// GENERATED AUTOMATICALLY FROM 'Assets/PlayerInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputs"",
    ""maps"": [
        {
            ""name"": ""Steering"",
            ""id"": ""ab0b29b2-eb37-47e3-ad3c-ae664a38e9eb"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""0e098217-1318-4520-ba56-ca7b06e7ffc1"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""773a3274-d766-45f9-8c71-bf0e8bc92b55"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""65c45e4d-a7d1-46b7-b58b-62a1f9d2d456"",
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
                    ""id"": ""16f1e602-6ea8-44e7-b977-3ca8e653f98f"",
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
                    ""id"": ""d02e431a-d650-4c24-9f7a-c5d6e5c658ba"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""356531fe-ded1-46f1-8ea9-a0efb4a70b3e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Steering
        m_Steering = asset.FindActionMap("Steering", throwIfNotFound: true);
        m_Steering_Move = m_Steering.FindAction("Move", throwIfNotFound: true);
        m_Steering_Fire = m_Steering.FindAction("Fire", throwIfNotFound: true);
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

    // Steering
    private readonly InputActionMap m_Steering;
    private ISteeringActions m_SteeringActionsCallbackInterface;
    private readonly InputAction m_Steering_Move;
    private readonly InputAction m_Steering_Fire;
    public struct SteeringActions
    {
        private @PlayerInputs m_Wrapper;
        public SteeringActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Steering_Move;
        public InputAction @Fire => m_Wrapper.m_Steering_Fire;
        public InputActionMap Get() { return m_Wrapper.m_Steering; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SteeringActions set) { return set.Get(); }
        public void SetCallbacks(ISteeringActions instance)
        {
            if (m_Wrapper.m_SteeringActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_SteeringActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_SteeringActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_SteeringActionsCallbackInterface.OnMove;
                @Fire.started -= m_Wrapper.m_SteeringActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_SteeringActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_SteeringActionsCallbackInterface.OnFire;
            }
            m_Wrapper.m_SteeringActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
            }
        }
    }
    public SteeringActions @Steering => new SteeringActions(this);
    public interface ISteeringActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
    }
}
