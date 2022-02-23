using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeAction : MonoBehaviour
{

    // Start is called before the first frame update
    public Axis axis;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cuberotate();
    }

    void cuberotate()
    {
        switch (axis)
        {
            case Axis.X:
                this.transform.Rotate(3, 0, 0);
                break;
            case Axis.Y:
                this.transform.Rotate(0, 3, 0);
                break;
            case Axis.Z:
                this.transform.Rotate(0, 0, 3);
                break;
            default:
                break;
        }
    }
}
public enum Axis
{
    X,
    Y,
    Z
}
