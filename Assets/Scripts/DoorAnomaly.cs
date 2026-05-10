using UnityEngine;

public class DoorAnomaly : Anomaly
{
    public Transform door;
    private Quaternion closedRotation;

    void Start()
    {
        if (door != null)
            closedRotation = door.rotation;
    }

    public override void Activate()
    {
        base.Activate();

        if (door != null)
            door.Rotate(0, 10f, 0); // open slightly
    }

    public override void Fix()
    {
        base.Fix();

        if (door != null)
            door.rotation = closedRotation;
    }
}
