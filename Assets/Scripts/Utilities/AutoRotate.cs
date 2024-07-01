#region

using UnityEngine;

#endregion

public class AutoRotate : MonoBehaviour
{
    private float _newValue;

    public RotateAxis axis = RotateAxis.Z;
    public float speed;

    private void Update()
    {
        _newValue += Time.deltaTime * speed;

        switch (axis)
        {
            case RotateAxis.X:
                transform.localEulerAngles = new Vector3(_newValue, transform.localEulerAngles.y, transform.localEulerAngles.z);
                break;
            case RotateAxis.Y:
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, _newValue, transform.localEulerAngles.z);
                break;
            case RotateAxis.Z:
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, _newValue);
                break;
        }
    }
}

public enum RotateAxis
{
    X,
    Y,
    Z
}