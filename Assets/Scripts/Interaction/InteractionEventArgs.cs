using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InteractionEvent : UnityEvent<InteractionEventArgs> { }

[System.Serializable]
public class InteractionEventArgs
{ 
    /// <summary>
    /// The controller that is sending the event associated with these args
    /// </summary>
    public VrController controller;
    
    /// <summary>
    /// The rigidbody of the object that is being interacted with
    /// </summary>
    public Rigidbody rb;
  
    /// <summary>
    /// The collider of the object being interacted with
    /// </summary>
    public Collider coll;

    public InteractionEventArgs(VrController _controller, Rigidbody _rb, Collider _coll)
    {
        controller = _controller;
        rb = _rb;
        coll = _coll;
    }
}
