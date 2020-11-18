using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.Events;
using System.Reflection;

public class VrControllerInput : MonoBehaviour
{
    [System.Serializable]
    public class InputEvent : UnityEvent<InputEventArgs> { }

    public VrController Controller { get { return controller; } }

    [SerializeField]
    private SteamVR_Action_Boolean grab;
    [SerializeField]
    private SteamVR_Action_Boolean pointer;
    [SerializeField]
    private SteamVR_Action_Boolean use;
    [SerializeField]
    private SteamVR_Action_Boolean teleport;
    [SerializeField]
    private SteamVR_Action_Vector2 touchpadAxis;

    public InputEvent onGrabPressed = new InputEvent();
    public InputEvent onGrabReleased = new InputEvent();
    
    public InputEvent onPointerPressed = new InputEvent();
    public InputEvent onPointerReleased = new InputEvent();

    public InputEvent onUsePressed = new InputEvent();
    public InputEvent onUseReleased = new InputEvent();

    public InputEvent onTeleportPressed = new InputEvent();
    public InputEvent onTeleportReleased = new InputEvent();

    public InputEvent onTouchpadAxisChanged = new InputEvent();

    private VrController controller;

    public void Setup(VrController _controller)
    { 
        controller = _controller;

        grab.AddOnStateDownListener(OnGrabDown, controller.InputSource);
        grab.AddOnStateUpListener(OnGrabUp, controller.InputSource);

        pointer.AddOnStateDownListener(OnPointerDown, controller.InputSource);
        pointer.AddOnStateUpListener(OnPointerUp, controller.InputSource);
        
        use.AddOnStateDownListener(OnUseDown, controller.InputSource);
        use.AddOnStateUpListener(OnUseUp, controller.InputSource);
        
        teleport.AddOnStateDownListener(OnTeleportDown, controller.InputSource);
        teleport.AddOnStateUpListener(OnTeleportUp, controller.InputSource);
        
        touchpadAxis.AddOnAxisListener(OnTouchpadAxis, controller.InputSource);
    }

    private InputEventArgs GenerateArgs()
    {
        return new InputEventArgs(controller, controller.InputSource, touchpadAxis.axis);
    }

    void OnGrabDown(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source)
    {
        onGrabPressed.Invoke(GenerateArgs());
    }

    void OnGrabUp(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source)
    {
        onGrabReleased.Invoke(GenerateArgs());
    }
    void OnPointerDown(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source)
    {
        onPointerPressed.Invoke(GenerateArgs());
    }
    void OnPointerUp(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source)
    {
        onPointerReleased.Invoke(GenerateArgs());
    }

    void OnUseDown(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source)
    {
        onUsePressed.Invoke(GenerateArgs());
    }
    void OnUseUp(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source)
    {
        onUseReleased.Invoke(GenerateArgs());
    }
    void OnTeleportDown(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source)
    {
        onTeleportPressed.Invoke(GenerateArgs());
    }
    void OnTeleportUp(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source)
    {
        onTeleportReleased.Invoke(GenerateArgs());
    }
    void OnTouchpadAxis(SteamVR_Action_Vector2 _action, SteamVR_Input_Sources _source, Vector2 _axis, Vector2 _delta)
    {
        onTouchpadAxisChanged.Invoke(GenerateArgs());
    }



}
