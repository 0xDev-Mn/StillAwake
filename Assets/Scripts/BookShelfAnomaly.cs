using UnityEngine;

public class BookshelfAnomaly : Anomaly
{
    public GameObject[] books;

    public override void Activate()
    {
        isActive = true;
        isFixed = false;

        foreach (GameObject book in books)
        {
            book.SetActive(true);
        }
    }

    public override void Fix()
    {
        isFixed = true;
        isActive = false;

        foreach (GameObject book in books)
        {
            book.SetActive(false);
        }
    }
}