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

        StartCoroutine(SmoothMovementToPoint(targetPosition, targetRotation));
    }

    public void MoveCard(Vector3 targetPosition, Vector3 targetRotation, float delay)
    {
        // https://stackoverflow.com/questions/66259215/move-gameobject-between-two-points-with-ease-in-and-ease-out
        if(currentlyMoving)
        {
            return;
        }

        StartCoroutine(SmoothMovementToPoint(targetPosition, targetRotation, delay));
    }

    public IEnumerator SmoothMovementToPoint(Vector3 targetPosition, Vector3 targetRotation)
    {
        // Don't try to move the card if it is already in motion
        if(currentlyMoving)
        {
            yield break;
        }

        currentlyMoving = true;

        Vector3 startPosition = transform.position;

        float distanceBetweenPoints = Vector3.Distance(startPosition, targetPosition);

        float movementDuration = distanceBetweenPoints / velocity;
        float movementDurationMidPoint = movementDuration/2;

        float timePassed = 0f;

        Coroutine[] coroutine = new Coroutine[1];

        while(timePassed < movementDuration)
        {
            if(timePassed < movementDuration)
            {
                // Amount of movement completed
                float movementFactor = timePassed / movementDuration;

                movementFactor = Mathf.SmoothStep(0, 1, movementFactor);

                transform.position = Vector3.Lerp(startPosition, targetPosition, movementFactor);
            }
            
            // Start card rotation once half the movement is complete
            if(!currentlyRotating && timePassed > movementDurationMidPoint)
            {
                coroutine[0] = StartCoroutine(SmoothRotationToEulerAngles(targetRotation));
            }

            yield return null;

            timePassed += Time.deltaTime;
        }

        // Final adjustment for clean coordinates
        transform.position = targetPosition;

        // Wait for rotation to complete
        yield return coroutine[0];
        
        currentlyMoving = false;
    }

    // SmoothMovementToPoint Coroutine but with a delay before movement begins
    public IEnumerator SmoothMovementToPoint(Vector3 targetPosition, Vector3 targetRotation, float delay)
    {
        // Don't try to move the card if it is already in motion
        if(currentlyMoving)
        {
            yield break;
        }

        currentlyMoving = true;

        Vector3 startPosition = transform.position;

        float distanceBetweenPoints = Vector3.Distance(startPosition, targetPosition);

        float movementDuration = distanceBetweenPoints / velocity;
        float movementDurationMidPoint = movementDuration/2;

        // Add a delay to each card
        float timePassed = 0f - delay;

        Coroutine[] coroutine = new Coroutine[1];

        while(timePassed < 0)
        {
            timePassed += Time.deltaTime;
            yield return null;
        }

        while(timePassed < movementDuration)
        {
            if(timePassed < movementDuration)
            {
                // Amount of movement completed
                float movementFactor = timePassed / movementDuration;

                movementFactor = Mathf.SmoothStep(0, 1, movementFactor);

                transform.position = Vector3.Lerp(startPosition, targetPosition, movementFactor);
            }
            
            // Start card rotation once half the movement is complete
            if(!currentlyRotating && timePassed > movementDurationMidPoint)
            {
                coroutine[0] = StartCoroutine(SmoothRotationToEulerAngles(targetRotation));
            }

            yield return null;

            timePassed += Time.deltaTime;
        }

        // Final adjustment for clean coordinates
        transform.position = targetPosition;

        // Wait for rotation to complete
        yield return coroutine[0];
        
        currentlyMoving = false;
    }

    private IEnumerator SmoothRotationToEulerAngles(Vector3 targetRotation)
    {
        // Don't try to rotate the card if it is already rotating
        if(currentlyRotating)
        {
            yield break;
        }

        currentlyRotating = true;

        Vector3 startRotation = transform.eulerAngles;

        float rotationalDifference = Vector3.Distance(startRotation, targetRotation);
        float rotationDuration = rotationalDifference / rotationalVelocity;

        float timePassed = 0f;

        while(timePassed < rotationDuration)
        {
            float rotationFactor = timePassed / rotationDuration;

            rotationFactor = Mathf.SmoothStep(0, 1, rotationFactor);

            transform.eulerAngles = Vector3.Lerp(-startRotation, targetRotation, rotationFactor);

            yield return null;

            timePassed += Time.deltaTime;
        }

        // Final adjustment for clean angles
        transform.eulerAngles = targetRotation;

        currentlyRotating = false;
    }
}