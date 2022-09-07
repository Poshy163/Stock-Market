using UnityEngine;

// ReSharper disable IdentifierTypo

public class InfoNavagation : MonoBehaviour
{
    [Header("Slide One")] public GameObject slideOne;

    [Header("Slide Two")] public GameObject slideTwo;


    private void Start()
    {
        slideOne.SetActive(true);
        slideTwo.SetActive(false);
    }


    public void SlideOneNext()
    {
        slideOne.SetActive(false);
        slideTwo.SetActive(true);
    }

    public void SlideTwoNext()
    {
        slideOne.SetActive(false);
        slideTwo.SetActive(true);
    }
}