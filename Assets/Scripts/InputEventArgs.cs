using UnityEngine;
using Valve.VR;

[System.Serializable]
public class InputEventArgs
{
    /// <summary>
    /// The controller firing the event
    /// </summary>

    public VrController controller;

    /// <summary>
    /// The input sauce the event is coming from
    /// </summary>
    public SteamVR_Input_Sources source;

    /// <summary>
    /// The position the player is touching the touchpad on
    /// </summary>
    public Vector2 touchpadAxis;

    public InputEventArgs(VrController _controller, SteamVR_Input_Sources _source, Vector2 _touchpadAxis)
    {
        controller = _controller;
        source = _source;
        touchpadAxis = _touchpadAxis;
    }

}
