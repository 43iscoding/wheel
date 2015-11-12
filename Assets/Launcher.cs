using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class Launcher : MonoBehaviour {

    public Rigidbody wheel;
    public MotionBlur blur;

    private bool launched;
    private Vector3 defaultPosition;
    private Quaternion defaultRotation;
    private int cullingMask;

    void Awake()
    {
        defaultPosition = wheel.transform.position;
        defaultRotation = wheel.transform.rotation;
        cullingMask = Camera.main.cullingMask;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Hidden())
            {
                Show();
                return;
            }

            if (!launched)
            {
                Launch();
            }
            else
            {
                Reset();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (Hidden())
            {
                Show();
            }
            else
            {
                Hide();
                Reset();
            }
        }
    }

    bool Hidden()
    {
        return Camera.main.cullingMask == 0;
    }

    void Show()
    {
        Camera.main.cullingMask = cullingMask;
        blur.enabled = true;
    }

    void Hide()
    {
        Camera.main.cullingMask = 0;
        blur.enabled = false;
    }

    void Launch()
    {
        wheel.AddTorque(0f, 0f, RandomTorque());
        launched = true;
    }

    void Reset()
    {
        wheel.transform.position = defaultPosition;
        //wheel.transform.rotation = defaultRotation;
        wheel.angularDrag = 0f;
        wheel.angularVelocity = Vector3.zero;
        launched = false;
    }

    float RandomTorque()
    {
        return Random.Range(-1000f, -300f);
    }
}
