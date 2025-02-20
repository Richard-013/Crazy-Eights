using System.Collections;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private float velocity = 10;
    [SerializeField] private float rotationalVelocity = 100;
    private bool currentlyMoving = false;
    private bool currentlyRotating = false;

    public int number;
    public int suit;

    public Material faceMaterial;
    [SerializeField] private Material blankFaceMaterial;

    public bool faceHidden;

    public string ReadCard()
    {
        string cardStatement;

        switch (suit)
        {
            case 0:
                cardStatement = "cardClubs";
                break;
            case 1:
                cardStatement = "cardDiamonds";
                break;
            case 2:
                cardStatement = "cardHearts";
                break;
            case 3:
                cardStatement = "cardSpades";
                break;
            default:
                cardStatement = "card" + suit;
                break;
        }

        switch (number)
        {
            case 1:
                cardStatement += "A";
                return cardStatement;
            case 11:
                cardStatement += "J";
                return cardStatement;
            case 12:
                cardStatement += "Q";
                return cardStatement;
            case 13:
                cardStatement += "K";
                return cardStatement;
            default:
                cardStatement += number.ToString();
                return cardStatement;
        }
    }

    public void HideCardFace()
    {
        transform.GetChild(0).GetComponent<MeshRenderer>().material = blankFaceMaterial;
        faceHidden = true;
    }

    public void ShowCardFace()
    {
        transform.GetChild(0).GetComponent<MeshRenderer>().material = faceMaterial;
        faceHidden = false;
    }

    public void MoveCard(Vector3 targetPosition, Vector3 targetRotation)
    {
        // https://stackoverflow.com/questions/66259215/move-gameobject-between-two-points-with-ease-in-and-ease-out
        if(currentlyMoving)
        {
            return;
        }

    }
}