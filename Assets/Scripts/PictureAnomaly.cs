using UnityEngine;

public class PictureAnomaly : Anomaly
{
    public Transform picture;

    private Quaternion originalRotation;

    void Start()
    {
        originalRotation = picture.rotation;
    }

    public override void Activate()
    {
        base.Activate();
        picture.Rotate(0, 0, 20f);
    }

    public override void Fix()
    {
        picture.rotation = originalRotation;
        base.Fix();
    }
}
