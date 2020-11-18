using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.Events;
public class Pointer : MonoBehaviour
{
    [System.Serializable]
    public class TeleportEvent : UnityEvent<Vector3> {}


    /// <summary>
    /// The last position the pointer was hitting, usde for teleportation
    /// <summary>
    public Vector3 Position {get; private set;} = Vector3.zero;

    [SerializeField]
    private SteamVR_Input_Sources source;
    [SerializeField]
    private LayerMask pointerLayers;
    [SerializeField]
    private float maxPointerLength = 100f;
    [Header("Rendering")]
    [SerializeField]
    private GameObject tracer;
    [SerializeField]
    private bool stretchTracerAlongRay = true;
    [SerializeField]
    private float tracerScaleFactor = 0.1f;
    [SerializeField]
    private GameObject cursor;
    [SerializeField]
    private float cursorScaleFactor = 0.25f;

    public Color activeColor = Color.green;
    public Color inactiveColor = Color.red;

    [Space]
    public TeleportEvent onTeleportRequested = new TeleportEvent();

    private VrControllerInput input;
    private bool isPointerActive = false;

    private MeshRenderer cursorRend;
    private MeshRenderer tracerRend;

    void Start()
    {
        if (source == SteamVR_Input_Sources.LeftHand)
        {
            input = VrRig.instance.LeftController.Input;
        }
        else if (source == SteamVR_Input_Sources.RightHand)
        {
            input = VrRig.instance.RightController.Input;
        }
        else
        {
            input =VrRig.instance.LeftController.Input;
        }

        input.onPointerPressed.AddListener(OnPointerActivate);
        input.onPointerReleased.AddListener(OnPointerDeactivate);
        input.onTeleportPressed.AddListener(onTeleportPressed);

        tracer.transform.parent = transform;
        cursor.transform.parent = transform;
        
        cursor.SetActive(false);
        tracer.SetActive(false);

        cursorRend = cursor.GetComponent<MeshRenderer>();
        tracerRend = tracer.GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if(isPointerActive)
        {
            transform.rotation = input.transform.rotation;
                
            if(Physics.Raycast(input.transform.position, input.transform.forward, out RaycastHit hit, maxPointerLength, pointerLayers))
            {
                Position = hit.point;

                Vector3 midpoint = Vector3.Lerp(transform.position, hit.point, 0.5f);
                tracer.transform.position = midpoint;
                tracer.transform.localScale = new Vector3(tracerScaleFactor, tracerScaleFactor, hit.distance);

                cursor.transform.position = hit.point;
                cursor.transform.localScale = Vector3.one * cursorScaleFactor;

                cursorRend.material.color = activeColor;
                tracerRend.material.color = activeColor;
            }
            else
            {
                Position = Vector3.zero;
                tracer.transform.position = transform.position + transform.forward * (maxPointerLength * 0.5f);
                tracer.transform.rotation = Quaternion.LookRotation(transform.forward);
                tracer.transform.localScale = new Vector3(tracerScaleFactor, tracerScaleFactor, maxPointerLength);

                cursor.transform.position = transform.position + transform.forward * maxPointerLength;
                cursor.transform.localScale = Vector3.one * cursorScaleFactor;
                
                cursorRend.material.color = inactiveColor;
                tracerRend.material.color = inactiveColor;
            }
        }
    }

    private void OnPointerActivate(InputEventArgs _args)
    {
        isPointerActive = true;
        cursor.SetActive(true);
        tracer.SetActive(true);

    }
    private void OnPointerDeactivate(InputEventArgs _args)
    {
        isPointerActive = false;
        cursor.SetActive(false);
        tracer.SetActive(false);

    }
    
    private void onTeleportPressed(InputEventArgs _args)
    {
        if(isPointerActive && Position != Vector3.zero)
        {
            onTeleportRequested.Invoke(Position);
        }
    }

}
