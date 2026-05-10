using UnityEngine;

public abstract class Anomaly : MonoBehaviour
{
    public bool isActive = false;
    public bool isFixed = false;

    public virtual void Activate()
    {
        isActive = true;
        isFixed = false;
    }

    public virtual void Fix()
    {
        isFixed = true;
        isActive = false;
    }
}
