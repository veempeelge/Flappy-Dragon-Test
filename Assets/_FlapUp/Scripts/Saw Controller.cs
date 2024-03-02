using UnityEngine;
using System.Collections;

public class SawController : MonoBehaviour
{
    public GameObject saw;

    public float movingSpeed;
    public float fluctuationRange;
    public float startDelay;

    private float firstRightObPosition;
    private float maxLengthMove;
    private int SawTurn = 1;

    Vector3 endPos;

    public bool vertical, horizontal, diagonalUpRight, diagonalUpLeft;
    // Use this for initialization
    void Start()
    {
        firstRightObPosition = Mathf.Abs(saw.transform.position.x);
        maxLengthMove = GameManager.Instance.maxObstacleFluctuationRange - .5f;


        StartCoroutine(MovingSaw());
        StartCoroutine(MovingObWhenGameOver());
    }




    IEnumerator MovingSaw()
    {
        yield return new WaitForSeconds(startDelay);
        while (true)
        {
            SawTurn = SawTurn * (-1);

            Vector3 startPos = saw.transform.position;


            if (SawTurn < 0)
            {
                if (vertical)
                {
                    endPos = saw.transform.position + new Vector3(0, -fluctuationRange, 0);
                }
                else if (horizontal)
                {
                    endPos = saw.transform.position + new Vector3(fluctuationRange, 0, 0);
                }
                else if (diagonalUpLeft)
                {
                    endPos = saw.transform.position + new Vector3(-fluctuationRange, fluctuationRange, 0);
                }
                else if (diagonalUpRight)
                {
                    endPos = saw.transform.position + new Vector3(-fluctuationRange, -fluctuationRange, 0);
                }

            }
            else
            {
                if (vertical)
                {
                    endPos = saw.transform.position + new Vector3(0, fluctuationRange, 0);
                }
                else if (horizontal)
                {
                    endPos = saw.transform.position + new Vector3(-fluctuationRange, 0, 0);
                }
                else if (diagonalUpLeft)
                {
                    endPos = saw.transform.position + new Vector3(fluctuationRange, -fluctuationRange, 0);
                }
                else if (diagonalUpRight)
                {
                    endPos = saw.transform.position + new Vector3(fluctuationRange, fluctuationRange, 0);
                }
            }

            float t = 0;
            while (t < movingSpeed && GameManager.Instance.GameState != GameState.GameOver)
            {
                t += Time.deltaTime;
                float fraction = t / movingSpeed;
                saw.transform.position = Vector3.Lerp(startPos, endPos, fraction);
                yield return null;
            }

            if (GameManager.Instance.GameState == GameState.GameOver)
            {
                yield break;
            }
        }
    }


    IEnumerator MovingObWhenGameOver()
    {
        while (true)
        {
            if (GameManager.Instance.GameState == GameState.GameOver)
            {
                yield return new WaitForSeconds(0.3f);

                float currentRightObXPosition = Mathf.Abs(saw.transform.position.x);

                if (currentRightObXPosition < firstRightObPosition + maxLengthMove)
                {
                    //float addedDistance = (firstRightObPosition + maxLengthMove) - currentRightObXPosition;

                    // Vector3 startPosLeftOb = saw.transform.position;
                    // Vector3 endPosLeftOb = saw.transform.position + new Vector3(-addedDistance, 0, 0);




                    float t = 0;
                    while (t < GameManager.Instance.minObstacleSpeedFactor / 2)
                    {
                        t += Time.deltaTime;
                        float fraction = t / (GameManager.Instance.minObstacleSpeedFactor / 2);
                        //   saw.transform.position = Vector3.Lerp(startPosLeftOb, endPosLeftOb, fraction);

                        yield return null;
                    }

                    yield break;

                }
                else
                {
                    yield break;
                }
            }
            yield return null;
        }
    }

}
