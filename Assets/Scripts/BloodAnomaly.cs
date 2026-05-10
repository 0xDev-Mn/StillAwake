using UnityEngine;

public class BloodAnomaly : Anomaly
{
    public GameObject blood; // your decal / quad

    void Start()
    {
        blood.SetActive(false); // hidden initially
    }

    public override void Activate()
    {
        base.Activate();
        blood.SetActive(true); // show blood
    }

    public override void Fix()
    {
        blood.SetActive(false); // clean blood
        base.Fix();
    }
}
